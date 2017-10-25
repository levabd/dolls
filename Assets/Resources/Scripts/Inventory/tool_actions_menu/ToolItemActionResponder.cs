using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class ToolItemActionResponder : MonoBehaviour {
	public bool debugMode = true;

	public ControlStatusDisplay ctrlStat;
    public ActionController actionCtrl;
    public MainLogController logController;
	public EndExamControlPanel examControl;
	private bool CheckAction;
	public GameObject colliderHit = null;
    // Use this for initialization
    void Start()
    {
        ToolItemActionDisplay.OnAction += HandleonClick;
    }

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    void onDestroy()
    {
        Debug.Log("Unsigned-up for onClick");
        ToolItemActionDisplay.OnAction -= HandleonClick;
    }

    void HandleonClick(string actionName)
    {
      

		bool activeControl = true;
		actionCtrl.ActionControl(activeControl, actionName);

        string errorMessage = "";
        CheckAction = CurrentExam.Instance.Exam.Action(ref CurrentTool.Instance.Tool, actionName, out errorMessage, colliderHit != null ? colliderHit.tag : null);
       
		if (!CheckAction) 
		{
            examControl.EndExam (false, errorMessage);
		} 

		GameObject.Find(CurrentTool.Instance.Tool.name + "_item").GetComponentInChildren<Text>().text = CurrentTool.Instance.Tool.Title;
		GameObject.Find(CurrentTool.Instance.Tool.name + "_item/Image").GetComponentInChildren<Image>().sprite = CurrentTool.Instance.Tool.Sprites[0];

		string examName = CurrentExam.Instance.Exam.Name;
        string logActionText = CurrentExam.Instance.Exam.CorrectSteps[CurrentExam.Instance.Exam.TakenSteps.Last().Item1 - 1].Item2;
        
        ctrlStat.ControlStatus(activeControl, examName, actionName, errorMessage);

        bool logActionTextColor = CurrentExam.Instance.Exam.TakenSteps.Last().Item2;

        logActionText = CurrentTool.Instance.Tool.CodeName + " " + actionName + " " + logActionText;

        logController.LogActionCreate(activeControl, logActionTextColor, logActionText);

       // if (debugMode) {Debug.Log (actionName);}

      //  if (debugMode) { Debug.Log("Item3= " + logActionText); }
        if (debugMode) { Debug.Log(CurrentExam.Instance.Exam.LastTakenStep().ToString()); }
        

        //Debug.Log("This Error = " + errorMessage);
        //Update ToolItem Title & Icon

        //Debug.Log(mainText);

        //Check ToolItem.StateParams
        //Dictionary<string, string> stateParams =  toolItem.StateParams;
        //foreach (KeyValuePair<string, string> stateParam in stateParams)
        //{
        //    Debug.Log( stateParam.Key +" " + stateParam.Value);

        //}

    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
