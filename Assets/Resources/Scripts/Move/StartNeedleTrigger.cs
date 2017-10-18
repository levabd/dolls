using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartNeedleTrigger : MonoBehaviour {



    private MockExam Check;
    private bool CheckObject;
    public ToolItem tool;
    public EndExamControlPanel examControl;
    //public GameObject MainToolObject;
    private string errorMessage;

    void Start () {
        Check = new MockExam();
        //tool = MainToolObject.GetComponent<ToolItem>();
    }

	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name != "Skin")
        {
            CheckObject = Check.Move(ref tool, col.gameObject.tag, out errorMessage);
            if (CheckObject == false)
            {
                //print(errorMessage);
                examControl.EndExam(false, errorMessage);
            }
        }
        

        print("куда-то вошла игла");
    }
    //void OnTriggerExit(Collider col)
    //{
    //    print("игла вышла");
    //}
    //void OnTriggerStay(Collider col)
    //{
    //	if (col.gameObject.name == "Needle") {
    //		print("Игла в вене");
    //	}
    //}
}
