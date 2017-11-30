﻿using UnityEngine;
using System.Collections.Generic;


public class PositionPieceBody : MonoBehaviour {


    public GameObject cameraPosition;
    //public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    public bool step1;
    private bool step2;
    private bool CheckPosition;
    private string errorMessage;
    private string tipMessage;
    private bool showAnimations;
    public bool embolia = true;
    public EndExamControlPanel examControl;
    public GameObject Syringe;
    public GameObject Venflon;
    public ActionController actionController;
    public ToolControllerSkin TCS;
	public ToolItemActionResponder TIAR;
    
    private List<string> piercingTools => new List<string>
    {
        "syringe",
        "venflon",
        "tweezers",
        "big",
        "cannule",
        "razor",
        "trocar",
        "stitch",
        "scalpel",
        "phonendoscope"
    };

    void Start ()
    {
        
    }
	
    void CheckPositionPiercingTools(string target)
    {
        foreach (var piercingTool in piercingTools)
        {
            if (piercingTool == CurrentTool.Instance.Tool.name && target == "body_target")
            {
                TIAR.CtrlStat.TipMessage("Ви не потрапили інструментом у потрібне місце");
            }
        }
    }

    void Embolism()
    {
        if (embolia)
        {
            if (CurrentExam.Instance?.Exam.CheckAirEmbolism() == "Повітряна емболія")
            {
                CurrentExam.Instance.Exam.AirEmbolismFinish("Повітряна емболія");
                examControl.EndExam(false, "Повітряна емболія");
                embolia = false;
            }
        }
        
    }

	void Update ()
    {
        if (CurrentExam.Instance.Exam.LoadName != "TrainingExam")
        {
            Embolism();
        }        
        if (step1 && CurrentTool.Instance.Tool.cursorTexture != null)
        {
            Cursor.SetCursor(CurrentTool.Instance.Tool.cursorTexture, hotSpot, cursorMode);
        }
        if (Input.GetMouseButtonDown(0) && step1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 1000))
            {
                Debug.Log(hit.transform.gameObject.tag);
                Debug.Log(CurrentTool.Instance.Tool.CodeName);

                CheckPosition = CurrentExam.Instance.Exam.Move(hit.transform.gameObject.tag, out errorMessage, out tipMessage);
                Cursor.SetCursor(null, hotSpot, cursorMode);

                CheckPositionPiercingTools(hit.transform.gameObject.tag);
                if (CheckPosition)
                {
                    if (TIAR.TrainingMode)
                    {
                        TIAR.TC.IsActive();
                    }
                    TIAR.colliderHit = hit.transform.gameObject;
                    switch (CurrentTool.Instance.Tool.name)
                    {
                        case "syringe":
                            string needle;
                            if (CurrentTool.Instance.Tool.StateParams.TryGetValue("has_needle", out needle) && needle == "true")
                            {
                                if (hit.transform.gameObject.tag == "vein_target")
                                {
                                    actionController.OffActionPosition(actionController.VeinPositionPoint);
                                    Syringe.SetActive(true);
                                    TCS.SkinCollider.SetActive(true);
                                }
                                else
                                {
                                    examControl.EndExam(false, "Шприц було спрямовано не в те місце");
                                }
                            }
                            else
                            {
                                TIAR.CtrlStat.TipMessage("Шприц без голки");
                            }
                            
                            break;
                        case "venflon":
                            actionController.OffActionPosition(actionController.VeinPositionPoint);

                            Venflon.SetActive(true);
                            TCS.SkinCollider.SetActive(true);
                            break;
                        case "big":
                            actionController.OffActionPosition(actionController.ActionPositionPoint);

                            CurrentTool.Instance.Tool.StateParams["entry_angle"] = "90";
                            actionController.CreateFromPrefab(actionController.TCS.BIG, actionController.TCS.SkinTransform, actionController.PrefabTransformCtrl.moveTools.BIG, 2000f);                            
                            //TIAR.CheckActionControl("big", hit.transform.gameObject);
                            break;
                        case "trocar":

                            actionController.CreateFromPrefab(actionController.TCS.DrenajToIncisionCreate, actionController.TCS.SkinTransform, actionController.PrefabTransformCtrl.animationTool.DrenajToIncision, 2000f);

                            break;
                        case "gauze_balls":
                            if (actionController.actionName != "")
                            {
                                switch (actionController.actionName)
                                {
                                    case "get_balls":
                                        
                                        actionController.OffActionPosition(actionController.ActionPositionPoint);

                                        actionController.CreateFromPrefab(TCS.GauzeBallsEncloseCreate, actionController.TCS.SkinTransform, actionController.PrefabTransformCtrl.animationTool.GauzeBallsEnclose, 2000f);
                                        TIAR.CheckActionControl("attach_balls", hit.transform.gameObject);
                                        break;
                                    case "get_top_down":

                                        actionController.CreateFromPrefab(TCS.GauzeBallsRubUpDownCreate, actionController.TCS.SkinTransform, actionController.PrefabTransformCtrl.animationTool.GauzeBallsRubUpDown, 5f);
                                        TIAR.CheckActionControl("top_down", hit.transform.gameObject);
                                        break;
                                }
                            }
                            else
                            {
                                Debug.Log("Action Name error " + CurrentTool.Instance.Tool.CodeName);
                            }
                            break;

                        case "patch":
                            if (hit.transform.gameObject.tag == "catheter")
                            {
                                actionController.CreateFromPrefab(TCS.PushCreate, hit.transform.gameObject, actionController.PrefabTransformCtrl.animationTool.HandWithPatch, 2000f);
                            }
                            TIAR.CheckActionControl("stick", hit.transform.gameObject);                            
                            break;

                        case "phonendoscope":
                            if (hit.transform.gameObject.tag == "phonendoscope_target")
                            {
                                TCS.StethoscopeCreate.SetActive(true);
                            } 
                            TIAR.CheckActionControl("set", hit.transform.gameObject);
                            break;

                        case "hand":
                            if (actionController.actionName != "")
                            {
                                switch (actionController.actionName)
                                {
                                    case "get_palpation":
                                        if (!GameObject.Find("Stethoscope"))
                                        {
                                            actionController.CreateFromPrefab(TCS.PalpationCreate, actionController.TCS.SkinTransform, actionController.PrefabTransformCtrl.animationTool.Paplation, 4f);
                                            TIAR.CheckActionControl("palpation", hit.transform.gameObject);
                                        }
                                        else
                                        {
                                            TIAR.CtrlStat.TipMessage("Напевно, ви робите зайву дію");
                                        }
                                        break;
                                    case "get_clamp":
                                        actionController.CreateFromPrefab(TCS.ClampVeinCreate, actionController.TCS.SkinTransform, actionController.PrefabTransformCtrl.animationTool.ClampVeins, 2000f);
                                        TIAR.CheckActionControl("clamp", hit.transform.gameObject);
                                        break;
                                    case "get_stretch_the_skin":
                                        actionController.CreateFromPrefab(TCS.StretchTheSkinLeftCreate, actionController.TCS.SkinTransform, actionController.PrefabTransformCtrl.animationTool.StretchTheSkinLeft, 2000f);
                                        TIAR.CheckActionControl("stretch_the_skin", hit.transform.gameObject);
                                        break;
                                }
                            }
                            else
                            {
                                Debug.Log("Action Name error " + CurrentTool.Instance.Tool.CodeName);
                            }
                            break;
                    }
                    if (cameraPosition.transform.position != Camera.main.transform.position && TIAR.CheckAction && (hit.transform.gameObject.tag == "disinfection_target" || hit.transform.gameObject.tag == "tourniquet_target" || hit.transform.gameObject.tag == "phonendoscope_target" || hit.transform.gameObject.tag == "palpation_target") )
                    {
                        step2 = true;
                    }
                }
                else
                {
                    examControl.EndExam(false, errorMessage);
                }

                step1 = false;
            }
        }
        else if (step2)
        {

            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, cameraPosition.transform.position, 2f);
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(Camera.main.transform.eulerAngles, cameraPosition.transform.eulerAngles, 2f);
            if (Camera.main.transform.position == cameraPosition.transform.position && Camera.main.transform.eulerAngles == cameraPosition.transform.eulerAngles)
                step2 = false;
        }

    }
}
