using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ActionMenu : MonoBehaviour {
    public TupleList<string, string> actions;
    public Transform mainTargetTransform;
    public ToolActionsDisplay toolActDPrefab;
    public ToolItem item;
    public bool isCreate = false;
    // Use this for initialization
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (isCreate)
        {
            CreateActionMenu(item);
        }
        
    }

    public void CreateActionMenu(ToolItem item)
    {
        actions = new Exam1().ToolActions(item);
        //Debug.Log(actions);

        if (toolActDPrefab != null)
        {
            Destroy(GameObject.Find("ActionsDisplay(Clone)"));
            ToolActionsDisplay actionMenu = Instantiate(toolActDPrefab) as ToolActionsDisplay;
            actionMenu.transform.SetParent(mainTargetTransform, false);
            actionMenu.Prime(actions);
            isCreate = false;
        }
    }

    }
