using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class BloodPressureExam2 : BaseExam
{
    public override string Name => "Измерение артериального давления (АД) настоящим манометром";
    public override string LoadName => "BloodPressureExam2";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "wear_examination_gloves",        "Надеть смотровые перчатки" },
        { "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        { "spirit_disinfection",            "Дезинфекция спиртом. Протереть сверху вниз." },
        { "wear_sterile_gloves",            "Сменить перчатки на стерильные" },
        { "sterile_tissue",                 "Накрываем операционное поле стерильными салфетками." },
        { "anesthesia_needle",              "Взять иглу для анестезии кожи." },
        { "anesthesia",                     "Сделать местную анестезию." },
        { "palpation",                      "Пальпируем артерию." },
        { "remove_mandren",                 "Вынимаем из канюли мандрен" },
        { "pull_cannula",                   "Канюля тянется на себя" },
        { "push_cannula",                   "Катерер заводится в глубину просвета артерии" },
        { "rinse_cannula",                  "Канюля промывается" },
        { "stitch",                         "Крылья канюли пришиваются к коже" },
        { "connect_sensor",                 "Соединить датчик инвазивного измерения с канюлей и монитором." }
    };

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "gloves",                         "Перчатки" },
        { "sterile_tissue",                 "Стерильные салфетки" },
        { "hand",                           "Рука для дополнительных действий" },
        { "syringe",                        "Шприц без иглы" },
        { "gauze_balls",                    "Стерильные марлевые шарики" },
        { "cannule",                        "Артериальная канюля." },
        { "invasive_sensor",                "Датчик инвазивного измерения АД с системой" },
        { "stitch",                         "Шовный материал + иглодержатель." }
    };

    public override TupleList<string, string> ToolActions(ToolItem tool)
    {
        switch (tool.CodeName)
        {
            case "gloves":
                return new TupleList<string, string>
                {
                    { "wear_examination", "Надеть смотровые перчатки"},
                    { "wear_sterile", "Сменить перчатки на стерильные"}
                };
            case "hand":
                return new TupleList<string, string>
                {
                    { "get_palpation",              "Пальпация" }
                };
            case "sterile_tissue":
                return new TupleList<string, string>
                {
                    { "put", "Накрываем операционное поле стерильными салфетками"}
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
                    { "a45_d4_punction_needle",   "Взять иглу для пункции вены длинной не менее 4 см с внутренним просветом канала 1,0-1,4 мм и срезом острия иглы под углом 40-45°" },
                };
            case "gauze_balls":
                return new TupleList<string, string>
                {
                    { "spirit_p70",  "Промокнуть в 70% раствором спирта" },
                    { "spirit_p60",  "Промокнуть в 60% раствор спирта" },
                    { "spirit_p80",  "Промокнуть в 80% раствор спирта" },
                    { "iodine_p1",   "Промокнуть в 1% раствором йодоната" },
                    { "iodine_p3",   "Промокнуть в 3% раствором йодоната" },
                    { "clear",      "Взять новый шарик (очистить)" },
                    { "null",         "---" },
                    { "throw_balls",  "Выкинуть шарики в мусорник" },
                    { "get_balls", "Приложить шарик" }, // "attach_balls"
                    { "get_top_down",     "Протереть сверху вниз" }, // "top_down"
                };
            case "invasive_sensor":
                return new TupleList<string, string>
                {
                    { "connect", "Соединить с канюлей и монитором" }
                };
            case "cannule":
                return new TupleList<string, string>
                {
                    { "get",                            "Взять" },
                    { "push",                           "Завести в глубину просвета" },
                    { "rinse",                          "Промыть канюлю" },
                    { "remove_mandren",                 "Вытащить мадрен" },
                    { "pull",                           "Потянуть канюлю на себя" }
                };
            case "stitch":
                return new TupleList<string, string>
                {
                    { "get", "Взять" }
                };
            default:
                return new TupleList<string, string>();
        }
    }

    public override bool CheckMove(string colliderTag, out string errorMessage)
    {
        errorMessage = "";

        if (!this.GenericMoveHelper(colliderTag, "radial_artery", ref errorMessage))
            return false;

        return true;
    }

    public override int? CheckAction(string actionCode, out string errorMessage, string locatedColliderTag = "")
    {
        errorMessage = "";

        // Безопасные операции
        if (this.BallClearAction(actionCode)) return null;
        if (this.RemoveBallsAction(actionCode)) return null;
        if (this.PistonPullingAction(actionCode)) return null;
        if (this.GetSyringeAction(actionCode, ref errorMessage)) return null;
        if (actionCode == "null") return null;

        int returnedStep;

        // Перчатки + Спирт
        if (this.BiosafetySpirit(actionCode, ref errorMessage, locatedColliderTag,  out returnedStep)) return returnedStep;

        // { "sterile_tissue",                    "Накрываем операционное поле стерильными салфетками." },
        if (CurrentTool.Instance.Tool.CodeName == "sterile_tissue" && actionCode == "put")
        {
            CurrentTool.Instance.Tool.StateParams["putted"] = "true";
            return 5;
        }

        //{ "anesthesia_needle",              "Взять иглу для анестезии кожи." },
        if (this.GetNeedleAction(actionCode, ref errorMessage, "anesthesia_needle", 5)) return 6;

        //{ "anesthesia",                     "Сделать местную анестезию." },
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "anesthesia")
        {
            SyringeHelper.CheckAnestesiaNeedle(out errorMessage);
            return 7;
        }

        // { "palpation",                      "Пальпируем артерию." },
        if (CurrentTool.Instance.Tool.CodeName == "hand" && actionCode == "palpation")
        {
            if (!locatedColliderTag.Contains("palpation_target"))
                errorMessage = "Пальпируется не то место";
            return 8;
        }

        // { "remove_mandren",                 "Вынимаем из канюли мандрен" },
        if (CurrentTool.Instance.Tool.CodeName == "cannule" && actionCode == "remove_mandren")
        {
            if (locatedColliderTag == "radial_artery")
                return 9;

            return null;
        }

        // { "pull_cannula",                   "Канюля тянется на себя" },
        if (CurrentTool.Instance.Tool.CodeName == "cannule" && actionCode == "pull")
        {
            if (LastTakenStep() != 9)
                errorMessage = "Сначала вытяните мандрен.";
            return 10;
        }

        // { "push_cannula",                   "Катерер заводится в глубину просвета артерии" },
        if (CurrentTool.Instance.Tool.CodeName == "cannule" && actionCode == "push")
        {
            if (LastTakenStep() != 10)
                errorMessage = "Сначала потяните канюлю";
            return 11;
        }

        // { "rinse_cannula",                  "Канюля промывается" },
        if (CurrentTool.Instance.Tool.CodeName == "cannule" && actionCode == "rinse")
        {
            if (LastTakenStep() != 11)
                errorMessage = "Сначала заведите канюлю";
            return 12;
        }

        // { "stitch",                         "Крылья канюли пришиваются к коже" },
        if (CurrentTool.Instance.Tool.CodeName == "stitch" && actionCode == "stitch")
        {
            if (LastTakenStep() != 12)
                errorMessage = "Нечего фиксировать";
            return 13;
        }

        // { "connect_sensor",                 "Соединить датчик инвазивного измерения с канюлей и монитором." }
        if (CurrentTool.Instance.Tool.CodeName == "invasive_sensor" && actionCode == "connect")
        {
            if (LastTakenStep() != 13)
                errorMessage = "Сначала возьмите шприц и наберите в него физраствор";
            return 14;
        }

        return null;
    }
}

