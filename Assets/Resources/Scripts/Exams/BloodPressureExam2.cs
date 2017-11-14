using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class BloodPressureExam2 : BaseExam
{
    public override string Name => "Вимірювання артеріального тиску справжнім манометром";
    public override string LoadName => "BloodPressureExam2";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        { "spirit_balls",                   "Промокнути марлеві кульки 70% розчином спирту" },
        { "spirit_disinfection",            "Дезінфекція спиртом. Протерти зверху вниз" },
        { "wear_sterile_gloves",            "Змінити рукавички на стерильні" },
        { "sterile_tissue",                 "Накрити операційне поле стерильними серветками" },
        { "anesthesia_needle",              "Взяти голку для анестезії шкіри" },
        { "anesthesia",                     "Зробити місцеву анестезію" },
        { "palpation",                      "Пальпувати артерію" },
        { "remove_mandren",                 "Вийняти з канюлі мандрен" },
        { "pull_cannula",                   "Канюля тягнеться на себе" },
        { "push_cannula",                   "Катетер заводиться в глибину просвіту артерії" },
        { "rinse_cannula",                  "Канюля промивається" },
        { "stitch",                         "Крила канюлі пришиваються до шкіри" },
        { "connect_sensor",                 "З'єднати датчик інвазивного вимірювання з канюлею і монітором" }
    };

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "gloves",                         "Рукавички" },
        { "sterile_tissue",                 "Стерильні серветки" },
        { "hand",                           "Рука для додаткових дій" },
        { "syringe",                        "Шприц без голки" },
        { "gauze_balls",                    "Стерильні марлеві кульки" },
        { "cannule",                        "Артеріальна канюля" },
        { "invasive_sensor",                "Датчик інвазивного вимірювання артеріального тиску з системою" },
        { "stitch",                         "Шовний матеріал + голкотримач" }
    };

    public override TupleList<string, string> ToolActions(ToolItem tool)
    {
        switch (tool.CodeName)
        {
            case "gloves":
                return new TupleList<string, string>
                {
                    { "wear_examination", "Одягти оглядові рукавички"},
                    { "wear_sterile",     "Змінити рукавички на стерильні"}
                };
            case "hand":
                return new TupleList<string, string>
                {
                    { "get_palpation",              "Пальпація" }
                };
            case "sterile_tissue":
                return new TupleList<string, string>
                {
                    { "put", "Накрити операційне поле стерильними серветками"}
                };
            case "syringe":
                return new TupleList<string, string>
                {
                    { "needle_removing",        "Від'єднати від голки" },
                    { "anesthesia",             "Зробити місцеву анестезію" },
                    { "piston_pulling",         "Потягнути поршень на себе" },
                    { "null",                   "---" },
                    { "anesthesia_needle",      "Взяти голку для анестезії шкіри і наповнити шприц анестетиком" },
                    { "g22G_needle",            "Взяти голку для спинномозкової анестезії 22G і наповнити шприц анестетиком" },
                    { "wire_needle",            "Взяти голку для провідникової анестезії та наповнити шприц анестетиком" },
                    { "a45_d10_punction_needle",  "Взяти голку для пункції вени довжиною 10 см з внутрішнім просвітом каналу 1,7 мм і зрізом вістря голки під кутом 45°" },
                    { "a45_d4_punction_needle",   "Взяти голку для пункції вени довжиною не менше 4 см з внутрішнім просвітом каналу 1,0-1,4 мм і зрізом вістря голки під кутом 40-45°" },
                };
            case "gauze_balls":
                return new TupleList<string, string>
                {
                    { "spirit_p70",  "Промокнути в 70% розчині спирту" },
                    { "spirit_p60",  "Промокнути в 60% розчині спирту" },
                    { "spirit_p80",  "Промокнути в 80% розчині спирту" },
                    { "iodine_p1",   "Промокнути в 1% розчині йодоната" },
                    { "iodine_p3",   "Промокнути в 3% розчині йодоната" },
                    { "clear",       "Взяти нову стерильну кульку" },
                    { "null",        "---" },
                    { "throw_balls", "Викинути кульки в смітник" },
                    { "get_balls",   "Прикласти кульку" }, // "attach_balls"
                    { "get_top_down","Протерти зверху вниз" }, // "top_down"
                };
            case "invasive_sensor":
                return new TupleList<string, string>
                {
                    { "connect", "З'єднати з канюлею і монітором" }
                };
            case "cannule":
                return new TupleList<string, string>
                {
                    { "get",                            "Взяти" },
                    { "push",                           "Завести в глибину просвіту" },
                    { "rinse",                          "Промити канюлю" },
                    { "remove_mandren",                 "Витягнути мадрен" },
                    { "pull",                           "Потягнути канюлю на себе" }
                };
            case "stitch":
                return new TupleList<string, string>
                {
                    { "cannule_stitch", "Пришить к коже" }
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

        // { "sterile_tissue",                    "Накриваємо операційне поле стерильними серветками" },
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
                errorMessage = "Пальпується не те місце";
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
                errorMessage = "Спочатку витягніть мандрен";
            return 10;
        }

        // { "push_cannula",                   "Катерер заводится в глубину просвета артерии" },
        if (CurrentTool.Instance.Tool.CodeName == "cannule" && actionCode == "push")
        {
            if (LastTakenStep() != 10)
                errorMessage = "Спочатку потягніть канюлю";
            return 11;
        }

        // { "rinse_cannula",                  "Канюля промывается" },
        if (CurrentTool.Instance.Tool.CodeName == "cannule" && actionCode == "rinse")
        {
            if (LastTakenStep() != 11)
                errorMessage = "Спочатку заведіть канюлю";
            return 12;
        }

        // { "stitch",                         "Крылья канюли пришиваются к коже" },
        if (CurrentTool.Instance.Tool.CodeName == "stitch" && actionCode == "cannule_stitch")
        {
            if (LastTakenStep() != 12)
                errorMessage = "Нічого фіксувати";
            return 13;
        }

        // { "connect_sensor",                 "Соединить датчик инвазивного измерения с канюлей и монитором." }
        if (CurrentTool.Instance.Tool.CodeName == "invasive_sensor" && actionCode == "connect")
        {
            if (LastTakenStep() != 13)
                errorMessage = "Спочатку візьміть шприц і наберіть в нього фізрозчин";
            return 14;
        }

        // Добавление иголки
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode.Contains("_needle"))
            SyringeHelper.TryGetNeedle(actionCode, out errorMessage, 2);

        return null;
    }
}

