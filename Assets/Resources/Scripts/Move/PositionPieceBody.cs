﻿using System.Collections;
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
	

	void Update () {
        if (step1 && CurrentTool.Instance.Tool.cursorTexture != null)
        {
            //Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
            Cursor.SetCursor(CurrentTool.Instance.Tool.cursorTexture, hotSpot, cursorMode);
        }
        if (Input.GetMouseButtonDown(0) && step1 == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            //print("куда-то нажал");
            if (Physics.Raycast(ray, out hit, 1000))
            {
                //print("получил луч");
                if (GameObject.Find("Skin/ActionPositionPoint"))
                {
                    GameObject.Find("Skin/ActionPositionPoint").SetActive(false);
                }               
                
                CheckPosition = CurrentExam.Instance.Exam.Move(hit.transform.gameObject.tag, out errorMessage);
                //CheckPosition = false;
                //errorMessage = "fdfdf";
                Cursor.SetCursor(null, hotSpot, cursorMode);

                
                if (CheckPosition)
                {
					TIAR.colliderHit = hit.transform.gameObject;
                    if (CurrentTool.Instance.Tool.name == "syringe")
                    {
                        Syringe.SetActive(true);
                        
                    }
                    if (CurrentTool.Instance.Tool.name == "patch")
                    {
                        actionController.CreateFromPrefab(TCS.PushCreate, hit.transform.gameObject, actionController.PrefabTransformCtrl.animationTool.HandWithPatch, 2000f);
                       
                        CurrentExam.Instance.Exam.Action("stick", out errorMessage, hit.transform.gameObject.tag);
                        TIAR.CreateLogEntry();
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
