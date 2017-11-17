using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class BloodPressureExam1 : BaseExam
{
    public override string Name => "NIMP Вимірювання артеріального тиску (метод Короткова)";
    public override string LoadName => "BloodPressureExam1";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "palpation",                    "Пальпуємо артерію" },
        { "manometer_close_piston",       "Закрити клапан манометра" },
        { "manometer_pump_it",            "Накачати манометр" },
        { "manometer_air_out",            "Випустити повітря" }
    };

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "manometer",                      "Манометр" },
        { "phonendoscope",                  "Фонендоскоп" },
        { "hand",                           "Рука для додаткових дій" },
    };

    public override TupleList<string, string> ToolActions(ToolItem tool)
    {
        switch (tool.CodeName)
        {
            case "hand":
                return new TupleList<string, string>
                {
                    { "get_palpation",        "Пальпація" }
                };
            case "manometer":
                return new TupleList<string, string>
                {
                    { "get",                  "Одягти" },
                    { "pump_it",              "Накачати" },
                    { "air_out",              "Випустить повітря" },
                    { "close_piston",         "Закрити клапан" },
                };
            case "phonendoscope":
                return new TupleList<string, string>
                {
                    { "get",                  "Взяти" }
                };
            default:
                return new TupleList<string, string>();
        }
    }

    public override bool CheckMove(string colliderTag, out string errorMessage)
    {
        errorMessage = "";

        if (CurrentTool.Instance.Tool.CodeName == "hand" && colliderTag != "palpation_target")
        {
            errorMessage = "Пальпується і затискається не те місце";
            return false;
        }

        if (CurrentTool.Instance.Tool.CodeName == "phonendoscope" && colliderTag != "phonendoscope_target")
        {
            errorMessage = "Фонендоскоп встановлений не туди";
            return false;
        }

        return true;
    }

    public override int? CheckAction(string actionCode, out string errorMessage, out bool showAnimation, string locatedColliderTag = "")
    {
        errorMessage = "";
        showAnimation = true;

        if (actionCode == "null") return null;

        // { "palpation",                      "Пальпируем." },
        if (CurrentTool.Instance.Tool.CodeName == "hand" && actionCode == "palpation")
        {
            if (!locatedColliderTag.Contains("palpation_target"))
                errorMessage = "Пальпується не те місце";
            return 1;
        }

        // { "manometer_close_piston",       "Закрыть клапан манометра" },
        if (CurrentTool.Instance.Tool.CodeName == "manometer" && actionCode == "close_piston") return 2;

        // { "manometer_pump_it",            "Накачать манометр" },
        if (CurrentTool.Instance.Tool.CodeName == "manometer" && actionCode == "pump_it")
        {
            if (LastTakenStep() != 2)
                errorMessage = "Клапан не був закритий";
            return 3;
        }

        // { "manometer_air_out",            "Выпустить воздух" }
        if (CurrentTool.Instance.Tool.CodeName == "manometer" && actionCode == "air_out")
        {
            if (LastTakenStep() != 3)
                errorMessage = "Не було закачане повітря";
            return 4;
        }

        // Добавление иголки
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode.Contains("_needle"))
            SyringeHelper.TryGetNeedle(actionCode, out errorMessage, 2);

        showAnimation = false;
        return null;
    }
}

