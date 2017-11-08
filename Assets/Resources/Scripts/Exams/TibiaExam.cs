using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class TibiaExam : BaseExam
{
    public override string Name => "Внутрикостный доступ в большеберцовую кость";
    public override string LoadName => "TibiaExam";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "wear_examination_gloves",        "Надеть смотровые перчатки" },
        { "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        { "spirit_disinfection",            "Дезинфекция спиртом. Протереть сверху вниз." },
        { "wear_sterile_gloves",            "Сменить перчатки на стерильные" },
        { "anesthesia_needle",              "Взять иглу для анестезии кожи." },
        { "anesthesia",                     "Сделать местную анестезию." },
        { "big_prepare",                    "Освободить защелку шприца-пистолета B.I.G." },
        { "big_activate",                   "Активировать пистолет B.I.G." },
        { "big_remove",                     "Извлечь стилет троакара" },
        { "fixation_with_plaster",          "Фиксация пластырем." },
        { "syringe_nacl",                   "Наполнить шприц физраствором." },
        { "big_nacl",                       "Ввести физраствор." },
        { "big_stopcock",                   "Подсоеденить запорный кран с системой для инфузии." }
    };

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "gloves",                         "Перчатки" },
        { "syringe",                        "Шприц без иглы" },
        { "gauze_balls",                    "Стерильные марлевые шарики" },
        { "big",                            "Шприц-пистолет для внутрикостных инъекций B.I.G." },
        { "stopcock",                       "Запорный кран для инфузий с системой" },
        { "patch",                          "Пластырь" }
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
                    { "filling_nacl",            "Наполнить 10-20 мл физраствора" },
                    { "big_nacl",                "Ввести физраствор в канюлю иглы" }
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
            case "big":
                return new TupleList<string, string>
                {
                    { "get", "взять" },
                    { "prepare", "Освободить защелку шприца-пистолета B.I.G." },
                    { "activate", "Активировать пистолет B.I.G." },
                    { "remove", "Извлечь стилет троакара" }
                };
            case "stopcock":
                return new TupleList<string, string>
                {
                    { "get", "Взять" }
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

    public override bool CheckMove(string colliderTag, out string errorMessage)
    {
        errorMessage = "";

        if (!this.GenericMoveHelper(colliderTag, "tubercle_the_tibia", ref errorMessage))
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

        // { "big_prepare",                    "Освободить защелку шприца-пистолета B.I.G." },
        if (CurrentTool.Instance.Tool.CodeName == "big" && actionCode == "prepare")
        {
            if (locatedColliderTag == "tubercle_the_tibia")
            {
                if (!CurrentTool.Instance.Tool.StateParams.ContainsKey("entry_angle") ||
                    !float.Parse(CurrentTool.Instance.Tool.StateParams["entry_angle"]).CheckRange(85, 95))
                    errorMessage = "Неправильный угол установки";
                return 7;
            }

            return null;
        }

        // { "big_activate",                   "Активировать пистолет B.I.G." },
        if (CurrentTool.Instance.Tool.CodeName == "big" && actionCode == "activate")
        {
            if (LastTakenStep() != 7)
                errorMessage = "Не удается активировать пистолет. Освободите защелку.";
            return 8;
        }

        // { "big_remove",                     "Извлечь стилет троакара" },
        if (CurrentTool.Instance.Tool.CodeName == "big" && actionCode == "remove")
        {
            if (LastTakenStep() != 8)
                errorMessage = "Нельзя удалять стилет до активации";
            return 9;
        }

        //{ "fixation_with_plaster",          "Фиксация пластырем." }
        if (CurrentTool.Instance.Tool.CodeName == "patch" && actionCode == "stick")
        {
            if (LastTakenStep() != 9)
                errorMessage = "Нечего фиксировать";
            if (!locatedColliderTag.Contains("big"))
                errorMessage = "Не то место установки. Сначала должен быть корректно установлен пистолет";
            return 10;
        }

        // { "syringe_nacl",                   "Наполнить шприц физраствором." },
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_nacl")
        {
            if (CurrentTool.Instance.Tool.StateParams.ContainsKey("has_needle") && Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["has_needle"]))
                errorMessage = "Нужно наполнять физраствором шприц без иглы для ввода в канюлю пистолета.";
            return 11;
        }

        // { "big_nacl",                       "Ввести физраствор." },
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "big_nacl")
        {
            if (LastTakenStep() != 11)
                errorMessage = "Сначала возьмите шприц и наберите в него физраствор";
            return 12;
        }

        // { "big_stopcock",                   "Подсоеденить запорный кран с системой для инфузии." }
        if (CurrentTool.Instance.Tool.CodeName == "stopcock" && actionCode == "connect")
        {
            if (LastTakenStep() != 12)
                errorMessage = "Сначала введите физраствор";
            return 13;
        }

        return null;
    }
}

