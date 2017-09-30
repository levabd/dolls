using UnityEngine;

// ReSharper disable once CheckNamespace
public class InventoryItemResponder : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InventoryItemDisplay.OnClick += HandleonClick;
	}

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    void onDestroy()
    {
        Debug.Log("Unsigned-up for onClick");
        InventoryItemDisplay.OnClick -= HandleonClick;
    }

    void HandleonClick(ToolItem item)
    {
        Debug.Log("Test Click" + item.Title);
        Debug.Log("Code name" + item.CodeName);
        TupleList<string, string>  itemAct = new  Exam1().ToolActions(item);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
