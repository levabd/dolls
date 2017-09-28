using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour {
    public Transform targetTransform;
    public InventoryItemDisplay itemDisplayPrefab;
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Prime(List<ToolItem> items)
    {
        foreach (ToolItem item in items)
        {
            InventoryItemDisplay display = (InventoryItemDisplay)Instantiate(itemDisplayPrefab);
            display.transform.SetParent(targetTransform, false);
            display.Prime(item);    

        }
    }
}
