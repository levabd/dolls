using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialController : MonoBehaviour
{
    public Image TutorialPagePrefab;
    public Transform TutorialPageContainer;
    private Sprite[] sp;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TutorialCreate()
    {
        //Sprite[] sp = Resources.LoadAll<Sprite>("Tutorials/Test" + CurrentExam.Instance.Exam.Name);
        sp = Resources.LoadAll<Sprite>("Tutorials/Test");
        foreach (var t in sp) Debug.Log(t.name);
        Prime(sp);
    }

    private void Prime(Sprite[] items)
    {
        foreach (Sprite item in items)
        {
            Image page = Instantiate(TutorialPagePrefab);
            page.name = "page"+ item.name;
            page.transform.SetParent(TutorialPageContainer, false);
            page.sprite = item;
            Debug.Log(page.name);
        }
    }
}
