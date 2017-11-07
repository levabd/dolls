using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartNeedleTrigger : MonoBehaviour {



    private bool CheckObject;
    public EndExamControlPanel examControl;
    //public GameObject MainToolObject;
    private string errorMessage;
    public ToolItemActionResponder TIAR;

    void Start () {
       
        //tool = MainToolObject.GetComponent<ToolItem>();
    }

	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {        
        CheckObject = CurrentExam.Instance.Exam.Move(col.gameObject.tag, out errorMessage);
        TIAR.colliderHit = col.gameObject;
        if (CheckObject == false)
        {
            examControl.EndExam(false, errorMessage);
        }
        if (col.gameObject.tag.Contains("vein"))
        {
            TIAR.MainLoglogCtrl.LogActionCreate(TIAR.ActionCtrl, TIAR.ActionCtrl, "Ила вошла в вену");
        }        
        print("куда-то вошла игла " + col.gameObject.tag);
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
