using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class BloodPressureExam1 : BaseExam
{
    public override string Name => "NIMP Измерение артериального давления (АД) (метод Короткова)";
    public override string LoadName => "BloodPressureExam1";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>
    {
        { "palpation",                    "Пальпируем артерию." },
        { "manometer_close_piston",       "Закрыть клапан манометра" },
        { "manometer_pump_it",            "Накачать манометр" },
        { "manometer_air_out",            "Выпустить воздух" }
    };

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>
    {
        { "manometer",                      "Манометр" },
        { "phonendoscope",                  "Фонендоскоп" },
        { "hand",                           "Рука для дополнительных действий" },
    };

    public override TupleList<string, string> ToolActions(ToolItem tool)
    {
        switch (tool.CodeName)
        {
            case "hand":
                return new TupleList<string, string>
                {
                    { "get_palpation",              "Пальпация" }
                };
            case "manometer":
                return new TupleList<string, string>
                {
                    { "get",                  "Взять" },
                    { "pump_it",              "Накачать" },
                    { "air_out",              "Выпустить воздух" },
                    { "close_piston",         "Закрыть клапан" },
                };
            case "phonendoscope":
                return new TupleList<string, string>
                {
                    { "get",              "Взять" }
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
            errorMessage = "Пальпируется и зажимается не то место";
            return false;
        }

        if (CurrentTool.Instance.Tool.CodeName == "phonendoscope" && colliderTag != "phonendoscope_target")
        {
            errorMessage = "Фонендоскоп установлен не туда";
            return false;
        }

        return true;
    }

    public override int? CheckAction(string actionCode, out string errorMessage, string locatedColliderTag = "")
    {
        errorMessage = "";

        if (actionCode == "null") return null;

        // { "palpation",                      "Пальпируем." },
        if (CurrentTool.Instance.Tool.CodeName == "hand" && actionCode == "palpation")
        {
            if (!locatedColliderTag.Contains("palpation_target"))
                errorMessage = "Пальпируется не то место";
            return 1;
        }

        // { "manometer_close_piston",       "Закрыть клапан манометра" },
        if (CurrentTool.Instance.Tool.CodeName == "manometer" && actionCode == "close_piston") return 2;

        // { "manometer_pump_it",            "Накачать манометр" },
        if (CurrentTool.Instance.Tool.CodeName == "manometer" && actionCode == "pump_it")
        {
            if (LastTakenStep() != 2)
                errorMessage = "Не был закрыт клапан.";
            return 3;
        }

        // { "manometer_air_out",            "Выпустить воздух" }
        if (CurrentTool.Instance.Tool.CodeName == "manometer" && actionCode == "air_out")
        {
            if (LastTakenStep() != 3)
                errorMessage = "Не был закачан воздух.";
            return 4;
        }

        return null;
    }
}

