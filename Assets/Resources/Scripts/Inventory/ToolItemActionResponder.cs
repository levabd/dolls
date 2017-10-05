using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolItemActionResponder : MonoBehaviour {
    public ControlStatusDisplay ctrlStat;
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

        Exam1 exam = new Exam1();
        string errorMessage = "";
        exam.Action(ref toolItem, actionName, out errorMessage);

        //Debug.Log("This Error = " + errorMessage);

        string examName = exam.Name;
        bool activeControl = true;
        ctrlStat.ControlStatus(activeControl, examName, ref toolItem, actionName, errorMessage);

        //Update ToolItem Title
        GameObject.Find(toolItem.name + "_item").GetComponentInChildren<Text>().text = toolItem.Title;

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
