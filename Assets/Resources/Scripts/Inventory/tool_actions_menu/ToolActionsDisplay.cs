using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolActionsDisplay : MonoBehaviour {
    public Transform targetTransform;
    public ToolItemActionDisplay toolItemActionDisplayPrefab;
    public ActionSeparator actionSeparator;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Prime(TupleList<string, string> actions, ToolItem item)
    {
        foreach (var action in actions)
        {
            if (action.Item1 == "null")
            {
                ActionSeparator separator = Instantiate(actionSeparator);
                separator.transform.SetParent(targetTransform, false);
            }
            else
            {
                ToolItemActionDisplay display = Instantiate(toolItemActionDisplayPrefab);
                display.transform.SetParent(targetTransform, false);
                display.Prime(action, item);
            }
            
        }
    }
}
