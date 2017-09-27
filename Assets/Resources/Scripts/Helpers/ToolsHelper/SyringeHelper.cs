using System;

class SyringeHelper
{
    public static bool TryGetNeedle(ref ToolItem tool, string needle, out string errorMessage)
    {
        errorMessage = "";

        bool hasNeedle = false;
        string value = "";
        if (tool.stateParams.TryGetValue("has_needle", out value))
            hasNeedle = Convert.ToBoolean(value);

        if (hasNeedle)
        {
            errorMessage = "Уже есть другая игла";
            return false;
        }
        else
        {
            tool.stateParams.Add("has_needle", "true");
            if (tool.stateParams.ContainsKey("needle"))
                tool.stateParams["needle"] = needle;
            else
                tool.stateParams.Add("needle", needle);

            tool.title = "Шприц с иглой";
        }

        return true;
    }

    public static bool CheckAnestesiaNeedle(ref ToolItem tool, out string errorMessage)
    {
        errorMessage = "";

        bool hasNeedle = false;
        string value = "";
        if (tool.stateParams.TryGetValue("has_needle", out value))
            hasNeedle = Convert.ToBoolean(value);

        if (!hasNeedle)
        {
            errorMessage = "Отсутсвует игла";
            return false;
        }

        if (tool.stateParams.ContainsKey("needle"))
            if (tool.stateParams["needle"] == "anesthesia_needle")
                return true;

        errorMessage = "Несоответствующая игла";
        return false;
    }

    public static void RemoveNeedle(ref ToolItem tool)
    {
        if (tool.stateParams.ContainsKey("has_needle"))
            tool.stateParams["has_needle"] = "false";
        else
            tool.stateParams.Add("has_needle", "false");

        if (tool.stateParams.ContainsKey("needle"))
            tool.stateParams.Remove("needle");

        tool.title = "Шприц без иглы";
    }
}