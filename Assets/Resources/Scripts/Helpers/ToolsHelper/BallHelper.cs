﻿using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
public static class BallHelper
{
    public static bool TryWetBall(ref ToolItem tool, string liquid, string targetLiquid, out string errorMessage)
    {
        List<string> liquidList = new List<string>
        {
            "spirit_70",
            "spirit_60",
            "spirit_80",
            "iodine_1",
            "iodine_3"
        };

        errorMessage = "";

        if (tool.StateParams.ContainsKey("wet") && Convert.ToBoolean(tool.StateParams["wet"]))
        {
            errorMessage = "Марлевые шарики уже мокрые";
            return false;
        }

        if (!liquidList.Contains(liquid))
            return false;

        tool.StateParams.Add("wet", "true");
        tool.StateParams["liquid"] = liquid;

        tool.Title = "Смоченные марлевые шарики";

        if (liquid != targetLiquid)
        {
            errorMessage = "Не та жидкость";
            return false;
        }

        return false;
    }

    public static bool BallClearAction(this BaseExam exam, ref ToolItem tool, string actionCode)
    {
        if (tool.CodeName == "gauze_balls" && actionCode == "clear")
        {
            tool.StateParams["wet"] = "false";
            tool.StateParams.Remove("liquid");

            tool.Title = "Стерильные марлевые шарики";
            return true;
        }

        return false;
    }
}