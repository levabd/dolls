﻿using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
public static class SyringeHelper
{
    public static bool TryGetNeedle(ref ToolItem tool, string needle, out string errorMessage)
    {
        errorMessage = "";

        if (tool.StateParams.ContainsKey("has_needle") && Convert.ToBoolean(tool.StateParams["has_needle"]))
        {
            errorMessage = "Уже есть другая игла";
            return false;
        }
        
        tool.StateParams.Add("has_needle", "true");
        tool.StateParams["needle"] = needle;

        tool.Title = "Шприц с иглой";

        return true;
    }

    public static bool CheckAnestesiaNeedle(ref ToolItem tool, out string errorMessage)
    {
        if (!tool.StateParams.ContainsKey("has_needle") || !Convert.ToBoolean(tool.StateParams["has_needle"]))
        {
            errorMessage = "Отсутсвует игла";
            return false;
        }

        errorMessage = tool.StateParams.ContainsKey("needle") && tool.StateParams["needle"] == "anesthesia_needle" ? "" : "Несоответствующая игла";

        return String.IsNullOrEmpty(errorMessage);
    }

    public static bool PistonPullingAction(this BaseExam exam, ref ToolItem tool, string actionCode)
    {
        if (tool.CodeName == "syringe" && actionCode == "piston_pulling")
        {
            tool.StateParams["piston_pulling"] = "true";
            return true;
        }

        return false;
    }

    public static bool GetNeedleAction(this BaseExam exam, ref ToolItem tool, string actionCode, ref string errorMessage, string targetNeedle, int lastStep)
    {
        List<string> needleList = new List<string>
        {
            "anesthesia_needle",
            "simple_needle",
            "g22G_needle",
            "wire_needle",
            "a45_d4_punction_needle",
            "a45_d10_punction_needle",
            "a45_d7_punction_needle",
            "a45_d8_punction_needle",
            "a45_d4_d14_punction_needle"
        };

        if (tool.CodeName != "syringe" || !needleList.Contains(actionCode))
            return false;

        if (actionCode == targetNeedle)
        {
            if (exam.LastTakenStep() != lastStep)
                errorMessage = "Не та игла на текущем шаге";
            else
                TryGetNeedle(ref tool, targetNeedle, out errorMessage);
        }

        errorMessage = "Не та игла";
        return true;
    }

    public static bool HalfFillingNovocaine(this BaseExam exam, ref ToolItem tool, string actionCode, ref string errorMessage)
    {
        if (tool.CodeName == "syringe" && actionCode == "filling_novocaine_full")
        {
            errorMessage = "Слишком много новокаина";
            return true;
        }
        if (tool.CodeName == "syringe" && actionCode == "filling_nacl_half")
        {
            errorMessage = "Не та жидкость";
            return true;
        }
        if (tool.CodeName == "syringe" && actionCode == "filling_novocaine_half")
            return true;
        return false;
    }

    public static bool HalfFillingNaCl(this BaseExam exam, ref ToolItem tool, string actionCode, ref string errorMessage)
    {
        if (tool.CodeName == "syringe" && actionCode == "filling_novocaine_full")
        {
            errorMessage = "Не та жидкость";
            return true;
        }
        if (tool.CodeName == "syringe" && actionCode == "filling_nacl_half")
            return true;
        if (tool.CodeName == "syringe" && actionCode == "filling_novocaine_half")
        {
            errorMessage = "Не та жидкость";
            return true;
        }
        return false;
    }

    public static bool NeedleRemovingAction(this BaseExam exam, ref ToolItem tool, string actionCode, ref string errorMessage, string locatedColliderTag, ref DateTime needleRemovingMoment, string targetLocatedColliderTag = "", float minAngle = 0, float maxAngle = 180)
    {
        if (tool.CodeName == "syringe" && actionCode == "needle_removing")
        {
            needleRemovingMoment = DateTime.Now;

            tool.StateParams.Remove("piston_pulling");
            tool.StateParams["has_needle"] = "false";
            tool.StateParams.Remove("needle");
            tool.Title = "Шприц без иглы";

            if (locatedColliderTag == targetLocatedColliderTag)
            {
                if (maxAngle < 180)
                    if (!tool.StateParams.ContainsKey("entry_angle") || !float.Parse(tool.StateParams["entry_angle"]).CheckRange(minAngle, maxAngle))
                        errorMessage = "Неправильный угол установки";

                if (!tool.StateParams.ContainsKey("blood_inside") || !Convert.ToBoolean(tool.StateParams["blood_inside"]))
                    errorMessage = "Во время углубления не был потянут поршень на себя";
            }
                
            return true;
        }

        return false;
    }

    public static bool BloodInsideMove(this BaseExam exam, ref ToolItem tool, string colliderTag, string targetColliderTag)
    {
        if (tool.CodeName == "syringe" && colliderTag.Contains(targetColliderTag))
        {
            bool pistonPulling = false;
            if (tool.StateParams.ContainsKey("piston_pulling"))
                pistonPulling = Convert.ToBoolean(tool.StateParams["piston_pulling"]);

            if (pistonPulling)
                tool.StateParams["blood_inside"] = "true";
                // Запустить анимацию крови

            return true;
        }

        return false;
    }
}