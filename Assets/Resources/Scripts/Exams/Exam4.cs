using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class Exam4 : BaseExam
{
    public override DateTime NeedleRemovingMoment { get; set; }

    public override string Name => "Периферичний венозний доступ №3 Пахвова вена";
    public override string LoadName => "Exam4";
    public override string HelpString => "Ми працюємо з правою частиною тіла";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "wear_examination_gloves",        "Одягти оглядові рукавички" },
        { "spirit_balls",                   "Промокнути марлеві кульки 70% розчином спирту" },
        { "tweezers_spirit_balls",          "Взяти змочені марлеві кульки" },
        { "spirit_disinfection",            "Дезінфекція спиртом. Обробити операційне поле" },
        { "iodine_balls",                   "Промокнути марлеві кульки 1% розчином йодоната" },
        { "tweezers_iodine_balls",          "Взяти змочені марлеві кульки" },
        { "iodine_disinfection",            "Дезінфекція йодом. Обробити операційне поле" },
        { "wear_sterile_gloves",            "Змінити рукавички на стерильні" },
        { "palpation",                      "Пальпація пахвової артерії" },
        { "tourniquet",                     "Взяти джгут і накласти на артерію" },
        { "puncture_needle",                "Взяти голку для пункції вени" },
        { "puncture_novocaine",             "Наповнити 0,25% новокаїну на половину" },
        { "remove_tourniquet",              "Зняти джгут" },
        { "disconnect_syringe",             "Від'єднати шприц від голки" },
        { "cover_cannula",                  "Швидко прикрити канюлю пальцем" },
        { "wire_insertion",                 "Вставка провідника" },
        { "needle_removing",                "Видалення голки" },
        { "catheter_insertion",             "Вставка катетера по провіднику" },
        { "catheter_pushing",               "Поглиблення обертальними рухами" },
        { "wire_removing",                  "Витягання провідника" },
        { "liquid_transfusion_connection",  "З'єднання з системою переливання рідини" },
        { "get_plaster",                    "Взяти пластир" },
        { "fixation_with_plaster",          "Фіксація пластиром" }
    };

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "gloves",                         "Рукавички" },
        { "syringe",                        "Шприц без голки" },
        { "gauze_balls",                    "Стерильні марлеві кульки" },
        { "tweezers",                       "Пінцет без нічого" },
        { "tourniquet",                     "Джгут" },
        { "hand",                           "Кисть для пальпації" },
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
            case "tourniquet":
                return new TupleList<string, string>
                {
                    { "get",    "Взяти і вибрати місце ⊕" },
                    { "lay",    "Накласти джгут" },
                    { "remove", "Зняти джгут " }
                };
            case "hand":
                return new TupleList<string, string>
                {
                    { "get_palpation", "Пальпація ⊕" },// "palpation"
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
                    { "filling_novocaine_full", "Наповнити 0,25% новокаїну повністю ⊕" },
                    { "filling_novocaine_half", "Наповнити 0,25% новокаїну наполовину ⊕" }
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
                    { "tweezers_balls", "Взяти кульку і вибрати місце дезінфекції на тілі ⊕" },
                    { "remove_balls",   "Скинути марлеві кульки" },
                    { "top_down",       "Обробити операційне поле" }                   
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
                    { "rotation_insertion",             "Поглибити обертальними рухами" },
                    { "direct_insertion",               "Поглибити прямими рухами" }
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
            { "axillary_artery", "Пункція пахвовій артерії. Не відступили 1 см від місця пальпації"},
            { "brachial_plexus", "Травма плечового нервового сплетіння"},
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

        if (!this.GenericMoveHelper(colliderTag, "axillary_vien_final_target", ref errorMessage, ref tipMessage))
            return false;

        this.BloodInsideMove(colliderTag, "axillary_vien_final_target");

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

        //{ "palpation",                      "Пальпируем подмышечную артерию." },
        if (CurrentTool.Instance.Tool.CodeName == "hand" && actionCode == "palpation")
        {
            if (!locatedColliderTag.Contains("palpation_target"))
            {
                showAnimation = false;
                errorMessage = "Пальпується не те місце";
            }
            return 9;
        }

        //{ "tourniquet",                     "Взяти джгут і накласти на артерию." },
        if (CurrentTool.Instance.Tool.CodeName == "tourniquet" && actionCode == "lay")
        {
            if (!locatedColliderTag.Contains("tourniquet_target"))
            {
                showAnimation = false;
                errorMessage = "Не туди накладено джгут";
            }
            return 10;
        }

        //{ "puncture_needle",                "Взяти голку для пункції вени" },
        if (this.GetNeedleAction(actionCode, ref errorMessage, "a45_d4_punction_needle", 10, ref showAnimation)) return 11;
        
        //{ "puncture_novocaine",             "Наповнити 0,25% новокаїну на половину" },
        if (this.HalfFillingNovocaine(actionCode, ref errorMessage)) return 12;

        if (errorMessage == "Відсутня голка") return null;

        //{ "tourniquet",                     "Зняти джгут" },
        if (CurrentTool.Instance.Tool.CodeName == "tourniquet" && actionCode == "remove") return 13;

        //{ "disconnect_syringe",             "Від'єднати шприц від голки" },
        if (this.NeedleRemovingAction(actionCode, ref errorMessage, ref tipMessage, locatedColliderTag, "axillary_vien_final_target", 28, 32)) return 14;

        // Отсоединяем в любом другом месте
        if (this.NeedleRemovingAction(actionCode, ref errorMessage, ref tipMessage, "")) return null;

        //{ "cover_cannula",                  "Швидко прикриваємо канюлю пальцем" },
        if (CurrentTool.Instance.Tool.CodeName == "needle" && actionCode == "finger_covering")
        {
            if ((DateTime.Now - NeedleRemovingMoment).TotalSeconds > 5)
            {
                errorMessage = "Повітряна емболія";
                return null;
            }
            return 15;
        }

        // Критическая ошибка
        if (CurrentTool.Instance.Tool.CodeName == "catheter" && actionCode == "remove")
        {
            errorMessage = "Катетер був витягнутий. Катетеризація провалена";
            return null;
        }

        // Вставка проводника, удаление иглы, Катетеризация, присоединение системы, фиксация пластырем
        if (this.CateterFinalise(actionCode, ref errorMessage, locatedColliderTag, "standart_catheter_conductor", 16, out returnedStep, ref showAnimation)) return returnedStep;

        // Добавление иголки
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode.Contains("_needle"))
            SyringeHelper.TryGetNeedle(actionCode, out errorMessage, 2);
        showAnimation = false;

        return null;
    }
}

