using System;
using System.Collections.Generic;

class Exam2: BaseExam
{
    private DateTime needleRemovingMoment;

    public override string Name
    {
        get { return "Центральный венозный доступ №2 Подключичная надключичный доступ"; }
    }

    public override TupleList<string, string> CorrectSteps
    {
        get
        {
            return new TupleList<string, string>
            {
                { "wear_gloves",                    "Надеть перчатки" }, // Я пошёл с Надеждой в душ. Вдруг пришёл надежин муж. То ли мне надеть одежду? То ли мне одеть Надежду? 
                { "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
                { "tweezers_spirit_balls",          "Взять смоченные марлевые шарики" },
                { "spirit_disinfection",            "Дезинфекция спиртом. Протереть сверху вниз." },
                { "iodine_balls",                   "Промокнуть марлевые шарики 1% раствором йодоната" },
                { "tweezers_iodine_balls",          "Взять смоченные марлевые шарики" },
                { "iodine_disinfection",            "Дезинфекция йодом. Протереть сверху вниз." },
                { "anesthesia_needle",              "Взять иглу для анестезии кожи." },
                { "anesthesia",                     "Сделать местную анестезию." },
                { "puncture_needle",                "Взять иглу для пункции вены." },
                { "puncture_novocaine",             "Наполнить 0,25% новокаина на половину." },
                { "disconnect_syringe",             "Отсоеденяем шприц от иглы." },
                { "cover_cannula",                  "Быстро прикрываем канюлю пальнцем." },
                { "wire_insertion",                 "Вставка проводника." },
                { "needle_removing",                "Удаление иглы." },
                { "catheter_insertion",             "Вставка катетера по проводнику." },
                { "catheter_pushing",               "Углубление вращательными движениями." },
                { "wire_removing",                  "Извлечение проводника." },
                { "liquid_transfusion_connection",  "Соединение с системой переливания жидкости." },
                { "fixation_with_plaster",          "Фиксация пластырем." }
            };
        }
    }

    public override Dictionary<string, string> InventoryTool
    {
        get
        {
            return new Dictionary<string, string>
            {
                { "gloves",                         "Перчатки" },
                { "syringe",                        "Шприц без иглы" },
                { "gauze_balls",                    "Стерильные марлевые шарики" },
                { "tweezers",                       "Пинцет без ничего" },
                { "standart_catheter_conductor",    "Стандартный гибкий проводник к катетеру" },
                { "soft_catheter_conductor",        "Мягкий гибкий проводник к катетеру" },
                { "catheter",                       "Катетер" },
                { "patch",                          "Пластырь" }
            };
        }
    }

    public override TupleList<string, string> ToolActions(ToolItem tool)
    {
        switch (tool.codeName)
        {
            case "gloves":
                return new TupleList<string, string>
                {
                    { "wear", "Надеть"}
                };
            case "syringe":
                return new TupleList<string, string>
                {
                    { "needle_removing",        "Отсоеденить от иглы" },
                    { "anesthesia",             "Сделать местную анестезию" },
                    { "piston_pulling",         "Потягивание поршня на себя" },
                    { "null",                   "---" },
                    { "anesthesia_needle",      "Взять иглу для анестезии кожи" },
                    { "22G_needle",             "Взять иглу для спинномозговой анестезии 22G" },
                    { "wire_needle",            "Взять иглу для проводниковой анестезии" },
                    { "45_punction_needle",     "Взять иглу для пункции вены длинной 10 см с внутренним просветом канала 1,7 мм и срезом острия иглы под углом 45°" },
                    { "65_punction_needle",     "Взять иглу для пункции вены длинной 10 см с внутренним просветом канала 1,8 мм и срезом острия иглы под углом 65°" },
                    { "filling_novocaine_full", "Наполнить 0,25% новокаина полностью" },
                    { "filling_novocaine_half", "Наполнить 0,25% новокаина наполовину" }
                };
            case "gauze_balls":
                return new TupleList<string, string>
                {
                    { "spirit_70",  "Промокнуть в 70% раствором спирта" },
                    { "spirit_60",  "Промокнуть в 60% раствор спирта" },
                    { "spirit_80",  "Промокнуть в 80% раствор спирта" },
                    { "iodine_1",   "Промокнуть в 1% раствором йодоната" },
                    { "iodine_3",   "Промокнуть в 3% раствором йодоната" },
                    { "clear",      "Взять новый шарик (очистить)" }
                };
            case "needle":
                return new TupleList<string, string>
                {
                    { "finger_covering", "Прикрыть пальцем" },
                    { "needle_removing", "Удалить иглу через проводник" }
                };
            case "tweezers":
                return new TupleList<string, string>
                {
                    { "tweezers_balls", "Взять стерильные шарики" },
                    { "remove_balls",   "Сбросить стерильные шарики" },
                    { "null",           "---" },
                    { "top_down",       "Протереть сверху вниз" },
                    { "right_left",     "Протереть справа налево" }
                };
            case "standart_catheter_conductor":
                return new TupleList<string, string>
                {
                    { "push", "Вставить проводник" },
                    { "pull", "Удалить проводник" }
                };
            case "soft_catheter_conductor":
                return new TupleList<string, string>
                {
                    { "push", "Вставить проводник" },
                    { "pull", "Удалить проводник" }
                };
            case "catheter":
                return new TupleList<string, string>
                {
                    { "push",                           "Вставить катетер по проводнику" },
                    { "remove",                         "Удалить катетер" },
                    { "liquid_transfusion_connection",  "Соединить с системой переливания жидкостей" },
                    { "rotation_insertion",             "Вставлять вращательными движениями" },
                    { "direct_insertion",               "Вставлять прямыми движениями" },
                };
            case "patch":
                return new TupleList<string, string>
                {
                    { "stick", "Наклеить" }
                };
            default:
                return new TupleList<string, string>();
        }
    }

    public override bool CheckMove(ref ToolItem tool, string colliderTag, out string errorMessage)
    {
        errorMessage = "";
        if (tool.codeName == "syringe" && colliderTag.Contains("arteria"))
        {
            errorMessage = "Пункция артерий";
            return false;
        }
        else if (tool.codeName == "syringe" && colliderTag.Contains("brachial_plexus"))
        {
            errorMessage = "Травма плечевого нервного сплетения";
            return false;
        }
        else if (tool.codeName == "syringe" && colliderTag.Contains("trachea"))
        {
            errorMessage = "Травма трахеи";
            return false;
        }
        else if (tool.codeName == "syringe" && colliderTag.Contains("thyroid"))
        {
            errorMessage = "Травма щитовидной железы";
            return false;
        }
        else if (tool.codeName == "syringe" && colliderTag.Contains("lungs"))
        {
            errorMessage = "Пневмоторакс";
            return false;
        }
        else if (tool.codeName == "syringe" && colliderTag.Contains("subclavian2_vein_final_target"))
        {
            bool pistonPulling = false;
            if (tool.stateParams.ContainsKey("piston_pulling"))
                pistonPulling = Convert.ToBoolean(tool.stateParams["piston_pulling"]);

            if (pistonPulling)
            {
                if (tool.stateParams.ContainsKey("blood_inside"))
                    tool.stateParams["blood_inside"] = "true";
                else
                    tool.stateParams.Add("blood_inside", "true");
                // TODO: Запустить анимацию крови
            }
            return true;
        }

        return true;
    }

    public override int? CheckAction(ref ToolItem tool, string actionCode, out string errorMessage, string locatedColliderTag = null)
    {
        errorMessage = "";

        // { "wear_gloves",                    "Надеть перчатки" },
        if (tool.codeName == "gloves" && actionCode == "wear")
            return 1;

        //{ "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        else if (tool.codeName == "gauze_balls" && actionCode == "spirit_60")
        {
            errorMessage = "Не та жидкость";
            return 2;
        }
        else if (tool.codeName == "gauze_balls" && actionCode == "spirit_70")
        {
            BallHelper.TryWetBall(ref tool, "spirit_70", out errorMessage);
            return 2;
        }
        else if (tool.codeName == "gauze_balls" && actionCode == "spirit_80")
        {
            errorMessage = "Не та жидкость";
            return 2;
        }
        else if (tool.codeName == "gauze_balls" && actionCode == "clear")
        {
            BallHelper.ClearBall(ref tool);
            return null;
        }

        //{ "tweezers_spirit_balls",          "Взять смоченные марлевые шарики" },
        else if (tool.codeName == "tweezers" && actionCode == "tweezers_balls")
        {
            TweezersHelper.GetBalls(ref tool);
            return LastTakenStep() == 2 ? 3 : 6;
        }
        else if (tool.codeName == "tweezers" && actionCode == "remove_balls")
        {
            TweezersHelper.RemoveBall(ref tool);
            return null;
        }

        //{ "spirit_disinfection",            "Дезинфекция спиртом. Протереть сверху вниз." },
        //{ "iodine_disinfection",            "Дезинфекция йодом. Протереть сверху вниз." },
        else if (tool.codeName == "tweezers" && actionCode == "top_down")
        {
            if (locatedColliderTag != "disinfection_subclavian2_target")
                errorMessage = "Дезинфекцию надо делать не тут";
            return LastTakenStep() == 3 ? 4 : 7;
        }
        else if (tool.codeName == "tweezers" && actionCode == "right_left")
        {
            errorMessage = "Не так происходит дезинфекция";
            return LastTakenStep() == 3 ? 4 : 7;
        }

        //{ "iodine_balls",                   "Промокнуть марлевые шарики 1% раствором йодоната" },
        else if (tool.codeName == "gauze_balls" && actionCode == "iodine_1")
        {
            BallHelper.TryWetBall(ref tool, "iodine_1", out errorMessage);
            return 5;
        }
        else if (tool.codeName == "gauze_balls" && actionCode == "iodine_3")
        {
            errorMessage = "Не та жидкость";
            return 5;
        }

        //{ "anesthesia_needle",              "Взять иглу для анестезии кожи." },
        else if (tool.codeName == "syringe" && actionCode == "anesthesia_needle")
        {
            if (LastTakenStep() != 7)
                errorMessage = "Не та игла на текущем шаге";
            else
                SyringeHelper.TryGetNeedle(ref tool, "anesthesia_needle", out errorMessage);
            return 8;
        }
        else if (tool.codeName == "syringe" && actionCode == "22G_needle")
        {
            errorMessage = "Не та игла";
            return 8;
        }
        else if (tool.codeName == "syringe" && actionCode == "wire_needle")
        {
            errorMessage = "Не та игла";
            return 8;
        }

        //{ "anesthesia",                     "Сделать местную анестезию." },
        else if (tool.codeName == "syringe" && actionCode == "anesthesia")
        {
            SyringeHelper.CheckAnestesiaNeedle(ref tool, out errorMessage);
            return 9;
        }

        //{ "puncture_needle",                "Взять иглу для пункции вены." },
        else if (tool.codeName == "syringe" && actionCode == "45_punction_needle")
        {
            if (LastTakenStep() != 9)
                errorMessage = "Не та игла на текущем шаге";
            else
                SyringeHelper.TryGetNeedle(ref tool, "45_punction_needle", out errorMessage);
            return 10;
        }
        else if (tool.codeName == "syringe" && actionCode == "65_punction_needle")
        {
            errorMessage = "Не та игла";
            return 10;
        }

        //{ "puncture_novocaine",             "Наполнить 0,25% новокаина на половину." },
        else if (tool.codeName == "syringe" && actionCode == "filling_novocaine_full")
        {
            errorMessage = "Слишком много новокаина";
            return 11;
        }
        else if (tool.codeName == "syringe" && actionCode == "filling_novocaine_half")
            return 11;

        else if (tool.codeName == "syringe" && actionCode == "piston_pulling")
        {
            errorMessage = "";
            if (tool.stateParams.ContainsKey("piston_pulling"))
                tool.stateParams["piston_pulling"] = "true";
            else
                tool.stateParams.Add("piston_pulling", "true");
            return null;
        }

        //{ "disconnect_syringe",             "Отсоеденяем шприц от иглы." },
        else if (tool.codeName == "syringe" && actionCode == "needle_removing")
        {
            needleRemovingMoment = DateTime.Now;

            if (tool.stateParams.ContainsKey("piston_pulling"))
                tool.stateParams.Remove("piston_pulling");

            string entryAngle = "";
            if (!tool.stateParams.TryGetValue("entry_angle", out entryAngle))
            {
                errorMessage = "Неправильный угол установки";
                return 12;
            }

            if (!float.Parse(entryAngle).CheckRange(15, 20))
            {
                errorMessage = "Неправильный угол установки";
                return 12;
            }

            string bloodInside = "";
            if (!tool.stateParams.TryGetValue("blood_inside", out bloodInside))
            {
                errorMessage = "Во время углубления не был потянут поршень на себя";
                return 12;
            }

            if (!Convert.ToBoolean(bloodInside))
            {
                errorMessage = "Во время углубления не был потянут поршень на себя";
                return 12;
            }

            SyringeHelper.RemoveNeedle(ref tool);
            if (locatedColliderTag == "subclavian2_vein_final_target")
                return 12;
            else
                return null;
        }

        //{ "cover_cannula",                  "Быстро прикрываем канюлю пальнцем." },
        else if (tool.codeName == "needle" && actionCode == "finger_covering")
        {
            if ((DateTime.Now - needleRemovingMoment).TotalSeconds > 3)
            {
                errorMessage = "Воздушная эмболия";
                return null;
            }
            return 13;
        }

        //{ "wire_insertion",                 "Вставка проводника." },
        else if (tool.codeName == "standart_catheter_conductor" && actionCode == "push")
        {
            if (LastTakenStep() != 13)
                errorMessage = "Некуда вставить проводник";
            return 14;
        }
        else if (tool.codeName == "soft_catheter_conductor" && actionCode == "push")
        {
            errorMessage = "Не тот проводник";
            return 14;
        }

        //{ "needle_removing",                "Удаление иглы." },
        else if (tool.codeName == "needle" && actionCode == "needle_removing")
        {
            if (LastTakenStep() != 14)
                errorMessage = "Нельзя удалять иглу без проводника";
            return 15;
        }
        //{ "catheter_insertion",             "Вставка катетера по проводнику." },
        else if (tool.codeName == "catheter" && actionCode == "push")
        {
            if (LastTakenStep() != 15)
                errorMessage = "Нельзя вставить катетер без проводника";

            return 16;
        }

        //{ "catheter_pushing",               "Углубление вращательными движениями." },
        else if (tool.codeName == "catheter" && actionCode == "rotation_insertion")
        {
            if (LastTakenStep() != 16)
                errorMessage = "Некуда углублять катетер";
            return 17;
        }
        else if (tool.codeName == "catheter" && actionCode == "direct_insertion")
        {
            errorMessage = "Некорректный способ углубления катетера";
            return 17;
        }
        else if (tool.codeName == "catheter" && actionCode == "remove")
        {
            errorMessage = "Катетер был извлечен. Катетеризация провалена";
            return null;
        }

        //{ "wire_removing",                  "Извлечение проводника." },
        else if (tool.codeName == "standart_catheter_conductor" && actionCode == "pull")
        {
            if (LastTakenStep() != 17)
                errorMessage = "Нельзя удалять иглу без катетера";
            return 18;
        }
        else if (tool.codeName == "soft_catheter_conductor" && actionCode == "pull")
        {
            errorMessage = "Не тот проводник";
            return 18;
        }

        //{ "liquid_transfusion_connection",  "Соединение с системой переливания жидкости." },
        else if (tool.codeName == "catheter" && actionCode == "liquid_transfusion_connection")
        {
            if (LastTakenStep() != 18)
                errorMessage = "Сначала должен быть корректно установлен катетер";
            return 19;
        }

        //{ "fixation_with_plaster",          "Фиксация пластырем." }
        else if (tool.codeName == "patch" && actionCode == "stick")
        {
            if (locatedColliderTag != "subclavian_vein_catheter")
                errorMessage = "Не то место установки. Сначала должен быть корректно установлен катетер";
            return 20;
        }

        else if (actionCode == "null")
            return null;

        return null;
    }
}

