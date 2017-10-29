using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class Exam11 : BaseExam
{
    public override string Name => "Периферический венозный доступ №11 Внтуртивенная инъекция в вену локтевого сгиба";
    public override string LoadName => "Exam11";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "wear_examination_gloves",        "Надеть смотровые перчатки" },
        { "puncture_needle",                "Взять иглу для забора крови" },
        { "filling_drug_solution",          "Наполнить лекарственным раствором" },
        { "tourniquet",                     "Взять жгут и наложить" },
        { "palpation",                      "Пальпируем вену." },
        { "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        { "balls_spirit_disinfection",      "Дезинфекция спиртом. Протереть сверху вниз." },
        { "throw_balls",                    "Выкинуть шарики." },
        { "wear_sterile_gloves",            "Сменить перчатки на стерильные" },
        { "stretch_the_skin",               "Натянуть кожу." },
        { "remove_tourniquet",              "Снимаем жгут." },
        { "administer_drug",                "Ввести препарат." },
        { "spirit_balls",                   "Промокнуть марлевые шарики 70% раствором спирта" },
        { "attach_balls",                   "Прикладываем к месту инъекции ватный шарик." },
        { "needle_pull",                    "Извлечь шприц с иглой." },
        { "put_on_the_cap",                 "Надеть колпачек на иглу." },
        { "throw_needle",                   "Выбросить иглу." }
    };

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "gloves",                         "Перчатки" },
        { "gown",                           "Халат" },
        { "hand",                           "Рука для дополнительных действий" },
        { "syringe",                        "Шприц без иглы" },
        { "gauze_balls",                    "Стерильные марлевые шарики" },
        { "tweezers",                       "Пинцет без ничего" },
        { "tourniquet",                     "Жгут" },
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
                    { "lay",    "Наложить жгут" },
                    { "remove", "Снять жгут" }
                };
            case "hand":
                return new TupleList<string, string>
                {
                    { "palpation",        "Пальпация" },
                    { "stretch_the_skin", "Натянуть кожу" },
                };
            case "syringe":
                return new TupleList<string, string>
                {
                    { "needle_removing",        "Отсоеденить от иглы" },
                    { "needle_pull",            "Извлечь шприц с иглой" },
                    { "anesthesia",             "Сделать местную анестезию" },
                    { "put_on_the_cap",         "Надеть колпачек на иглу." },
                    { "throw_needle",           "Выбросить иглу." },
                    { "take_the_blood_ml10",      "Забор крови оттягивая поршень шприца, набирая 10мл крови" },
                    { "null",                   "---" },
                    { "anesthesia_needle",      "Взять иглу для анестезии кожи и наполнить шприц анестетиком" },
                    { "simple_needle",          "Взять иглу для забора крови" },
                    { "g22G_needle",             "Взять иглу для спинномозговой анестезии 22G и наполнить шприц анестетиком" },
                    { "wire_needle",            "Взять иглу для проводниковой анестезии и наполнить шприц анестетиком" },
                    { "a45_d10_punction_needle",  "Взять иглу для пункции вены длинной 10 см с внутренним просветом канала 1,7 мм и срезом острия иглы под углом 45°" },
                    { "a45_d7_punction_needle",   "Взять иглу для пункции вены длинной  4-7 см с внутренним просветом канала 1,0-1,5 мм и срезом острия иглы под углом 40-45°" },
                    { "filling_drug_solution",  "Наполнить лекарственным раствором" },
                    { "administer_drug",        "Ввести препарат" }
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
                    { "remove_balls",   "Сбросить марлевые шарики" },
                    { "null",           "---" },
                    { "top_down",       "Протереть сверху вниз" },
                    { "right_left",     "Протереть справа налево" }
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
            if (CurrentTool.Instance.Tool.CodeName == "syringe" && colliderTag.Contains(syringeError.Item1))
            {
                errorMessage = syringeError.Item2;
                return false;
            }
        }

        if (CurrentTool.Instance.Tool.CodeName == "syringe" && colliderTag == "medial_saphenous_vein_final_target")
            NeedleInsideTarget = true;

        if (!this.GenericMoveHelper(colliderTag, "medial_saphenous_vein_final_target", ref errorMessage))
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
        if (actionCode == "null") return null;

        int returnedStep;

        if (this.FenceInjections(actionCode, ref errorMessage, locatedColliderTag, out returnedStep, "medial_saphenous_vein_final_target", true))
            return returnedStep;

        return null;
    }
}

