using System;

class TweezersHelper
{
    public static void GetBalls(ref ToolItem tool)
    {
        if (tool.stateParams.ContainsKey("has_balls"))
            tool.stateParams["has_balls"] = "true";
        else
            tool.stateParams.Add("has_balls", "true");

        tool.title = "Пинцет с ваткой";
    }

    public static void RemoveBall(ref ToolItem tool)
    {
        if (tool.stateParams.ContainsKey("has_balls"))
            tool.stateParams["has_balls"] = "false";
        else
            tool.stateParams.Add("has_balls", "false");

        tool.title = "Пинцет без ничего";
    }
}