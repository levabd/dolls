using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceExamController : MonoBehaviour {
	public bool action = false;
	public BaseExam exam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (action)
		{
			CheckAction();
		}
	}


	public void ExamControl(bool action, ref BaseExam baseExam)
	{
		exam = baseExam;
		this.action = action;
	}

	private void CheckAction()
	{
	    Debug.Log("Loading Scene " + exam.Name);
	    CurrentExam.Instance.Exam = exam;
#pragma warning disable 618
        Application.LoadLevel(exam.LoadName);
#pragma warning restore 618
		action = false;
	}
}
