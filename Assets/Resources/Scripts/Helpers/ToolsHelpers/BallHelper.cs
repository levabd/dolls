using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
public static class BallHelper
{
    public static bool TryWetBall(string liquid, string targetLiquid, out string errorMessage)
    {
        Dictionary<string, int> liquidDict = new Dictionary<string, int>
        {
            { "spirit_p70", 2},
            { "spirit_p60", 2},
            { "spirit_p80", 2},
            { "iodine_p1", 3},
            { "iodine_p3", 3}
        };

        List<string> liquidList = new List<string>(liquidDict.Keys);

        errorMessage = "";

        if (CurrentTool.Instance.Tool.StateParams.ContainsKey("wet") && Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["wet"]))
        {
            errorMessage = "Марлевые шарики уже мокрые";
            return false;
        }

        if (!liquidList.Contains(liquid))
            return false;

        CurrentTool.Instance.Tool.StateParams["wet"] = "true";
        CurrentTool.Instance.Tool.StateParams["liquid"] = liquid;
        CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[liquidDict[liquid]];

        CurrentTool.Instance.Tool.Title = "Смоченные марлевые шарики";

        if (liquid != targetLiquid)
        {
            errorMessage = "Не та жидкость";
            return false;
        }

        return false;
    }

    public static bool BallClearAction(this BaseExam exam, string actionCode)
    {
        if (CurrentTool.Instance.Tool.CodeName == "gauze_balls" && actionCode == "clear")
        {
            ClearBall(exam);
            return true;
        }

        return false;
    }

    public static void ClearBall(BaseExam exam)
    {
        CurrentTool.Instance.Tool.StateParams["wet"] = "false";
        CurrentTool.Instance.Tool.StateParams.Remove("liquid");
        CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[1];
        exam.CurrentBallLiquid = "none";

        CurrentTool.Instance.Tool.Title = "Стерильные марлевые шарики";
    }
}