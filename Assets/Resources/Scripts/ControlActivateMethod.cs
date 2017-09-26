using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlActivateMethod: MonoBehaviour {

    public GameObject ItemPanel;
    public Text StatusBarText;
    


    public void Start()
    {
        ItemPanel.SetActive(false);
    }

    public void ControlItems(string ActivateNewMethod)
    {
       switch (ActivateNewMethod)
        {
            case "Syringe":                
                Debug.Log("Панель шприца");              
                break;
            case "Catheter":
                //statusItem = "Катетер";
                Debug.Log("Панель катетера");
                break;
        }
    }

   

    public void ItemPanelActive (GameObject obj)    {
        
        obj.SetActive(true);
    }
    public void ChoseNeedle(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void OpenCatheterPanel(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void CloseCatheterPanel(GameObject obj)
    {
        obj.SetActive(true);
    }
}
