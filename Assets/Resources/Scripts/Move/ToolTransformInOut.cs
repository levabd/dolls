using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTransformInOut : MonoBehaviour {


    public GameObject ToolPrefab;
    private GameObject Tool;
	// Use this for initialization
	void Start () {
        Tool = GameObject.Find(ToolPrefab.name);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            
            TransformIn();


        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            TransformOut();
        }
    }
    public void TransformIn()
    {
        Tool.transform.localPosition = new Vector3(Tool.transform.localPosition.x, Tool.transform.localPosition.y + 0.005f, Tool.transform.localPosition.z);

    }
    public void TransformOut()
    {
        Tool.transform.localPosition = new Vector3(Tool.transform.localPosition.x, Tool.transform.localPosition.y - 0.005f, Tool.transform.localPosition.z);
    }
}
