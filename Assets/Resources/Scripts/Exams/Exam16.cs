using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class Exam16 : BaseExam
{
    public override string Name => "Периферический венозный доступ №16 Постановка внутривенного катетера venflon в дорсальные запястные вены";
    public override string LoadName => "Exam16";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "wear_examination_gloves",        "Надеть смотровые перчатки" },
        { "tourniquet",                     "Взять жгут и наложить" },
        { "palpation",                      "Пальпируем вену." },
        { "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        { "balls_spirit_disinfection",      "Дезинфекция спиртом. Протереть сверху вниз." },
        { "throw_balls",                    "Выкинуть шарики." },
        { "wear_sterile_gloves",            "Сменить перчатки на стерильные" },
        { "stretch_the_skin",               "Натянуть кожу." },
        { "pull_mandren",                   "Потягиваем мадрен." },
        { "remove_tourniquet",              "Снимаем жгут." },
        { "clamp_the_vein",                 "Пережать вену." },
        { "remove_mandren",                 "Вытаскиваем мадрен." },
        { "liquid_transfusion_connection",  "Соединение с системой переливания жидкости." },
        { "get_plaster",                    "Взять пластырь" },
        { "fixation_with_plaster",          "Фиксация пластырем." },
    };

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "gloves",                         "Перчатки" },
        { "gown",                           "Халат" },
        { "hand",                           "Рука для дополнительных действий" },
        { "gauze_balls",                    "Стерильные марлевые шарики" },
        { "tweezers",                       "Пинцет без ничего" },
        { "tourniquet",                     "Жгут" },
        { "venflon",                        "Катетер Venflon"},
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
            case "gown":
                return new TupleList<string, string>
                {
                    { "wear", "Надеть"}
                };
            case "tourniquet":
                return new TupleList<string, string>
                {
                    { "get",    "Взять жгут" },
                    { "lay",    "Наложить жгут" },
                    { "remove", "Снять жгут" }
                };
            case "hand":
                return new TupleList<string, string>
                {
                    { "get_palpation",              "Пальпация" },// "palpation"
                    { "get_stretch_the_skin",              "Натянуть кожу" }, // "stretch_the_skin"
                    { "get_clamp",              "Зажать вену" }, //clamp
                    { "clamp_out",        "Отпустить вену" },
                };
            case "gauze_balls":
                return new TupleList<string, string>
                {
                    { "spirit_p70",    "Промокнуть в 70% раствором спирта" },
                    { "spirit_p60",    "Промокнуть в 60% раствор спирта" },
                    { "spirit_p80",    "Промокнуть в 80% раствор спирта" },
                    { "iodine_p1",     "Промокнуть в 1% раствором йодоната" },
                    { "iodine_p3",     "Промокнуть в 3% раствором йодоната" },
                    { "null",         "---" },
                    { "throw_balls",  "Выкинуть шарики в мусорник" },
                    { "attach_balls", "Приложить шарик" },
                    { "top_down",     "Протереть сверху вниз" },
                };
            case "tweezers":
                return new TupleList<string, string>
                {
                    { "tweezers_balls", "Взять марлевые шарики" },
                    { "remove_balls",   "Сбросить марлевые шарики" },
                    { "null",           "---" },
                    { "top_down",       "Протереть сверху вниз" },
                    { "right_left",     "Протереть справа налево" }
                };
            case "venflon":
                return new TupleList<string, string>
                {
                    { "get",                            "Взять" },
                    { "remove",                         "Удалить катетер" },
                    { "liquid_transfusion_connection",  "Соединить с системой переливания жидкостей" },
                    { "remove_mandren",                 "Вытащить мадрен" },
                    { "pull_mandren",                   "Потягивать мадрен" }
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

        TupleList<string, string> criticalSyringeErrors = new TupleList<string, string>
        {
            { "nerves", "Повреждение нервных узлов"},
            { "lymph", "Повреждение лимфатических узлов"},
            { "bones", "Попадание в кость"},
        };

        foreach (var syringeError in criticalSyringeErrors)
        {
            if (CurrentTool.Instance.Tool.CodeName == "venflon" && colliderTag.Contains(syringeError.Item1))
            {
                errorMessage = syringeError.Item2;
                return false;
            }
        }

        if (CurrentTool.Instance.Tool.CodeName == "venflon" && colliderTag == "dorsal_metacarpal_vein_final_target")
            NeedleInsideTarget = true;

        if (!this.GenericMoveHelper(colliderTag, "dorsal_metacarpal_vein_final_target", ref errorMessage))
            return false;

        this.BloodInsidePavilion(colliderTag, "dorsal_metacarpal_vein_final_target");

        return true;
    }

    public override int? CheckAction(string actionCode, out string errorMessage, string locatedColliderTag = "")
    {
        errorMessage = "";

        // Безопасные операции

		if (this.BallClearAction(actionCode)) return null;
        if (this.GetSyringeAction(actionCode, ref errorMessage)) return null;
        if (this.RemoveBallsAction(actionCode)) return null;
        if (actionCode == "null") return null;

        int returnedStep;


		if (this.VenflonInstallation(actionCode, ref errorMessage, locatedColliderTag, out returnedStep, "dorsal_metacarpal_vein_final_target"))

            return returnedStep;

        // Критическая ошибка
        if (CurrentTool.Instance.Tool.CodeName == "venflon" && actionCode == "remove")
        {
            errorMessage = "Катетер был извлечен. Катетеризация провалена";
            return null;
        }

        return null;
    }
}

