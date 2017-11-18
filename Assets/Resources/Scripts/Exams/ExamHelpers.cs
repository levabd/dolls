using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
public static class ExamHelpers
{
    public static bool GenericMoveHelper(this BaseExam exam, string colliderTag, string finalColliderTag, ref string errorMessage, ref string tipMessage)
    {
        if ((CurrentTool.Instance.Tool.CodeName == "syringe" || CurrentTool.Instance.Tool.CodeName == "venflon" ||
             CurrentTool.Instance.Tool.CodeName == "big" || CurrentTool.Instance.Tool.CodeName == "cannule" ||
             CurrentTool.Instance.Tool.CodeName == "trocar")
            && colliderTag == finalColliderTag)
        {
            tipMessage = "Кінцеве місце пункції досягнуте";
        }

        if ((CurrentTool.Instance.Tool.CodeName == "syringe" || CurrentTool.Instance.Tool.CodeName == "venflon" || 
            CurrentTool.Instance.Tool.CodeName == "big" || CurrentTool.Instance.Tool.CodeName == "cannule" || 
            CurrentTool.Instance.Tool.CodeName == "trocar")
            && colliderTag != finalColliderTag && colliderTag != "vein_target")
        {
            errorMessage = "Пункція не в тому місці";
            if (exam.NeedleInsideTarget) // Прошли вену навылет
                errorMessage = "Пройшли вену навиліт. Гематома";
            return false;
        }

        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && colliderTag != "disinfection_target")
        {
            errorMessage = "Дезінфекція не в тому місці";
            return false;
        }

        if (CurrentTool.Instance.Tool.CodeName == "tweezers" && colliderTag != "disinfection_target")
        {
            errorMessage = "Дезінфекція не в тому місці";
            return false;
        }

        if (CurrentTool.Instance.Tool.CodeName == "scalpel" && colliderTag != "scalpel_target")
        {
            errorMessage = "Надріз не в тому місці";
            return false;
        }

        if (CurrentTool.Instance.Tool.CodeName == "tourniquet" && colliderTag != "tourniquet_target")
        {
            errorMessage = "Не туди накладено джгут";
            return false;
        }

        if (CurrentTool.Instance.Tool.CodeName == "hand" && colliderTag != "palpation_target" && colliderTag != "clamp_target" && colliderTag != "stretch_target")
        {
            errorMessage = "Пальпується і затискається не те місце";
            return false;
        }

        return true;
    }

    public static bool CateterFinalise(this BaseExam exam, string actionCode, ref string errorMessage, string locatedColliderTag, string catheterConductor, int expectedFirstStep, out int returnedStep, ref bool showAnimation)
    {
        //{ "wire_insertion",                 "Вставка провідника" },
        if (CurrentTool.Instance.Tool.CodeName == "standart_catheter_conductor" && actionCode == "push")
        {
            if (exam.LastTakenStep() != expectedFirstStep - 1)
            {
                errorMessage = "Нікуди вставити провідник";
                showAnimation = false;
            }
            returnedStep = expectedFirstStep;
            return true;
        }
        if (CurrentTool.Instance.Tool.CodeName == "soft_catheter_conductor" && actionCode == "push")
        {
            showAnimation = false;
            errorMessage = "Не той провідник";
            returnedStep = expectedFirstStep;
            return true;
        }

        //{ "needle_removing",                "Видалення голки" },
        if (CurrentTool.Instance.Tool.CodeName == "needle" && actionCode == "needle_removing")
        {
            if (exam.LastTakenStep() != expectedFirstStep)
            {
                errorMessage = "Не можна видаляти голку без провідника";
                showAnimation = false;
            }
            returnedStep = expectedFirstStep + 1;
            return true;
        }
        //{ "catheter_insertion",             "Вставка катетера по провіднику" },
        if (CurrentTool.Instance.Tool.CodeName == "catheter" && actionCode == "push")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 1)
            {
                errorMessage = "Не можна вставити катетер без провідника";
                showAnimation = false;
            }
            returnedStep = expectedFirstStep + 2;
            return true;
        }

        //{ "catheter_pushing",               "Поглиблення обертальними рухами" },
        if (CurrentTool.Instance.Tool.CodeName == "catheter" && actionCode == "rotation_insertion")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 2)
            {
                showAnimation = false;
                errorMessage = "Нікуди поглиблювати катетер";
            }
            returnedStep = expectedFirstStep + 3;
            return true;
        }
        if (CurrentTool.Instance.Tool.CodeName == "catheter" && actionCode == "direct_insertion")
        {
            errorMessage = "Некоректний спосіб поглиблення катетера";
            returnedStep = expectedFirstStep + 3;
            return true;
        }

        //{ "wire_removing",                  "Витягнути провідник" },
        if (CurrentTool.Instance.Tool.CodeName == "standart_catheter_conductor" && actionCode == "pull")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 3)
            {
                errorMessage = CurrentTool.Instance.Tool.CodeName == catheterConductor
                    ? "Не можна видаляти провідник на цьому кроці"
                    : "Не той провідник";
                showAnimation = false;
            }
            returnedStep = expectedFirstStep + 4;
            return true;
        }
        if (CurrentTool.Instance.Tool.CodeName == "soft_catheter_conductor" && actionCode == "pull")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 3)
            {
                errorMessage = CurrentTool.Instance.Tool.CodeName == catheterConductor
                    ? "Не можна видаляти провідник на цьому кроці"
                    : "Не той провідник";
                showAnimation = false;
            }
            returnedStep = expectedFirstStep + 4;
            return true;
        }

        //{ "liquid_transfusion_connection",  "З'єднати з системою переливання рідини" },
        if (CurrentTool.Instance.Tool.CodeName == "catheter" && actionCode == "liquid_transfusion_connection")
        {
            if (exam.LastTakenStep() != expectedFirstStep + 4)
            {
                errorMessage = "Спочатку повинен бути коректно встановлений катетер";
                showAnimation = false;
            }
            returnedStep = expectedFirstStep + 5;
            return true;
        }

        // { "get_plaster",                    "Взяти пластир" },
        if (CurrentTool.Instance.Tool.CodeName == "patch" && actionCode == "get")
        {
            returnedStep = expectedFirstStep + 6;
            return true;
        }

        //{ "fixation_with_plaster",          "Фіксація пластиром" }
        if (CurrentTool.Instance.Tool.CodeName == "patch" && actionCode == "stick")
        {
            if (!locatedColliderTag.Contains("catheter"))
            {
                errorMessage = "Не те місце установки. Спочатку повинен бути коректно встановлений катетер";
                showAnimation = false;
            }
            returnedStep = expectedFirstStep + 7;
            return true;
        }

        returnedStep = 0;
        return false;
    }

    public static bool BiosafetySpirit(this BaseExam exam, string actionCode, ref string errorMessage, string locatedColliderTag, out int returnedStep, ref bool showAnimation)
    {
        // { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        if (CurrentTool.Instance.Tool.CodeName == "gloves" && actionCode == "wear_examination")
        {
            CurrentTool.Instance.Tool.Title = "Оглядові рукавички одягнуті";
            CurrentTool.Instance.Tool.StateParams["weared_examination"] = "true";
            CurrentTool.Instance.Tool.StateParams["weared_sterile"] = "false";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[2];
            returnedStep = 1;
            return true;
        }

        //{ "spirit_balls",                   "Промокнути марлеві кульки 70% розчином спирту" },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode.Contains("spirit"))
        {
            BallHelper.TryWetBall(actionCode, "spirit_p70", out errorMessage);
            returnedStep = 2;
            exam.CurrentBallLiquid = "spirit";
            return true;
        }

        // { "balls_spirit_disinfection",      "Дезінфекція спиртом. Протерти зверху вниз" },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode == "top_down")
        {
            if (locatedColliderTag != "disinfection_target")
            {
                showAnimation = false;
                errorMessage = "Дезінфекцію треба робити не тут";
            }

            returnedStep = 3;
            return true;
        }
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode == "right_left")
        {
            errorMessage = "Не так відбувається дезінфекція";
            returnedStep = 3;
            return true;
        }

        // { "wear_sterile_gloves",        "Змінити рукавички на стерильні" },
        if (CurrentTool.Instance.Tool.CodeName == "gloves" && actionCode == "wear_sterile")
        {
            CurrentTool.Instance.Tool.Title = "Стерильні рукавички одягнуті";
            CurrentTool.Instance.Tool.StateParams["weared_sterile"] = "true";
            CurrentTool.Instance.Tool.StateParams["weared_examination"] = "false";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[3];
            returnedStep = 4;
            return true;
        }

        returnedStep = 0;
        return false;
    }

    public static bool BiosafetySpiritIodine(this BaseExam exam, string actionCode, ref string errorMessage, string locatedColliderTag, out int returnedStep, ref bool showAnimation, bool wearGown = false, bool shave = false)
    {
        // { "shave_pubis",                    "Поголити лобковую зону" },
        if (shave && CurrentTool.Instance.Tool.CodeName == "razor" && actionCode == "shave_pubis")
        {
            CurrentTool.Instance.Tool.StateParams["shave_pubis"] = "true";
            returnedStep = 1;
            return true;
        }

        // { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        if (CurrentTool.Instance.Tool.CodeName == "gloves" && actionCode == "wear_examination")
        {
            CurrentTool.Instance.Tool.Title = "Оглядові рукавички одягнуті";
            CurrentTool.Instance.Tool.StateParams["weared_examination"] = "true";
            CurrentTool.Instance.Tool.StateParams["weared_sterile"] = "false";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[2];
            returnedStep = !shave ? 1 : 2;
            return true;
        }

        // { "wear_gown",                      "Одягти халат" },
        if (wearGown && CurrentTool.Instance.Tool.CodeName == "gown" && actionCode == "wear")
        {
            CurrentTool.Instance.Tool.StateParams["weared"] = "true";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[2];
            returnedStep = !shave ? 2 : 3;
            return true;
        }

        //{ "spirit_balls",                   "Промокнути марлеві кульки 70% розчином спирту" },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode.Contains("spirit"))
        {
            BallHelper.TryWetBall(actionCode, "spirit_p70", out errorMessage);
            returnedStep = !wearGown ? 2 : 3;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            exam.CurrentBallLiquid = "spirit";
            return true;
        }

        //{ "tweezers_spirit_balls",          "Взяти змочені марлеві кульки"},
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

        //{ "spirit_disinfection",            "Дезінфекція спиртом. Протерти зверху вниз" },
        //{ "iodine_disinfection",            "Дезінфекція йодом. Протерти зверху вниз" },
        if (CurrentTool.Instance.Tool.CodeName == "tweezers" && actionCode == "top_down")
        {
            if (locatedColliderTag != "disinfection_target")
            {
                errorMessage = "Дезінфекцію треба робити не тут";
                showAnimation = false;
            }

            int lastStep4Spirit = !wearGown ? 3 : 4;
            lastStep4Spirit = !shave ? lastStep4Spirit : lastStep4Spirit + 1;
            returnedStep = exam.LastTakenStep() == lastStep4Spirit ? 4 : 7;
            returnedStep = !wearGown ? returnedStep : returnedStep + 1;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }
        if (CurrentTool.Instance.Tool.CodeName == "tweezers" && actionCode == "right_left")
        {
            errorMessage = "Не так відбувається дезінфекція";
            int lastStep4Spirit = !wearGown ? 3 : 4;
            lastStep4Spirit = !shave ? lastStep4Spirit : lastStep4Spirit + 1;
            returnedStep = exam.LastTakenStep() == lastStep4Spirit ? 4 : 7;
            returnedStep = !wearGown ? returnedStep : returnedStep + 1;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            return true;
        }

        //{ "iodine_balls",                   "Промокнути марлеві кульки 1% розчином йодоната" },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode.Contains("iodine"))
        {
            BallHelper.TryWetBall(actionCode, "iodine_p1", out errorMessage);
            returnedStep = !wearGown ? 5 : 6;
            returnedStep = !shave ? returnedStep : returnedStep + 1;
            exam.CurrentBallLiquid = "iodine";
            return true;
        }

        // { "wear_sterile_gloves",        "Змінити рукавички на стерильні" },
        if (CurrentTool.Instance.Tool.CodeName == "gloves" && actionCode == "wear_sterile")
        {
            CurrentTool.Instance.Tool.Title = "Стерильні рукавички одягнуті";
            CurrentTool.Instance.Tool.StateParams["weared_sterile"] = "true";
            CurrentTool.Instance.Tool.StateParams["weared_examination"] = "false";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[3];
            returnedStep = 8;
            returnedStep = !wearGown ? returnedStep : returnedStep + 1;
            returnedStep = !shave ? returnedStep : returnedStep + 2;
            return true;
        }

        returnedStep = 0;
        return false;
    }

    public static bool FenceInjections(this BaseExam exam, string actionCode, ref string errorMessage, string locatedColliderTag, out int returnedStep, string finalTarget, ref bool showAnimation, bool injection = false)
    {
        // { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        if (CurrentTool.Instance.Tool.CodeName == "gloves" && actionCode == "wear_examination")
        {
            CurrentTool.Instance.Tool.Title = "Оглядові рукавички надіті";
            CurrentTool.Instance.Tool.StateParams["weared_examination"] = "true";
            CurrentTool.Instance.Tool.StateParams["weared_sterile"] = "false";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[2];
            returnedStep = 1;
            return true;
        }

        // { "puncture_needle",                "Взяти голку для забору крові" },
		if (exam.GetNeedleAction(actionCode, ref errorMessage, "simple_needle", 1, ref showAnimation))
        {
            returnedStep = 2;
            return true;
        }

        // { "filling_drug solution",          "Наповнити лікарським розчином" },
        if (injection && CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_drug_solution")
        {
            returnedStep = 3;
            return true;
        }

        // { "tourniquet",                     "Взяти джгут і накласти" },
        if (CurrentTool.Instance.Tool.CodeName == "tourniquet" && actionCode == "lay")
        {
            if (!locatedColliderTag.Contains("tourniquet_target"))
                errorMessage = "Не туди накладено джгут";
            // Вена увеличивается.
            returnedStep = injection ? 4 : 3;
            return true;
        }

        // { "palpation",                      "Пальпуємо вену" },
        if (CurrentTool.Instance.Tool.CodeName == "hand" && actionCode == "palpation")
        {
            if (!locatedColliderTag.Contains("palpation_target"))
                errorMessage = "Пальпується не те місце";
            returnedStep = injection ? 5 : 4;
            return true;
        }

        //{ "spirit_balls",                   "Промокнути марлеві кульки 70% розчином спирту" },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode.Contains("spirit"))
        {
            BallHelper.TryWetBall(actionCode, "spirit_p70", out errorMessage);
            int expectedLastTakenStep = injection ? 5 : 4;
            returnedStep = exam.LastTakenStep() == expectedLastTakenStep ? 5 : 12;
            returnedStep = injection ? returnedStep + 1 : returnedStep;
            exam.CurrentBallLiquid = "spirit";
            return true;
        }

        // { "balls_spirit_disinfection",      "Дезінфекція спиртом. Протерти зверху вниз" },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode == "top_down")
        {
            if (locatedColliderTag != "disinfection_target")
                errorMessage = "Дезінфекцію треба робити не тут";
            returnedStep = injection ? 7 : 6;
            return true;
        }

        // { "throw_balls",                    "Викинути кульки" },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode == "throw_balls")
        {
            BallHelper.ClearBall(exam);
            returnedStep = injection ? 8 : 7;
            return true;
        }

        // { "wear_sterile_gloves",        "Змінити рукавички на стерильні" },
        if (CurrentTool.Instance.Tool.CodeName == "gloves" && actionCode == "wear_sterile")
        {
            CurrentTool.Instance.Tool.Title = "Стерильні рукавички надіті";
            CurrentTool.Instance.Tool.StateParams["weared_examination"] = "false";
            CurrentTool.Instance.Tool.StateParams["weared_sterile"] = "true";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[3];
            returnedStep = injection ? 9 : 8;
            return true;
        }

        // { "stretch_the_skin",               "Натягнути шкіру" },
        if (CurrentTool.Instance.Tool.CodeName == "hand" && actionCode == "stretch_the_skin")
        {
            if (!locatedColliderTag.Contains("stretch_target"))
                errorMessage = "Натягнуто не те місце";
            returnedStep = injection ? 10 : 9;
            return true;
        }

        // { "take_the_blood_ml10",              "Набрати 10мл. крові" },
        if (!injection && CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "take_the_blood_ml10")
        {
            if (!CurrentTool.Instance.Tool.StateParams.ContainsKey("has_needle") || !Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["has_needle"]))
            {
                errorMessage = "Відсутня голка";
                returnedStep = 0;
                return false;
            }

            if (locatedColliderTag != finalTarget)
                errorMessage = "Забір крові не з того місця";
            else
            {
                if (exam.LastTakenStep() != 9)
                    errorMessage = "Не була натягнута шкіра";
                else if (!CurrentTool.Instance.Tool.StateParams.ContainsKey("entry_angle") || !float.Parse(CurrentTool.Instance.Tool.StateParams["entry_angle"]).CheckRange(29, 31))
                    errorMessage = "Неправильний кут установки";
                else
                {
                    CurrentTool.Instance.Tool.StateParams["blood_inside"] = "true";
                    // Запустить анимацию крови
                    Material matBlood = Resources.Load("Prefabs/Medicine_and_Health/Models/Materials/Syringe_df_bloodfull", typeof(Material)) as Material;
                    Material[] mats = GameObject.Find("SyringeElone").transform.GetChild(0).gameObject.GetComponent<Renderer>().materials;
                    mats[0] = matBlood;
                    GameObject.Find("SyringeElone").transform.GetChild(0).gameObject.GetComponent<Renderer>().materials = mats;
                }
            }
            returnedStep = 10;
            return true;
        }

        // { "remove_tourniquet",              "Зняти джгут" },
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
                errorMessage = "Введення препарату не в те місце";
            else
            {
                if (!CurrentTool.Instance.Tool.StateParams.ContainsKey("entry_angle") || !float.Parse(CurrentTool.Instance.Tool.StateParams["entry_angle"]).CheckRange(29, 31))
                    errorMessage = "Неправильний кут установки";
            }
            returnedStep = 12;
            return true;
        }

        // { "attach_balls",                  "Прикладаємо до місця ін'єкції ватну кульку" },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode == "attach_balls")
        {
            if (locatedColliderTag != "disinfection_target")
                errorMessage = "Дезінфекцію треба робити не тут";
            returnedStep = injection ? 14 : 13;
            return true;
        }

        // { "needle_pull",                    "Извлечь иглу." },
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "needle_pull")
        {
            if (exam.LastTakenStep() != (injection ? 14 : 13))
            {
                errorMessage = "Сепсис";
                showAnimation = false;
            }
            // else
            //     Рука сгибается.
            returnedStep = injection ? 15 : 14;
            return true;
        }

        // { "put_on_the_cap",                 "Одягти ковпачок на голку" },
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "put_on_the_cap")
        {
            if (exam.LastTakenStep() != (injection ? 15 : 14))
            {
                errorMessage = "Голка в тілі або була давно вилучена. Спочатку треба було витягти і відразу надіти ковпачок";
                showAnimation = false;
            }
            returnedStep = injection ? 16 : 15;
            return true;
        }

        // { "throw_needle",                   "Викинути голку" }
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "throw_needle")
        {
            if (exam.LastTakenStep() != (injection ? 16 : 15))
            {
                errorMessage = "Цю дію треба здійснювати відразу після надягання на голку ковпачка";
                showAnimation = false;
            }
            returnedStep = injection ? 17 : 16;
            return true;
        }

        returnedStep = 0;
        return false;
    }

    public static bool VenflonInstallation(this BaseExam exam, string actionCode, ref string errorMessage, string locatedColliderTag, out int returnedStep, string finalTarget, ref bool showAnimation, bool head = false)
    {
        // { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        if (CurrentTool.Instance.Tool.CodeName == "gloves" && actionCode == "wear_examination")
        {
            CurrentTool.Instance.Tool.Title = "Оглядові рукавички надіті";
            CurrentTool.Instance.Tool.StateParams["weared_examination"] = "true";
            CurrentTool.Instance.Tool.StateParams["weared_sterile"] = "false";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[2];
            returnedStep = 1;
            return true;
        }

        // { "tourniquet",                     "Взяти джгут і накласти" },
        if (CurrentTool.Instance.Tool.CodeName == "tourniquet" && actionCode == "lay")
        {
            if (!locatedColliderTag.Contains("tourniquet_target"))
                errorMessage = "Не туди накладено джгут";
            // Вена увеличивается.
            returnedStep = 2;
            return true;
        }

        // { "palpation",                      "Пальпуємо вену" },
        if (CurrentTool.Instance.Tool.CodeName == "hand" && actionCode == "palpation")
        {
            if (!locatedColliderTag.Contains("palpation_target"))
                errorMessage = "Пальпується не те місце";
            returnedStep = 3;
            return true;
        }

        //{ "spirit_balls",                   "Промокнути марлеві кульки 70% розчином спирту" },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode.Contains("spirit"))
        {
            BallHelper.TryWetBall(actionCode, "spirit_p70", out errorMessage);
            exam.CurrentBallLiquid = "spirit";
            returnedStep = 4;
            return true;
        }

        // { "balls_spirit_disinfection",      "Дезінфекція спиртом. Протерти зверху вниз" },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode == "top_down")
        {
            if (locatedColliderTag != "disinfection_target")
                errorMessage = "Дезінфекцію треба робити не тут";
            returnedStep = 5;
            return true;
        }

        // { "throw_balls",                    "Викинути кульки" },
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode == "throw_balls")
        {
            BallHelper.ClearBall(exam);
            returnedStep = 6;
            return true;
        }

        // { "wear_sterile_gloves",        "Змінити рукавички на стерильні" },
        if (CurrentTool.Instance.Tool.CodeName == "gloves" && actionCode == "wear_sterile")
        {
            CurrentTool.Instance.Tool.Title = "Стерильні рукавички надіті";
            CurrentTool.Instance.Tool.StateParams["weared_sterile"] = "true";
            CurrentTool.Instance.Tool.StateParams["weared_examination"] = "false";
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[3];
            returnedStep = 7;
            return true;
        }

        // { "stretch_the_skin",               "Натягнути шкіру" },
        if (CurrentTool.Instance.Tool.CodeName == "hand" && actionCode == "stretch_the_skin")
        {
            if (!locatedColliderTag.Contains("stretch_target"))
                errorMessage = "Натягнуто не те місце";
            returnedStep = 8;
            return true;
        }

        // { "pull_mandren",                   "Потягнути мадрен" },
        if (CurrentTool.Instance.Tool.CodeName == "venflon" && actionCode == "pull_mandren")
        {
            if (locatedColliderTag != finalTarget)
                errorMessage = "Не те місце для катетеризації";
            else
            {
                if (exam.LastTakenStep() != 8)
                {
                    errorMessage = "Не була натягнута шкіра";
                    showAnimation = false;
                }
                else if (!CurrentTool.Instance.Tool.StateParams.ContainsKey("entry_angle") || !float
                             .Parse(CurrentTool.Instance.Tool.StateParams["entry_angle"])
                             .CheckRange(head ? 20 : 10, head ? 30 : 20))
                    errorMessage = "Неправильний кут установки";
                else
                    CurrentTool.Instance.Tool.StateParams["mandren_pulling"] = "true";
            }
            returnedStep = 9;
            return true;
        }

        // { "remove_tourniquet",              "Зняти джгут" },
        if (CurrentTool.Instance.Tool.CodeName == "tourniquet" && actionCode == "remove")
        {
            returnedStep = 10;
            return true;
        }

        // { "clamp_the_vein",                 "Перетиснути вену" },
        if (CurrentTool.Instance.Tool.CodeName == "hand" && actionCode == "clamp")
        {
            if (!locatedColliderTag.Contains("clamp_target"))
                errorMessage = "Здавлена не та вена (місце)";
            returnedStep = 11;
            return true;
        }

        // { "remove_mandren",                 "Витягнути мадрен" },
        if (CurrentTool.Instance.Tool.CodeName == "venflon" && actionCode == "remove_mandren")
        {
            if (exam.LastTakenStep() != 11)
            {
                errorMessage = "Не була перетиснута вена";
                showAnimation = false;
            }
            returnedStep = 12;
            return true;
        }

        // { "filling_nacl_half",              "Наповнити 0,9% розчином натрію хлориду наполовину"},
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_nacl_half")
        {
            returnedStep = 13;
            return true;
        }

        // { "nacl_to_cateter",                "Ввести фізрозчин в катетер" },
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "nacl_to_cateter")
        {
            returnedStep = 14;
            return true;
        }

        // { "liquid_transfusion_connection",  "З'єднати з системою переливання рідини" },
        if (CurrentTool.Instance.Tool.CodeName == "venflon" && actionCode == "liquid_transfusion_connection")
        {
            if (exam.LastTakenStep() != (head ? 14 : 12))
            {
                errorMessage = "Спочатку повинен бути коректно встановлений катетер";
                showAnimation = false;
            }
            returnedStep = head ? 15 : 13;
            return true;
        }

        // { "get_plaster",                    "Взяти пластир" },
        if (CurrentTool.Instance.Tool.CodeName == "patch" && actionCode == "get")
        {
            returnedStep = head ? 16 : 14;
            return true;
        }

        // { "fixation_with_plaster",          "Фіксація пластиром" }
        if (CurrentTool.Instance.Tool.CodeName == "patch" && actionCode == "stick")
        {
            if (!locatedColliderTag.Contains("catheter"))
                errorMessage = "Не те місце установки. Спочатку повинен бути коректно встановлений катетер";
            returnedStep = head ? 17 : 15;
            return true;
        }

        returnedStep = 0;
        return false;
    }
}

