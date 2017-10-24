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
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (activeControl == true)
        {
            LogActionUpdate();
        }
    }

    public void LogActionCreate(bool activeControl, string actionLogText)
    {

        this.activeControl = activeControl;
        actionLog = actionLogText;

    }

    void LogActionUpdate()
    {
        Text mainLogItem = Instantiate(mainLogItemPrefab);
        mainLogItem.transform.SetParent(mainLogDisplay.transform, false);
        Debug.Log(actionLog);
        mainLogItem.text = actionLog;
        activeControl = false;
    }
}
