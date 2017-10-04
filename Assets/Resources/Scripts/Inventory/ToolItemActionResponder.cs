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

    void HandleonClick(string actionName)
    {
        Debug.Log("Test Click   " + actionName);

        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
