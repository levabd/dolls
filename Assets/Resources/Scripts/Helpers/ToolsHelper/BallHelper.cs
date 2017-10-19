﻿using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
public static class BallHelper
{
    public static bool TryWetBall(ref ToolItem tool, string liquid, string targetLiquid, out string errorMessage)
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

        if (tool.StateParams.ContainsKey("wet") && Convert.ToBoolean(tool.StateParams["wet"]))
        {
            errorMessage = "Марлевые шарики уже мокрые";
            return false;
        }



        if (!liquidList.Contains(liquid))
            return false;

        tool.StateParams["wet"] = "true";
        tool.StateParams["liquid"] = liquid;
        tool.Sprites[0] = tool.Sprites[liquidDict[liquid]];

        tool.Title = "Смоченные марлевые шарики";

        if (liquid != targetLiquid)
        {
            errorMessage = "Не та жидкость";
            return false;
        }

        return false;
    }

    public static bool BallClearAction(this BaseExam exam, ref ToolItem tool, string actionCode, ref string currentBallLiquid)
    {
        if (tool.CodeName == "gauze_balls" && actionCode == "clear")
        {
            tool.StateParams["wet"] = "false";
            tool.StateParams.Remove("liquid");
            tool.Sprites[0] = tool.Sprites[1];
            currentBallLiquid = "none";

            tool.Title = "Стерильные марлевые шарики";
            return true;
        }

        return false;
    }
}