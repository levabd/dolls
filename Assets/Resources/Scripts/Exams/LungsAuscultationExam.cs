using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class LungsAuscultationExam : BaseExam
{
    public override string Name => "Тренажер для аускультації легень";
    public override string LoadName => "AuscultationLungsExam";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>();

    public override TupleList<string, string> ToolActions(ToolItem tool)
    {
        return new TupleList<string, string> ();
    }

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string> ();

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

