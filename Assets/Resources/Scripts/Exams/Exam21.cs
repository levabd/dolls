using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class Exam21 : BaseExam
{
    public override DateTime NeedleRemovingMoment { get; set; }

    public override string Name => "Периферичний венозний доступ №20 Забір крові з серединної вени ліктя";
    public override string LoadName => "Exam21";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        { "puncture_needle",                "Взяти голку для забору крові" },
        { "tourniquet",                     "Взяти джгут і накласти" },
        { "palpation",                      "Пальпуємо вену" },
        { "spirit_balls",                   "Промокнути марлеві кульки 70% розчином спирту" },
        { "balls_spirit_disinfection",      "Дезінфекція спиртом. Протерти зверху вниз" },
        { "throw_balls",                    "Викинути кульки" },
        { "wear_sterile_gloves",            "Змінити рукавички на стерильні" },
        { "stretch_the_skin",               "Натягнути шкіру" },
        { "take_the_blood_ml10",            "Набрати 10мл. крові" },
        { "remove_tourniquet",              "Зняти джгут" },
        { "spirit_balls",                   "Промокнути марлеві кульки 70% розчином спирту" },
        { "attach_balls",                   "Прикладаємо до місця ін'єкції ватну кульку" },
        { "needle_pull",                    "Витягти шприц з голкою" },
        { "put_on_the_cap",                 "Одягти ковпачок на голку" },
        { "throw_needle",                   "Викинути голку" }
    };

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "gloves",                         "Рукавички" },
        { "gown",                           "Халат" },
        { "hand",                           "Рука для додаткових дій" },
        { "syringe",                        "Шприц без голки" },
        { "gauze_balls",                    "Стерильні марлеві кульки" },
        { "tweezers",                       "Пінцет без нічого" },
        { "tourniquet",                     "Джгут" },
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
            case "tourniquet":
                return new TupleList<string, string>
                {
                    { "get",    "Взяти джгут" },
                    { "lay",    "Накласти джгут" },
                    { "remove", "Зняти джгут" }
                };
            case "hand":
                return new TupleList<string, string>
                {
                    { "get_palpation",        "Пальпація" },// "palpation"
                    { "get_stretch_the_skin", "Натягнути шкіру" }, // "stretch_the_skin"
                };
            case "syringe":
                return new TupleList<string, string>
                {
                    { "get",                    "Взяти шприц з голкою" },
                    { "needle_removing",        "Від'єднати від голки" },
                    { "needle_pull",            "Витягти шприц з голкою" },
                    { "anesthesia",             "Зробити місцеву анестезію" },
                    { "put_on_the_cap",         "Одягти ковпачок на голку" },
                    { "throw_needle",           "Викинути голку" },
                    { "take_the_blood_ml10",    "Забір крові відтягуючи поршень шприца, набираючи 10 мл крові" },
                    { "null",                   "---" },
                    { "anesthesia_needle",      "Взяти голку для анестезії шкіри і наповнити шприц анестетиком" },
                    { "simple_needle",          "Взяти голку для забору крові" },
                    { "a45_d10_punction_needle","Взяти голку для пункції вени довжиною 10 см з внутрішнім просвітом каналу 1,7 мм і зрізом вістря голки під кутом 45°" },
                    { "a45_d7_punction_needle", "Взяти голку для пункції вени довжиною 4-7 см з внутрішнім просвітом каналу 1,0-1,5 мм і зрізом вістря голки під кутом 40-45°" },
                    { "filling_novocaine_half", "Наповнити 0,25% новокаїну наполовину" }
                };
            case "gauze_balls":
                return new TupleList<string, string>
                {
                    { "spirit_p70",    "Промокнути в 70% розчині спирту" },
                    { "spirit_p60",    "Промокнути в 60% розчині спирту" },
                    { "spirit_p80",    "Промокнути в 80% розчині спирту" },
                    { "iodine_p1",     "Промокнути в 1% розчині йодоната" },
                    { "iodine_p3",     "Промокнути в 3% розчині йодоната" },
                    { "null",          "---" },
                    { "throw_balls",   "Викинути кульки в смітник" },
                    { "get_balls",     "Прикласти кульку" }, // "attach_balls"
                    { "get_top_down",  "Протерти зверху вниз" }, // "top_down"
                };
            case "needle":
                return new TupleList<string, string>
                {
                    { "finger_covering", "Прикрити пальцем" },
                    { "needle_removing", "Видалити голку через провідник" }
                };
            case "tweezers":
                return new TupleList<string, string>
                {
                    { "tweezers_balls", "Взяти марлеві кульки"},
                    { "remove_balls",   "Скинути марлеві кульки" },
                    { "null",           "---" },
                    { "top_down",       "Протерти зверху вниз" },
                    { "right_left",     "Протерти справа наліво" }
                };
            case "patch":
                return new TupleList<string, string>
                {
                    { "get", "Взяти" }
                };
            default:
                return new TupleList<string, string>();
        }
    }

    public override bool CheckMove(string colliderTag, out string errorMessage, out string tipMessage)
    {
        errorMessage = "";
        tipMessage = "";

        TupleList<string, string> criticalSyringeErrors = new TupleList<string, string>
        {
            { "nerves","Пошкодження нервових вузлів"},
            { "lymph", "Пошкодження лімфатичних вузлів"},
            { "bones", "Попадання в кістку"},
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

        if (!this.GenericMoveHelper(colliderTag, "medial_saphenous_vein_final_target", ref errorMessage, ref tipMessage))
            return false;

        return true;
    }

    public override int? CheckAction(string actionCode, out string errorMessage, ref string tipMessage, out bool showAnimation, string locatedColliderTag = "")
    {
        errorMessage = "";
        showAnimation = true;

        // Безопасные операции
        if (this.GetActions(actionCode)) return null;
        if (this.BallClearAction(actionCode)) return null;
        if (this.RemoveBallsAction(actionCode)) return null;
        if (this.PistonPullingAction(actionCode)) return null;
        if (this.GetSyringeAction(actionCode, ref errorMessage)) return null;
        if (actionCode == "null") return null;

        int returnedStep;

        if (this.FenceInjections(actionCode, ref errorMessage, locatedColliderTag, out returnedStep, "medial_saphenous_vein_final_target", ref showAnimation))
            return returnedStep;

        // Добавление иголки
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode.Contains("_needle"))
            SyringeHelper.TryGetNeedle(actionCode, out errorMessage, 2);

        showAnimation = false;
        return null;
    }
}

