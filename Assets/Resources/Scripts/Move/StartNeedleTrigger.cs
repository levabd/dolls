using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartNeedleTrigger : MonoBehaviour {



    private MockExam Check;
    private bool CheckObject;
    private ToolItem tool;
    public GameObject MainToolObject;
    private string errorMessage;

    void Start () {
        Check = new MockExam();
        tool = MainToolObject.GetComponent<ToolItem>();
    }

	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        CheckObject = Check.Move(ref tool, col.gameObject.tag, out errorMessage);
        if (CheckObject == false)
        {
            print(errorMessage);
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
