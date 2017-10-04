using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolItemActionResponder : MonoBehaviour {

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

    void HandleonClick(string actionName, ToolItem toolItem)
    {
        Debug.Log("This ToolAction = " + actionName);
        Debug.Log("This ToolItem = " + toolItem.name);
        Exam1 exam = new Exam1();
        string errorMessage = "";
        exam.Action(ref toolItem,actionName, out errorMessage);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
