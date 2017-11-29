using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionMaskDisplay : MonoBehaviour
{
    public Transform ParentTransform;
    public ActionMask ActionMaskPrefab;
    public SeparatorMask SeparatorMaskPrefab;
    public Sprite spriteTrueAction;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Prime(string itemName)
    {       
        if (itemName.Contains("Separator"))
        {
            SeparatorMask Mask = Instantiate(SeparatorMaskPrefab);
            Mask.transform.SetParent(ParentTransform, false);
            Mask.name = itemName;
        }
        else
        {
        ActionMask Mask = Instantiate(ActionMaskPrefab);
        Mask.transform.SetParent(ParentTransform, false);
        if (itemName.Contains("true"))
        {
            Mask.GetComponent<Button>().interactable = true;
            Mask.GetComponent<Image>().raycastTarget = false;
            Mask.GetComponent<Image>().sprite = spriteTrueAction;
        }
        Mask.name = itemName;
        }
    }
}
