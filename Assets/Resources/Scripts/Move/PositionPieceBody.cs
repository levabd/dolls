using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PositionPieceBody : MonoBehaviour {


    public GameObject cameraPosition;
    //public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    public bool step1 = false;
    private bool step2 = false;
    private bool CheckPosition;
    private string errorMessage;
    public EndExamControlPanel examControl;
    public GameObject Syringe;
    public ActionController actionController;
    public ToolControllerSkin TCS;
	public ToolItemActionResponder TIAR;
    

    void Start () {
    }
	

	void Update ()
    {
        if (step1 && CurrentTool.Instance.Tool.cursorTexture != null)
        {
            Cursor.SetCursor(CurrentTool.Instance.Tool.cursorTexture, hotSpot, cursorMode);
        }
        if (Input.GetMouseButtonDown(0) && step1 == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 1000))
            {
                Debug.Log(hit.transform.gameObject.tag);
                Debug.Log(CurrentTool.Instance.Tool.CodeName);

                CheckPosition = CurrentExam.Instance.Exam.Move(hit.transform.gameObject.tag, out errorMessage);
                Cursor.SetCursor(null, hotSpot, cursorMode);

                
                if (CheckPosition)
                {
					TIAR.colliderHit = hit.transform.gameObject;
                    switch (CurrentTool.Instance.Tool.name)
                    {
                        case "syringe":
                            actionController.OffActionPosition(actionController.VeinPositionPoint);

                            Syringe.SetActive(true);
                            TCS.SkinCollider.SetActive(true);
                            break;

                        case "patch":
                            actionController.CreateFromPrefab(TCS.PushCreate, hit.transform.gameObject, actionController.PrefabTransformCtrl.animationTool.HandWithPatch, 2000f);

                            CurrentExam.Instance.Exam.Action("stick", out errorMessage, hit.transform.gameObject.tag);
                            TIAR.CreateLogEntry();
                            //foreach (var item in CurrentExam.Instance.Exam.TakenSteps)
                            //{
                            //    Debug.Log($"{System.Convert.ToString(item.Item1)} + {item.Item2} + {System.Convert.ToString(item.Item3)}");
                            //}
                            break;

                        case "hand":
                            if (actionController.actionName != "")
                            {
                                switch (actionController.actionName)
                                {
                                    case "get_palpation":
                                        actionController.CreateFromPrefab(TCS.PalpationCreate, actionController.TCS.SkinTransform, actionController.PrefabTransformCtrl.animationTool.Paplation, 4f);

                                        CurrentExam.Instance.Exam.Action("palpation", out errorMessage, hit.transform.gameObject.tag);
                                        TIAR.CreateLogEntry();
                                        break;
                                    case "get_clamp":
                                        actionController.CreateFromPrefab(TCS.ClampVeinCreate, actionController.TCS.SkinTransform, actionController.PrefabTransformCtrl.animationTool.ClampVeins, 2000f);
                                        CurrentExam.Instance.Exam.Action("clamp", out errorMessage, hit.transform.gameObject.tag);
                                        TIAR.CreateLogEntry();
                                        break;
                                    case "get_stretch_the_skin":
                                        actionController.CreateFromPrefab(TCS.PalpationCreate, actionController.TCS.SkinTransform, actionController.PrefabTransformCtrl.animationTool.Paplation, 4f);
                                        CurrentExam.Instance.Exam.Action("stretch_the_skin", out errorMessage, hit.transform.gameObject.tag);
                                        TIAR.CreateLogEntry();
                                        break;
                                }
                            }
                            else
                            {
                                Debug.Log("Action Name error " + CurrentTool.Instance.Tool.CodeName);
                            }
                            break;

                        default:
                            break;
                    }
                    if (cameraPosition.transform.position != Camera.main.transform.position)
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
        else if (step2 == true)
        {

            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, cameraPosition.transform.position, 2f);
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(Camera.main.transform.eulerAngles, cameraPosition.transform.eulerAngles, 2f);
            if (Camera.main.transform.position == cameraPosition.transform.position && Camera.main.transform.eulerAngles == cameraPosition.transform.eulerAngles)
            {
                step2 = false;
            }
            
        }

    }
}
