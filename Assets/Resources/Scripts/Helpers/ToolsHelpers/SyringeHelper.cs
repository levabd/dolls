using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public static class SyringeHelper
{ 
    public static bool TryGetNeedle(string needle, out string errorMessage, int spriteIndex)
    {
        errorMessage = "";

        Debug.Log("ExamToolName: " + CurrentTool.Instance.Tool.CodeName);

        if (CurrentTool.Instance.Tool.StateParams.ContainsKey("has_needle") && Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["has_needle"]))
        {
            errorMessage = "Вже приєднана інша голка";
            return false;
        }
        
        CurrentTool.Instance.Tool.StateParams["has_needle"] = "true";
        CurrentTool.Instance.Tool.StateParams["needle"] = needle;
        CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[spriteIndex];

        CurrentTool.Instance.Tool.Title = "Шприц з голкою";

        return true;
    }

    // ReSharper disable once RedundantAssignment
    public static bool CheckAnestesiaNeedle(out string errorMessage, ref bool showAnimation)
    {
        if (!CurrentTool.Instance.Tool.StateParams.ContainsKey("has_needle") || !Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["has_needle"]))
        {
            errorMessage = "Відсутня голка";
            showAnimation = false;
            return false;
        }

        errorMessage = CurrentTool.Instance.Tool.StateParams.ContainsKey("needle") && CurrentTool.Instance.Tool.StateParams["needle"] == "anesthesia_needle" ? "" : "Невідповідна голка";

        showAnimation = String.IsNullOrEmpty(errorMessage);
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
                errorMessage = "Відсутня голка для шприца";                

            return true;
        }

        return false;
    }

    // ReSharper disable once RedundantAssignment
    public static bool GetNeedleAction(this BaseExam exam, string actionCode, ref string errorMessage, string targetNeedle, int lastStep, ref bool showAnimation)
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
            {
                errorMessage = "Не та голка на поточному кроці";
                showAnimation = false;
            }
            else
                TryGetNeedle(targetNeedle, out errorMessage, needleDict[targetNeedle]);
        }
        else
        {
            if (exam.LastTakenStep() == lastStep)
                errorMessage = "Не та голка";
            else
                return false;
        }

        return true;
    }

    public static bool HalfFillingNovocaine(this BaseExam exam, string actionCode, ref string errorMessage)
    {
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode.Contains("filling_") &&
            (!CurrentTool.Instance.Tool.StateParams.ContainsKey("has_needle") || !Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["has_needle"])))
        {
            errorMessage = "Відсутня голка";
            return false;
        }
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_novocaine_full")
        {
            errorMessage = "Занадто багато новокаїну";
            return true;
        }
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_nacl_half")
        {
            errorMessage = "Не та рідина";
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
            errorMessage = "Відсутня голка";
            return false;
        }
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_novocaine_full")
        {
            errorMessage = "Не та рідина";
            return true;
        }
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_nacl_half")
            return true;
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "filling_novocaine_half")
        {
            errorMessage = "Не та рідина";
            return true;
        }
        return false;
    }

    public static bool NeedleRemovingAction(this BaseExam exam, string actionCode, ref string errorMessage, ref string tipMessage, string locatedColliderTag, string targetLocatedColliderTag = "", float minAngle = 0, float maxAngle = 180)
    {
        if (CurrentTool.Instance.Tool.CodeName == "syringe" && actionCode == "needle_removing")
        {
            CurrentTool.Instance.Tool.StateParams.Remove("piston_pulling");
            CurrentTool.Instance.Tool.StateParams["has_needle"] = "false";
            CurrentTool.Instance.Tool.StateParams.Remove("needle");
            CurrentTool.Instance.Tool.Sprites[0] = CurrentTool.Instance.Tool.Sprites[1];
            CurrentTool.Instance.Tool.Title = "Шприц без голки";

            if (!String.IsNullOrEmpty(targetLocatedColliderTag) && !String.IsNullOrEmpty(locatedColliderTag))
            {
                if (locatedColliderTag == targetLocatedColliderTag)
                {
                    exam.NeedleRemovingMoment = DateTime.Now;

                    tipMessage = "До від'єднання голки кінцева точка пункції була досягнута";
                    if (maxAngle < 180)
                        if (!CurrentTool.Instance.Tool.StateParams.ContainsKey("entry_angle") || !float
                                .Parse(CurrentTool.Instance.Tool.StateParams["entry_angle"])
                                .CheckRange(minAngle, maxAngle))
                            errorMessage = "Неправильний кут установки";
                    if (CurrentTool.Instance.Tool.StateParams.ContainsKey("blood_inside"))
                    {
                        if (!Convert.ToBoolean(CurrentTool.Instance.Tool.StateParams["blood_inside"]))
                            errorMessage = "Під час поглиблення не було потягнуто поршень на себе";
                    }
                    else
                        errorMessage = "Під час поглиблення не було потягнуто поршень на себе";
                }
                else
                {
                    tipMessage = "До від'єднання голки кінцева точка пункції не була досягнута";
                    return false;
                }
            }
            else
                tipMessage = "Голка успішно від'єднана";
                
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