using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class ItemCreator : MonoBehaviour {
    public List<string> Items = new List<string>();
    public  Dictionary<string, string> InventoryTools = new Exam1().InventoryTool;

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    void Start () {
        foreach (KeyValuePair<string, string> examTool in InventoryTools)
        {
           // Debug.Log("Key = {0}, Value = {1}"+ examTool.Key + examTool.Value);
            Items.Add(examTool.Key);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
