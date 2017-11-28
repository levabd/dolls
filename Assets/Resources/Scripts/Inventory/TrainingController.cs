using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TrainingController : MonoBehaviour
{
    public Transform ItemMaskTransform;
    public GameObject ItemMaskPrefab;
    public Transform ActionMaskTransform;
    public GameObject ActionMaskPrefab;
    public GameObject SeparatorPrefab;
    public Sprite spriteTrueItem;
    public Sprite spriteTrueAction;
    public List<string> listItemsG;
    public bool onr = true;
    public int stepN = 1;
    public Transform MaskDisplayTransform;
    public GameObject ItemMaskDisplayPrefab;
    public GameObject ActionMaskDisplayPrefab;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onr)
        {            
            TrainingControl(stepN);
            onr = false;
        }
    }


    public void ItemMaskCreate(int trueItemN, int listCount)
    {
        //if (GameObject.Find("Main Interface/MainToolsDisplay(Clone)"))
        //{
            MainListGenerate("itemMask", trueItemN, 0, listCount);
            PrimeTransform(ItemMaskDisplayPrefab, MaskDisplayTransform);
            //ItemMaskTransform = GameObject.Find("Main Interface/ItemMask").transform;
            SetOn(listItemsG);
       // }
       
    }

    public void ActionMaskCreate(int trueItemN,int separator, int listCount)
    {
        //if (GameObject.Find("Main Interface/ActionsDisplay(Clone)"))
        //{
            MainListGenerate("actionMask", trueItemN, separator, listCount);
            PrimeTransform(ActionMaskDisplayPrefab, MaskDisplayTransform);
            //ActionMaskTransform = GameObject.Find("Main Interface/ActionMask").transform;
            SetOn(listItemsG);
        // }
    }

    public void TrainingControl(int step)
    {
        switch (step)
        {
            case 1:
                ItemMaskCreate(1, 9);
                break;
            case 2:
                DestroyChildren(GameObject.Find("Main Interface/ItemMask"));
                ItemMaskCreate(0, 9);
                ActionMaskCreate(1, 0, 2);
                Debug.Log("ActionCreate True");
                break;
            case 3:
                DestroyChildren(GameObject.Find("Main Interface/ItemMask"));
                DestroyChildren(GameObject.Find("Main Interface/ActionMask"));
                ItemMaskCreate(3, 9);
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;


            default:
                break;
        }
    }


    public void DestroyChildren(GameObject go)
    {

        List<GameObject> children = new List<GameObject>();
        foreach (Transform tran in go.transform)
        {
            children.Add(tran.gameObject);
        }
        children.ForEach(child => GameObject.Destroy(child));
    }

    public void StepCounter()
    {
        stepN = stepN + 1;

        Debug.Log("Next step = " + stepN);
    }


    public void SetAc()
    {
        StepCounter();
        onr = true;        
    }

    public void MainListGenerate(string itemMask, int itemN = 0, int separator = 0, int listCount = 8)
    {
        listItemsG = new List<string>();
        for (int i = 0; i < (separator != 0 ? listCount - 2: listCount - 1); i++)
        {
            listItemsG.Add(itemMask);
        }
        if (itemN != 0)
        {
            if (itemMask.Contains("item"))
            {
                listItemsG.Insert(itemN - 1, "trueItem");
            }
            if (itemMask.Contains("action"))
            {
                listItemsG.Insert(itemN - 1, "trueAction");
            }
        }
        else
        {
            listItemsG.Add(itemMask);
        }       
        if (separator != 0)
        {
            listItemsG.Insert(separator-1, "Separator");
        }
    }
    

    public void SetOn(List<string> items) {
        foreach (var item in items)
        {
            if (item.Contains("item"))
            {
                Prime(ItemMaskPrefab, GameObject.Find("Main Interface/ItemMask").transform, "Item");
            }
            if (item.Contains("action"))
            {
                Prime(ActionMaskPrefab, GameObject.Find("Main Interface/ActionMask").transform, "Action");
            }
            if (item.Contains("trueItem"))
            {
                Prime(ItemMaskPrefab, GameObject.Find("Main Interface/ItemMask").transform, "trueItem");
            }
            if (item.Contains("trueAction"))
            {
                Prime(ActionMaskPrefab, GameObject.Find("Main Interface/ActionMask").transform, "trueAction");
            }
            if (item.Contains("Separator"))
            {
                Prime(SeparatorPrefab, GameObject.Find("Main Interface/ActionMask").transform, "Separator");
            }
        }
        
    }

    private void Prime(GameObject prefab, Transform transform, string itemName)
    {         
         GameObject Mask = Instantiate(prefab);
         Mask.transform.SetParent(transform, false);
            Mask.name = itemName;
         if (itemName.Contains("true"))
         {
            Mask.GetComponent<Button>().interactable = true;
            Mask.GetComponent<Button>().onClick.AddListener(SetAc);
            Mask.GetComponent<Image>().raycastTarget = false;
            if (itemName.Contains("item"))
            {
                Mask.GetComponent<Image>().sprite = spriteTrueItem;
            }
            else
            {
                Mask.GetComponent<Image>().sprite = spriteTrueAction;
            }            
         }
    }

    private void PrimeTransform(GameObject prefab, Transform transform)
    {
        GameObject transformGO = Instantiate(prefab);
        transformGO.transform.SetParent(transform, false);
        transformGO.name = prefab.name;
    }
}
