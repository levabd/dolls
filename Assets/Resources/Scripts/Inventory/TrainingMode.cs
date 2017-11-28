using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;

public class TrainingMode : MonoBehaviour
{
    public bool start = true;
    public bool update;
    public bool defaultState;
    public Sprite spriteOnItem;
    public Sprite spriteOnAction;
    public Sprite spriteItemDefault;
    public Sprite spriteActionDefault;
    public int steps = 0;

    // Use this for initialization
    void Start()
    {

           
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void DefaultState()
    {
        IsInteractible("Main Interface/MainToolsDisplay(Clone)", true, "", false);
        IsInteractible("Main Interface/ActionsDisplay(Clone)", true, "", false);
        Debug.Log("Default set");
    }

    public void AtStart()
    {
        IsInteractible("Main Interface/MainToolsDisplay(Clone)", false, "gloves_item", true);
        IsInteractible("Main Interface/ActionsDisplay(Clone)", false, "wear_examination_action", true);
        
    }

    public void AtSteps(string step)
    {
        switch (step)
        {
            case "1":
                IsInteractible("Main Interface/MainToolsDisplay(Clone)", false, "gauze_balls_item", true);
                IsInteractible("Main Interface/ActionsDisplay(Clone)", false, "spirit_p70_action", true);

                update = false;
                break;
            case "2":
                IsInteractible("Main Interface/MainToolsDisplay(Clone)", false, "tweezers_item", true);
                IsInteractible("Main Interface/ActionsDisplay(Clone)", false, "tweezers_balls_action", true);
                update = false;
                break;
            case "3":
                IsInteractible("Main Interface/MainToolsDisplay(Clone)", false, "tweezers_item", true);
                IsInteractible("Main Interface/ActionsDisplay(Clone)", false, "top_down_action", true);
                update = false;
                break;
            case "4":
                IsInteractible("Main Interface/MainToolsDisplay(Clone)", false, "gloves_item", true);
                IsInteractible("Main Interface/ActionsDisplay(Clone)", false, "wear_sterile_action", true);
                update = false;
                break;
            case "5":
                IsInteractible("Main Interface/MainToolsDisplay(Clone)", false, "syringe_item", true);
                IsInteractible("Main Interface/ActionsDisplay(Clone)", false, "anesthesia_needle_action", true);
                update = false;
                break;
            case "6":
                IsInteractible("Main Interface/MainToolsDisplay(Clone)", false, "syringe_item", true);
                IsInteractible("Main Interface/ActionsDisplay(Clone)", false, "anesthesia_action", true);
                break;
            case "7":
                IsInteractible("Main Interface/MainToolsDisplay(Clone)", false, "syringe_item", true);
                IsInteractible("Main Interface/ActionsDisplay(Clone)", false, "a45_d10_punction_needle_action", true);
                break;
            case "8":
                IsInteractible("Main Interface/MainToolsDisplay(Clone)", false, "syringe_item", true);
                IsInteractible("Main Interface/ActionsDisplay(Clone)", false, "filling_novocaine_half_action", true);
                break;
            case "9":
                IsInteractible("Main Interface/MainToolsDisplay(Clone)", false, "tweezers_item_action", true);
                IsInteractible("Main Interface/ActionsDisplay(Clone)", false, "tweezers_balls_action", true);
                break;
            case "10":
                IsInteractible("Main Interface/MainToolsDisplay(Clone)", false, "tweezers_item_action", true);
                IsInteractible("Main Interface/ActionsDisplay(Clone)", false, "tweezers_balls_action", true);
                break;
            default:
                break;
        }
    }

    public void IsInteractible(string goPath, bool state, string goExclusion, bool spriteOn)
    {
        if (GameObject.Find(goPath))
        {
            Transform goTransform = GameObject.Find(goPath).GetComponent<Transform>();
            if (GameObject.Find(goPath).name.Contains("MainToolsDisplay"))
            {
                if (goTransform != null)
                {
                    for (int i = 0; i < goTransform.childCount; ++i)
                    {
                        if (goTransform.GetChild(i).name == goExclusion && goTransform.GetChild(i).name.Contains("item") && spriteOn)
                        {
                            goTransform.GetChild(i).GetComponent<Image>().sprite = spriteOnItem;
                        }
                        if (goTransform.GetChild(i).name == goExclusion && goTransform.GetChild(i).name.Contains("item") && !spriteOn)
                        {
                            goTransform.GetChild(i).GetComponent<Image>().sprite = spriteItemDefault;
                        }
                        if (goTransform.GetChild(i).name != goExclusion && goTransform.GetChild(i).GetComponent<Button>())
                        {
                            goTransform.GetChild(i).GetComponent<Button>().interactable = state;
                            Debug.Log("State in"+ goExclusion);
                        }
                    }
                }
            }
            if (GameObject.Find(goPath).name.Contains("ActionsDisplay"))
            {
                if (goTransform != null)
                {
                    for (int i = 0; i < goTransform.childCount; ++i)
                    {
                        if (goTransform.GetChild(i).name == goExclusion && goTransform.GetChild(i).name.Contains("action") && spriteOn)
                        {
                            goTransform.GetChild(i).GetComponent<Image>().sprite = spriteOnAction;
                        }
                        if (goTransform.GetChild(i).name == goExclusion && goTransform.GetChild(i).name.Contains("action") && !spriteOn)
                        {
                            goTransform.GetChild(i).GetComponent<Image>().sprite = spriteActionDefault;
                        }                    
                        if (goTransform.GetChild(i).name != goExclusion && goTransform.GetChild(i).GetComponent<Button>())
                        {
                            goTransform.GetChild(i).GetComponent<Button>().interactable = state;
                            Debug.Log("State in");

                            start = false;
                        }
                    }
                }
            }
           
                    //if (goTransform.GetChild(i).name == goExclusion)
                    //{
                    //        goTransform.GetChild(i).GetComponent<Image>().color = new Color32(15, 121, 195, 255); 
                    //        var colors = goTransform.GetChild(i).GetComponent<Button>().colors;
                    //        colors.normalColor = new Color32(15, 121, 195, 255);
                    //        goTransform.GetChild(i).GetComponent<Button>().colors = colors;
                    //}
                
            
        }   
    }
}
//public static class Extensions
//{
//    public static Transform Search(this Transform target, string name)
//    {
//        if (target.name == name) return target;

//        for (int i = 0; i < target.childCount; ++i)
//        {
//            var result = Search(target.GetChild(i), name);

//            if (result != null) return result;
//        }

//        return null;
//    }
//}
