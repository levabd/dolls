using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
public static class SyringeHelper
{
    public static bool TryGetNeedle(ref ToolItem tool, string needle, out string errorMessage, int spriteIndex)
    {
        errorMessage = "";

        if (tool.StateParams.ContainsKey("has_needle") && Convert.ToBoolean(tool.StateParams["has_needle"]))
        {
            errorMessage = "Уже есть другая игла";
            return false;
        }
        
        tool.StateParams["has_needle"] = "true";
        tool.StateParams["needle"] = needle;
        tool.Sprites[0] = tool.Sprites[spriteIndex];

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
        Dictionary<string, int> needleDict = new Dictionary<string, int>
        {
            { "anesthesia_needle", 3},
            { "simple_needle", 2},
            { "g22G_needle", 3},
            { "wire_needle", 3},
            { "a45_d4_punction_needle", 2},
            { "a45_d10_punction_needle", 2},
            { "a45_d7_punction_needle", 2},
            { "a45_d8_punction_needle", 2},
            { "a45_d4_d14_punction_needle", 2}
        };

        List<string> needleList = new List<string>(needleDict.Keys);

        errorMessage = "";
        

        if (tool.CodeName != "syringe" || !needleList.Contains(actionCode))
            return false;

        if (actionCode == targetNeedle)
        {
            
            if (exam.LastTakenStep() != lastStep)
                errorMessage = "Не та игла на текущем шаге";                
            else
                TryGetNeedle(ref tool, targetNeedle, out errorMessage, needleDict[targetNeedle]);
        }
        else
        {
            if (exam.LastTakenStep() == lastStep)
                errorMessage = "Не та игла";
            else
                return false;
        }

        return true;
    }

    public static bool HalfFillingNovocaine(this BaseExam exam, ref ToolItem tool, string actionCode, ref string errorMessage)
    {
        if (tool.CodeName == "syringe" && (!tool.StateParams.ContainsKey("has_needle") || !Convert.ToBoolean(tool.StateParams["has_needle"])))
        {
            errorMessage = "Отсутсвует игла";
            return false;
        }
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
        if (tool.CodeName == "syringe" && (!tool.StateParams.ContainsKey("has_needle") || !Convert.ToBoolean(tool.StateParams["has_needle"])))
        {
            errorMessage = "Отсутсвует игла";
            return false;
        }
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
            tool.Sprites[0] = tool.Sprites[1];
            tool.Title = "Шприц без иглы";

            if (!String.IsNullOrEmpty(targetLocatedColliderTag))
            {
                if (locatedColliderTag == targetLocatedColliderTag)
                {
                    if (maxAngle < 180)
                        if (!tool.StateParams.ContainsKey("entry_angle") || !float.Parse(tool.StateParams["entry_angle"]).CheckRange(minAngle, maxAngle))
                            errorMessage = "Неправильный угол установки";
                    if (tool.StateParams.ContainsKey("blood_inside"))
                    {
                        if (!Convert.ToBoolean(tool.StateParams["blood_inside"]))
                            errorMessage = "Во время углубления не был потянут поршень на себя";
                    }
                    else
                        errorMessage = "Во время углубления не был потянут поршень на себя";
                }
                else
                    return false;
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