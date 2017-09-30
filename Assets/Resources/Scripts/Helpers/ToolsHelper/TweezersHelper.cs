
// ReSharper disable once CheckNamespace
public static class TweezersHelper
{
    public static void GetBalls(ref ToolItem tool)
    {
        tool.StateParams["has_balls"] = "true";

        tool.Title = "Пинцет с ваткой";
    }

    public static void RemoveBall(ref ToolItem tool)
    {
        tool.StateParams["has_balls"] = "false";

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