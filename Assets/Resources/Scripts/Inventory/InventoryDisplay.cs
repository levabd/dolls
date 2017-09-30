using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class InventoryDisplay : MonoBehaviour {
    public Transform TargetTransform;
    public InventoryItemDisplay ItemDisplayPrefab;
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
            InventoryItemDisplay display = Instantiate(ItemDisplayPrefab);
            display.transform.SetParent(TargetTransform, false);
            display.Prime(item);    

        }
    }
}
