using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class TrainingExam : BaseExam
{
    public override DateTime NeedleRemovingMoment { get; set; }

    public override string Name => "Тестовий сценарій";
    public override string LoadName => "TrainingExam";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        { "wear_examination_gloves",        "Одягти оглядові рукавички" }

    };


    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "gloves",                         "Рукавички" },
        { "syringe",                        "Шприц без голки" },
        { "gauze_balls",                    "Стерильні марлеві кульки" },
        { "tweezers",                       "Пінцет без нічого" },
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
                    { "wire_needle",            "Взяти голку для провідникової анестезії та наповнити шприц анестетиком" },
                    { "a45_d4_punction_needle", "Взяти голку для пункції вени довжиною не менше 4 см з внутрішнім просвітом каналу 1,0-1,4 мм і зрізом вістря голки під кутом 40-45°" },
                    { "filling_nacl",           "Наповнити 10-20 мл фізрозчином" },
                };
            case "gauze_balls":
                return new TupleList<string, string>
                {                    
                    { "clear",       "Взяти нову стерильну кульку" },
                    { "spirit_p80",  "Промокнути в 80% розчині спирту" },
                    { "iodine_p1",   "Промокнути в 1% розчині йодоната" },
                    { "null",        "---" },
                    { "throw_balls", "Викинути кульки в смітник" },
                    { "get_balls",   "Прикласти кульку" }, // "attach_balls"
                    { "get_top_down","Протерти зверху вниз" }, // "top_down"
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

