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
    public ToolControllerVenflon TCV;
    public PrefabTransformController PrefabTransformCtrl;
    public GameObject ActionPositionPoint;
    public GameObject VeinPositionPoint;
    public GameObject TourniquetPositionPoint;

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
                        case "get":

                            if (debugMode) { Debug.Log("Запуск позиционирования шприца"); }

                            TCSWC.SyringeEloneOff.SetActive(true);

                            OffActionPosition(ActionPositionPoint);
                            OnActionPosition(VeinPositionPoint, "vein_target");

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
                        case "take_the_blood_ml10":
                            if (debugMode) { Debug.Log("Запуск анимации набирания крови"); }
                            
                            Destroy(GameObject.Find("TransformSkin/StretchTheSkinLeft"));
                            
                            break;
                        case "needle_pull":
                            if (debugMode) { Debug.Log("Запуск анимации извлечения шприца"); }

                            PBD.Syringe.SetActive(false);
                            break;
                        case "administer_drug":
                            if (debugMode) { Debug.Log("Запуск анимации ввода препарата"); }
                            Material matBlood2 = Resources.Load("Prefabs/Medicine_and_Health/Models/Materials/Syringe_df_empty", typeof(Material)) as Material;
                            Material[] mats2 = GameObject.Find("SyringeElone").transform.GetChild(0).gameObject.GetComponent<Renderer>().materials;
                            mats2[0] = matBlood2;
                            GameObject.Find("SyringeElone").transform.GetChild(0).gameObject.GetComponent<Renderer>().materials = mats2;
                            break;
                        case "nacl_to_cateter":
                            CreateFromPrefab(TCV.NaclToVenflonCreate, TCV.Transform, PrefabTransformCtrl.animationTool.NaclToVenflon, 4f);
                            break;
                        case "big_nacl":
                            CreateFromPrefab(TCV.NaclToVenflonCreate, TCV.Transform, PrefabTransformCtrl.animationTool.NaclToVenflon, 4f);
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

                            Destroy(GameObject.Find("TransformSkin/ClampVeins"));
                            ActionPositionPoint.SetActive(false);
                            VeinPositionPoint.SetActive(false);
                            if (debugMode) {Debug.Log ("Запуск позиционирования пластыря");}						    
						    PBD.step1 = true;
						
                            break;

                        case "impose":

                            if (debugMode) { Debug.Log("Запуск анимация пластыря"); }

                            //CurrentExam.Instance.Exam.Action("patch", out errorMessage, locatedColliderTag.Contains("big"));
                            CreateFromPrefab(TCS.PatchToBIGNeedleCreate, TCS.SkinTransform, PrefabTransformCtrl.animationTool.PatchToBIGNeedle, 2000f);

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

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации зажать вену"); }
                            OnActionPosition(ActionPositionPoint, "clamp_target");
                            PBD.step1 = true;
                            break;
                        case "get_stretch_the_skin":

                            if (debugModeForAnimation) { Debug.Log("Запуск анимации натягивания кожи"); }
                            OnActionPosition(ActionPositionPoint, "stretch_target");
                            PBD.step1 = true;
                            break;
                        case "clamp_out":
                            Destroy(GameObject.Find("TransformSkin/ClampVeins"));
                            //foreach (var item in CurrentExam.Instance.Exam.TakenSteps)
                            //{
                            //    Debug.Log($"{System.Convert.ToString(item.Item1)} + {item.Item2} + {System.Convert.ToString(item.Item3)}");
                            //}
                            break;
                    }
                }
                else
                {
                    Debug.Log("Action Name error " + CurrentTool.Instance.Tool.CodeName);
                }
                break;

            case "tourniquet":

                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "get":

                            if (debugMode) { Debug.Log("Запуск позиционирования жгута"); }

                            OnActionPosition(TourniquetPositionPoint, "tourniquet_target");
                            PBD.step1 = true;

                            break;
                        case "lay":

                            if (debugMode) { Debug.Log("Запуск жгута"); }

                            OffActionPosition(TourniquetPositionPoint);
                            TCS.TourniquetTransform.SetActive(true);

                            break;
                        case "remove":
                            if (debugMode) { Debug.Log("Выключить жгут"); }

                            Destroy(GameObject.Find("TransformSkin/StretchTheSkinLeft"));
                            TCS.TourniquetTransform.SetActive(false);


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

            case "gauze_balls":

                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "get_balls":

                            if (debugMode) { Debug.Log("Запуск позиционирования ватки"); }

                            OnActionPosition(ActionPositionPoint, "disinfection_target");
                            PBD.step1 = true;

                            break;
                        case "get_top_down":

                            if (debugMode) { Debug.Log("Запуск анимацию протирания ваткой"); }

                            OnActionPosition(ActionPositionPoint, "disinfection_target");
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
            case "venflon":

                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "get":

                            if (debugMode) { Debug.Log("Запуск позиционирования венфлона"); }

                            OffActionPosition(ActionPositionPoint);
                            OnActionPosition(VeinPositionPoint, "vein_target");

                            PBD.TIAR.CtrlStat.HintPanel.SetActive(true);
                            PBD.step1 = true;

                            break;
                        case "liquid_transfusion_connection":

                            if (debugMode) { Debug.Log("Запуск анимацию соеденения с сист. пер. жидкостей"); }

                            CreateFromPrefab(TCV.VenflonTransfusionCreate, TCV.Transform, PrefabTransformCtrl.animationTool.VenflonTransfusion, 2000f);

                            break;
                        case "remove_mandren":

                            if (debugMode) { Debug.Log("Запуск анимацию вытаскивания мадрена"); }

                            TCV.VenflonMainOff.SetActive(false);
                            CreateFromPrefab(TCV.VenflonGetMandrenCreate, TCV.Transform, PrefabTransformCtrl.animationTool.VenflonGetMandren, 2000f);

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
            case "razor":

                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "get_razor":

                            if (debugMode) { Debug.Log("Запуск позиционирования бритвы"); }

                            OnActionPosition(ActionPositionPoint, "disinfection_target");
                            PBD.step1 = true;

                            break;
                        case "shave_pubis":

                            if (debugMode) { Debug.Log("Запуск анимацию бритья"); }
                            OffActionPosition(ActionPositionPoint);
                            CreateFromPrefab(TCS.ShaveCreate, TCS.SkinTransform, PrefabTransformCtrl.animationTool.Shave, 4.5f);
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
            case "sterile_tissue":

                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "put":

                            if (debugMode) { Debug.Log("Запуск анимацию бритья"); }
                            OffActionPosition(ActionPositionPoint);
                            CreateFromPrefab(TCS.NapkinPutCreate, TCS.SkinTransform, PrefabTransformCtrl.animationTool.NapkinPut, 4.5f);
                            StartCoroutine(CreateToolFromPrefab(TCS.SterileTissueCreate, TCS.SkinTransform, PrefabTransformCtrl.moveTools.SterileTissue, 3f));
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
            case "trocar":

                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "get":

                            if (debugMode) { Debug.Log("Запуск позиционирования трокара"); }

                            OnActionPosition(ActionPositionPoint, "vein_target");
                            PBD.step1 = true;

                            break;
                        case "pull":
                            
                            break;
                        case "trocar_connect":

                            break;
                        case "trocar_connect_valve":

                            break;
                        case "clamp_out":

                            break;
                        case "clamp":

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
            case "stitch":

                if (actionName != "")
                {
                    switch (actionName)
                    {
                        case "stitch":

                            if (debugMode) { Debug.Log("Запуск анимацию пришивания"); }
                            OffActionPosition(ActionPositionPoint);
                            CreateFromPrefab(TCS.NapkinPutCreate, TCS.SkinTransform, PrefabTransformCtrl.animationTool.NapkinPut, 4.5f);
                            StartCoroutine(CreateToolFromPrefab(TCS.SterileTissueCreate, TCS.SkinTransform, PrefabTransformCtrl.moveTools.SterileTissue, 3f));
                            break;
                        case "cannule_stitch":
                            Destroy(GameObject.Find("TransformSkin/CannuleRinse"));
                            if (debugMode) { Debug.Log("Запуск анимацию пришивания канюли"); }
                            OffActionPosition(ActionPositionPoint);
                            CreateFromPrefab(TCS.CannuleStitchCreate, TCS.gameObject, PrefabTransformCtrl.animationTool.CannuleStitch, 2000f);
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
            case "scalpel":

                if (actionName != "")
                {
                    switch (actionName)
                    {

                        case "get":

                            if (debugMode) { Debug.Log("Запуск позиционирования скальпеля"); }

                            OnActionPosition(ActionPositionPoint, "scalpel_target");
                            PBD.step1 = true;

                            break;
                        case "incision":

                            if (debugMode) { Debug.Log("Запуск анимацию пришивания"); }
                            OffActionPosition(ActionPositionPoint);
                            CreateFromPrefab(TCS.NapkinPutCreate, TCS.SkinTransform, PrefabTransformCtrl.animationTool.NapkinPut, 4.5f);
                            StartCoroutine(CreateToolFromPrefab(TCS.SterileTissueCreate, TCS.SkinTransform, PrefabTransformCtrl.moveTools.SterileTissue, 3f));
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
            case "bobrov_bank":

                if (actionName != "")
                {
                    switch (actionName)
                    {

                        case "connect":

                            if (debugMode) { Debug.Log("Запуск анимацию соединения с банкой"); }
                            OffActionPosition(ActionPositionPoint);
                            CreateFromPrefab(TCS.NapkinPutCreate, TCS.SkinTransform, PrefabTransformCtrl.animationTool.NapkinPut, 4.5f);
                            StartCoroutine(CreateToolFromPrefab(TCS.SterileTissueCreate, TCS.SkinTransform, PrefabTransformCtrl.moveTools.SterileTissue, 3f));
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
            case "big":

                if (actionName != "")
                {
                    switch (actionName)
                    {

                        case "get":

                            if (debugMode) { Debug.Log("Запуск позиционирования шприца BIG"); }

                            OnActionPosition(ActionPositionPoint, "tubercle_the_tibia");
                            PBD.step1 = true;

                            break;
                        case "prepare":

                            if (debugMode) { Debug.Log("Запуск анимацию снятия предохранителя"); }

                            OffActionPosition(ActionPositionPoint);
                            CreateFromPrefab(TCS.BIGRemoveTheFuseCreate, TCS.SkinTransform, PrefabTransformCtrl.animationTool.BIGRemoveTheFuse, 2000f);
                            Destroy(GameObject.Find("TransformSkin/BIG"));

                            break;
                        case "activate":

                            if (debugMode) { Debug.Log("Запуск анимацию выстрела пистолета"); }

                            OffActionPosition(ActionPositionPoint);
                            CreateFromPrefab(TCS.BIGFireCreate, TCS.SkinTransform, PrefabTransformCtrl.animationTool.BIGFire, 2000f);
                            Destroy(GameObject.Find("TransformSkin/BIGRemoveTheFuse"));

                            break;
                        case "remove":

                            if (debugMode) { Debug.Log("Запуск анимацию удаления"); }

                            Destroy(GameObject.Find("TransformSkin/BIGFire"));
                            CreateFromPrefab(TCS.BIGNeedleOutCreate, TCS.SkinTransform, PrefabTransformCtrl.animationTool.BIGNeedleOut, 2000f);

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
            case "stopcock":

                if (actionName != "")
                {
                    switch (actionName)
                    {

                        case "impose":

                            if (debugMode) { Debug.Log("Запуск позиционирования скальпеля"); }

                            OnActionPosition(ActionPositionPoint, "scalpel_target");
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
            case "manometer":

                if (actionName != "")
                {
                    switch (actionName)
                    {

                        case "get":

                            if (debugMode) { Debug.Log("Запуск позиционирования скальпеля"); }

                            OnActionPosition(ActionPositionPoint, "scalpel_target");
                            PBD.step1 = true;

                            break;
                        case "pump_it":
                            
                            break;
                        case "close_piston":

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
            case "phonendoscope":

                if (actionName != "")
                {
                    switch (actionName)
                    {

                        case "get":

                            if (debugMode) { Debug.Log("Запуск позиционирования скальпеля"); }

                            OnActionPosition(ActionPositionPoint, "phonendoscope_target");
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
            case "invasive_sensor":

                if (actionName != "")
                {
                    switch (actionName)
                    {

                        case "connect":

                            if (debugMode) { Debug.Log("Запуск анимации присоединения к датчику"); }
                            OffActionPosition(ActionPositionPoint);
                            Destroy(GameObject.Find("TransformSkin/CannuleStitch"));
                            CreateFromPrefab(TCS.InvasiveSensorConnectCreate, TCS.gameObject, PrefabTransformCtrl.animationTool.InvasiveSensorConnect, 2000);
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
            case "cannule":

                if (actionName != "")
                {
                    switch (actionName)
                    {

                        case "get":

                            if (debugMode) { Debug.Log("Запуск позиционирования канюли"); }
                            OffActionPosition(ActionPositionPoint);
                            OnActionPosition(VeinPositionPoint, "radial_artery");
                            PBD.step1 = true;

                            break;
                        case "push":
                            if (debugMode) { Debug.Log("Запуск анимации введения канюли в глубину просвета артерии"); }
                            OffActionPosition(ActionPositionPoint);
                            Destroy(GameObject.Find("TransformSkin/CannulePull"));
                            CreateFromPrefab(TCS.CannulePushCreate, TCS.gameObject, PrefabTransformCtrl.animationTool.CannulePush, 2000);
                            break;
                        case "rinse":
                            if (debugMode) { Debug.Log("Запуск анимацию промывания канюли"); }
                            OffActionPosition(ActionPositionPoint);
                            Destroy(GameObject.Find("TransformSkin/CannulePush"));
                            CreateFromPrefab(TCS.CannuleRinseCreate, TCS.gameObject, PrefabTransformCtrl.animationTool.CannuleRinse, 2000);
                            break;
                        case "remove_mandren":
                            if (debugMode) { Debug.Log("Запуск анимации удаления мандрена"); }
                            OffActionPosition(ActionPositionPoint);
                            Destroy(GameObject.Find("TransformSkin/CannuleEnter"));
                            CreateFromPrefab(TCS.CannuleRemoveMandrenCreate, TCS.gameObject, PrefabTransformCtrl.animationTool.CannuleRemoveMandren, 2000);
                            break;
                        case "pull":
                            if (debugMode) { Debug.Log("Запуск анимации потягивания канюли на себя"); }
                            OffActionPosition(ActionPositionPoint);
                            Destroy(GameObject.Find("TransformSkin/CannuleRemoveMandren"));
                            CreateFromPrefab(TCS.CannulePullCreate, TCS.gameObject, PrefabTransformCtrl.animationTool.CannulePull, 2000);
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

            default:
                    break;
        }
        action = false;

    }
}


