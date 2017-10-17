using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
public class ExamView : MonoBehaviour {
	public Text TextName;
	public Image Sprite;

	public delegate void ExamViewDelegate(BaseExam ex);
	public static event ExamViewDelegate OnClick;

	public BaseExam exam;
	// Use this for initialization
	// ReSharper disable once UnusedMember.Local
	void Start () {
		if (exam != null) Prime(exam);
	} 

	// Update is called once per frame
	void Update () {

	}

	public void Prime(BaseExam ex)
	{
		exam = ex;
		if (TextName != null)
		{
			TextName.text = exam.Name;
		}
		// if (Sprite != null)
		//  {
		//	Sprite.sprite = ex.Sprite;
		// }
	}

	public void Click()
	{
		//Debug.Log("You clicked on " + Item.Title);
		if (OnClick != null)
		{
			OnClick.Invoke(exam);
		}
		else
		{
			Debug.Log("Nobody was listening to" + exam.Name);
		}
	}
}
