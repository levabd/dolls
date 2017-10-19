using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class Exam9 : BaseExam
{
    private DateTime _needleRemovingMoment;
    private string _currentBallLiquid = "none";

    public override string Name => "Центральный венозный доступ №9 Бедренная вена";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "shave_pubis",                    "Побрить лобковую зону" },
        { "wear_gloves",                    "Надеть перчатки" },
        { "wear_gown",                      "Надеть халат" },
        { "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        { "tweezers_spirit_balls",          "Взять смоченные марлевые шарики" },
        { "spirit_disinfection",            "Дезинфекция спиртом. Протереть сверху вниз." },
        { "iodine_balls",                   "Промокнуть марлевые шарики 1% раствором йодоната" },
        { "tweezers_iodine_balls",          "Взять смоченные марлевые шарики" },
        { "iodine_disinfection",            "Дезинфекция йодом. Протереть сверху вниз." },
        { "sterile_tissue",                 "Накрываем операционное поле стерильными салфетками." },
        { "palpation",                      "Пальпируем бедренную артерию." },
        { "puncture_needle",                "Взять иглу для пункции вены." },
        { "disconnect_syringe",             "Отсоеденяем шприц от иглы." },
        { "cover_cannula",                  "Быстро прикрываем канюлю пальнцем." },
        { "wire_insertion",                 "Вставка проводника." },
        { "needle_removing",                "Удаление иглы." },
        { "catheter_insertion",             "Вставка катетера по проводнику." },
        { "catheter_pushing",               "Углубление вращательными движениями." },
        { "wire_removing",                  "Извлечение проводника." },
        { "liquid_transfusion_connection",  "Соединение с системой переливания жидкости." },
        { "get_plaster",                    "Взять пластырь" },
        { "fixation_with_plaster",          "Фиксация пластырем." }
    };

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "razor",                          "Бритва"},
        { "gloves",                         "Перчатки" },
        { "gown",                           "Халат" },
        { "syringe",                        "Шприц без иглы" },
        { "needle",                         "Игла(отдельно)" },
        { "gauze_balls",                    "Стерильные марлевые шарики" },
        { "sterile_tissue",                 "Стерильные салфетки" },
        { "tweezers",                       "Пинцет без ничего" },
        { "hand",                           "Кисть для пальпации" },
        { "standart_catheter_conductor",    "Стандартный гибкий проводник к катетеру" },
        { "soft_catheter_conductor",        "Мягкий гибкий проводник к катетеру" },
        { "catheter_d1",                     "Катетер с канюлей и заглушкой диаметром 0,8 – 1 мм" },
        { "catheter_d06",                    "Катетер с канюлей и заглушкой диаметром 0,6 – 0.8 мм" },
        { "patch",                          "Пластырь" }
    };

    public override TupleList<string, string> ToolActions(ToolItem tool)
    {
        switch (tool.CodeName)
        {
            case "razor":
                return new TupleList<string, string>
                {
                    { "shave_pubis", "Побрить лобковую зону"}
                };
            case "sterile_tissue":
                return new TupleList<string, string>
                {
                    { "put", "Накрываем операционное поле стерильными салфетками"}
                };
            case "gloves":
                return new TupleList<string, string>
                {
                    { "wear", "Надеть"}
                };
            case "gown":
                return new TupleList<string, string>
                {
                    { "wear", "Надеть"}
                };
            case "hand":
                return new TupleList<string, string>
                {
                    { "palpation", "Пальпация" },
                };
            case "syringe":
                return new TupleList<string, string>
                {
                    { "needle_removing",        "Отсоеденить от иглы" },
                    { "anesthesia",             "Сделать местную анестезию" },
                    { "piston_pulling",         "Потягивание поршня на себя" },
                    { "null",                   "---" },
                    { "anesthesia_needle",      "Взять иглу для анестезии кожи и наполнить шприц анестетиком" },
                    { "g22G_needle",             "Взять иглу для спинномозговой анестезии 22G и наполнить шприц анестетиком" },
                    { "wire_needle",            "Взять иглу для проводниковой анестезии и наполнить шприц анестетиком" },
                    { "a45_d10_punction_needle",  "Взять иглу для пункции вены длинной 10 см с внутренним просветом канала 1,7 мм и срезом острия иглы под углом 45°" },
                    { "a45_d7_punction_needle",   "Взять иглу для пункции вены длинной  4-7 см с внутренним просветом канала 1,0-1,5 мм и срезом острия иглы под углом 40-45°" },
                    { "filling_novocaine_full", "Наполнить 0,25% новокаина полностью" },
                    { "filling_novocaine_half", "Наполнить 0,25% новокаина наполовину" },
                    { "filling_nacl_half",      "Наполнить 0,9% раствором натрия хлорида наполовину"}
                };
            case "gauze_balls":
                return new TupleList<string, string>
                {
                    { "spirit_p70",  "Промокнуть в 70% раствором спирта" },
                    { "spirit_p60",  "Промокнуть в 60% раствор спирта" },
                    { "spirit_p80",  "Промокнуть в 80% раствор спирта" },
                    { "iodine_p1",   "Промокнуть в 1% раствором йодоната" },
                    { "iodine_p3",   "Промокнуть в 3% раствором йодоната" },
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
                    { "tweezers_balls", "Взять марлевые шарики" },
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
            case "catheter_d1":
                return new TupleList<string, string>
                {
                    { "push",                           "Вставить катетер по проводнику" },
                    { "remove",                         "Удалить катетер" },
                    { "liquid_transfusion_connection",  "Соединить с системой переливания жидкостей" },
                    { "rotation_insertion",             "Угулубить вращательными движениями" },
                    { "direct_insertion",               "Угулубить прямыми движениями" }
                };
            case "catheter_d06":
                return new TupleList<string, string>
                {
                    { "push",                           "Вставить катетер по проводнику" },
                    { "remove",                         "Удалить катетер" },
                    { "liquid_transfusion_connection",  "Соединить с системой переливания жидкостей" },
                    { "rotation_insertion",             "Угулубить вращательными движениями" },
                    { "direct_insertion",               "Угулубить прямыми движениями" }
                };
            case "patch":
                return new TupleList<string, string>
                {
                    { "get", "Взять" }
                };
            default:
                return new TupleList<string, string>();
        }
    }

    public override bool CheckMove(int lastTakenStep, ref ToolItem tool, string colliderTag, out string errorMessage)
    {
        errorMessage = "";

        TupleList<string, string> criticalSyringeErrors = new TupleList<string, string>
        {
            { "femoral_artery", "Пункция бедренной артерии"},
            { "femoral_nerve", "Травма бедренного нерва"},
            { "nerves", "Повреждение нервных узлов"},
            { "lymph", "Повреждение лимфатических узлов"},
            { "bones", "Попадание в кость"},
        };

        foreach (var syringeError in criticalSyringeErrors)
        {
            if (tool.CodeName == "syringe" && colliderTag.Contains(syringeError.Item1))
            {
                errorMessage = syringeError.Item2;
                return false;
            }
        }

        if (tool.CodeName == "syringe" && (colliderTag != "femoral_vien_final_target" || colliderTag != "femoral_vien_target"))
        {
            errorMessage = "Пункция не в том месте";
            return false;
        }

        if (tool.CodeName == "tweezers" && colliderTag != "inguinal_area")
        {
            errorMessage = "Дезинфекция не в том месте";
            return false;
        }

        if (tool.CodeName == "hand" && colliderTag != "femoral_artery")
        {
            errorMessage = "Пальпируется не то место";
            return false;
        }

        this.BloodInsideMove(ref tool, colliderTag, "femoral_vien_final_target");

        return true;
    }

    public override int? CheckAction(int lastTakenStep, ref ToolItem tool, string actionCode, out string errorMessage, string locatedColliderTag = "")
    {
        errorMessage = "";

        // Безопасные операции
        if (this.BallClearAction(ref tool, actionCode, ref _currentBallLiquid)) return null;
        if (this.RemoveBallsAction(ref tool, actionCode)) return null;
        if (this.PistonPullingAction(ref tool, actionCode)) return null;
        if (actionCode == "null") return null;

        int returnedStep;

        // Перчатки + Халат + Спирт + Йод
        if (this.BiosafetySpiritIodine(lastTakenStep, ref tool, actionCode, ref errorMessage, locatedColliderTag,
            "inguinal_area", out returnedStep, ref _currentBallLiquid, true, true)) return returnedStep;

        // { "sterile_tissue",                    "Накрываем операционное поле стерильными салфетками." },
        if (tool.CodeName == "sterile_tissue" && actionCode == "put")
        {
            tool.StateParams["putted"] = "true";
            return 10;
        }

        // { "palpation",                      "Пальпируем бедренную артерию." },
        if (tool.CodeName == "hand" && actionCode == "palpation")
        {
            if (!locatedColliderTag.Contains("femoral_artery"))
                errorMessage = "Пальпируется не то место";
            return 11;
        }

        //{ "puncture_needle",                "Взять иглу для пункции вены." },
        if (this.GetNeedleAction(lastTakenStep, ref tool, actionCode, ref errorMessage, "a45_d10_punction_needle", 11)) return 12;

        //{ "disconnect_syringe",             "Отсоеденяем шприц от иглы." },
        if (this.NeedleRemovingAction(ref tool, actionCode, ref errorMessage, locatedColliderTag,
            ref _needleRemovingMoment, "femoral_vien_final_target", 30, 45)) return 13;

        // Отсоединяем в любом другом месте
        if (this.NeedleRemovingAction(ref tool, actionCode, ref errorMessage, locatedColliderTag, ref _needleRemovingMoment)) return null;

        //{ "cover_cannula",                  "Быстро прикрываем канюлю пальнцем." },
        if (tool.CodeName == "needle" && actionCode == "finger_covering")
        {
            if ((DateTime.Now - _needleRemovingMoment).TotalSeconds > 3)
            {
                errorMessage = "Воздушная эмболия";
                return null;
            }
            return 14;
        }

        // Критическая ошибка
        if (tool.CodeName == "catheter_d1" && actionCode == "remove")
        {
            errorMessage = "Катетер был извлечен. Катетеризация провалена";
            return null;
        }

        // Вставка проводника, удаление иглы, Катетеризация, присоединение системы, фиксация пластырем
        if (this.CateterFinalise(lastTakenStep, ref tool, actionCode, ref errorMessage, locatedColliderTag,
            "catheter_d1", "standart_catheter_conductor", 15, out returnedStep)) return returnedStep;

        return null;
    }
}

