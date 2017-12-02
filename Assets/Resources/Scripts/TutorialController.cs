using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

    public void TutorialCreate(string examLoadName)
    {        
        Debug.Log("t.name");
        sp = Resources.LoadAll<Sprite>("Tutorials/" + examLoadName);
        //foreach (var t in sp) Debug.Log(t.name);
        Prime(sp);
    }

    private void Prime(Sprite[] items)
    {
        if (TutorialPageContainer.childCount > 0)
        {
            DestroyChildren(TutorialPageContainer.gameObject);
        }
        foreach (Sprite item in items)
        {
            Image page = Instantiate(TutorialPagePrefab);
            page.name = "page"+ item.name;
            page.transform.SetParent(TutorialPageContainer, false);
            page.sprite = item;
            Debug.Log(page.name);
        }
    }
    void DestroyChildren(GameObject go)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform tran in go.transform)
        {
            children.Add(tran.gameObject);
        }
        children.ForEach(child => GameObject.Destroy(child));
    }
}
