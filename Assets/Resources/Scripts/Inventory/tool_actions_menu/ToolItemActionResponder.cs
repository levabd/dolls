using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolItemActionResponder : MonoBehaviour {
	public bool debugMode = true;

	public ControlStatusDisplay ctrlStat;
    public ActionController actionCtrl;
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

    void HandleonClick(string actionName,ref ToolItem toolItem)
    {
        //Debug.Log("This ToolAction = " + actionName);
        //Debug.Log("This ToolItem = " + toolItem.name);
		bool activeControl = true;
		actionCtrl.ActionControl(activeControl, ref toolItem, actionName);
        string errorMessage = "";
        CheckAction = CurrentExam.Instance.Exam.Action(ref toolItem, actionName, out errorMessage, colliderHit != null ? colliderHit.tag : null);

		if (!CheckAction) 
		{
			examControl.EndExam (false, errorMessage);
		} 
		GameObject.Find(toolItem.name + "_item").GetComponentInChildren<Text>().text = toolItem.Title;
		GameObject.Find(toolItem.name + "_item/Image").GetComponentInChildren<Image>().sprite = toolItem.Sprites[0];

		string examName = CurrentExam.Instance.Exam.Name;

		ctrlStat.ControlStatus(activeControl, examName, ref toolItem, actionName, errorMessage);
		if (debugMode) {Debug.Log (actionName);}


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
