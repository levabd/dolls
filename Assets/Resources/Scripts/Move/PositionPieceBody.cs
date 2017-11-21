using UnityEngine;

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
    public EndExamControlPanel examControl;
    public GameObject Syringe;
    public GameObject Venflon;
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
                        case "venflon":
                            actionController.OffActionPosition(actionController.VeinPositionPoint);

                            Venflon.SetActive(true);
                            TCS.SkinCollider.SetActive(true);
                            break;
                        case "big":
                            actionController.OffActionPosition(actionController.ActionPositionPoint);

                            CurrentTool.Instance.Tool.StateParams["entry_angle"] = "90";
                            actionController.CreateFromPrefab(actionController.TCS.BIG, actionController.TCS.SkinTransform, actionController.PrefabTransformCtrl.moveTools.BIG, 2000f);
                            bool big = CurrentExam.Instance.Exam.Action("big", out errorMessage, out tipMessage, out showAnimations, hit.transform.gameObject.tag);
                            TIAR.CheckActionControl(big, showAnimations, errorMessage, tipMessage, "big");

                            break;
                        case "cannule":
                            actionController.OffActionPosition(actionController.VeinPositionPoint);

                            actionController.CreateFromPrefab(actionController.TCS.CannuleEnterCreate, actionController.TCS.gameObject, actionController.PrefabTransformCtrl.animationTool.CannuleEnter, 2000f);
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
                                        bool attach_balls = CurrentExam.Instance.Exam.Action("attach_balls", out errorMessage, out tipMessage, out showAnimations, hit.transform.gameObject.tag);
                                        TIAR.CheckActionControl(attach_balls, showAnimations, errorMessage, tipMessage, "attach_balls");
                                        break;
                                    case "get_top_down":
                                        actionController.CreateFromPrefab(TCS.GauzeBallsRubUpDownCreate, actionController.TCS.SkinTransform, actionController.PrefabTransformCtrl.animationTool.GauzeBallsRubUpDown, 5f);
                                        bool top_down = CurrentExam.Instance.Exam.Action("top_down", out errorMessage, out tipMessage, out showAnimations, hit.transform.gameObject.tag);
                                        TIAR.CheckActionControl(top_down, showAnimations, errorMessage, tipMessage, "top_down");
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
                            bool stick = CurrentExam.Instance.Exam.Action("stick", out errorMessage, out tipMessage, out showAnimations, hit.transform.gameObject.tag);
                            TIAR.CheckActionControl(stick, showAnimations, errorMessage, tipMessage, "stick");
                            break;

                        case "hand":
                            if (actionController.actionName != "")
                            {
                                switch (actionController.actionName)
                                {
                                    case "get_palpation":
                                        actionController.CreateFromPrefab(TCS.PalpationCreate, actionController.TCS.SkinTransform, actionController.PrefabTransformCtrl.animationTool.Paplation, 4f);

                                        bool palpation = CurrentExam.Instance.Exam.Action("palpation", out errorMessage, out tipMessage, out showAnimations, hit.transform.gameObject.tag);
                                        TIAR.CheckActionControl(palpation, showAnimations, errorMessage, tipMessage, "palpation");
                                        break;
                                    case "get_clamp":
                                        actionController.CreateFromPrefab(TCS.ClampVeinCreate, actionController.TCS.SkinTransform, actionController.PrefabTransformCtrl.animationTool.ClampVeins, 2000f);
                                        bool clamp = CurrentExam.Instance.Exam.Action("clamp", out errorMessage, out tipMessage, out showAnimations, hit.transform.gameObject.tag);
                                        TIAR.CheckActionControl(clamp, showAnimations, errorMessage, tipMessage, "clamp");
                                        break;
                                    case "get_stretch_the_skin":
                                        actionController.CreateFromPrefab(TCS.StretchTheSkinLeftCreate, actionController.TCS.SkinTransform, actionController.PrefabTransformCtrl.animationTool.StretchTheSkinLeft, 2000f);
                                        bool stretch_the_skin = CurrentExam.Instance.Exam.Action("stretch_the_skin", out errorMessage, out tipMessage, out showAnimations, hit.transform.gameObject.tag);
                                        TIAR.CheckActionControl(stretch_the_skin, showAnimations, errorMessage, tipMessage, "stretch_the_skin");
                                        break;
                                }
                            }
                            else
                            {
                                Debug.Log("Action Name error " + CurrentTool.Instance.Tool.CodeName);
                            }
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
        else if (step2)
        {

            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, cameraPosition.transform.position, 2f);
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(Camera.main.transform.eulerAngles, cameraPosition.transform.eulerAngles, 2f);
            if (Camera.main.transform.position == cameraPosition.transform.position && Camera.main.transform.eulerAngles == cameraPosition.transform.eulerAngles)
                step2 = false;
        }

    }
}
