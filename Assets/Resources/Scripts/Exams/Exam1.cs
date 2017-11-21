using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class Exam1 : BaseExam
{
    private DateTime _needleRemovingMoment;

    public override string Name => "Центральний венозний доступ №1 Підключичної підключичний доступ";
    public override string LoadName => "Exam1";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        { "spirit_balls",                   "Промокнути марлеві кульки 70% розчином спирту" },
        { "tweezers_spirit_balls",          "Взяти змочені марлеві кульки" },
        { "spirit_disinfection",            "Дезінфекція спиртом. Протерти зверху вниз" },
        { "iodine_balls",                   "Промокнути марлеві кульки 1% розчином йодоната" },
        { "tweezers_iodine_balls",          "Взяти змочені марлеві кульки" },
        { "iodine_disinfection",            "Дезінфекція йодом. Протерти зверху вниз" },
        { "wear_sterile_gloves",            "Змінити рукавички на стерильні" },
        { "anesthesia_needle",              "Взяти голку для анестезії шкіри" },
        { "anesthesia",                     "Зробити місцеву анестезію" },
        { "puncture_needle",                "Взяти голку для пункції вени" },
        { "puncture_novocaine",             "Наповнити 0,25% новокаїну на половину" },
        { "disconnect_syringe",             "Від'єднати шприц від голки" },
        { "cover_cannula",                  "Швидко прикриваємо канюлю пальцем" },
        { "wire_insertion",                 "Вставка провідника" },
        { "needle_removing",                "Видалення голки" },
        { "catheter_insertion",             "Вставка катетера по провіднику" },
        { "catheter_pushing",               "Поглиблення обертальними рухами" },
        { "wire_removing",                  "Витягнути провідник" },
        { "liquid_transfusion_connection",  "З'єднати з системою переливання рідини" },
        { "get_plaster",                    "Взяти пластир" },
        { "fixation_with_plaster",          "Фіксація пластиром" }
    };

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "gloves",                         "Рукавички" },
        { "syringe",                        "Шприц без голки" },
        { "gauze_balls",                    "Стерильні марлеві кульки" },
        { "tweezers",                       "Пінцет без нічого" },
        { "standart_catheter_conductor",    "Стандартний гнучкий провідник до катетера" },
        { "soft_catheter_conductor",        "М'який гнучкий провідник до катетера" },
        { "catheter",                       "Катетер з канюлею і заглушкою" },
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
                    { "null",                   "---" },
                    { "filling_novocaine_full", "Наповнити 0,25% новокаїну повністю" },
                    { "filling_novocaine_half", "Наповнити 0,25% новокаїну наполовину" }
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
            case "needle":
                return new TupleList<string, string>
                {
                    { "finger_covering", "Прикрити пальцем" },
                    { "needle_removing", "Видалити голку через провідник" }
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
            case "standart_catheter_conductor":
                return new TupleList<string, string>
                {
                    { "push", "Вставити провідник" },
                    { "pull", "Видалити провідник" }
                };
            case "soft_catheter_conductor":
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
                    { "null",                   "---" },
                    { "rotation_insertion",             "Поглибити обертальними рухами" },
                    { "direct_insertion",               "Поглибити прямими рухами" }
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
            { "arteria",         "Пункція артерії"},
            { "brachial_plexus", "Травма плечового нервового сплетіння"},
            { "trachea",         "Травма трахеї"},
            { "thyroid",         "Травма щитовидної залози"},
            { "lungs",           "Пневмоторакс"},
            { "nerves",          "Пошкодження нервових вузлів"},
            { "lymph",           "Пошкодження лімфатичних вузлів"},
            { "bones",           "Попадання в кістку"},
        };

        foreach (var syringeError in criticalSyringeErrors)
        {
            if (CurrentTool.Instance.Tool.CodeName == "syringe" && colliderTag.Contains(syringeError.Item1))
            {
                errorMessage = syringeError.Item2;
                return false;
            }
        }

        if (!this.GenericMoveHelper(colliderTag, "subclavian_vein_final_target", ref errorMessage, ref tipMessage))
            return false;

        this.BloodInsideMove(colliderTag, "subclavian_vein_final_target");

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

        // Перчатки + Спирт + Йод
        if (this.BiosafetySpiritIodine(actionCode, ref errorMessage, locatedColliderTag, out returnedStep, ref showAnimation)) return returnedStep;

        //{ "anesthesia_needle",              "Взять иглу для анестезии кожи." },
        if (this.GetNeedleAction(actionCode, ref errorMessage, "anesthesia_needle", 8, ref showAnimation)) return 9;

        //{ "anesthesia",                     "Сделать местную анестезию." },
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "anesthesia")
        {
            SyringeHelper.CheckAnestesiaNeedle(out errorMessage);
            return 10;
        }

        //{ "puncture_needle",                "Взяти голку для пункції вени" },
        if (this.GetNeedleAction(actionCode, ref errorMessage, "a45_d10_punction_needle", 10, ref showAnimation)) return 11;

        //{ "puncture_novocaine",             "Наповнити 0,25% новокаїну на половину" },
        if (this.HalfFillingNovocaine(actionCode, ref errorMessage)) return 12;
        if (errorMessage == "Відсутня голка") return null;

        //{ "disconnect_syringe",             "Від'єднати шприц від голки" },
        if (this.NeedleRemovingAction(actionCode, ref errorMessage, ref tipMessage, locatedColliderTag, ref _needleRemovingMoment, "subclavian_vein_final_target", 30, 40)) return 13;

        // Отсоединяем в любом другом месте
        if (this.NeedleRemovingAction(actionCode, ref errorMessage, ref tipMessage, "", ref _needleRemovingMoment)) return null;

        //{ "cover_cannula",                  "Швидко прикриваємо канюлю пальцем" },
        if (CurrentTool.Instance.Tool.CodeName == "needle" && actionCode == "finger_covering")
        {
            if ((DateTime.Now - _needleRemovingMoment).TotalSeconds > 5)
            {
                errorMessage = "Повітряна емболія";
                return null;
            }
            return 14;
        }

        // Критическая ошибка
        if (CurrentTool.Instance.Tool.CodeName == "catheter" && actionCode == "remove")
        {
            errorMessage = "Катетер був витягнутий. Катетеризація провалена";
            return null;
        }

        // Вставка проводника, удаление иглы, Катетеризация, присоединение системы, фиксация пластырем
        if (this.CateterFinalise(actionCode, ref errorMessage, locatedColliderTag, "standart_catheter_conductor", 15,
            out returnedStep, ref showAnimation)) return returnedStep;

        // Добавление иголки
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode.Contains("_needle"))
            SyringeHelper.TryGetNeedle(actionCode, out errorMessage, 2);

        showAnimation = false;
        return null;
    }
}

