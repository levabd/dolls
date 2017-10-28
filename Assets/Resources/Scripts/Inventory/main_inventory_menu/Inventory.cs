using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class Inventory : MonoBehaviour {
    public List<ToolItem> Items = new List<ToolItem>();
    public Transform TargetTransform;
    public InventoryDisplay InventoryDisplayPrefab; 
    //public Dictionary<string, string> InventoryTools; // Delete in production

    // Use in production 
     public Dictionary<string, string> InventoryTools = CurrentExam.Instance.Exam.InventoryTool;

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    void Start () {
        //CurrentExam.Instance.Exam = new Exam5(); // Delete in production
        //InventoryTools = CurrentExam.Instance.Exam.InventoryTool; // Delete in production
        AddItemsFromExamToList();
        InventoryDisplay inventory = Instantiate(InventoryDisplayPrefab);
        inventory.transform.SetParent(TargetTransform, false);
        inventory.Prime(Items);
        
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    void AddItemsFromExamToList()
    {
        foreach (KeyValuePair<string, string> examTool in InventoryTools)
        {
            GameObject tool = (GameObject)Instantiate(Resources.Load("Prefabs/Tools/" + examTool.Key));
            tool.name = examTool.Key.ToString();
            ToolItem item = tool.GetComponent<ToolItem>();
            item.Title = examTool.Value;
            Items.Add(item);
        }
    }

}
