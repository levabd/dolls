using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour {
    public Text textName;
    public Image sprite;

    public delegate void InventoryItemDisplayDelegate(ToolItem item);
    public static event InventoryItemDisplayDelegate onClick;

    public ToolItem item;
	// Use this for initialization
	void Start () {
        if (item != null) Prime(item);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Prime(ToolItem item)
    {
        this.item = item;
        if (textName != null)
        {
            textName.text = item.title;
        }
        if (sprite != null)
        {
            sprite.sprite = item.sprite;
        }
    }

    public void Click()
    {
        Debug.Log("Clicked" + item.title);
        if (onClick != null)
        {
            onClick.Invoke(item);
        }
        else
        {
            Debug.Log("Nobody was listening to" + item.title);
        }
    }
}
