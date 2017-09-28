using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemResponder : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InventoryItemDisplay.onClick += HandleonClick;
	}

    void onDestroy()
    {
        Debug.Log("Unsigned-up for onClick");
        InventoryItemDisplay.onClick -= HandleonClick;
    }

    void HandleonClick(ToolItem item)
    {
        Debug.Log("Test Click" + item.title);
        Debug.Log("Code name" + item.codeName);
        TupleList<string, string>  itemAct = new  Exam1().ToolActions(item);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
