using System.Collections.Generic;

// ReSharper disable once CheckNamespace
public static class TweezersHelper
{
    public static void GetBalls(ref ToolItem tool, string ballLiquid = "none")
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

        tool.StateParams["has_balls"] = "true";
        tool.StateParams["balls_liquid"] = ballLiquid;
        tool.Sprites[0] = tool.Sprites[liquidDict[ballLiquid]];

        tool.Title = "Пинцет с шариками";
    }

    public static void RemoveBall(ref ToolItem tool)
    {
        tool.StateParams["has_balls"] = "false";
        tool.Sprites[0] = tool.Sprites[1];
        tool.Title = "Пинцет без ничего";
    }

    public static bool RemoveBallsAction(this BaseExam exam, ref ToolItem tool, string actionCode)
    {
        if (tool.CodeName == "tweezers" && actionCode == "remove_balls")
        {
            RemoveBall(ref tool);
            return true;
        }

        return false;
    }
}