using UnityEngine;
using System.Collections;

public class TutorialCreator : MonoBehaviour
{
    public Transform parentTransform;
    public GameObject TutorialContainer;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Create()
    {
        if (GameObject.Find("Main Interface/Tutorial"))
        {
            Destroy(GameObject.Find("Main Interface/Tutorial"));
        }
        GameObject tutorial = Instantiate(TutorialContainer);
        tutorial.name = TutorialContainer.name;
        tutorial.transform.SetParent(parentTransform, false);
        tutorial.transform.GetChild(0).GetComponent<TutorialController>().TutorialCreate(CurrentExam.Instance.Exam.LoadName);
    }

}
