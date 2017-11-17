﻿using System.Collections.Generic;

// ReSharper disable once CheckNamespace
class EyeRetinaExam : BaseExam
{
    public override string Name => "Відшарування сітківки";
    public override string LoadName => "EyeRetinaExam";

    public override TupleList<string, string> CorrectSteps => new TupleList<string, string>();

    public override TupleList<string, string> ToolActions(ToolItem tool)
    {
        return new TupleList<string, string>();
    }

    public override Dictionary<string, string> InventoryTool => new Dictionary<string, string>();

    public override bool CheckMove(string colliderTag, out string errorMessage)
    {
        errorMessage = "";
        return true;
    }

    public override int? CheckAction(string actionCode, out string errorMessage, out bool showAnimation, string locatedColliderTag = "")
    {
        errorMessage = "";
        showAnimation = true;
        return 1;
    }
}

