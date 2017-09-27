using System;

class BallHelper
{
    public static bool TryWetBall(ref ToolItem tool, string liquid, out string errorMessage)
    {
        errorMessage = "";

        bool wet = false;
        string value = "";
        if (tool.stateParams.TryGetValue("wet", out value))
            wet = Convert.ToBoolean(value);

        if (wet)
        {
            errorMessage = "Марлевые шарики уже мокрые";
            return false;
        }
        else
        {
            tool.stateParams.Add("wet", "true");
            if (tool.stateParams.ContainsKey("liquid"))
                tool.stateParams["liquid"] = liquid;
            else
                tool.stateParams.Add("liquid", liquid);

            tool.title = "Смоченные марлевые шарики";
        }

        return true;
    }

    public static void ClearBall(ref ToolItem tool)
    {
        if (tool.stateParams.ContainsKey("wet"))
            tool.stateParams["wet"] = "false";
        else
            tool.stateParams.Add("wet", "false");

        if (tool.stateParams.ContainsKey("liquid"))
            tool.stateParams.Remove("liquid");

        tool.title = "Стерильные марлевые шарики";
    }
}