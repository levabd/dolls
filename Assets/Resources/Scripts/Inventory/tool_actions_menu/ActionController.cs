using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {
	public bool debugMode = false;
    public bool action = false;
    public string actionName = "";
    public ToolItem toolItem;
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

    public void ActionControl(bool action, ref ToolItem item, string actionName)
    {
        toolItem = item;
        this.actionName = actionName;
        this.action = action;
    }

    private void CheckAction()
    {

        switch (toolItem.CodeName)
        {
           
            case "syringe":

                if (actionName != "")
                {
                    switch (actionName)
                    {
						case "needle_removing":
							Debug.Log ("needle_removing Enter");
							TCSWC.SyringeEloneOff.SetActive (false);
                            break;
                        case "anesthesia":
                            Debug.Log("start animation anesthesia");
					CreateFromPrefab (TCS.AnestesiaCreate, TCS.SkinTransform, 6f);
                            break;
                        case "piston_pulling":
                            //Debug.Log("piston_pulling Enter");
                            break;
                        case "filling_novocaine_half":
					
							if (debugMode) {Debug.Log ("start_syringe_positions_script");}

							OnActionPosition (ActionPositionPoint, "disinfection_subclavian_target");
							PBD.tool = toolItem;
							PBD.step1 = true;	
					        
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Debug.Log("Action Name error " + toolItem.CodeName);
                }
               
                break;
           
            case "tweezers":

                if (actionName != "")
                {
                    switch (actionName)
                    {
				case "tweezers_balls":
					
					if (debugMode) {Debug.Log ("start_tweezers_positions_script");}

					OnActionPosition (ActionPositionPoint, "disinfection_subclavian_target");
					PBD.tool = toolItem;
					PBD.step1 = true;	
							break;
						case "top_down":
                            Debug.Log("top_down Enter");
					OffActionPosition (ActionPositionPoint);
                            break;
                        case "right_left":
                            Debug.Log("right_left Enter");
					OffActionPosition (ActionPositionPoint);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Debug.Log("Action Name error " + toolItem.CodeName);
                }
                
                break;

            case "standart_catheter_conductor":

                if (actionName != "")
                {
                    switch (actionName)
                    {
				case "push":
					Debug.Log ("push Enter");
					CreateFromPrefab (TCSWC.ConductorInANeedleCreate, TCSWC.Transform, 6f);
                            break;
                        case "pull":
                            Debug.Log("pull Enter");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Debug.Log("Action Name error " + toolItem.CodeName);
                }
               
                break;

            case "soft_catheter_conductor":

                if (actionName != "")
                {
                    switch (actionName)
                    {
				case "push":
					Debug.Log ("push Enter");
					CreateFromPrefab (TCSWC.ConductorInANeedleCreate, TCSWC.Transform, 6f);
					CreateToolFromPrefab (TCSWC.ConductorCreate,TCSWC.Transform);

                            break;
						case "pull":
							Debug.Log ("pull Enter");
					Destroy (GameObject.Find ("Transform/Conductor(Clone)"));
							CreateFromPrefab (TCSWC.ConductorOutCreate, TCSWC.Transform, 6f);
							
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Debug.Log("Action Name error " + toolItem.CodeName);
                }   
                
                break;

            case "catheter_d1":
                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "push":
                            Debug.Log("push Enter");
							CreateToolFromPrefab (TCSWC.CatheterInConductorCreate, TCSWC.Transform);
                            break;
                        case "remove":
							Debug.Log("remove Enter");
							Destroy (GameObject.Find ("Transform/Catheter(Clone)"));
							CreateFromPrefab (TCSWC.CatheterOutCreate, TCSWC.Transform, 5f);
                            break;
                        case "liquid_transfusion_connection":
                            Debug.Log("liquid_transfusion_connection Enter");
							CreateFromPrefab (TCSWC.CatheterTransfusion, TCSWC.Transform, 1500f);
                            break;
						case "rotation_insertion":
							Debug.Log ("rotation_insertion Enter");
							Destroy (GameObject.Find ("Transform/CatcheterInConductor(Clone)"));
							CreateFromPrefab (TCSWC.CatcheterRotateToConductor, TCSWC.Transform, 6f);
							CreateToolFromPrefab (TCSWC.CatheterCreate, TCSWC.Transform);
							
                            break;
						case "direct_insertion":
							Debug.Log ("direct_insertion Enter");				
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
                    Debug.Log("Action Name error " + toolItem.CodeName);
                }
                
                break;

            case "catheter_d06":
                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "push":
                            Debug.Log("push Enter");
							CreateToolFromPrefab (TCSWC.CatheterInConductorCreate, TCSWC.Transform);
                            break;
                        case "remove":
                            Debug.Log("remove Enter");
							Destroy (GameObject.Find ("Transform/Catheter(Clone)"));
							CreateFromPrefab (TCSWC.CatheterOutCreate, TCSWC.Transform, 5f);
                            break;
                        case "liquid_transfusion_connection":
                            Debug.Log("liquid_transfusion_connection Enter");
							CreateFromPrefab (TCSWC.CatheterTransfusion, TCSWC.Transform, 1500f);
                            break;
                        case "rotation_insertion":
                            Debug.Log("rotation_insertion Enter");
							Destroy (GameObject.Find ("Transform/CatcheterInConductor(Clone)"));
							CreateFromPrefab (TCSWC.CatcheterRotateToConductor, TCSWC.Transform, 6f);
							CreateToolFromPrefab (TCSWC.CatheterCreate, TCSWC.Transform);
                            break;
                        case "direct_insertion":
							Debug.Log ("direct_insertion Enter");				
							Destroy (GameObject.Find ("Transform/CatcheterInConductor(Clone)"));
							CreateFromPrefab (TCSWC.CatcheterToConductorCreate, TCSWC.Transform, 6f);
							CreateToolFromPrefab (TCSWC.CatheterCreate, TCSWC.Transform);
							break;
                    }
                }
                else
                {
                    Debug.Log("Action Name error " + toolItem.CodeName);
                }                
                break;

            case "patch":
                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "get":
						Debug.Log("Start patch position");
						PBD.tool = toolItem;
						PBD.step1 = true;
						
                            break;
                    }
                }
                else
                {
                    Debug.Log("Action Name error " + toolItem.CodeName);
                }               
                break;

		case "needle":
			if (actionName != "")
			{
				switch (actionName)
				{
				case "finger_covering":
					Debug.Log("finger_covering start position");
					break;
				case "needle_removing":
					Debug.Log ("needle_removing start position");
					CreateFromPrefab (TCSWC.NeedleOutCreate, TCSWC.Transform, 6f);
					TCSWC.NeedleOff.SetActive (false);
					break;
				}
			}
			else
			{
				Debug.Log("Action Name error " + toolItem.CodeName);
			}               
			break;

            default:
                break;
        }
        action = false;

    }
}


