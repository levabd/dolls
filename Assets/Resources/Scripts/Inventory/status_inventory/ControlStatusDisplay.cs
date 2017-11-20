using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlStatusDisplay : MonoBehaviour {
    public bool activeControl = false;
    [Header("Exam Status Panel")]
    public Text examStatus;
    [Header("ToolItem Status Panel")]
    public Text itemStatus;
    public Image itemStatusSprite;
    [Header("Needle Panel")]
    public GameObject NeedlePanel;
    [Header("Syringe Hint Panel")]
    public GameObject HintPanel;
    public Text entryAngle;
    [Header("Tip Panel")]
    public GameObject TipDisplayPrefab;
    public Transform TipTargetTransform;   

    // Use this for initialization
    void Start () {
        examStatus.text = CurrentExam.Instance.Exam.Name;
        itemStatus.text = "";
        itemStatusSprite.sprite = null;
    }
	
	// Update is called once per frame
	void Update () {

        if (activeControl == true)
        {
            ControlStatusUpdate();
        }    
	}   

    void ControlStatusUpdate()
    {        
        itemStatus.text = CurrentTool.Instance.Tool.Title;
        itemStatusSprite.gameObject.SetActive(true);
        itemStatusSprite.sprite = CurrentTool.Instance.Tool.Sprites[0];
        if (CurrentTool.Instance.Tool.name != "needle")
        {
            GameObject.Find(CurrentTool.Instance.Tool.name + "_item").GetComponentInChildren<Text>().text = CurrentTool.Instance.Tool.Title;
            GameObject.Find(CurrentTool.Instance.Tool.name + "_item/Image").GetComponentInChildren<Image>().sprite = CurrentTool.Instance.Tool.Sprites[0];
        }
            
        activeControl = false;
    }

    public void TipMessage(string tipMessage) {
        GameObject tipDisplay = Instantiate(TipDisplayPrefab);
        tipDisplay.name = "Tip";
        tipDisplay.transform.SetParent(TipTargetTransform, false);
        tipDisplay.GetComponentInChildren<Text>().text = tipMessage;
        Destroy(tipDisplay, 5f);
    }
}
