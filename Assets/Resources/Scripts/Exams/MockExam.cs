using System.Collections.Generic;

class MockExam: BaseExam
{
    public override string Name
    {
        get { return "Тестовый сценарий";  }
    }
    
    public override TupleList<string, string> CorrectSteps
    {
        get { return new TupleList<string, string> { { "testStep", "Тестовый шаг" } }; }
    }

    public override TupleList<string, string> ToolActions(ToolItem tool)
    {
        return new TupleList<string, string> { { "testAction", "Тестовое действие" } };
    }

    public override Dictionary<string, string> InventoryTool
    {
        get { return new Dictionary<string, string> { { "default_tool", "Любой инструмент" } }; }
    }

    public override bool CheckMove(ref ToolItem tool, string colliderTag, out string errorMessage)
    {
        errorMessage = "";
        return true;
    }

    public override int? CheckAction(ref ToolItem tool, string actionCode, out string errorMessage, string locatedColliderTag = null)
    {
        errorMessage = "";
        return 1;
    }
}

