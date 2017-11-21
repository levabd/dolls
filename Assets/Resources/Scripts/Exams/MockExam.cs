using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class MockExam : BaseExam
{
    public override DateTime NeedleRemovingMoment { get; set; }

    public override string Name => "Тестовий сценарій";
    public override string LoadName => "1";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string> { { "testStep", "Тестовий крок" } };

    public override TupleList<string, string> ToolActions(ToolItem tool)
    {
        return new TupleList<string, string> { { "testAction", "Тестова дія" } };
    }

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string> { { "default_tool", "Будь-який інструмент" } };

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
        return 1;
    }
}

