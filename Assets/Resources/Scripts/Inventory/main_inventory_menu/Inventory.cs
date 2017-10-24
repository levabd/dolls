using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class Inventory : MonoBehaviour {
    public List<ToolItem> Items = new List<ToolItem>();
    public Transform TargetTransform;
    public InventoryDisplay InventoryDisplayPrefab;
    public Dictionary<string, string> InventoryTools;
    // Use in production 
    // public Dictionary<string, string> InventoryTools = CurrentExam.Instance.Exam.InventoryTool;

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    void Start () {
        CurrentExam.Instance.Exam = new Exam1(); // Delete in production
        InventoryTools = CurrentExam.Instance.Exam.InventoryTool; // Delete in production
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
            // Debug.Log("Key = {0}, Value = {1}"+ examTool.Key + examTool.Value);
            //Debug.Log("Create new ToolItem from Prefabs/Tools/" + examTool.Key);

            GameObject tool = (GameObject)Instantiate(Resources.Load("Prefabs/Tools/" + examTool.Key));
            tool.name = examTool.Key.ToString();
                //string.Format(examTool.Key);
            

           // Debug.Log(tool.name);

            ToolItem it = tool.GetComponent<ToolItem>();
            it.Title = examTool.Value;
            Items.Add(it);
        }
    }

}
