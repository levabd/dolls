using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderController : MonoBehaviour {

    public bool action = false;
    public AsyncOperation async;
    public GameObject loader;
    public Text loaderText;
    public GameObject loaderImage;

    private float load = 0;
    private bool activeLoader;
    private Image mFill;
    private string examScene;
    // Use this for initialization
    void Start () {
        mFill = loaderImage.GetComponent<Image>();
        mFill.fillAmount = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {

            if (action && !activeLoader)
            {
                loader.SetActive(true);
                activeLoader = true;
                StartCoroutine(LoadNewScene(examScene));
                action = false;
            }
            if (activeLoader)
            {
                StartCoroutine(CounterLoader());
                
            }

    }

    public void ActiveLoader(string examScene)
    {
        this.examScene = examScene;
        action = true;

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
            loaderText.text = $"{System.Convert.ToInt32(load)}%";
            mFill.fillAmount = (load / 100);
        }
    }
}
