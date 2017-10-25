using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlStatusDisplay : MonoBehaviour {
    [Header("Exam Status Panel")]
    public Text examStatus;
    [Header("ToolItem Status Panel")]
    public Text itemStatus;
    public Image itemStatusSprite;    
    [Header("ToolAction Status Panel")]
    public Text ActionStatus;
    [Header("Error Display Panel")]
    public Text errorStatus;

    public bool activeControl = false;

    private string examName;
    private ToolItem item;
    private string actionName;
    private string errorMessage;

    // Use this for initialization
    void Start () {
        examStatus.text = "";
        itemStatus.text = "";
        itemStatusSprite.sprite = null;
        ActionStatus.text = "";
        errorStatus.text = "";
    }
	
	// Update is called once per frame
	void Update () {

        if (activeControl == true)
        {
            ControlStatusUpdate();
        }    
	}
    public void ControlStatus(bool activeControl, string examName,  string actionName, string errorMessage)
    {
        this.examName = examName;       
        this.actionName = actionName;
        this.errorMessage = errorMessage;
        this.activeControl = activeControl;
    }

    void ControlStatusUpdate()
    {
        examStatus.text = examName;
        itemStatus.text = CurrentTool.Instance.Tool.Title;
        itemStatusSprite.gameObject.SetActive(true);
        itemStatusSprite.sprite = CurrentTool.Instance.Tool.Sprites[0];
        ActionStatus.text = actionName;
        errorStatus.text = errorMessage;
        activeControl = false;
    }
}
