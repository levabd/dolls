using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class Exam15 : BaseExam
{
    private bool _needleInsideTarget;
    private string _currentBallLiquid = "none";

    public override string Name => "Периферический венозный доступ №15 Постановка внутривенного катетера venflon в латеральную вену локтевого сгиба";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "wear_gloves",                    "Надеть перчатки" },
        { "tourniquet",                     "Взять жгут и наложить" },
        { "palpation",                      "Пальпируем вену." },
        { "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        { "balls_spirit_disinfection",      "Дезинфекция спиртом. Протереть сверху вниз." },
        { "throw_balls",                    "Выкинуть шарики." },
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
                    { "wear", "Надеть"}
                };
            case "gown":
                return new TupleList<string, string>
                {
                    { "wear", "Надеть"}
                };
            case "tourniquet":
                return new TupleList<string, string>
                {
                    { "lay",    "Наложить жгут" },
                    { "remove", "Снять жгут" }
                };
            case "hand":
                return new TupleList<string, string>
                {
                    { "palpation",        "Пальпация" },
                    { "stretch_the_skin", "Натянуть кожу" },
                    { "clamp",            "Зажать вену" },
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
                    { "remove_balls",   "Сбросить стерильные шарики" },
                    { "null",           "---" },
                    { "top_down",       "Протереть сверху вниз" },
                    { "right_left",     "Протереть справа налево" }
                };
            case "venflon":
                return new TupleList<string, string>
                {
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

    public override bool CheckMove(ref ToolItem tool, string colliderTag, out string errorMessage)
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
            if (tool.CodeName == "venflon" && colliderTag.Contains(syringeError.Item1))
            {
                errorMessage = syringeError.Item2;
                return false;
            }
        }

        if (tool.CodeName == "venflon" && colliderTag == "lateral_subcutaneous_arm_vein_final_target")
            _needleInsideTarget = true;

        if (tool.CodeName == "venflon" && (colliderTag != "lateral_subcutaneous_arm_vein_final_target" || colliderTag != "lateral_subcutaneous_arm_vein"))
        {
            errorMessage = "Пункция не в том месте";
            if (_needleInsideTarget) // Прошли вену навылет
                errorMessage = "Гематома";
            return false;
        }

        if (tool.CodeName == "gauze_balls" && colliderTag != "ulnar_fold")
        {
            errorMessage = "Дезинфекция не в том месте";
            return false;
        }

        if (tool.CodeName == "tourniquet" && colliderTag != "below_the_shoulder")
        {
            errorMessage = "Не туда наложен жгут";
            return false;
        }

        if (tool.CodeName == "hand" && (colliderTag != "medial_saphenous_vein" || colliderTag != "lateral_subcutaneous_arm_vein"))
        {
            errorMessage = "Пальпируется не то место";
            return false;
        }

        this.BloodInsidePavilion(ref tool, colliderTag, "lateral_subcutaneous_arm_vein_final_target");

        return true;
    }

    public override int? CheckAction(ref ToolItem tool, string actionCode, out string errorMessage, string locatedColliderTag = "")
    {
        errorMessage = "";

        // Безопасные операции
        if (this.BallClearAction(ref tool, actionCode, ref _currentBallLiquid)) return null;
        if (this.RemoveBallsAction(ref tool, actionCode)) return null;
        if (actionCode == "null") return null;

        int returnedStep;

        if (this.VenflonInstallation(LastTakenStep(), ref tool, actionCode, ref errorMessage, locatedColliderTag, out returnedStep,
            "below_the_shoulder", "ulnar_fold", "medial_saphenous_vein", "lateral_subcutaneous_arm_vein", "lateral_subcutaneous_arm_vein_final_target", ref _currentBallLiquid))
            return returnedStep;

        // Критическая ошибка
        if (tool.CodeName == "venflon" && actionCode == "remove")
        {
            errorMessage = "Катетер был извлечен. Катетеризация провалена";
            return null;
        }

        return null;
    }
}

