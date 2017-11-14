using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class TibiaExam : BaseExam
{
    public override string Name => "Внутрішньокістковий доступ в великогомілкову кістку";
    public override string LoadName => "TibiaExam";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        { "spirit_balls",                   "Промокнути марлеві кульки 70% розчином спирту" },
        { "spirit_disinfection",            "Дезінфекція спиртом. Протерти зверху вниз" },
        { "wear_sterile_gloves",            "Змінити рукавички на стерильні" },
        { "anesthesia_needle",              "Взяти голку для анестезії шкіри" },
        { "anesthesia",                     "Зробити місцеву анестезію" },
        { "big_prepare",                    "Звільнити засувку шприца-пістолета B.I.G." },
        { "big_activate",                   "Активувати пістолет B.I.G." },
        { "big_remove",                     "Витягти стилет троакара" },
        { "fixation_with_plaster",          "Фіксація пластиром" },
        { "syringe_nacl",                   "Наповнити шприц фізрозчином" },
        { "big_nacl",                       "Ввести фізрозчин" },
        { "big_stopcock",                   "Приєднати запірний кран з системою для інфузії" }
    };

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "gloves",                         "Рукавички" },
        { "syringe",                        "Шприц без голки" },
        { "gauze_balls",                    "Стерильні марлеві кульки" },
        { "big",                            "Шприц-пістолет для внутрішньокісткових ін'єкцій B.I.G." },
        { "stopcock",                       "Запірний кран для інфузій з системою" },
        { "patch",                          "Пластир" }
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
                    { "filling_nacl",           "Наповнити 10-20 мл фізрозчином" },
                    { "big_nacl",               "Ввести фізрозчин в канюлю голки" }
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
            case "big":
                return new TupleList<string, string>
                {
                    { "get",      "Взяти" },
                    { "prepare",  "Звільнити засувку шприца-пістолета B.I.G." },
                    { "activate", "Активувати пістолет B.I.G." },
                    { "remove",   "Витягти стилет троакара" }
                };
            case "stopcock":
                return new TupleList<string, string>
                {
                    { "impose", "Зафіксувати" },
                    { "connect", "Приєднати запірний кран з системою" }
                };
            case "patch":
                return new TupleList<string, string>
                {
                    { "stick", "Наклеїти" }
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
            if (!CurrentTool.Instance.Tool.StateParams.ContainsKey("entry_angle") ||
                !float.Parse(CurrentTool.Instance.Tool.StateParams["entry_angle"]).CheckRange(85, 95))
                errorMessage = "Неправильний кут установки";
            return 7;
        }

        // { "big_activate",                   "Активировать пистолет B.I.G." },
        if (CurrentTool.Instance.Tool.CodeName == "big" && actionCode == "activate")
        {
            if (LastTakenStep() != 7)
                errorMessage = "Неможливо активувати пістолет. Звільніть засувку";
            return 8;
        }

        // { "big_remove",                     "Извлечь стилет троакара" },
        if (CurrentTool.Instance.Tool.CodeName == "big" && actionCode == "remove")
        {
            if (LastTakenStep() != 8)
                errorMessage = "Не можна видаляти стилет до активації";
            return 9;
        }

        //{ "fixation_with_plaster",          "Фіксація пластиром" }
        if (CurrentTool.Instance.Tool.CodeName == "patch" && actionCode == "stick")
        {
            if (LastTakenStep() != 9)
                errorMessage = "Нічого фіксувати";
            return 10;
        }

        // Отсоединяем в любом другом месте
        DateTime mockDateTime = DateTime.Now;
        if (this.NeedleRemovingAction(actionCode, ref errorMessage, locatedColliderTag, ref mockDateTime)) return null;

        // { "syringe_nacl",                   "Наполнить шприц физраствором." },
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_nacl")
        {
            if (CurrentTool.Instance.Tool.StateParams.ContainsKey("has_needle") && Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["has_needle"]))
                errorMessage = "Потрібно наповнювати фізрозчином шприц без голки для введення в канюлю пістолета";
            return 11;
        }

        // { "big_nacl",                       "Ввести физраствор." },
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "big_nacl")
        {
            if (LastTakenStep() != 11)
                errorMessage = "Спочатку візьміть шприц і наберіть в нього фізрозчин";
            return 12;
        }

        // { "big_stopcock",                   "Подсоеденить запорный кран с системой для инфузии." }
        if (CurrentTool.Instance.Tool.CodeName == "stopcock" && actionCode == "connect")
        {
            if (LastTakenStep() != 12)
                errorMessage = "Спочатку введіть фізрозчин";
            return 13;
        }

        // Добавление иголки
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode.Contains("_needle"))
            SyringeHelper.TryGetNeedle(actionCode, out errorMessage, 2);

        return null;
    }
}

