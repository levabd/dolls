using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndNeedleTrigger : MonoBehaviour {

    // Use this for initialization
    public ControlTransformSyringe controlTool;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name != "ActionPositionPoint")
        {
            controlTool.EndNeedleInCollider = true;
        }
       
    }
    void OnTriggerExit(Collider col)
    {
        controlTool.EndNeedleInCollider = false;
    }
}
