using UnityEngine;

// ReSharper disable once CheckNamespace
public class InventoryItemResponder : MonoBehaviour {
    public ActionMenu actionMenu;
    public ControlStatusDisplay ctrlStat;
    public TrainingController TC;
    public bool TrainingMode = false;
    // Use this for initialization
    void Start () {
        InventoryItemDisplay.OnClick += HandleonClick;
	}

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    void OnDestroy()
    {
        //Debug.Log("Unsigned-up for onClick");
        InventoryItemDisplay.OnClick -= HandleonClick;
    }

    void HandleonClick(ToolItem item)
    {        
        CurrentTool.Instance.Tool = item;

        actionMenu.isCreate = true;
        
        string examName = CurrentExam.Instance.Exam.Name;

        ctrlStat.activeControl = true;
        if (TrainingMode)
        {
            TC.IsActive();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
