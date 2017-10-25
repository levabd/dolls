﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolItemActionDisplay : MonoBehaviour {
    public Text actionTextName;
    string actionName;
    public delegate void ToolItemActionDisplayDelegate(string actionName);
    public static event ToolItemActionDisplayDelegate OnAction;
    Tuple<string, string> action;
    // Use this for initialization
    void Start()
    {
        //if (action != null) Prime(action);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Prime(Tuple<string, string> action)
    {
        this.action = action;
        if (actionTextName != null)
        {
            actionTextName.text = action.Item2;
        }

        actionName = action.Item1;

    }

    public void Click()
    {
        //Debug.Log("You clicked on " + actionName);
        if (OnAction != null)
        {
            OnAction.Invoke(actionName);
        }
        else
        {
            Debug.Log("Nobody was listening to " + actionName);
        }
    }
}
