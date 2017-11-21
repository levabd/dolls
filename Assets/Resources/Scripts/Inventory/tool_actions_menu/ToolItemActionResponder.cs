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
    public bool activeControl = true;
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
        CheckActionControl(CheckAction, showAnimations, errorMessage, tipMessage, actionName);

        if (debugMode) { Debug.Log(CurrentExam.Instance.Exam.LastTakenStep().ToString()); }
    }

    public void CheckActionControl(bool _checkAction,  bool _showAnimations, string _errorMessage, string _tipMessage, string _actionName)
    {
        if (!CheckAction)
        {
            examControl.EndExam(false, _errorMessage);
        }
        if (!String.IsNullOrWhiteSpace(tipMessage))
        {
            CtrlStat.TipMessage(tipMessage);
        }
        if (showAnimations)
        {
            CtrlStat.activeControl = true;
            ActionCtrl.ActionControl(activeControl, _actionName);
        }
        CreateLogEntry(_errorMessage);
    }


    public void CreateLogEntry(string _errorMessage)
    {
        if (CurrentExam.Instance.Exam.TakenSteps.Count == MainLoglogCtrl.mainLogDisplay.transform.childCount)
        {
            return;
        }
        string logActionText = CurrentExam.Instance.Exam.CorrectSteps[CurrentExam.Instance.Exam.TakenSteps.Last().Item1 - 1].Item2;

        bool logActionTextColor = CurrentExam.Instance.Exam.TakenSteps.Last().Item2;
        if (!String.IsNullOrWhiteSpace(_errorMessage))
        {
            logActionText = logActionText + "/" + _errorMessage;
        }
        MainLoglogCtrl.LogActionCreate(activeControl, logActionTextColor, logActionText);
    }

    // Update is called once per frame
    void Update()
    {

    }
}