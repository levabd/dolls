﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {
	public bool debugMode = false;
	public bool debugModeForAnimation = false;
    public bool action = false;
    public string actionName = "";
	public PositionPieceBody PBD;
	public ToolControllerSyringeWithConductor TCSWC;
	public ToolControllerSkin TCS;
    public PrefabTransformController PrefabTransformCtrl;
    public GameObject ActionPositionPoint;
    public GameObject VeinPositionPoint;

    // Use this for initialization
    void Start () {
		
	}
	 
	// Update is called once per frame
	void Update () {
        if (action)
        {
            CheckAction();
        }
        
    }

    IEnumerator CreateToolFromPrefab(GameObject prefabTool, GameObject transformGO, Vector3 prefabRotation, float waitTime)
    {
        if (!GameObject.Find("TransformSkin/" + prefabTool.name))
        {
            yield return new WaitForSeconds(waitTime);
            GameObject prefabAmination = Instantiate(prefabTool);
            prefabAmination.name = prefabTool.name;
            prefabAmination.transform.SetParent(transformGO.transform);
            prefabAmination.transform.localPosition = new Vector3(0,0,0);
            prefabAmination.transform.rotation = transformGO.transform.rotation;
            prefabAmination.transform.localEulerAngles = prefabRotation;
        }
    }

    public void CreateFromPrefab(GameObject prefabTool, GameObject transformGO, Vector3 prefabRotation, float destroyTime)
    {
        if (!GameObject.Find("TransformSkin/" + prefabTool.name))
        {
            GameObject prefabAmination = Instantiate(prefabTool);
            prefabAmination.name = prefabTool.name;
            prefabAmination.transform.SetParent(transformGO.transform);
            prefabAmination.transform.localPosition = new Vector3(0, 0, 0);
            prefabAmination.transform.rotation = transformGO.transform.rotation;
            prefabAmination.transform.localEulerAngles = prefabRotation;
            Destroy(prefabAmination, destroyTime);
        }
    }

    public void OnActionPosition(GameObject position, string tag)
	{
		position.SetActive (true);
		position.tag = tag;

		if (debugMode) {Debug.Log ("Position Activate: " + " position.name" + position.name + " position.tag " + tag);}
	}

	public void OffActionPosition (GameObject position)
	{
		position.SetActive (false);

		if (debugMode) {Debug.Log ("Position Deactivate: " + "Tag Clear = " + position.tag);}
	}
    
    public void ActionControl(bool action, string actionName)
    {
        
        this.actionName = actionName;
        this.action = action;
    }

    private void CheckAction()
    {

        switch (CurrentTool.Instance.Tool.CodeName)
        {
           
            case "syringe":

                if (actionName != "")
                {
                    switch (actionName)
                    {
						case "needle_removing":

							if (debugModeForAnimation) {Debug.Log ("Шприц отсоединен от иглы");}

							TCSWC.SyringeEloneOff.SetActive (false);
                            if (CurrentTool.Instance.Tool.StateParams.ContainsKey("entry_angle"))
                            {
                                PBD.TIAR.CtrlStat.NeedlePanel.SetActive(true);
                            }                            
                            PBD.TIAR.CtrlStat.HintPanel.SetActive(false);

                            break;
                        case "anesthesia":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации анестезии"); }                            

							CreateFromPrefab (TCS.AnestesiaCreate, TCS.SkinTransform, PrefabTransformCtrl.animationTool.AnastesiaV2,  5f);

                            break;
                        case "piston_pulling":
                            //Debug.Log("piston_pulling Enter");   
                            break;
                        case "filling_novocaine_half":
					
							if (debugMode) {Debug.Log ("Запуск позиционирования шприца");}

                            TCSWC.SyringeEloneOff.SetActive(true);

                            OffActionPosition(ActionPositionPoint);
                            OnActionPosition (VeinPositionPoint, "vein_target");

                            PBD.TIAR.CtrlStat.HintPanel.SetActive(true);
                            PBD.step1 = true;	
					        
                            break;
                        case "filling_nacl_half":

                            if (debugMode) { Debug.Log("Запуск позиционирования шприца"); }

                            TCSWC.SyringeEloneOff.SetActive(true);

                            OffActionPosition(ActionPositionPoint);
                            OnActionPosition(VeinPositionPoint, "vein_target");

                            PBD.TIAR.CtrlStat.HintPanel.SetActive(true);
                            PBD.step1 = true;

                            break;                            
                        default:
                            break;
                    }
                }
                else
                {
                    Debug.Log("Action Name error " + CurrentTool.Instance.Tool.CodeName);
                }
               
                break;
           
            case "tweezers":

                if (actionName != "")
                {
                    switch (actionName)
                    {
				        case "tweezers_balls":
					
					        if (debugMode) {Debug.Log ("Запуск позиционирования пинцета");}

					        OnActionPosition (ActionPositionPoint, "disinfection_target");
					        PBD.step1 = true;
                            
					        break;
				        case "top_down":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации дезинфекции(сверху-вниз)"); }

					        OffActionPosition (ActionPositionPoint);
					        CreateFromPrefab (TCS.DesinfectionCreate, TCS.SkinTransform, PrefabTransformCtrl.animationTool.Desinfection, 4.5f);

                            break;
                        case "right_left":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации дезинфекции(слева-направо)"); }

					        OffActionPosition (ActionPositionPoint);
                            CreateFromPrefab(TCS.DesinfectionCreate, TCS.SkinTransform, PrefabTransformCtrl.animationTool.Desinfection, 4.5f);

                            break;
                         default:
                            break;
                    }
                }
                else
                {
                    Debug.Log("Action Name error " + CurrentTool.Instance.Tool.CodeName);
                }
                
                break;

            case "standart_catheter_conductor":

                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "push":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации вставка проводника"); }

                            CreateFromPrefab(TCSWC.ConductorInANeedleCreate, TCSWC.CatheterTransform, PrefabTransformCtrl.animationTool.ConductorInANeedle, 5f);
                            StartCoroutine(CreateToolFromPrefab(TCSWC.ConductorCreate, TCSWC.CatheterTransform, PrefabTransformCtrl.moveTools.Conductor, 0.1f));

                            break;
                        case "pull":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации удаление проводника"); }

                            Destroy(GameObject.Find("TransformCatheter/Conductor"));
                            CreateFromPrefab(TCSWC.ConductorOutCreate, TCSWC.CatheterTransform, PrefabTransformCtrl.animationTool.ConductorOut, 2.5f);

                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Debug.Log("Action Name error " + CurrentTool.Instance.Tool.CodeName);
                }
               
                break;

            case "soft_catheter_conductor":

                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "push":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации вставка проводника"); }

                            CreateFromPrefab(TCSWC.ConductorInANeedleCreate, TCSWC.CatheterTransform, PrefabTransformCtrl.animationTool.ConductorInANeedle, 5f);
                            StartCoroutine(CreateToolFromPrefab(TCSWC.ConductorCreate, TCSWC.CatheterTransform, PrefabTransformCtrl.moveTools.Conductor, 0.1f));

                            break;
                        case "pull":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации удаление проводника"); }

                            Destroy(GameObject.Find("TransformCatheter/Conductor"));
                            CreateFromPrefab(TCSWC.ConductorOutCreate, TCSWC.CatheterTransform, PrefabTransformCtrl.animationTool.ConductorOut, 2.5f);

                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Debug.Log("Action Name error " + CurrentTool.Instance.Tool.CodeName);
                }

                break;

            case "catheter":
                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "push":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации вставка катетера по проводнику"); }

                            StartCoroutine(CreateToolFromPrefab(TCSWC.CatheterInConductorCreate, TCSWC.CatheterTransform, PrefabTransformCtrl.moveTools.CatcheterInConductor, 0.1f));

                            break;
                        case "remove":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации удаление катетера"); }

                            Destroy (GameObject.Find ("TransformCatheter/Catheter"));
							CreateFromPrefab (TCSWC.CatheterOutCreate, TCSWC.CatheterTransform, PrefabTransformCtrl.animationTool.CatcheterOut, 3f);

                            break;
                        case "liquid_transfusion_connection":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации подключение катетера к системе переливания жидкостей"); }

                            CreateFromPrefab (TCSWC.CatheterTransfusion, TCSWC.CatheterTransform, PrefabTransformCtrl.animationTool.CatheterTransfunsion, 1500f);

                            break;
						case "rotation_insertion":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации углубление катетера по проводнику вращательными движениями"); }

                            Destroy (GameObject.Find ("TransformCatheter/CatcheterInConductor"));
							CreateFromPrefab (TCSWC.CatcheterRotateToConductor, TCSWC.CatheterTransform, PrefabTransformCtrl.animationTool.CatcheterRotateToConductor, 4f);
                            StartCoroutine(CreateToolFromPrefab(TCSWC.CatheterCreate, TCSWC.CatheterTransform, PrefabTransformCtrl.moveTools.Catheter, 3f));

                            break;
						case "direct_insertion":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации углубление катетера по проводнику прямым движением"); }

                            Destroy (GameObject.Find ("TransformCatheter/CatcheterInConductor"));
							CreateFromPrefab (TCSWC.CatcheterToConductorCreate, TCSWC.CatheterTransform, PrefabTransformCtrl.animationTool.CatcheterToConductor,  4f);
                            StartCoroutine(CreateToolFromPrefab(TCSWC.CatheterCreate, TCSWC.CatheterTransform, PrefabTransformCtrl.moveTools.Catheter, 3f));

                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Debug.Log("Action Name error " + CurrentTool.Instance.Tool.CodeName);
                }
                
                break;
           

            case "patch":
                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "get":

				            if (debugMode) {Debug.Log ("Запуск позиционирования пластыря");}						    
						    PBD.step1 = true;
						
                            break;
                    }
                }
                else
                {
                    Debug.Log("Action Name error " + CurrentTool.Instance.Tool.CodeName);
                }               
                break;

		    case "needle":
			    if (actionName != "")
			    {
				    switch (actionName)
				    {
				        case "finger_covering":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации прикрытия иглы пальцем"); }
                            ActionPositionPoint.SetActive(false);
                            VeinPositionPoint.SetActive(false);

                            break;
				        case "needle_removing":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации удаление иглы"); }

                            CreateFromPrefab (TCSWC.NeedleOutCreate, TCSWC.Transform, PrefabTransformCtrl.animationTool.NeedleOut, 3f);
					        TCSWC.NeedleOff.SetActive (false);

					        break;
				    }
			    }
			    else
			    {
				    Debug.Log("Action Name error " + CurrentTool.Instance.Tool.CodeName);
			    }               
			    break;

            case "hand":
                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "get_palpation":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации пальпации"); }
                            OnActionPosition(ActionPositionPoint, "palpation_target");
                            PBD.step1 = true;
                            break;
                        case "get_clamp":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации пальпации"); }
                            OnActionPosition(ActionPositionPoint, "clamp_target");
                            PBD.step1 = true;
                            break;
                        case "get_stretch_the_skin":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации пальпации"); }
                            OnActionPosition(ActionPositionPoint, "stretch_target");
                            PBD.step1 = true;
                            break;
                        case "clamp_out":
                            Destroy(GameObject.Find("TransformSkin/ClampVeins"));
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
        action = false;

    }
}


