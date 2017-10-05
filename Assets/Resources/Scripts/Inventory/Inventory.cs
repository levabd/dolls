using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class Inventory : MonoBehaviour {
    public List<ToolItem> Items = new List<ToolItem>();
    public Transform TargetTransform;
    public InventoryDisplay InventoryDisplayPrefab;
    public Dictionary<string, string> InventoryTools = new Exam1().InventoryTool;
   
    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    void Start () {

        AddItemsFromExamToList();
        InventoryDisplay inventory = Instantiate(InventoryDisplayPrefab);
        inventory.transform.SetParent(TargetTransform, false);
        inventory.Prime(Items);

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void AddItemsFromExamToList() {
        foreach (KeyValuePair<string, string> examTool in InventoryTools)
        {
            // Debug.Log("Key = {0}, Value = {1}"+ examTool.Key + examTool.Value);
            //Debug.Log("Create new ToolItem from Prefabs/Tools/" + examTool.Key);

            GameObject tool = (GameObject)Instantiate(Resources.Load("Prefabs/Tools/" + examTool.Key));
            tool.name = string.Format(examTool.Key);

           // Debug.Log(tool.name);

            ToolItem it = tool.GetComponent<ToolItem>();
            Items.Add(it);
        }
    }

}
