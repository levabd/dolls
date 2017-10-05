using UnityEngine;

// ReSharper disable once CheckNamespace
public class InventoryItemResponder : MonoBehaviour {
    public ActionMenu actionMenu;
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
        //Debug.Log("You clicked on " + item.CodeName);
        actionMenu.item = item;
        actionMenu.isCreate = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
