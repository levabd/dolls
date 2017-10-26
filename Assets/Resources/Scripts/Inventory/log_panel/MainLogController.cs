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
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (activeControl == true)
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
        Debug.Log(actionLog);
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
        // GameObject.Find("Scroll Rect").GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
        // int cCount = mainLogDisplay.transform.childCount;
        // int width = 932;
        // int height = ;
        //float pTop = mainLogDisplay.GetComponent<VerticalLayoutGroup>().padding.top;
        //float pBottom = mainLogDisplay.GetComponent<VerticalLayoutGroup>().padding.bottom;
        //float pSpacing = mainLogDisplay.GetComponent<VerticalLayoutGroup>().spacing;

        //mainLogDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }
}
