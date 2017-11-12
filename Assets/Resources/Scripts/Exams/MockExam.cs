using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class MockExam : BaseExam
{
    public override string Name => "Тестовий сценарій";
    public override string LoadName => "1";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string> { { "testStep", "Тестовий крок" } };

    public override TupleList<string, string> ToolActions(ToolItem tool)
    {
        return new TupleList<string, string> { { "testAction", "Тестова дія" } };
    }

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string> { { "default_tool", "Будь-який інструмент" } };

    public override bool CheckMove(string colliderTag, out string errorMessage)
    {
        errorMessage = "";
        return true;
    }

    public override int? CheckAction(string actionCode, out string errorMessage, string locatedColliderTag = "")
    {
        errorMessage = "";
        return 1;
    }
}

