using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class HydrotoraxExam : BaseExam
{
    public override string Name => "Декомпрессия и дренирование плевральной полости больному с гидротораксом/пиотораксом/гемотораксом слева";
    public override string LoadName => "HydrotoraxExam";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "wear_examination_gloves",        "Надеть смотровые перчатки" },
        { "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        { "spirit_disinfection",            "Дезинфекция спиртом. Протереть сверху вниз." },
        { "wear_sterile_gloves",            "Сменить перчатки на стерильные" },
        { "anesthesia_needle",              "Взять иглу для анестезии кожи." },
        { "anesthesia",                     "Сделать местную анестезию." },
        { "scalpel_incision",               "Надрез для облегчения вхождения троакара" },
        { "trocar_pull",                    "Вытянуть стилет заводя ПВХ трубку в плевральную полость."},
        { "trocar_clamp",                   "Пережать зажимом"},
        { "stitch",                         "Пришиваем к коже" },
        { "trocar_connect",                 "Подсоединяем удлинитель"},
        { "trocar_connect_valve",           "Подсоединен дренажный вентиль"},
        { "bobrov_bank_connect",            "Подсоеденить вентиль с клапаном к банке"},
        { "trocar_clamp_out",               "Снять зажим с катетера" }
    };

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "gloves",                         "Перчатки" },
        { "trocar",                         "Дренаж торакальный" },
        { "hand",                           "Рука для дополнительных действий" },
        { "syringe",                        "Шприц без иглы" },
        { "gauze_balls",                    "Стерильные марлевые шарики" },
        { "scalpel",                        "Скальпель" },
        { "bobrov_bank",                    "Банка Боброва наполненная антисептиком" },
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
            case "scalpel":
                return new TupleList<string, string>
                {
                    { "get", "Взять"},
                    { "incision", "Сделатть надрез"}
                };
            case "bobrov_bank":
                return new TupleList<string, string>
                {
                    { "connect", "Подсоеденить вентиль с клапаном к банке"}
                };
            case "trocar":
                return new TupleList<string, string>
                {
                    { "get", "Взять"},
                    { "pull", "Вытянуть стилет заводя ПВХ трубку в плевральную полость"},
                    { "trocar_connect", "Подсоединить удлинитель"},
                    { "trocar_connect_valve", "Подсоединить дренажный вентиль"},
                    { "clamp_out", "Снять зажим"},
                    { "clamp", "Пережать зажимом"}
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
            case "stitch":
                return new TupleList<string, string>
                {
                    { "stitch", "Пришить катетер к коже" }
                };
            default:
                return new TupleList<string, string>();
        }
    }

    public override bool CheckMove(string colliderTag, out string errorMessage)
    {
        errorMessage = "";

        if (!this.GenericMoveHelper(colliderTag, "pleural_cavity", ref errorMessage))
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

        //{ "anesthesia_needle",              "Взять иглу для анестезии кожи." },
        if (this.GetNeedleAction(actionCode, ref errorMessage, "anesthesia_needle", 4)) return 5;

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
                errorMessage = "Надрез не в том месте";
            return 7;
        }

        // { "trocar_pull",                    "Вытянуть стилет заводя ПВХ трубку в плевральную полость."},
        if (CurrentTool.Instance.Tool.CodeName == "trocar" && actionCode == "pull")
        {
            if (locatedColliderTag != "pleural_cavity")
                errorMessage = "Дренажный троакар не достиг цели";

            return 8;
        }

        // { "trocar_clamp",                   "Пережать зажимом"},
        if (CurrentTool.Instance.Tool.CodeName == "trocar" && actionCode == "clamp")
        {
            if (LastTakenStep() != 8)
                errorMessage = "Сначала вытяните стилет.";
            return 9;
        }

        // { "stitch",                         "Пришиваем к коже" },
        if (CurrentTool.Instance.Tool.CodeName == "stitch" && actionCode == "stitch")
        {
            if (LastTakenStep() != 9)
                errorMessage = "Нечего фиксировать или не пережато зажимом";
            return 10;
        }

        // { "trocar_connect",                 "Подсоединяем удлинитель"},
        if (CurrentTool.Instance.Tool.CodeName == "trocar" && actionCode == "trocar_connect")
        {
            if (LastTakenStep() != 10)
                errorMessage = "Сначала зафиксируйте подшиванием к коже";
            return 11;
        }

        // { "trocar_connect_valve",           "Подсоединен дренажный вентиль"},
        if (CurrentTool.Instance.Tool.CodeName == "trocar" && actionCode == "trocar_connect_valve")
        {
            if (LastTakenStep() != 11)
                errorMessage = "Сначала удлинитель";
            return 12;
        }

        // { "bobrov_bank_connect",            "Подсоеденить вентиль с клапаном к банке"},
        if (CurrentTool.Instance.Tool.CodeName == "bobrov_bank" && actionCode == "connect")
        {
            if (LastTakenStep() != 12)
                errorMessage = "Сначала подсоедините вентиль";
            return 13;
        }

        // { "trocar_clamp_out",               "Снять зажим с катетера" }
        if (CurrentTool.Instance.Tool.CodeName == "trocar" && actionCode == "clamp_out")
        {
            if (LastTakenStep() != 13)
                errorMessage = "Сначала соедините c банкой Боброва";
            return 14;
        }

        return null;
    }
}

