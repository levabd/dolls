﻿using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class Exam16 : BaseExam
{
    public override DateTime NeedleRemovingMoment { get; set; }

    public override string Name => "Периферичний венозний доступ №15 Постановка внутрішньовенного катетера venflon в дорсальні зап'ястні вени";
    public override string LoadName => "Exam16";
    public override string HelpString => "Ми працюємо з правою рукою";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        { "tourniquet",                     "Взяти джгут і накласти" },
        { "palpation",                      "Пальпуємо вену" },
        { "spirit_balls",                   "Промокнути марлеві кульки 70% розчином спирту" },
        { "balls_spirit_disinfection",      "Дезінфекція спиртом. Обробити операційне поле" },
        { "throw_balls",                    "Викинути кульки" },
        { "wear_sterile_gloves",            "Змінити рукавички на стерильні" },
        { "stretch_the_skin",               "Натягнути шкіру" },
        { "pull_mandren",                   "Потягнути мадрен" },
        { "remove_tourniquet",              "Зняти джгут" },
        { "clamp_the_vein",                 "Перетиснути вену" },
        { "remove_mandren",                 "Витягнути мадрен" },
        { "liquid_transfusion_connection",  "З'єднати з системою переливання рідини" },
        { "get_plaster",                    "Взяти пластир" },
        { "fixation_with_plaster",          "Фіксація пластиром" },
    };

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "gloves",                         "Рукавички" },
        { "hand",                           "Рука для додаткових дій" },
        { "gauze_balls",                    "Стерильні марлеві кульки" },
//      { "tweezers",                       "Пінцет без нічого" },
        { "tourniquet",                     "Джгут" },
        { "venflon",                        "Катетер Venflon"},
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
                    { "get",    "Взяти джгут і вибрати місце ⊕" },
                    { "lay",    "Накласти джгут" },
                    { "remove", "Зняти джгут" }
                };
            case "hand":
                return new TupleList<string, string>
                {
                    { "get_palpation",              "Пальпація ⊕" },// "palpation"
                    { "get_stretch_the_skin",       "Натягнути шкіру на тілі⊕" }, // "stretch_the_skin"
                    { "get_clamp",                  "Затиснути вену на тілі⊕" }, //clamp
                    { "clamp_out",                  "Відпустити вену"},
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
                    { "get_balls",     "Прикласти кульку ⊕" }, // "attach_balls"
                    { "get_top_down",  "Обробити операційне поле ⊕" }, // "top_down"
                };
       //   case "tweezers":
       //         return new TupleList<string, string>
       //        {
       //            { "tweezers_balls", "Взяти марлеві кульки ⊕"},
       //            { "remove_balls",   "Скинути марлеві кульки" },
       //             { "top_down",       "Обробити операційне поле" }
       //         };
            case "venflon":
                return new TupleList<string, string>
                {
                    { "get",                            "Взяти і вибрати місце ⊕" },
                    { "remove",                         "Видалити катетер" },
                    { "liquid_transfusion_connection",  "З'єднати з системою переливання рідин" },
                    { "remove_mandren",                 "Витягнути мадрен" },
                    { "pull_mandren",                   "Потягнути мадрен" }
                };
            case "patch":
                return new TupleList<string, string>
                {
                    { "get", "Взяти і накласти ⊕" }
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
            if (CurrentTool.Instance.Tool.CodeName == "venflon" && colliderTag.Contains(syringeError.Item1))
            {
                errorMessage = syringeError.Item2;
                return false;
            }
        }

        if (CurrentTool.Instance.Tool.CodeName == "venflon" && colliderTag == "dorsal_metacarpal_vein_final_target")
            NeedleInsideTarget = true;

        if (!this.GenericMoveHelper(colliderTag, "dorsal_metacarpal_vein_final_target", ref errorMessage, ref tipMessage))
            return false;

        this.BloodInsidePavilion(colliderTag, "dorsal_metacarpal_vein_final_target");

        return true;
    }

    public override int? CheckAction(string actionCode, out string errorMessage, ref string tipMessage, out bool showAnimation, string locatedColliderTag = "")
    {
        errorMessage = "";
        showAnimation = true;

        // Безопасные операции
        if (this.GetActions(actionCode)) return null;
        if (this.BallClearAction(actionCode)) return null;
        if (this.GetSyringeAction(actionCode, ref errorMessage)) return null;
        if (this.RemoveBallsAction(actionCode)) return null;
        if (actionCode == "null") return null;

        int returnedStep;


		if (this.VenflonInstallation(actionCode, ref errorMessage, locatedColliderTag, out returnedStep, "dorsal_metacarpal_vein_final_target", ref showAnimation))

            return returnedStep;

        // Критическая ошибка
        if (CurrentTool.Instance.Tool.CodeName == "venflon" && actionCode == "remove")
        {
            errorMessage = "Катетер був витягнутий. Катетеризація провалена";
            return null;
        }

        // Добавление иголки
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode.Contains("_needle"))
            SyringeHelper.TryGetNeedle(actionCode, out errorMessage, 2);

        showAnimation = false;
        return null;
    }
}

