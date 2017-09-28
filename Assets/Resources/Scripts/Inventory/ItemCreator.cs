using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreator : MonoBehaviour {
    public List<string> items = new List<string>();
    public  Dictionary<string, string> InventoryTools = new Exam1().InventoryTool;

    // Use this for initialization
    void Start () {
        foreach (KeyValuePair<string, string> examTool in InventoryTools)
        {
           // Debug.Log("Key = {0}, Value = {1}"+ examTool.Key + examTool.Value);
            items.Add(examTool.Key);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
