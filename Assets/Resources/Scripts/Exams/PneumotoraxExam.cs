using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class PneumotoraxExam : BaseExam
{
    public override string Name => "Декомпресія і дренування плевральної порожнини хворому з пневмотораксом зліва";
    public override string LoadName => "PneumotoraxExam";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        { "spirit_balls",                   "Промокнути марлеві кульки 70% розчином спирту" },
        { "spirit_disinfection",            "Дезінфекція спиртом. Протерти зверху вниз" },
        { "wear_sterile_gloves",            "Змінити рукавички на стерильні" },
        { "anesthesia_needle",              "Взяти голку для анестезії шкіри" },
        { "anesthesia",                     "Зробити місцеву анестезію" },
        { "scalpel_incision",               "Надріз для полегшення входження троакара" },
        { "trocar_pull",                    "Витягнути стилет заводячи ПВХ трубку в плевральну порожнину"},
        { "trocar_clamp",                   "Перетиснути затискачем"},
        { "stitch",                         "Пришиваємо до шкіри" },
        { "trocar_connect",                 "Під'єднуємо подовжувач"},
        { "trocar_connect_valve",           "Приєднаний дренажний вентиль"},
        { "bobrov_bank_connect",            "Приєднати вентиль з клапаном до банки"},
        { "trocar_clamp_out",               "Зняти затискач з катетера"}
    };

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "gloves",                         "Рукавички" },
        { "trocar",                         "Дренаж торакальний" },
        { "hand",                           "Рука для додаткових дій" },
        { "syringe",                        "Шприц без голки" },
        { "gauze_balls",                    "Стерильні марлеві кульки" },
        { "scalpel",                        "Скальпель" },
        { "bobrov_bank",                    "Банка Боброва наповнена антисептиком" },
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
                    { "get_palpation",    "Пальпація" }
                };
            case "scalpel":
                return new TupleList<string, string>
                {
                    { "get",      "Взяти"},
                    { "incision", "Зробити надріз"}
                };
            case "bobrov_bank":
                return new TupleList<string, string>
                {
                    { "connect", "Приєднати вентиль з клапаном до банки"}
                };
            case "trocar":
                return new TupleList<string, string>
                {
                    { "get",                   "Взяти"},
                    { "pull",                  "Витягнути стилет заводячи ПВХ трубку в плевральну порожнину"},
                    { "trocar_connect",        "Приєднати подовжувач"},
                    { "trocar_connect_valve",  "Приєднати дренажний вентиль"},
                    { "clamp_out",             "Зняти затискач"},
                    { "clamp",                 "Перетиснути затискачем"}
                };
            case "syringe":
                return new TupleList<string, string>
                {
                    { "needle_removing",        "Від'єднати від голки" },
                    { "anesthesia",             "Зробити місцеву анестезію" },
                    { "piston_pulling",         "Потягування поршня на себе" },
                    { "null",                   "---" },
                    { "anesthesia_needle",      "Взяти голку для анестезії шкіри і наповнити шприц анестетиком" },
                    { "g22G_needle",            "Взяти голку для спинномозкової анестезії 22G і наповнити шприц анестетиком" },
                    { "wire_needle",            "Взяти голку для провідникової анестезії та наповнити шприц анестетиком" },
                    { "a45_d10_punction_needle","Взяти голку для пункції вени довжиною 10 см з внутрішнім просвітом каналу 1,7 мм і зрізом вістря голки під кутом 45°" },
                    { "a45_d4_punction_needle", "Взяти голку для пункції вени довжиною не менше 4 см з внутрішнім просвітом каналу 1,0-1,4 мм і зрізом вістря голки під кутом 40-45°" },
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
            case "stitch":
                return new TupleList<string, string>
                {
                    { "stitch", "Пришити катетер до шкіри" }
                };
            default:
                return new TupleList<string, string>();
        }
    }

    public override bool CheckMove(string colliderTag, out string errorMessage, out string tipMessage)
    {
        errorMessage = "";
        tipMessage = "";

        if (!this.GenericMoveHelper(colliderTag, "pleural_cavity", ref errorMessage, ref tipMessage))
            return false;

        return true;
    }

    public override int? CheckAction(string actionCode, out string errorMessage, ref string tipMessage, out bool showAnimation, string locatedColliderTag = "")
    {
        errorMessage = "";
        showAnimation = true;

        // Безопасные операции
        if (this.BallClearAction(actionCode)) return null;
        if (this.RemoveBallsAction(actionCode)) return null;
        if (this.PistonPullingAction(actionCode)) return null;
        if (this.GetSyringeAction(actionCode, ref errorMessage)) return null;
        if (actionCode == "null") return null;

        int returnedStep;

        // Перчатки + Спирт
        if (this.BiosafetySpirit(actionCode, ref errorMessage, locatedColliderTag,  out returnedStep, ref showAnimation)) return returnedStep;

        //{ "anesthesia_needle",              "Взять иглу для анестезии кожи." },
        if (this.GetNeedleAction(actionCode, ref errorMessage, "anesthesia_needle", 4, ref showAnimation)) return 5;

        //{ "anesthesia",                     "Сделать местную анестезию." },
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "anesthesia")
        {
            SyringeHelper.CheckAnestesiaNeedle(out errorMessage);
            return 6;
        }

        // { "scalpel_incision",               "Надрез для облегчения вхождения троакара" },
        if (CurrentTool.Instance.Tool.CodeName == "scalpel" && actionCode == "incision")
        {
            if (!locatedColliderTag.Contains("scalpel_target"))
                errorMessage = "Надріз не в тому місці";
            return 7;
        }

        // { "trocar_pull",                    "Вытянуть стилет заводя ПВХ трубку в плевральную полость."},
        if (CurrentTool.Instance.Tool.CodeName == "trocar" && actionCode == "pull")
        {
            if (locatedColliderTag != "pleural_cavity")
                errorMessage = "Дренажний троакар не досяг цілі";

            return 8;
        }

        // { "trocar_clamp",                   "Пережать зажимом"},
        if (CurrentTool.Instance.Tool.CodeName == "trocar" && actionCode == "clamp")
        {
            if (LastTakenStep() != 8)
            {
                showAnimation = false;
                errorMessage = "Спочатку витягніть стилет";
            }
            return 9;
        }

        // { "stitch",                         "Пришиваем к коже" },
        if (CurrentTool.Instance.Tool.CodeName == "stitch" && actionCode == "stitch")
        {
            if (LastTakenStep() != 9)
            {
                showAnimation = false;
                errorMessage = "Нічого фіксувати або не затиснуто затискачем";
            }
            return 10;
        }

        // { "trocar_connect",                 "Подсоединяем удлинитель"},
        if (CurrentTool.Instance.Tool.CodeName == "trocar" && actionCode == "trocar_connect")
        {
            if (LastTakenStep() != 10)
            {
                showAnimation = false;
                errorMessage = "Спочатку зафіксуйте підшиванням до шкіри";
            }
            return 11;
        }

        // { "trocar_connect_valve",           "Подсоединен дренажный вентиль"},
        if (CurrentTool.Instance.Tool.CodeName == "trocar" && actionCode == "trocar_connect_valve")
        {
            if (LastTakenStep() != 11)
            {
                showAnimation = false;
                errorMessage = "Спочатку подовжувач";
            }
            return 12;
        }

        // { "bobrov_bank_connect",            "Подсоеденить вентиль с клапаном к банке"},
        if (CurrentTool.Instance.Tool.CodeName == "bobrov_bank" && actionCode == "connect")
        {
            if (LastTakenStep() != 12)
            {
                showAnimation = false;
                errorMessage = "Спочатку під'єднайте вентиль";
            }
            return 13;
        }

        // { "trocar_clamp_out",               "Снять зажим с катетера" }
        if (CurrentTool.Instance.Tool.CodeName == "trocar" && actionCode == "clamp_out")
        {
            if (LastTakenStep() != 13)
            {
                showAnimation = false;
                errorMessage = "Спочатку з'єднайте з банкою Боброва";
            }
            return 14;
        }

        // Добавление иголки
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode.Contains("_needle"))
            SyringeHelper.TryGetNeedle(actionCode, out errorMessage, 2);

        showAnimation = false;
        return null;
    }
}

