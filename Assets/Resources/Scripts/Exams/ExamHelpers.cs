using System;

// ReSharper disable once CheckNamespace
public static class ExamHelpers
{
    public static bool CateterFinalise(this BaseExam exam, ref ToolItem tool, string actionCode, ref string errorMessage, string locatedColliderTag, string catheterConductor, int expectedFirstStep, out int returnedStep)
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
            return true;
        }

        //{ "needle_removing",                "Удаление иглы." },
        if (tool.CodeName == "needle" && actionCode == "needle_removing")
        {
            if (exam.LastTakenStep() != expectedFirstStep)
                errorMessage = "Нельзя удалять иглу без проводника";
            returnedStep = expectedFirstStep + 1;
            return true;
        }
        //{ "catheter_insertion",             "Вставка катетера по проводнику." },
        if (tool.CodeName == "catheter" && actionCode == "push")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 1)
                errorMessage = "Нельзя вставить катетер без проводника";
            returnedStep = expectedFirstStep + 2;
            return true;
        }

        //{ "catheter_pushing",               "Углубление вращательными движениями." },
        if (tool.CodeName == "catheter" && actionCode == "rotation_insertion")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 2)
                errorMessage = "Некуда углублять катетер";
            returnedStep = expectedFirstStep + 3;
            return true;
        }
        if (tool.CodeName == "catheter" && actionCode == "direct_insertion")
        {
            errorMessage = "Некорректный способ углубления катетера";
            returnedStep = expectedFirstStep + 3;
            return true;
        }

        //{ "wire_removing",                  "Извлечение проводника." },
        if (tool.CodeName == "standart_catheter_conductor" && actionCode == "pull")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 3)
                errorMessage = tool.CodeName == catheterConductor ? "Нельзя удалять проводник на этом шаге" : "Не тот проводник";
            returnedStep = expectedFirstStep + 4;
            return true;
        }
        if (tool.CodeName == "soft_catheter_conductor" && actionCode == "pull")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 3)
                errorMessage = tool.CodeName == catheterConductor ? "Нельзя удалять проводник на этом шаге" : "Не тот проводник";
            returnedStep = expectedFirstStep + 4;
            return true;
        }

        //{ "liquid_transfusion_connection",  "Соединение с системой переливания жидкости." },
        if (tool.CodeName == "catheter" && actionCode == "liquid_transfusion_connection")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 4)
                errorMessage = "Сначала должен быть корректно установлен катетер";
            returnedStep = expectedFirstStep + 5;
            return true;
        }

        // { "get_plaster",                    "Взять пластырь" },
        if (tool.CodeName == "patch" && actionCode == "get")
        {
            returnedStep = expectedFirstStep + 6;
            return true;
        }

        //{ "fixation_with_plaster",          "Фиксация пластырем." }
        if (tool.CodeName == "patch" && actionCode == "stick")
        {
            if (!locatedColliderTag.Contains("catheter"))
                errorMessage = "Не то место установки. Сначала должен быть корректно установлен катетер";
            returnedStep = expectedFirstStep + 7;
            return true;
        }

        returnedStep = 0;
        return false;
    }

    public static bool BiosafetySpiritIodine(this BaseExam exam, ref ToolItem tool, string actionCode, ref string errorMessage, string locatedColliderTag, string targetLocatedColliderTag, out int returnedStep, ref string currentBallLiquid, bool wearGown = false, bool shave = false)
    {
        // { "shave_pubis",                    "Побрить лобковую зону" },
        if (shave && tool.CodeName == "razor" && actionCode == "shave_pubis")
        {
            tool.StateParams["shave_pubis"] = "true";
            returnedStep = 1;
            return true;
        }

        // { "wear_examination_gloves",        "Надеть смотровые перчатки" },
        if (tool.CodeName == "gloves" && actionCode == "wear_examination_gloves")
        {
            tool.Title = "Смотровые перчатки надеты";
            tool.StateParams["weared_examination"] = "true";
            tool.StateParams["weared_sterile"] = "false";
            tool.Sprites[0] = tool.Sprites[2];
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
        if (tool.CodeName == "gauze_balls" && actionCode.Contains("spirit"))
        {
            BallHelper.TryWetBall(ref tool, actionCode, "spirit_p70", out errorMessage);
            returnedStep = !wearGown ? 2 : 3;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            currentBallLiquid = "spirit";
            return true;
        }

        //{ "tweezers_spirit_balls",          "Взять смоченные марлевые шарики" },
        if (tool.CodeName == "tweezers" && actionCode == "tweezers_balls")
        {
            int lastStep4Spirit = !wearGown ? 2 : 3;
            lastStep4Spirit = !shave ? lastStep4Spirit : lastStep4Spirit + 1;
            TweezersHelper.GetBalls(ref tool, currentBallLiquid);
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
        if (tool.CodeName == "gauze_balls" && actionCode.Contains("iodine"))
        {
            BallHelper.TryWetBall(ref tool, actionCode, "iodine_p1", out errorMessage);
            returnedStep = !wearGown ? 5 : 6;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            currentBallLiquid = "iodine";
            return true;
        }

        // { "wear_sterile_gloves",        "Сменить перчатки на стерильные" },
        if (tool.CodeName == "gloves" && actionCode == "wear_sterile_gloves")
        {
            tool.Title = "Стерильные перчатки надеты";
            tool.StateParams["weared_sterile"] = "true";
            tool.StateParams["weared_examination"] = "false";
            tool.Sprites[0] = tool.Sprites[3];
            returnedStep = 8;
            returnedStep = !wearGown ? returnedStep : returnedStep + 1;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }

        returnedStep = 0;
        return false;
    }

    public static bool FenceInjections(this BaseExam exam, ref ToolItem tool, string actionCode, ref string errorMessage, string locatedColliderTag, out int returnedStep,
        string tourniquetCollider, string disinfectionCollider, string palpationCollider, string stretchCollider, string finalTarget, ref string currentBallLiquid, bool injection = false)
    {
        // { "wear_examination_gloves",        "Надеть смотровые перчатки" },
        if (tool.CodeName == "gloves" && actionCode == "wear_examination_gloves")
        {
            tool.Title = "Смотровые перчатки надеты";
            tool.StateParams["weared_examination"] = "true";
            tool.StateParams["weared_sterile"] = "false";
            tool.Sprites[0] = tool.Sprites[2];
            returnedStep = 1;
            return true;
        }

        // { "puncture_needle",                "Взять иглу для забора крови" },
		if (exam.GetNeedleAction(ref tool, actionCode, ref errorMessage, "simple_needle", 1))
        {
            returnedStep = 2;
            return true;
        }

        // { "filling_drug solution",          "Наполнить лекарственным раствором" },
        if (injection && tool.CodeName == "syringe" && actionCode == "filling_drug_solution")
        {
            returnedStep = 3;
            return true;
        }

        // { "tourniquet",                     "Взять жгут и наложить" },
        if (tool.CodeName == "tourniquet" && actionCode == "lay")
        {
            if (!locatedColliderTag.Contains(tourniquetCollider))
                errorMessage = "Не туда наложен жгут";
            // Вена увеличивается.
            returnedStep = injection ? 4 : 3;
            return true;
        }

        // { "palpation",                      "Пальпируем вену." },
        if (tool.CodeName == "hand" && actionCode == "palpation")
        {
            if (!locatedColliderTag.Contains(palpationCollider))
                errorMessage = "Пальпируется не то место";
            returnedStep = injection ? 5 : 4;
            return true;
        }

        //{ "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        if (tool.CodeName == "gauze_balls" && actionCode.Contains("spirit"))
        {
            BallHelper.TryWetBall(ref tool, actionCode, "spirit_p70", out errorMessage);
            returnedStep = exam.LastTakenStep() == 4 ? 5 : 12;
            returnedStep = injection ? returnedStep + 1 : returnedStep;
            currentBallLiquid = "spirit";
            return true;
        }

        // { "balls_spirit_disinfection",      "Дезинфекция спиртом. Протереть сверху вниз." },
        if (tool.CodeName == "gauze_balls" && actionCode == "top_down")
        {
            if (locatedColliderTag != disinfectionCollider)
                errorMessage = "Дезинфекцию надо делать не тут";
            returnedStep = injection ? 7 : 6;
            return true;
        }

        // { "throw_balls",                    "Выкинуть шарики." },
        if (tool.CodeName == "gauze_balls" && actionCode == "throw_balls")
        {
            returnedStep = injection ? 8 : 7;
            return true;
        }

        // { "wear_sterile_gloves",        "Сменить перчатки на стерильные" },
        if (tool.CodeName == "gloves" && actionCode == "wear_sterile_gloves")
        {
            tool.Title = "Стерильные перчатки надеты";
            tool.StateParams["weared_examination"] = "false";
            tool.StateParams["weared_sterile"] = "true";
            tool.Sprites[0] = tool.Sprites[3];
            returnedStep = injection ? 9 : 8;
            return true;
        }

        // { "stretch_the_skin",               "Натянуть кожу." },
        if (tool.CodeName == "hand" && actionCode == "stretch_the_skin")
        {
            if (!locatedColliderTag.Contains(stretchCollider))
                errorMessage = "Натянуто не то место";
            returnedStep = injection ? 10 : 9;
            return true;
        }

        // { "take_the_blood_ml10",              "Набрать 10мл. крови." },
        if (!injection && tool.CodeName == "syringe" && actionCode == "take_the_blood_ml10")
        {
            if (!tool.StateParams.ContainsKey("has_needle") || !Convert.ToBoolean(tool.StateParams["has_needle"]))
            {
                errorMessage = "Отсутсвует игла";
                returnedStep = 0;
                return false;
            }

            if (locatedColliderTag != finalTarget)
                errorMessage = "Забор крови не из того места";
            else
            {
                if (exam.LastTakenStep() != 8)
                    errorMessage = "Не была натянута кожа";
                else if (!tool.StateParams.ContainsKey("entry_angle") || !float.Parse(tool.StateParams["entry_angle"]).CheckRange(29, 31))
                    errorMessage = "Неправильный угол установки";
                else
                    tool.StateParams["blood_inside"] = "true";
                // Запустить анимацию крови
            }
            returnedStep = 10;
            return true;
        }

        // { "remove_tourniquet",              "Снимаем жгут." },
        if (tool.CodeName == "tourniquet" && actionCode == "remove")
        {
            if (!locatedColliderTag.Contains(tourniquetCollider))
                errorMessage = "Не туда наложен жгут";
            // Вена руки уменьшается.
            returnedStep = 11;
            return true;
        }

        // { "administer_drug",        "Ввести препарат" }
        if (injection && tool.CodeName == "syringe" && actionCode == "administer_drug")
        {
            if (locatedColliderTag != finalTarget)
                errorMessage = "Воод препарата не в то место";
            else
            {
                if (!tool.StateParams.ContainsKey("entry_angle") || !float.Parse(tool.StateParams["entry_angle"]).CheckRange(29, 31))
                    errorMessage = "Неправильный угол установки";
                else
                    tool.StateParams["blood_inside"] = "true";
                // Запустить анимацию крови
            }
            returnedStep = 12;
            return true;
        }

        // { "attach_balls",                  "Прикладываем к месту инъекции ватный шарик." },
        if (tool.CodeName == "gauze_balls" && actionCode == "attach_balls")
        {
            if (locatedColliderTag != disinfectionCollider)
                errorMessage = "Дезинфекцию надо делать не тут";
            returnedStep = injection ? 14 : 13;
            return true;
        }

        // { "needle_pull",                    "Извлечь иглу." },
        if (tool.CodeName == "syringe" && actionCode == "needle_pull")
        {
            if (exam.LastTakenStep() != (injection ? 14 : 13))
                errorMessage = "Сепсис";
            // else
            //     Рука сгибается.
            returnedStep = injection ? 15 : 14;
            return true;
        }

        // { "put_on_the_cap",                 "Надеть колпачек на иглу." },
        if (tool.CodeName == "syringe" && actionCode == "put_on_the_cap")
        {
            if (exam.LastTakenStep() != (injection ? 15 : 14))
                errorMessage = "Игла в теле или была давно извлечена. Сначала надо было извлечь и сразу надеть колпачек";
            returnedStep = injection ? 16 : 15;
            return true;
        }

        // { "throw_needle",                   "Выбросить иглу." }
        if (tool.CodeName == "syringe" && actionCode == "put_on_the_cap")
        {
            if (exam.LastTakenStep() != (injection ? 16 : 15))
                errorMessage = "Это действие надо совершать сразу после надевания на иглу колпачка";
            returnedStep = injection ? 17 : 16;
            return true;
        }

        returnedStep = 0;
        return false;
    }

    public static bool VenflonInstallation(this BaseExam exam, ref ToolItem tool, string actionCode, ref string errorMessage, string locatedColliderTag, out int returnedStep,
        string tourniquetCollider, string disinfectionCollider, string palpationCollider, string stretchCollider, string finalTarget, ref string currentBallLiquid, bool head = false)
    {
        // { "wear_examination_gloves",        "Надеть смотровые перчатки" },
        if (tool.CodeName == "gloves" && actionCode == "wear_examination_gloves")
        {
            tool.Title = "Смотровые перчатки надеты";
            tool.StateParams["weared_examination"] = "true";
            tool.StateParams["weared_sterile"] = "false";
            tool.Sprites[0] = tool.Sprites[2];
            returnedStep = 1;
            return true;
        }

        // { "tourniquet",                     "Взять жгут и наложить" },
        if (tool.CodeName == "tourniquet" && actionCode == "lay")
        {
            if (!locatedColliderTag.Contains(tourniquetCollider))
                errorMessage = "Не туда наложен жгут";
            // Вена увеличивается.
            returnedStep = 2;
            return true;
        }

        // { "palpation",                      "Пальпируем вену." },
        if (tool.CodeName == "hand" && actionCode == "palpation")
        {
            if (!locatedColliderTag.Contains(palpationCollider))
                errorMessage = "Пальпируется не то место";
            returnedStep = 3;
            return true;
        }

        //{ "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        if (tool.CodeName == "gauze_balls" && actionCode.Contains("spirit"))
        {
            BallHelper.TryWetBall(ref tool, actionCode, "spirit_p70", out errorMessage);
            currentBallLiquid = "spirit";
            returnedStep = 4;
            return true;
        }

        // { "balls_spirit_disinfection",      "Дезинфекция спиртом. Протереть сверху вниз." },
        if (tool.CodeName == "gauze_balls" && actionCode == "top_down")
        {
            if (locatedColliderTag != disinfectionCollider)
                errorMessage = "Дезинфекцию надо делать не тут";
            returnedStep = 5;
            return true;
        }

        // { "throw_balls",                    "Выкинуть шарики." },
        if (tool.CodeName == "gauze_balls" && actionCode == "throw_balls")
        {
            returnedStep = 6;
            return true;
        }

        // { "wear_sterile_gloves",        "Сменить перчатки на стерильные" },
        if (tool.CodeName == "gloves" && actionCode == "wear_sterile_gloves")
        {
            tool.Title = "Стерильные перчатки надеты";
            tool.StateParams["weared_sterile"] = "true";
            tool.StateParams["weared_examination"] = "false";
            tool.Sprites[0] = tool.Sprites[3];
            returnedStep = 7;
            return true;
        }

        // { "stretch_the_skin",               "Натянуть кожу." },
        if (tool.CodeName == "hand" && actionCode == "stretch_the_skin")
        {
            if (!locatedColliderTag.Contains(stretchCollider))
                errorMessage = "Натянуто не то место";
            returnedStep = 8;
            return true;
        }

        // { "pull_mandren",                   "Потягиваем мадрен." },
        if (tool.CodeName == "venflon" && actionCode == "pull_mandren")
        {
            if (locatedColliderTag != finalTarget)
                errorMessage = "Не то место для катетеризации";
            else
            {
                if (exam.LastTakenStep() != 8)
                    errorMessage = "Не была натянута кожа";
                else if (!tool.StateParams.ContainsKey("entry_angle") || !float.Parse(tool.StateParams["entry_angle"]).CheckRange(head ? 20 : 10, head ? 30 : 20))
                    errorMessage = "Неправильный угол установки";
                else
                    tool.StateParams["mandren_pulling"] = "true";
            }
            returnedStep = 9;
            return true;
        }

        // { "remove_tourniquet",              "Снимаем жгут." },
        if (tool.CodeName == "tourniquet" && actionCode == "remove")
        {
            if (!locatedColliderTag.Contains(tourniquetCollider))
                errorMessage = "Не туда наложен жгут";
            returnedStep = 10;
            return true;
        }

        // { "clamp_the_vein",                 "Пережать вену." },
        if (tool.CodeName == "hand" && actionCode == "clamp")
        {
            if (!locatedColliderTag.Contains("external_jugular_vein"))
                errorMessage = "Сдавлена не та вена(место)";
            returnedStep = 11;
            return true;
        }

        // { "remove_mandren",                 "Вытаскиваем мадрен." },
        if (tool.CodeName == "venflon" && actionCode == "remove_mandren")
        {
            if (exam.LastTakenStep() != 10)
                errorMessage = "Не была пережата вена";
            returnedStep = 12;
            return true;
        }

        // { "filling_nacl_half",              "Наполнить 0,9% раствором натрия хлорида наполовину"},
        if (tool.CodeName == "syringe" && actionCode == "filling_nacl_half")
        {
            returnedStep = 13;
            return true;
        }

        // { "nacl_to_cateter",                "Ввести физраствор в катетер" },
        if (tool.CodeName == "syringe" && actionCode == "nacl_to_cateter")
        {
            returnedStep = 14;
            return true;
        }

        // { "liquid_transfusion_connection",  "Соединение с системой переливания жидкости." },
        if (tool.CodeName == "venflon" && actionCode == "liquid_transfusion_connection")
        {
            if (exam.LastTakenStep() != (head ? 14 : 12))
                errorMessage = "Сначала должен быть корректно установлен катетер";
            returnedStep = head ? 15 : 13;
            return true;
        }

        // { "get_plaster",                    "Взять пластырь" },
        if (tool.CodeName == "patch" && actionCode == "get")
        {
            returnedStep = head ? 16 : 14;
            return true;
        }

        // { "fixation_with_plaster",          "Фиксация пластырем." }
        if (tool.CodeName == "patch" && actionCode == "stick")
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

