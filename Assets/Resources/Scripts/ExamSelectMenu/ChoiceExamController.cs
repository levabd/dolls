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
		if (exam.Name.Contains("№1")) {
			Debug.Log ("Loading Scene #1");
			//Application.LoadLevel (1);
		} else 
			if (exam.Name.Contains("№2")) {
				Debug.Log ("Loading Scene #2");
		}
		action = false;
	}
}
