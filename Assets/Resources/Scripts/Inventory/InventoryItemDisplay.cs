﻿using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
public class InventoryItemDisplay : MonoBehaviour {
    public Text TextName;
    public Image Sprite;

    public delegate void InventoryItemDisplayDelegate(ToolItem item);
    public static event InventoryItemDisplayDelegate OnClick;

    public ToolItem Item;
	// Use this for initialization
    // ReSharper disable once UnusedMember.Local
	void Start () {
        if (Item != null) Prime(Item);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Prime(ToolItem item)
    {
        Item = item;
        if (TextName != null)
        {
            TextName.text = item.Title;
        }
        if (Sprite != null)
        {
            Sprite.sprite = item.Sprite;
        }
    }

    public void Click()
    {
        Debug.Log("Clicked" + Item.Title);
        if (OnClick != null)
        {
            OnClick.Invoke(Item);
        }
        else
        {
            Debug.Log("Nobody was listening to" + Item.Title);
        }
    }
}
