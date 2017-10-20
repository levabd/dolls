using UnityEngine;

// ReSharper disable once CheckNamespace
public class InventoryItemResponder : MonoBehaviour {
    public ActionMenu actionMenu;
    public ControlStatusDisplay ctrlStat;
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

        string errorMessage = "";
        string examName = CurrentExam.Instance.Exam.Name;
        string actionName = "";
        bool activeControl = true;
        ctrlStat.ControlStatus(activeControl, examName, ref item, actionName, errorMessage);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
