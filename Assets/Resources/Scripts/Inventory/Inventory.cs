using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public List<ToolItem> items = new List<ToolItem>();
    public InventoryDisplay inventoryDisplayPrefab;
    public Dictionary<string, string> InventoryTools = new Exam1().InventoryTool;
    // Use this for initialization
    void Start () {

        AddItemsFromExamToList();
        InventoryDisplay inventory = (InventoryDisplay)Instantiate(inventoryDisplayPrefab);
        inventory.Prime(items);

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void AddItemsFromExamToList() {
        foreach (KeyValuePair<string, string> examTool in InventoryTools)
        {// Debug.Log("Key = {0}, Value = {1}"+ examTool.Key + examTool.Value);

            Debug.Log("Prefabs/Tools/" + examTool.Key);
            GameObject tool = (GameObject)Instantiate(Resources.Load("Prefabs/Tools/" + examTool.Key));
            tool.name = string.Format(examTool.Key);
            Debug.Log(tool.name);
            ToolItem it = tool.GetComponent<ToolItem>();
            items.Add(it);
        }
    }

}
