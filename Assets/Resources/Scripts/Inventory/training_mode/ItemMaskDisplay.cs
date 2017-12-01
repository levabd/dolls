using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemMaskDisplay : MonoBehaviour
{
    public Transform ParentTransform;
    public ItemMask ItemMaskPrefab;
    public Sprite spriteTrueItem;
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
        ItemMask Mask = Instantiate(ItemMaskPrefab);
        Mask.transform.SetParent(ParentTransform, false);
        if (itemName.Contains("true"))
        {
            Mask.GetComponent<Button>().interactable = true;
            Mask.GetComponent<Image>().raycastTarget = false;
            Mask.GetComponent<Image>().sprite = spriteTrueItem;
            Mask.GetComponent<Image>().fillAmount = 0.05f;
        }
        Mask.name = itemName;
    }
}
