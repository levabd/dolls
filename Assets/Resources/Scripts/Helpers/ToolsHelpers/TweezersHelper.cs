using System.Collections.Generic;

// ReSharper disable once CheckNamespace
public static class TweezersHelper
{
    public static void GetBalls(string ballLiquid = "none")
    {
        Dictionary<string, int> liquidDict = new Dictionary<string, int>
        {
            { "spirit_p70", 2},
            { "spirit_p60", 2},
            { "spirit_p80", 2},
            { "iodine_p1", 3},
            { "iodine_p3", 3},
            { "spirit", 2},
            { "iodine", 3},
            { "none", 4}
        };

        CurrentTool.Instance.Tool.StateParams["has_balls"] = "true";
        CurrentTool.Instance.Tool.StateParams["balls_liquid"] = ballLiquid;
        CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[liquidDict[ballLiquid]];

        CurrentTool.Instance.Tool.Title = "Пинцет с шариками";
    }

    public static void RemoveBall()
    {
        CurrentTool.Instance.Tool.StateParams["has_balls"] = "false";
        CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[1];
        CurrentTool.Instance.Tool.Title = "Пинцет без ничего";
    }

    public static bool RemoveBallsAction(this BaseExam exam, string actionCode)
    {
        if (CurrentTool.Instance.Tool.CodeName == "tweezers" && actionCode == "remove_balls")
        {
            RemoveBall();
            return true;
        }

        return false;
    }
}