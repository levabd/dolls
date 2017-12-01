using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceExamController : MonoBehaviour {
	public bool action = false;
	public BaseExam exam;
    public AsyncOperation async;
    public GameObject loader;
    SceneListCheck sceneListCheck;
    private float load = 0;
    private bool activeLoader;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (activeLoader)
        {
            StartCoroutine(CounterLoader());
        }
        if (action)
		{
            loader.SetActive(true);
            activeLoader = true;
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

        SceneListCheck sceneListCheck = new SceneListCheck();
        if (sceneListCheck.Has(exam.LoadName))
        {
            Debug.Log("Loading Scene " + exam.Name);
            CurrentExam.Instance.Exam = null;
            CurrentExam.Instance.Exam = exam;
#pragma warning disable 618

            //Application.LoadLevel(exam.LoadName);
            StartCoroutine(LoadNewScene(exam.LoadName));
#pragma warning restore 618
        }
        else
        {
            Debug.Log("Scene " + exam.Name + " not found");
        }    
		action = false;
	}

    IEnumerator LoadNewScene(string scene)
    {

        yield return new WaitForSeconds(3);

        async = Application.LoadLevelAsync(scene);

        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            Debug.Log(async.progress);
            if (load == 100)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }

    }

    IEnumerator CounterLoader()
    {
        yield return new WaitForSeconds(1);
        if (load < 100)
        {
            load += 0.5f;
            GameObject.Find("Main Interface/ProgressRadialPlain").transform.GetChild(1).GetChild(0).transform.gameObject.GetComponent<Text>().text = $"{System.Convert.ToInt32(load)}%";
        }
    }
}
