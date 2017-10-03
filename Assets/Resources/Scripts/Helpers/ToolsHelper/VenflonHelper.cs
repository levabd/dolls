using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
public static class VenflonHelper
{

    public static bool BloodInsidePavilion(this BaseExam exam, ref ToolItem tool, string colliderTag, string targetColliderTag)
    {
        if (tool.CodeName == "venflon" && colliderTag.Contains(targetColliderTag))
        {
            tool.StateParams["blood_inside"] = "true";
            // Запустить анимацию крови

            return true;
        }

        return false;
    }
}