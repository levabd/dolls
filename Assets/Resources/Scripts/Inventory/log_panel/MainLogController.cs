using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainLogController : MonoBehaviour {
    [Header("Action Log Panel")]
    private string actionLog;
    public MainLogDisplay mainLogDisplay;
    public Text mainLogItemPrefab;
    public bool activeControl = false;
    public bool trueColor = false;
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
        if (activeControl)
        {
            LogActionUpdate();
        }
    }

    public void LogActionCreate(bool activeControl, bool whatColor, string actionLogText)
    {
        this.activeControl = activeControl;
        actionLog = actionLogText;
        trueColor = whatColor;
    }

    void LogActionUpdate()
    {        
        Text mainLogItem = Instantiate(mainLogItemPrefab);
        mainLogItem.transform.SetParent(mainLogDisplay.transform, false);
        mainLogItem.text = actionLog;
        
        if (trueColor)
        {
            mainLogItem.color = new Color(0, 255, 0); 
        }
        else
        {
            mainLogItem.color = new Color(255, 0, 0);
        }
        Canvas.ForceUpdateCanvases();
        GameObject.Find("Scroll View").GetComponent<ScrollRect>().verticalScrollbar.value = 0f;
        Canvas.ForceUpdateCanvases();

        activeControl = false;
    }
}
