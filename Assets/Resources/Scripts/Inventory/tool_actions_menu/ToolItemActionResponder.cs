using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class ToolItemActionResponder : MonoBehaviour
{
    public bool debugMode = true;

    public ControlStatusDisplay CtrlStat;
    public ActionController ActionCtrl;
    public MainLogController MainLoglogCtrl;
    public EndExamControlPanel examControl;
    public GameObject colliderHit = null;
    private string errorMessage = "";
    private string tipMessage = "";
    private bool showAnimations;
    private bool CheckAction;
    private bool activeControl = true;
    // Use this for initialization
    void Start()
    {
        ToolItemActionDisplay.OnAction += HandleonClick;
    }

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    void OnDestroy()
    {
        //Debug.Log("Unsigned-up for onClick");
        ToolItemActionDisplay.OnAction -= HandleonClick;
    }

    public void HandleonClick(string actionName)
    {

        CheckAction = CurrentExam.Instance.Exam.Action(actionName, out errorMessage, out tipMessage, out showAnimations, colliderHit != null ? colliderHit.tag : null);


        if (!CheckAction)
        {
            examControl.EndExam(false, errorMessage);
        }
        if (tipMessage != "")
        {
            CtrlStat.TipMessage(tipMessage);
        }
        if (showAnimations == true)
        {
            CtrlStat.activeControl = true;
            ActionCtrl.ActionControl(activeControl, actionName);
        }
        CreateLogEntry();

        if (debugMode) { Debug.Log(CurrentExam.Instance.Exam.LastTakenStep().ToString()); }
    }

    public void CreateLogEntry()
    {
        if (CurrentExam.Instance.Exam.TakenSteps.Count() == MainLoglogCtrl.mainLogDisplay.transform.childCount)
        {
            return;
        }
        string logActionText = CurrentExam.Instance.Exam.CorrectSteps[CurrentExam.Instance.Exam.TakenSteps.Last().Item1 - 1].Item2;

        bool logActionTextColor = CurrentExam.Instance.Exam.TakenSteps.Last().Item2;
        if (errorMessage != "")
        {
            logActionText = logActionText + "/" + errorMessage;
        }
        //if (CurrentTool.Instance.Tool.StateParams.ContainsKey("entry_angle") && CurrentTool.Instance.Tool.StateParams["entry_angle"] != "")
        //{
        //    logActionText = "Угол наклона шприца = " + CurrentTool.Instance.Tool.StateParams["entry_angle"];
        //}
        MainLoglogCtrl.LogActionCreate(activeControl, logActionTextColor, logActionText);
    }

    // Update is called once per frame
    void Update()
    {

    }
}