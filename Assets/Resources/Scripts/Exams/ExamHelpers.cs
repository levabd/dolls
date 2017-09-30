using System.Collections.Generic;

// ReSharper disable once CheckNamespace
public static class ExamHelpers
{
    public static bool CateterFinalise(this BaseExam exam, ref ToolItem tool, string actionCode, ref string errorMessage, string locatedColliderTag, string catheter, string catheterConductor, int expectedFirstStep, out int returnedStep)
    {
        //{ "wire_insertion",                 "Вставка проводника." },
        if (tool.CodeName == "standart_catheter_conductor" && actionCode == "push")
        {
            if (exam.LastTakenStep() != expectedFirstStep - 1)
                errorMessage = "Некуда вставить проводник";
            returnedStep = expectedFirstStep;
            return true;
        }
        if (tool.CodeName == "soft_catheter_conductor" && actionCode == "push")
        {
            errorMessage = "Не тот проводник";
            returnedStep = expectedFirstStep;
        }

        //{ "needle_removing",                "Удаление иглы." },
        if (tool.CodeName == "needle" && actionCode == "needle_removing")
        {
            if (exam.LastTakenStep() != expectedFirstStep)
                errorMessage = "Нельзя удалять иглу без проводника";
            returnedStep = expectedFirstStep + 1;
        }
        //{ "catheter_insertion",             "Вставка катетера по проводнику." },
        if (tool.CodeName == catheter && actionCode == "push")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 1)
                errorMessage = "Нельзя вставить катетер без проводника";
            returnedStep = expectedFirstStep + 2;
        }

        //{ "catheter_pushing",               "Углубление вращательными движениями." },
        if (tool.CodeName == catheter && actionCode == "rotation_insertion")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 2)
                errorMessage = "Некуда углублять катетер";
            returnedStep = expectedFirstStep + 3;
        }
        if (tool.CodeName == catheter && actionCode == "direct_insertion")
        {
            errorMessage = "Некорректный способ углубления катетера";
            returnedStep = expectedFirstStep + 3;
        }

        //{ "wire_removing",                  "Извлечение проводника." },
        if (tool.CodeName == "standart_catheter_conductor" && actionCode == "pull")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 3)
                errorMessage = tool.CodeName == catheterConductor ? "Нельзя удалять иглу без катетера" : "Не тот проводник";
            returnedStep = expectedFirstStep + 4;
        }
        if (tool.CodeName == "soft_catheter_conductor" && actionCode == "pull")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 3)
                errorMessage = tool.CodeName == catheterConductor ? "Нельзя удалять иглу без катетера" : "Не тот проводник";
            returnedStep = expectedFirstStep + 4;
        }

        //{ "liquid_transfusion_connection",  "Соединение с системой переливания жидкости." },
        if (tool.CodeName == catheter && actionCode == "liquid_transfusion_connection")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 4)
                errorMessage = "Сначала должен быть корректно установлен катетер";
            returnedStep = expectedFirstStep + 5;
        }

        //{ "fixation_with_plaster",          "Фиксация пластырем." }
        if (tool.CodeName == "patch" && actionCode == "stick")
        {
            if (!locatedColliderTag.Contains("catheter"))
                errorMessage = "Не то место установки. Сначала должен быть корректно установлен катетер";
            returnedStep = expectedFirstStep + 6;
        }

        returnedStep = 0;
        return false;
    }

    public static bool BiosafetySpiritIodine(this BaseExam exam, ref ToolItem tool, string actionCode, ref string errorMessage, string locatedColliderTag, string targetLocatedColliderTag, out int returnedStep, bool wearGown = false, bool shave = false)
    {
        // { "shave_pubis",                    "Побрить лобковую зону" },
        if (shave && tool.CodeName == "razor" && actionCode == "shave_pubis")
        {
            tool.StateParams["shave_pubis"] = "true";
            returnedStep = 1;
            return true;
        }

        // { "wear_gloves",                    "Надеть перчатки" },
        if (tool.CodeName == "gloves" && actionCode == "wear")
        {
            tool.StateParams["weared"] = "true";
            returnedStep = !shave ? 1 : 2;
            return true;
        }

        // { "wear_gown",                      "Надеть халат" },
        if (wearGown && tool.CodeName == "gown" && actionCode == "wear")
        {
            tool.StateParams["weared"] = "true";
            returnedStep = !shave ? 2 : 3;
            return true;
        }

        //{ "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        if (tool.CodeName == "gauze_balls" && actionCode == "spirit_60")
        {
            errorMessage = "Не та жидкость";
            returnedStep = !wearGown ? 2 : 3;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }
        if (tool.CodeName == "gauze_balls" && actionCode == "spirit_70")
        {
            BallHelper.TryWetBall(ref tool, "spirit_70", out errorMessage);
            returnedStep = !wearGown ? 2 : 3;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }
        if (tool.CodeName == "gauze_balls" && actionCode == "spirit_80")
        {
            errorMessage = "Не та жидкость";
            returnedStep = !wearGown ? 2 : 3;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }


        //{ "tweezers_spirit_balls",          "Взять смоченные марлевые шарики" },
        if (tool.CodeName == "tweezers" && actionCode == "tweezers_balls")
        {
            TweezersHelper.GetBalls(ref tool);
            int lastStep4Spirit = !wearGown ? 2 : 3;
            lastStep4Spirit = !shave ? lastStep4Spirit : lastStep4Spirit + 1;
            returnedStep = exam.LastTakenStep() == lastStep4Spirit ? 3 : 6;
            returnedStep = !wearGown ? returnedStep : returnedStep + 1;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }

        //{ "spirit_disinfection",            "Дезинфекция спиртом. Протереть сверху вниз." },
        //{ "iodine_disinfection",            "Дезинфекция йодом. Протереть сверху вниз." },
        if (tool.CodeName == "tweezers" && actionCode == "top_down")
        {
            if (locatedColliderTag != targetLocatedColliderTag)
                errorMessage = "Дезинфекцию надо делать не тут";

            int lastStep4Spirit = !wearGown ? 3 : 4;
            lastStep4Spirit = !shave ? lastStep4Spirit : lastStep4Spirit + 1;
            returnedStep = exam.LastTakenStep() == lastStep4Spirit ? 4 : 7;
            returnedStep = !wearGown ? returnedStep : returnedStep + 1;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }
        if (tool.CodeName == "tweezers" && actionCode == "right_left")
        {
            errorMessage = "Не так происходит дезинфекция";
            int lastStep4Spirit = !wearGown ? 3 : 4;
            lastStep4Spirit = !shave ? lastStep4Spirit : lastStep4Spirit + 1;
            returnedStep = exam.LastTakenStep() == lastStep4Spirit ? 4 : 7;
            returnedStep = !wearGown ? returnedStep : returnedStep + 1;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }

        //{ "iodine_balls",                   "Промокнуть марлевые шарики 1% раствором йодоната" },
        if (tool.CodeName == "gauze_balls" && actionCode == "iodine_1")
        {
            BallHelper.TryWetBall(ref tool, "iodine_1", out errorMessage);
            returnedStep = !wearGown ? 5 : 6;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }
        if (tool.CodeName == "gauze_balls" && actionCode == "iodine_3")
        {
            errorMessage = "Не та жидкость";
            returnedStep = !wearGown ? 5 : 6;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }

        returnedStep = 0;
        return false;
    }

    public static bool BiosafetyInjections(this BaseExam exam, ref ToolItem tool, string actionCode, ref string errorMessage, string locatedColliderTag, string targetLocatedColliderTag, out int returnedStep, bool wearGown = false, bool shave = false)
    {
        // { "shave_pubis",                    "Побрить лобковую зону" },
        if (shave && tool.CodeName == "razor" && actionCode == "shave_pubis")
        {
            tool.StateParams["shave_pubis"] = "true";
            returnedStep = 1;
            return true;
        }

        // { "wear_gloves",                    "Надеть перчатки" },
        if (tool.CodeName == "gloves" && actionCode == "wear")
        {
            tool.StateParams["weared"] = "true";
            returnedStep = !shave ? 1 : 2;
            return true;
        }

        // { "wear_gown",                      "Надеть халат" },
        if (wearGown && tool.CodeName == "gown" && actionCode == "wear")
        {
            tool.StateParams["weared"] = "true";
            returnedStep = !shave ? 2 : 3;
            return true;
        }

        //{ "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        if (tool.CodeName == "gauze_balls" && actionCode == "spirit_60")
        {
            errorMessage = "Не та жидкость";
            returnedStep = !wearGown ? 2 : 3;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }
        if (tool.CodeName == "gauze_balls" && actionCode == "spirit_70")
        {
            BallHelper.TryWetBall(ref tool, "spirit_70", out errorMessage);
            returnedStep = !wearGown ? 2 : 3;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }
        if (tool.CodeName == "gauze_balls" && actionCode == "spirit_80")
        {
            errorMessage = "Не та жидкость";
            returnedStep = !wearGown ? 2 : 3;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }


        //{ "tweezers_spirit_balls",          "Взять смоченные марлевые шарики" },
        if (tool.CodeName == "tweezers" && actionCode == "tweezers_balls")
        {
            TweezersHelper.GetBalls(ref tool);
            int lastStep4Spirit = !wearGown ? 2 : 3;
            lastStep4Spirit = !shave ? lastStep4Spirit : lastStep4Spirit + 1;
            returnedStep = exam.LastTakenStep() == lastStep4Spirit ? 3 : 6;
            returnedStep = !wearGown ? returnedStep : returnedStep + 1;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }

        //{ "spirit_disinfection",            "Дезинфекция спиртом. Протереть сверху вниз." },
        //{ "iodine_disinfection",            "Дезинфекция йодом. Протереть сверху вниз." },
        if (tool.CodeName == "tweezers" && actionCode == "top_down")
        {
            if (locatedColliderTag != targetLocatedColliderTag)
                errorMessage = "Дезинфекцию надо делать не тут";

            int lastStep4Spirit = !wearGown ? 3 : 4;
            lastStep4Spirit = !shave ? lastStep4Spirit : lastStep4Spirit + 1;
            returnedStep = exam.LastTakenStep() == lastStep4Spirit ? 4 : 7;
            returnedStep = !wearGown ? returnedStep : returnedStep + 1;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }
        if (tool.CodeName == "tweezers" && actionCode == "right_left")
        {
            errorMessage = "Не так происходит дезинфекция";
            int lastStep4Spirit = !wearGown ? 3 : 4;
            lastStep4Spirit = !shave ? lastStep4Spirit : lastStep4Spirit + 1;
            returnedStep = exam.LastTakenStep() == lastStep4Spirit ? 4 : 7;
            returnedStep = !wearGown ? returnedStep : returnedStep + 1;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }

        //{ "iodine_balls",                   "Промокнуть марлевые шарики 1% раствором йодоната" },
        if (tool.CodeName == "gauze_balls" && actionCode == "iodine_1")
        {
            BallHelper.TryWetBall(ref tool, "iodine_1", out errorMessage);
            returnedStep = !wearGown ? 5 : 6;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }
        if (tool.CodeName == "gauze_balls" && actionCode == "iodine_3")
        {
            errorMessage = "Не та жидкость";
            returnedStep = !wearGown ? 5 : 6;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }

        returnedStep = 0;
        return false;
    }
}

