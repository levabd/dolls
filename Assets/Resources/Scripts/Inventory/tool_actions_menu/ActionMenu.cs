using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ActionMenu : MonoBehaviour {
    public TupleList<string, string> actions;
    public Transform mainTargetTransform;
    public ToolActionsDisplay toolActDPrefab;
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
            CreateActionMenu(CurrentTool.Instance.Tool);
        }
        
    }

    public void CreateActionMenu(ToolItem item)
    {
        actions = CurrentExam.Instance.Exam.ToolActions(item);

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
