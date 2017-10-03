using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolRotate : MonoBehaviour {


    public GameObject ToolPrefab;
    private GameObject Tool;
    public Text ToolAngels;
    private int toolAngel = 0;
    private ToolItem TI;


	// Use this for initialization
	void Start () {
        Tool = GameObject.Find(ToolPrefab.name);
        //TI = Tool.GetComponent<ToolItem>();
        Tool.GetComponent<ToolItem>().StateParams.Add("entry_age", "0");
        //TI.StateParams.Add("entry_age", "0");

	}
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
            IncreaseAngle();
            //print($"{TI.StateParams["entry_age"]}");
            print($"{Tool.GetComponent<ToolItem>().StateParams["entry_age"]}");
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ReduceAngle();
        }
    }
    public void IncreaseAngle()
    {
        Tool.transform.localEulerAngles = new Vector3(Tool.transform.localEulerAngles.x, Tool.transform.localEulerAngles.y, Tool.transform.localEulerAngles.z - 5f);
        toolAngel += 10;
        Tool.GetComponent<ToolItem>().StateParams["entry_age"] = System.Convert.ToString(toolAngel);

    }
    public void ReduceAngle()
    {
        Tool.transform.localEulerAngles = new Vector3(Tool.transform.localEulerAngles.x, Tool.transform.localEulerAngles.y, Tool.transform.localEulerAngles.z + 5f);
        toolAngel -= 10;
        Tool.GetComponent<ToolItem>().StateParams["entry_age"] = "работает";
        //TI.StateParams["entry_age"] = "работает";
    }
}
