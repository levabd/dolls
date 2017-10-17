using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {
    public bool action = false;
    public string actionName = "";
    public ToolItem toolItem;
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
                            Debug.Log("needle_removing Enter");
                            break;
                        case "anesthesia":
                            Debug.Log("start animation anesthesia");
                            break;
                        case "piston_pulling":
                            Debug.Log("piston_pulling Enter");
                            break;
                        case "filling_novocaine_half":
                            Debug.Log("Start main action");
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
                        case "top_down":
                            Debug.Log("top_down Enter");
                            break;
                        case "right_left":
                            Debug.Log("right_left Enter");
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
                            Debug.Log("push Enter");
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
                            Debug.Log("push Enter");
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

            case "catheter_d1":
                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "push":
                            Debug.Log("push Enter");
                            break;
                        case "remove":
                            Debug.Log("remove Enter");
                            break;
                        case "liquid_transfusion_connection":
                            Debug.Log("liquid_transfusion_connection Enter");
                            break;
                        case "rotation_insertion":
                            Debug.Log("rotation_insertion Enter");
                            break;
                        case "direct_insertion":
                            Debug.Log("direct_insertion Enter");
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
                            break;
                        case "remove":
                            Debug.Log("remove Enter");
                            break;
                        case "liquid_transfusion_connection":
                            Debug.Log("liquid_transfusion_connection Enter");
                            break;
                        case "rotation_insertion":
                            Debug.Log("rotation_insertion Enter");
                            break;
                        case "direct_insertion":
                            Debug.Log("direct_insertion Enter");
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
                            Debug.Log("get Enter");
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
