using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolRotate : MonoBehaviour {


    public GameObject ToolPrefab;
    private GameObject Tool;
    public Text ToolAngels;
    private int toolAngel = 0;
    public ToolItem toolItem;


	// Use this for initialization
	void Start () {
        Tool = GameObject.Find(ToolPrefab.name);

        toolItem.StateParams.Add("entry_age", "0");;

	}
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
            IncreaseAngle();

            print($"{toolItem.StateParams["entry_age"]}");
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ReduceAngle();
        }
    }
    public void IncreaseAngle()
    {
        Tool.transform.localEulerAngles = new Vector3(Tool.transform.localEulerAngles.x, Tool.transform.localEulerAngles.y - 5f, Tool.transform.localEulerAngles.z );
        toolAngel += 10;
        toolItem.StateParams["entry_age"] = System.Convert.ToString(toolAngel);

    }
    public void ReduceAngle()
    {
        Tool.transform.localEulerAngles = new Vector3(Tool.transform.localEulerAngles.x, Tool.transform.localEulerAngles.y + 5f, Tool.transform.localEulerAngles.z );
        toolAngel -= 10;
        toolItem.StateParams["entry_age"] = "работает";
        
    }
}
