using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartNeedleTrigger : MonoBehaviour {



    private bool CheckObject;
    public EndExamControlPanel examControl;
    //public GameObject MainToolObject;
    private string errorMessage;
    private string tipMessage;
    public ToolItemActionResponder TIAR;
    private bool vein;

    void Start () {
       
        //tool = MainToolObject.GetComponent<ToolItem>();
    }

	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {        
        CheckObject = CurrentExam.Instance.Exam.Move(col.gameObject.tag, out errorMessage, out tipMessage);
        TIAR.colliderHit = col.gameObject;
        if (CheckObject == false)
        {
            examControl.EndExam(false, errorMessage);
        }
        if (!vein)
        {
            if (col.gameObject.tag.Contains("vein") || col.gameObject.tag.Contains("vien"))
            {
                TIAR.needleInVein = true;
                TIAR.MainLoglogCtrl.LogActionCreate(TIAR.ActionCtrl, TIAR.ActionCtrl, "Игла вошла в вену");
                vein = true;
            }        
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
