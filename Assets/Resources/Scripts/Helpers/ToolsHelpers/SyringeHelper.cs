using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public static class SyringeHelper
{ 
    public static bool TryGetNeedle(string needle, out string errorMessage, int spriteIndex)
    {
        errorMessage = "";

        if (CurrentTool.Instance.Tool.StateParams.ContainsKey("has_needle") && Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["has_needle"]))
        {
            errorMessage = "Уже есть другая игла";
            return false;
        }
        
        CurrentTool.Instance.Tool.StateParams["has_needle"] = "true";
        CurrentTool.Instance.Tool.StateParams["needle"] = needle;
        CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[spriteIndex];

        CurrentTool.Instance.Tool.Title = "Шприц с иглой";

        return true;
    }

    public static bool CheckAnestesiaNeedle(out string errorMessage)
    {
        if (!CurrentTool.Instance.Tool.StateParams.ContainsKey("has_needle") || !Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["has_needle"]))
        {
            errorMessage = "Отсутсвует игла";
            return false;
        }

        errorMessage = CurrentTool.Instance.Tool.StateParams.ContainsKey("needle") && CurrentTool.Instance.Tool.StateParams["needle"] == "anesthesia_needle" ? "" : "Несоответствующая игла";

        return String.IsNullOrEmpty(errorMessage);
    }

    public static bool PistonPullingAction(this BaseExam exam, string actionCode)
    {
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "piston_pulling")
        {
            CurrentTool.Instance.Tool.StateParams["piston_pulling"] = "true";
            return true;
        }

        return false;
    }

    public static bool GetSyringeAction(this BaseExam exam, string actionCode, ref string errorMessage)
    {
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "get")
        {

            if (!CurrentTool.Instance.Tool.StateParams.ContainsKey("has_needle") ||
                !Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["has_needle"]))
                errorMessage = "Отсутсвует игла для шприца";                

            return true;
        }

        return false;
    }

    public static bool GetNeedleAction(this BaseExam exam, string actionCode, ref string errorMessage, string targetNeedle, int lastStep)
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
        errorMessage = "";

        List<string> needleList = new List<string>(needleDict.Keys);     

        if (CurrentTool.Instance.Tool.CodeName != "syringe" || !needleList.Contains(actionCode))
            return false;

        if (actionCode == targetNeedle)
        {
            
            if (exam.LastTakenStep() != lastStep)
                errorMessage = "Не та игла на текущем шаге";                
            else
                TryGetNeedle(targetNeedle, out errorMessage, needleDict[targetNeedle]);
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

    public static bool HalfFillingNovocaine(this BaseExam exam, string actionCode, ref string errorMessage)
    {
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && (!CurrentTool.Instance.Tool.StateParams.ContainsKey("has_needle") || !Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["has_needle"])))
        {
            errorMessage = "Отсутсвует игла";
            return false;
        }
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_novocaine_full")
        {
            errorMessage = "Слишком много новокаина";
            return true;
        }
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_nacl_half")
        {
            errorMessage = "Не та жидкость";
            return true;
        }
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_novocaine_half")
            return true;
        return false;
    }

    public static bool HalfFillingNaCl(this BaseExam exam, string actionCode, ref string errorMessage)
    {
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && (!CurrentTool.Instance.Tool.StateParams.ContainsKey("has_needle") || !Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["has_needle"])))
        {
            errorMessage = "Отсутсвует игла";
            return false;
        }
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_novocaine_full")
        {
            errorMessage = "Не та жидкость";
            return true;
        }
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_nacl_half")
            return true;
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_novocaine_half")
        {
            errorMessage = "Не та жидкость";
            return true;
        }
        return false;
    }

    public static bool NeedleRemovingAction(this BaseExam exam, string actionCode, ref string errorMessage, string locatedColliderTag, ref DateTime needleRemovingMoment, string targetLocatedColliderTag = "", float minAngle = 0, float maxAngle = 180)
    {
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "needle_removing")
        {
            needleRemovingMoment = DateTime.Now;

            CurrentTool.Instance.Tool.StateParams.Remove("piston_pulling");
            CurrentTool.Instance.Tool.StateParams["has_needle"] = "false";
            CurrentTool.Instance.Tool.StateParams.Remove("needle");
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[1];
            CurrentTool.Instance.Tool.Title = "Шприц без иглы";

            if (!String.IsNullOrEmpty(targetLocatedColliderTag))
            {
                if (locatedColliderTag == targetLocatedColliderTag)
                {
                    if (maxAngle < 180)
                        if (!CurrentTool.Instance.Tool.StateParams.ContainsKey("entry_angle") || !float.Parse(CurrentTool.Instance.Tool.StateParams["entry_angle"]).CheckRange(minAngle, maxAngle))
                            errorMessage = "Неправильный угол установки";
                    if (CurrentTool.Instance.Tool.StateParams.ContainsKey("blood_inside"))
                    {
                        if (!Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["blood_inside"]))
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

    public static bool BloodInsideMove(this BaseExam exam, string colliderTag, string targetColliderTag)
    {
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && colliderTag.Contains(targetColliderTag))
        {
           
            bool pistonPulling = false;
            if (CurrentTool.Instance.Tool.StateParams.ContainsKey("piston_pulling"))
                pistonPulling = Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["piston_pulling"]);
            Debug.Log(CurrentTool.Instance.Tool.StateParams["piston_pulling"]);
            if (pistonPulling)
            {
                CurrentTool.Instance.Tool.StateParams["blood_inside"] = "true";
                // Запустить анимацию крови
                Material matBlood = Resources.Load("Prefabs/Medicine_and_Health/Models/Materials/Syringe_df_blood", typeof(Material)) as Material;
                Material[] mats = GameObject.Find("SyringeElone").transform.GetChild(0).gameObject.GetComponent<Renderer>().materials;
                mats[0] = matBlood;
                GameObject.Find("SyringeElone").transform.GetChild(0).gameObject.GetComponent<Renderer>().materials = mats;
            }
            return true;
        }

        return false;
    }
}