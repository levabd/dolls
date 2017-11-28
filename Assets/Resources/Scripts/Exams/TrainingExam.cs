using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class TrainingExam : BaseExam
{
    public override DateTime NeedleRemovingMoment { get; set; }

    public override string Name => "Тестовий сценарій";
    public override string LoadName => "TrainingExam";
    public override string HelpString => "";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        { "spirit_balls",                   "Промокнути марлеві кульки 70% розчином спирту" },
        { "tweezers_spirit_balls",          "Взяти змочені марлеві кульки" },
        { "spirit_disinfection",            "Дезінфекція спиртом. Протерти зверху вниз" },
        { "wear_sterile_gloves",            "Змінити рукавички на стерильні" },
        { "anesthesia_needle",              "Взяти голку для анестезії шкіри" },
        { "anesthesia",                     "Зробити місцеву анестезію" },
        { "puncture_needle",                "Взяти голку для пункції вени" },
        { "puncture_novocaine",             "Наповнити 0,25% новокаїну на половину" },
        { "disconnect_syringe",             "Від'єднати шприц від голки" }

    };


    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "gloves",                         "Рукавички" },
        { "syringe",                        "Шприц без голки" },
        { "gauze_balls",                    "Стерильні марлеві кульки" },
        { "tweezers",                       "Пінцет без нічого" },
        { "hand",                           "Рука для додаткових дій" },
        { "standart_catheter_conductor",    "Стандартний гнучкий провідник до катетера" },
        { "catheter",                       "Катетер з канюлею і заглушкою" },
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
            case "syringe":
                return new TupleList<string, string>
                {
                    { "needle_removing",        "Від'єднати від голки" },
                    { "anesthesia",             "Зробити місцеву анестезію" },
                    { "piston_pulling",         "Потягування поршня на себе" },
                    { "null",                   "---" },
                    { "anesthesia_needle",      "Взяти голку для анестезії шкіри і наповнити шприц анестетиком" },
                    { "g22G_needle",            "Взяти голку для спинномозкової анестезії 22G і наповнити шприц анестетиком" },
                    { "a45_d10_punction_needle",  "Взяти голку для пункції вени довжиною 10 см з внутрішнім просвітом каналу 1,7 мм і зрізом вістря голки під кутом 45°" },
                    { "null",                   "---" },
                    { "filling_novocaine_full", "Наповнити 0,25% новокаїну повністю" },
                    { "filling_novocaine_half", "Наповнити 0,25% новокаїну наполовину" },
                    { "filling_nacl_half",      "Наповнити 0,9% розчином натрію хлориду наполовину"}
                };
            case "gauze_balls":
                return new TupleList<string, string>
                {
                    { "clear",       "Взяти нову стерильну кульку" },
                    { "spirit_p70",  "Промокнути в 70% розчині спирту" },
                    { "spirit_p60",  "Промокнути в 60% розчині спирту" },
                    { "spirit_p80",  "Промокнути в 80% розчині спирту" },
                    { "iodine_p1",   "Промокнути в 1% розчині йодоната" },
                    { "iodine_p3",   "Промокнути в 3% розчині йодоната" }
                };
            case "tweezers":
                return new TupleList<string, string>
                {
                    { "tweezers_balls", "Взяти кульки і вибрати місце дезінфекції на тілі" },
                    { "remove_balls",   "Скинути марлеві кульки" },
                    { "null",           "---" },
                    { "top_down",       "Протерти зверху вниз" },
                    { "right_left",     "Протерти справа наліво" }
                };
            case "hand":
                return new TupleList<string, string>
                {
                    { "get_palpation",        "Пальпація" },// "palpation"
                    { "get_stretch_the_skin", "Натягнути шкіру і зафіксувати вену" }, // "stretch_the_skin"
                    { "get_clamp",            "Затиснути вену" }, //clamp
                    { "clamp_out",            "Відпустити вену"},
                };
            case "standart_catheter_conductor":
                return new TupleList<string, string>
                {
                    { "push", "Вставити провідник" },
                    { "pull", "Видалити провідник" }
                };
            case "catheter":
                return new TupleList<string, string>
                {
                    { "push",                           "Вставити катетер по провіднику" },
                    { "remove",                         "Видалити катетер" },
                    { "liquid_transfusion_connection",  "З'єднати з системою переливання рідин" },
                    { "rotation_insertion",             "Поглибити обертальними рухами" },
                    { "direct_insertion",               "Поглибити прямими рухами" }
                };
            case "venflon":
                return new TupleList<string, string>
                {
                    { "get",                            "Взяти" },
                    { "remove",                         "Видалити катетер" },
                    { "liquid_transfusion_connection",  "З'єднати з системою переливання рідин" },
                    { "remove_mandren",                 "Витягнути мадрен" },
                    { "pull_mandren",                   "Потягнути мадрен" }
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

    public override bool CheckMove(string colliderTag, out string errorMessage, out string tipMessage)
    {
        errorMessage = "";
        tipMessage = "";
        return true;
    }

    public override int? CheckAction(string actionCode, out string errorMessage, ref string tipMessage, out bool showAnimation, string locatedColliderTag = "")
    {
        errorMessage = "";
        showAnimation = true;
        if (CurrentTool.Instance.Tool.CodeName == "gloves" && actionCode == "wear_examination")
        {
            return 1;
        }
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "anesthesia_needle")
        {
            return 2;
        }
        if (CurrentTool.Instance.Tool.CodeName == "big" && actionCode == "prepare")
        {
            return 3;
        }
        if (CurrentTool.Instance.Tool.CodeName == "big" && actionCode == "prepare")
        {
            return 4;
        }
        if (CurrentTool.Instance.Tool.CodeName == "big" && actionCode == "prepare")
        {
            return 4;
        }
        if (CurrentTool.Instance.Tool.CodeName == "big" && actionCode == "prepare")
        {
            return 4;
        }
        if (CurrentTool.Instance.Tool.CodeName == "big" && actionCode == "prepare")
        {
            return 4;
        }
        if (CurrentTool.Instance.Tool.CodeName == "big" && actionCode == "prepare")
        {
            return 4;
        }
        if (CurrentTool.Instance.Tool.CodeName == "big" && actionCode == "prepare")
        {
            return 4;
        }
        return null;
    }
}

