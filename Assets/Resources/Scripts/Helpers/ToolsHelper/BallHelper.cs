using System;

// ReSharper disable once CheckNamespace
public static class BallHelper
{
    public static bool TryWetBall(ref ToolItem tool, string liquid, out string errorMessage)
    {
        errorMessage = "";

        if (tool.StateParams.ContainsKey("wet") && Convert.ToBoolean(tool.StateParams["wet"]))
        {
            errorMessage = "Марлевые шарики уже мокрые";
            return false;
        }

        tool.StateParams.Add("wet", "true");
        tool.StateParams["liquid"] = liquid;

        tool.Title = "Смоченные марлевые шарики";

        return true;
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