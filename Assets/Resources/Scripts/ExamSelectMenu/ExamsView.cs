using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamsView : MonoBehaviour {

	public Transform TargetTransform;
	public ExamView ExamViewPrefab;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void Prime(List<BaseExam> exams)
	{
		foreach (BaseExam exam in exams)
		{
			ExamView display = Instantiate(ExamViewPrefab);
			//display.name = exam.name+"_item";
			display.transform.SetParent(TargetTransform, false);
			display.Prime(exam);    

		}
	}
}
