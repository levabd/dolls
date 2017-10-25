using System;
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
	public GameObject ActionPositionPoint;
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

	public void OnActionPosition(GameObject position, string tag)
	{
		position.SetActive (true);
		position.tag = tag;

		if (debugMode) {Debug.Log ("Position Activate: " + " position.name" + position.name + " position.tag " + tag);}
	}

	public void OffActionPosition (GameObject position)
	{
		//position.tag = " ";
		position.SetActive (false);

		if (debugMode) {Debug.Log ("Position Deactivate: " + "Tag Clear = " + position.tag);}
	}

	public void CreateFromPrefab(GameObject prefabTool, GameObject transformGO, float destroyTime)
	{
		//transformGO = TCSWC.Transform;
		GameObject prefabAmination = Instantiate (prefabTool);
		prefabAmination.transform.SetParent (transformGO.transform);
		prefabAmination.transform.localPosition = new Vector3(0,0,0);
		//prefabAmination.transform.position = Vector3.zero;
		//prefabAmination.transform.position = transformGO.transform.position;
		prefabAmination.transform.rotation = transformGO.transform.rotation;
		Destroy (prefabAmination, destroyTime);
	}

	public void CreateToolFromPrefab(GameObject prefabTool, GameObject transformGO)
	{
		//transformGO = TCSWC.Transform;
		GameObject prefabAmination = Instantiate (prefabTool);
		prefabAmination.transform.SetParent (transformGO.transform);
		prefabAmination.transform.localPosition = new Vector3(0,0,0);
		//prefabAmination.transform.position = transformGO.transform.position;
		prefabAmination.transform.rotation = transformGO.transform.rotation;
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

                            break;
                        case "anesthesia":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации анестезии"); }                            

							CreateFromPrefab (TCS.AnestesiaCreate, TCS.SkinTransform, 6f);

                            break;
                        case "piston_pulling":
                            //Debug.Log("piston_pulling Enter");
                            break;
                        case "filling_novocaine_half":
					
							if (debugMode) {Debug.Log ("Запуск позиционирования шприца");}

                            TCSWC.SyringeEloneOff.SetActive(true);

                            OnActionPosition (ActionPositionPoint, "subclavian_vein_target");     
                            
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

					        OnActionPosition (ActionPositionPoint, "disinfection_subclavian_target");
					        PBD.step1 = true;
                            
					        break;
				        case "top_down":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации дезинфекции(сверху-вниз)"); }

					        OffActionPosition (ActionPositionPoint);
					        CreateFromPrefab (TCS.DesinfectionCreate, TCS.SkinTransform, 3f);

                            break;
                        case "right_left":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации дезинфекции(слева-направо)"); }

					        OffActionPosition (ActionPositionPoint);
					        CreateFromPrefab (TCS.DesinfectionCreate, TCS.SkinTransform, 3f);

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

                            CreateFromPrefab(TCSWC.ConductorInANeedleCreate, TCSWC.Transform, 6f);
                            CreateToolFromPrefab(TCSWC.ConductorCreate, TCSWC.Transform);

                            break;
                        case "pull":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации удаление проводника"); }

                            Destroy(GameObject.Find("Transform/Conductor(Clone)"));
                            CreateFromPrefab(TCSWC.ConductorOutCreate, TCSWC.Transform, 6f);

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

                            CreateFromPrefab(TCSWC.ConductorInANeedleCreate, TCSWC.Transform, 6f);
                            CreateToolFromPrefab(TCSWC.ConductorCreate, TCSWC.Transform);

                            break;
                        case "pull":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации удаление проводника"); }

                            Destroy(GameObject.Find("Transform/Conductor(Clone)"));
                            CreateFromPrefab(TCSWC.ConductorOutCreate, TCSWC.Transform, 6f);

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

							CreateToolFromPrefab (TCSWC.CatheterInConductorCreate, TCSWC.Transform);

                            break;
                        case "remove":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации удаление катетера"); }

                            Destroy (GameObject.Find ("Transform/Catheter(Clone)"));
							CreateFromPrefab (TCSWC.CatheterOutCreate, TCSWC.Transform, 5f);

                            break;
                        case "liquid_transfusion_connection":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации подключение катетера к системе переливания жидкостей"); }

                            CreateFromPrefab (TCSWC.CatheterTransfusion, TCSWC.Transform, 1500f);

                            break;
						case "rotation_insertion":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации углубление катетера по проводнику вращательными движениями"); }

                            Destroy (GameObject.Find ("Transform/CatcheterInConductor(Clone)"));
							CreateFromPrefab (TCSWC.CatcheterRotateToConductor, TCSWC.Transform, 6f);
							CreateToolFromPrefab (TCSWC.CatheterCreate, TCSWC.Transform);
							
                            break;
						case "direct_insertion":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации углубление катетера по проводнику прямым движением"); }

                            Destroy (GameObject.Find ("Transform/CatcheterInConductor(Clone)"));
							CreateFromPrefab (TCSWC.CatcheterToConductorCreate, TCSWC.Transform, 6f);
							CreateToolFromPrefab (TCSWC.CatheterCreate, TCSWC.Transform);

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

                            break;
				        case "needle_removing":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации удаление иглы"); }

                            CreateFromPrefab (TCSWC.NeedleOutCreate, TCSWC.Transform, 6f);
					        TCSWC.NeedleOff.SetActive (false);

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


