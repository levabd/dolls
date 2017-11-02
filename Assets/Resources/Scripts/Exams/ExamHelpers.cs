using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
public static class ExamHelpers
{
    public static bool GenericMoveHelper(this BaseExam exam, string colliderTag, string finalColliderTag, ref string errorMessage)
    {

        if ((CurrentTool.Instance.Tool.CodeName == "syringe" || CurrentTool.Instance.Tool.CodeName == "venflon")
            && colliderTag != finalColliderTag && colliderTag != "vein_target")
        {
            errorMessage = "Пункция не в том месте";
            if (exam.NeedleInsideTarget) // Прошли вену навылет
                errorMessage = "Прошли вену навылет. Гематома";
            return false;
        }

        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && colliderTag != "disinfection_target")
        {
            errorMessage = "Дезинфекция не в том месте";
            return false;
        }

        if (CurrentTool.Instance.Tool.CodeName == "tweezers" && colliderTag != "disinfection_target")
        {
            errorMessage = "Дезинфекция не в том месте";
            return false;
        }

        if (CurrentTool.Instance.Tool.CodeName == "tourniquet" && colliderTag != "tourniquet_target")
        {
            errorMessage = "Не туда наложен жгут";
            return false;
        }

        if (CurrentTool.Instance.Tool.CodeName == "hand" && colliderTag != "palpation_target" && colliderTag != "clamp_target" && colliderTag != "stretch_target")
        {
            errorMessage = "Пальпируется и зажимается не то место";
            return false;
        }

        return true;
    }

    public static bool CateterFinalise(this BaseExam exam, string actionCode, ref string errorMessage, string locatedColliderTag, string catheterConductor, int expectedFirstStep, out int returnedStep)
    {
        //{ "wire_insertion",                 "Вставка проводника." },
        if (CurrentTool.Instance.Tool.CodeName == "standart_catheter_conductor" && actionCode == "push")
        {
            if (exam.LastTakenStep() != expectedFirstStep - 1)
                errorMessage = "Некуда вставить проводник";
            returnedStep = expectedFirstStep;
            return true;
        }
        if (CurrentTool.Instance.Tool.CodeName == "soft_catheter_conductor" && actionCode == "push")
        {
            errorMessage = "Не тот проводник";
            returnedStep = expectedFirstStep;
            return true;
        }

        //{ "needle_removing",                "Удаление иглы." },
        if (CurrentTool.Instance.Tool.CodeName == "needle" && actionCode == "needle_removing")
        {
            if (exam.LastTakenStep() != expectedFirstStep)
                errorMessage = "Нельзя удалять иглу без проводника";
            returnedStep = expectedFirstStep + 1;
            return true;
        }
        //{ "catheter_insertion",             "Вставка катетера по проводнику." },
        if (CurrentTool.Instance.Tool.CodeName == "catheter" && actionCode == "push")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 1)
                errorMessage = "Нельзя вставить катетер без проводника";
            returnedStep = expectedFirstStep + 2;
            return true;
        }

        //{ "catheter_pushing",               "Углубление вращательными движениями." },
        if (CurrentTool.Instance.Tool.CodeName == "catheter" && actionCode == "rotation_insertion")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 2)
                errorMessage = "Некуда углублять катетер";
            returnedStep = expectedFirstStep + 3;
            return true;
        }
        if (CurrentTool.Instance.Tool.CodeName == "catheter" && actionCode == "direct_insertion")
        {
            errorMessage = "Некорректный способ углубления катетера";
            returnedStep = expectedFirstStep + 3;
            return true;
        }

        //{ "wire_removing",                  "Извлечение проводника." },
        if (CurrentTool.Instance.Tool.CodeName == "standart_catheter_conductor" && actionCode == "pull")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 3)
                errorMessage = CurrentTool.Instance.Tool.CodeName == catheterConductor ? "Нельзя удалять проводник на этом шаге" : "Не тот проводник";
            returnedStep = expectedFirstStep + 4;
            return true;
        }
        if (CurrentTool.Instance.Tool.CodeName == "soft_catheter_conductor" && actionCode == "pull")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 3)
                errorMessage = CurrentTool.Instance.Tool.CodeName == catheterConductor ? "Нельзя удалять проводник на этом шаге" : "Не тот проводник";
            returnedStep = expectedFirstStep + 4;
            return true;
        }

        //{ "liquid_transfusion_connection",  "Соединение с системой переливания жидкости." },
        if (CurrentTool.Instance.Tool.CodeName == "catheter" && actionCode == "liquid_transfusion_connection")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 4)
                errorMessage = "Сначала должен быть корректно установлен катетер";
            returnedStep = expectedFirstStep + 5;
            return true;
        }

        // { "get_plaster",                    "Взять пластырь" },
        if (CurrentTool.Instance.Tool.CodeName == "patch" && actionCode == "get")
        {
            returnedStep = expectedFirstStep + 6;
            return true;
        }

        //{ "fixation_with_plaster",          "Фиксация пластырем." }
        if (CurrentTool.Instance.Tool.CodeName == "patch" && actionCode == "stick")
        {
            if (!locatedColliderTag.Contains("catheter"))
                errorMessage = "Не то место установки. Сначала должен быть корректно установлен катетер";
            returnedStep = expectedFirstStep + 7;
            return true;
        }

        returnedStep = 0;
        return false;
    }

    public static bool BiosafetySpiritIodine(this BaseExam exam, string actionCode, ref string errorMessage, string locatedColliderTag, out int returnedStep, bool wearGown = false, bool shave = false)
    {
        // { "shave_pubis",                    "Побрить лобковую зону" },
        if (shave && CurrentTool.Instance.Tool.CodeName == "razor" && actionCode == "shave_pubis")
        {
            CurrentTool.Instance.Tool.StateParams["shave_pubis"] = "true";
            returnedStep = 1;
            return true;
        }

        // { "wear_examination_gloves",        "Надеть смотровые перчатки" },
        if (CurrentTool.Instance.Tool.CodeName == "gloves" && actionCode == "wear_examination")
        {
            CurrentTool.Instance.Tool.Title = "Смотровые перчатки надеты";
            CurrentTool.Instance.Tool.StateParams["weared_examination"] = "true";
            CurrentTool.Instance.Tool.StateParams["weared_sterile"] = "false";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[2];
            returnedStep = !shave ? 1 : 2;
            return true;
        }

        // { "wear_gown",                      "Надеть халат" },
        if (wearGown && CurrentTool.Instance.Tool.CodeName == "gown" && actionCode == "wear")
        {
            CurrentTool.Instance.Tool.StateParams["weared"] = "true";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[2];
            returnedStep = !shave ? 2 : 3;
            return true;
        }

        //{ "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode.Contains("spirit"))
        {
            BallHelper.TryWetBall(actionCode, "spirit_p70", out errorMessage);
            returnedStep = !wearGown ? 2 : 3;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            exam.CurrentBallLiquid = "spirit";
            return true;
        }

        //{ "tweezers_spirit_balls",          "Взять смоченные марлевые шарики" },
        if (CurrentTool.Instance.Tool.CodeName == "tweezers" && actionCode == "tweezers_balls")
        {
            int lastStep4Spirit = !wearGown ? 2 : 3;
            lastStep4Spirit = !shave ? lastStep4Spirit : lastStep4Spirit + 1;
            TweezersHelper.GetBalls(exam.CurrentBallLiquid);
            returnedStep = exam.LastTakenStep() == lastStep4Spirit ? 3 : 6;
            returnedStep = !wearGown ? returnedStep : returnedStep + 1;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }

        //{ "spirit_disinfection",            "Дезинфекция спиртом. Протереть сверху вниз." },
        //{ "iodine_disinfection",            "Дезинфекция йодом. Протереть сверху вниз." },
        if (CurrentTool.Instance.Tool.CodeName == "tweezers" && actionCode == "top_down")
        {
            if (locatedColliderTag != "disinfection_target")
                errorMessage = "Дезинфекцию надо делать не тут";

            int lastStep4Spirit = !wearGown ? 3 : 4;
            lastStep4Spirit = !shave ? lastStep4Spirit : lastStep4Spirit + 1;
            returnedStep = exam.LastTakenStep() == lastStep4Spirit ? 4 : 7;
            returnedStep = !wearGown ? returnedStep : returnedStep + 1;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }
        if (CurrentTool.Instance.Tool.CodeName == "tweezers" && actionCode == "right_left")
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
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode.Contains("iodine"))
        {
            BallHelper.TryWetBall(actionCode, "iodine_p1", out errorMessage);
            returnedStep = !wearGown ? 5 : 6;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            exam.CurrentBallLiquid = "iodine";
            return true;
        }

        // { "wear_sterile_gloves",        "Сменить перчатки на стерильные" },
        if (CurrentTool.Instance.Tool.CodeName == "gloves" && actionCode == "wear_sterile")
        {
            CurrentTool.Instance.Tool.Title = "Стерильные перчатки надеты";
            CurrentTool.Instance.Tool.StateParams["weared_sterile"] = "true";
            CurrentTool.Instance.Tool.StateParams["weared_examination"] = "false";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[3];
            returnedStep = 8;
            returnedStep = !wearGown ? returnedStep : returnedStep + 1;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }

        returnedStep = 0;
        return false;
    }

    public static bool FenceInjections(this BaseExam exam, string actionCode, ref string errorMessage, string locatedColliderTag, out int returnedStep, string finalTarget, bool injection = false)
    {
        // { "wear_examination_gloves",        "Надеть смотровые перчатки" },
        if (CurrentTool.Instance.Tool.CodeName == "gloves" && actionCode == "wear_examination")
        {
            CurrentTool.Instance.Tool.Title = "Смотровые перчатки надеты";
            CurrentTool.Instance.Tool.StateParams["weared_examination"] = "true";
            CurrentTool.Instance.Tool.StateParams["weared_sterile"] = "false";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[2];
            returnedStep = 1;
            return true;
        }

        // { "puncture_needle",                "Взять иглу для забора крови" },
		if (exam.GetNeedleAction(actionCode, ref errorMessage, "simple_needle", 1))
        {
            returnedStep = 2;
            return true;
        }

        // { "filling_drug solution",          "Наполнить лекарственным раствором" },
        if (injection && CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_drug_solution")
        {
            returnedStep = 3;
            return true;
        }

        // { "tourniquet",                     "Взять жгут и наложить" },
        if (CurrentTool.Instance.Tool.CodeName == "tourniquet" && actionCode == "lay")
        {
            if (!locatedColliderTag.Contains("tourniquet_target"))
                errorMessage = "Не туда наложен жгут";
            // Вена увеличивается.
            returnedStep = injection ? 4 : 3;
            return true;
        }

        // { "palpation",                      "Пальпируем вену." },
        if (CurrentTool.Instance.Tool.CodeName == "hand" && actionCode == "palpation")
        {
            if (!locatedColliderTag.Contains("palpation_target"))
                errorMessage = "Пальпируется не то место";
            returnedStep = injection ? 5 : 4;
            return true;
        }

        //{ "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode.Contains("spirit"))
        {
            BallHelper.TryWetBall(actionCode, "spirit_p70", out errorMessage);
            returnedStep = exam.LastTakenStep() == 4 ? 5 : 12;
            returnedStep = injection ? returnedStep + 1 : returnedStep;
            exam.CurrentBallLiquid = "spirit";
            return true;
        }

        // { "balls_spirit_disinfection",      "Дезинфекция спиртом. Протереть сверху вниз." },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode == "top_down")
        {
            if (locatedColliderTag != "disinfection_target")
                errorMessage = "Дезинфекцию надо делать не тут";
            returnedStep = injection ? 7 : 6;
            return true;
        }

        // { "throw_balls",                    "Выкинуть шарики." },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode == "throw_balls")
        {
            BallHelper.ClearBall(exam);
            returnedStep = injection ? 8 : 7;
            return true;
        }

        // { "wear_sterile_gloves",        "Сменить перчатки на стерильные" },
        if (CurrentTool.Instance.Tool.CodeName == "gloves" && actionCode == "wear_sterile")
        {
            CurrentTool.Instance.Tool.Title = "Стерильные перчатки надеты";
            CurrentTool.Instance.Tool.StateParams["weared_examination"] = "false";
            CurrentTool.Instance.Tool.StateParams["weared_sterile"] = "true";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[3];
            returnedStep = injection ? 9 : 8;
            return true;
        }

        // { "stretch_the_skin",               "Натянуть кожу." },
        if (CurrentTool.Instance.Tool.CodeName == "hand" && actionCode == "stretch_the_skin")
        {
            if (!locatedColliderTag.Contains("stretch_target"))
                errorMessage = "Натянуто не то место";
            returnedStep = injection ? 10 : 9;
            return true;
        }

        // { "take_the_blood_ml10",              "Набрать 10мл. крови." },
        if (!injection && CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "take_the_blood_ml10")
        {
            if (!CurrentTool.Instance.Tool.StateParams.ContainsKey("has_needle") || !Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["has_needle"]))
            {
                errorMessage = "Отсутсвует игла";
                returnedStep = 0;
                return false;
            }

            if (locatedColliderTag != finalTarget)
                errorMessage = "Забор крови не из того места";
            else
            {
                if (exam.LastTakenStep() != 9)
                    errorMessage = "Не была натянута кожа";
                else if (!CurrentTool.Instance.Tool.StateParams.ContainsKey("entry_angle") || !float.Parse(CurrentTool.Instance.Tool.StateParams["entry_angle"]).CheckRange(29, 31))
                    errorMessage = "Неправильный угол установки";
                else
                    CurrentTool.Instance.Tool.StateParams["blood_inside"] = "true";
                // Запустить анимацию крови
                    Material mat_blood = Resources.Load("Prefabs/Medicine_and_Health/Models/Materials/Syringe_df_blood", typeof(Material)) as Material;
                    Material[] mats = GameObject.Find("SyringeElone").transform.GetChild(0).gameObject.GetComponent<Renderer>().materials;
                    mats[0] = mat_blood;
                    GameObject.Find("SyringeElone").transform.GetChild(0).gameObject.GetComponent<Renderer>().materials = mats;
            }
            returnedStep = 10;
            return true;
        }

        // { "remove_tourniquet",              "Снимаем жгут." },
        if (CurrentTool.Instance.Tool.CodeName == "tourniquet" && actionCode == "remove")
        {
            // Вена руки уменьшается.
            returnedStep = 11;
            return true;
        }

        // { "administer_drug",        "Ввести препарат" }
        if (injection && CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "administer_drug")
        {
            if (locatedColliderTag != finalTarget)
                errorMessage = "Воод препарата не в то место";
            else
            {
                if (!CurrentTool.Instance.Tool.StateParams.ContainsKey("entry_angle") || !float.Parse(CurrentTool.Instance.Tool.StateParams["entry_angle"]).CheckRange(29, 31))
                    errorMessage = "Неправильный угол установки";
                else
                    CurrentTool.Instance.Tool.StateParams["blood_inside"] = "true";
                // Запустить анимацию крови
                    Material mat_blood = Resources.Load("Prefabs/Medicine_and_Health/Models/Materials/Syringe_df_blood", typeof(Material)) as Material;
                    Material[] mats = GameObject.Find("SyringeElone").transform.GetChild(0).gameObject.GetComponent<Renderer>().materials;
                    mats[0] = mat_blood;
                    GameObject.Find("SyringeElone").transform.GetChild(0).gameObject.GetComponent<Renderer>().materials = mats;
            }
            returnedStep = 12;
            return true;
        }

        // { "attach_balls",                  "Прикладываем к месту инъекции ватный шарик." },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode == "attach_balls")
        {
            if (locatedColliderTag != "disinfection_target")
                errorMessage = "Дезинфекцию надо делать не тут";
            returnedStep = injection ? 14 : 13;
            return true;
        }

        // { "needle_pull",                    "Извлечь иглу." },
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "needle_pull")
        {
            if (exam.LastTakenStep() != (injection ? 14 : 13))
                errorMessage = "Сепсис";
            // else
            //     Рука сгибается.
            returnedStep = injection ? 15 : 14;
            return true;
        }

        // { "put_on_the_cap",                 "Надеть колпачек на иглу." },
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "put_on_the_cap")
        {
            if (exam.LastTakenStep() != (injection ? 15 : 14))
                errorMessage = "Игла в теле или была давно извлечена. Сначала надо было извлечь и сразу надеть колпачек";
            returnedStep = injection ? 16 : 15;
            return true;
        }

        // { "throw_needle",                   "Выбросить иглу." }
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "throw_needle")
        {
            if (exam.LastTakenStep() != (injection ? 16 : 15))
                errorMessage = "Это действие надо совершать сразу после надевания на иглу колпачка";
            returnedStep = injection ? 17 : 16;
            return true;
        }

        returnedStep = 0;
        return false;
    }

    public static bool VenflonInstallation(this BaseExam exam, string actionCode, ref string errorMessage, string locatedColliderTag, out int returnedStep, string finalTarget, bool head = false)
    {
        // { "wear_examination_gloves",        "Надеть смотровые перчатки" },
        if (CurrentTool.Instance.Tool.CodeName == "gloves" && actionCode == "wear_examination")
        {
            CurrentTool.Instance.Tool.Title = "Смотровые перчатки надеты";
            CurrentTool.Instance.Tool.StateParams["weared_examination"] = "true";
            CurrentTool.Instance.Tool.StateParams["weared_sterile"] = "false";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[2];
            returnedStep = 1;
            return true;
        }

        // { "tourniquet",                     "Взять жгут и наложить" },
        if (CurrentTool.Instance.Tool.CodeName == "tourniquet" && actionCode == "lay")
        {
            if (!locatedColliderTag.Contains("tourniquet_target"))
                errorMessage = "Не туда наложен жгут";
            // Вена увеличивается.
            returnedStep = 2;
            return true;
        }

        // { "palpation",                      "Пальпируем вену." },
        if (CurrentTool.Instance.Tool.CodeName == "hand" && actionCode == "palpation")
        {
            if (!locatedColliderTag.Contains("palpation_target"))
                errorMessage = "Пальпируется не то место";
            returnedStep = 3;
            return true;
        }

        //{ "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode.Contains("spirit"))
        {
            BallHelper.TryWetBall(actionCode, "spirit_p70", out errorMessage);
            exam.CurrentBallLiquid = "spirit";
            returnedStep = 4;
            return true;
        }

        // { "balls_spirit_disinfection",      "Дезинфекция спиртом. Протереть сверху вниз." },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode == "top_down")
        {
            if (locatedColliderTag != "disinfection_target")
                errorMessage = "Дезинфекцию надо делать не тут";
            returnedStep = 5;
            return true;
        }

        // { "throw_balls",                    "Выкинуть шарики." },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode == "throw_balls")
        {
            BallHelper.ClearBall(exam);
            returnedStep = 6;
            return true;
        }

        // { "wear_sterile_gloves",        "Сменить перчатки на стерильные" },
        if (CurrentTool.Instance.Tool.CodeName == "gloves" && actionCode == "wear_sterile")
        {
            CurrentTool.Instance.Tool.Title = "Стерильные перчатки надеты";
            CurrentTool.Instance.Tool.StateParams["weared_sterile"] = "true";
            CurrentTool.Instance.Tool.StateParams["weared_examination"] = "false";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[3];
            returnedStep = 7;
            return true;
        }

        // { "stretch_the_skin",               "Натянуть кожу." },
        if (CurrentTool.Instance.Tool.CodeName == "hand" && actionCode == "stretch_the_skin")
        {
            if (!locatedColliderTag.Contains("stretch_target"))
                errorMessage = "Натянуто не то место";
            returnedStep = 8;
            return true;
        }

        // { "pull_mandren",                   "Потягиваем мадрен." },
        if (CurrentTool.Instance.Tool.CodeName == "venflon" && actionCode == "pull_mandren")
        {
            if (locatedColliderTag != finalTarget)
                errorMessage = "Не то место для катетеризации";
            else
            {
                if (exam.LastTakenStep() != 8)
                    errorMessage = "Не была натянута кожа";
                else if (!CurrentTool.Instance.Tool.StateParams.ContainsKey("entry_angle") || !float.Parse(CurrentTool.Instance.Tool.StateParams["entry_angle"]).CheckRange(head ? 20 : 10, head ? 30 : 20))
                    errorMessage = "Неправильный угол установки";
                else
                    CurrentTool.Instance.Tool.StateParams["mandren_pulling"] = "true";
            }
            returnedStep = 9;
            return true;
        }

        // { "remove_tourniquet",              "Снимаем жгут." },
        if (CurrentTool.Instance.Tool.CodeName == "tourniquet" && actionCode == "remove")
        {
            if (!locatedColliderTag.Contains("tourniquet_target"))
                errorMessage = "Не туда наложен жгут";
            returnedStep = 10;
            return true;
        }

        // { "clamp_the_vein",                 "Пережать вену." },
        if (CurrentTool.Instance.Tool.CodeName == "hand" && actionCode == "clamp")
        {
            if (!locatedColliderTag.Contains("clamp_target"))
                errorMessage = "Сдавлена не та вена(место)";
            returnedStep = 11;
            return true;
        }

        // { "remove_mandren",                 "Вытаскиваем мадрен." },
        if (CurrentTool.Instance.Tool.CodeName == "venflon" && actionCode == "remove_mandren")
        {
            if (exam.LastTakenStep() != 10)
                errorMessage = "Не была пережата вена";
            returnedStep = 12;
            return true;
        }

        // { "filling_nacl_half",              "Наполнить 0,9% раствором натрия хлорида наполовину"},
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_nacl_half")
        {
            returnedStep = 13;
            return true;
        }

        // { "nacl_to_cateter",                "Ввести физраствор в катетер" },
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "nacl_to_cateter")
        {
            returnedStep = 14;
            return true;
        }

        // { "liquid_transfusion_connection",  "Соединение с системой переливания жидкости." },
        if (CurrentTool.Instance.Tool.CodeName == "venflon" && actionCode == "liquid_transfusion_connection")
        {
            if (exam.LastTakenStep() != (head ? 14 : 12))
                errorMessage = "Сначала должен быть корректно установлен катетер";
            returnedStep = head ? 15 : 13;
            return true;
        }

        // { "get_plaster",                    "Взять пластырь" },
        if (CurrentTool.Instance.Tool.CodeName == "patch" && actionCode == "get")
        {
            returnedStep = head ? 16 : 14;
            return true;
        }

        // { "fixation_with_plaster",          "Фиксация пластырем." }
        if (CurrentTool.Instance.Tool.CodeName == "patch" && actionCode == "stick")
        {
            if (!locatedColliderTag.Contains("catheter"))
                errorMessage = "Не то место установки. Сначала должен быть корректно установлен катетер";
            returnedStep = head ? 16 : 14;
            return true;
        }

        returnedStep = 0;
        return false;
    }
}

