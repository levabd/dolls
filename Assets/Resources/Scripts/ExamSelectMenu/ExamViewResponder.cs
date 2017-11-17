using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamViewResponder : MonoBehaviour {
	public ChoiceExamController choiceExamController;
	// Use this for initialization
	void Start () {
		ExamView.OnClick += HandleonClick;
	}

	// ReSharper disable once InconsistentNaming
	// ReSharper disable once UnusedMember.Local
	void OnDestroy()
	{
		//Debug.Log("Unsigned-up for onClick");
		ExamView.OnClick -= HandleonClick;
	}

	void HandleonClick(BaseExam exam)
	{
		Debug.Log ("Clicked on " + exam.Name);
		bool action = true;
		choiceExamController.ExamControl (action, ref exam);

	}

	// Update is called once per frame
	void Update () {

	}
}
