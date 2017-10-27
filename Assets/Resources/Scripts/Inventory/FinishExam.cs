﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishExam : MonoBehaviour {
    public EndExamControlPanel examControl;
    private bool Finished;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
                
	}

    public void FinishExamClick()
    {
        foreach (var item in CurrentExam.Instance.Exam.TakenSteps)
        {
            Debug.Log(item.Item1);
            Debug.Log(item.Item2);
        }

        Finished = CurrentExam.Instance.Exam.Finish();
        if (Finished == true)
        {
            examControl.EndExam(Finished, "Поздравляем с успешным завершением");
        }
        else
        {
            examControl.EndExam(Finished, "В ходе экзамена были допущены ошибки");
        }
                
    }
}