using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolActionsDisplay : MonoBehaviour {
    public Transform targetTransform;
    public ToolItemActionDisplay toolItemActionDisplayPrefab;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Prime(TupleList<string, string> actions)
    {
        foreach (var action in actions)
        {
            ToolItemActionDisplay display = Instantiate(toolItemActionDisplayPrefab);
            display.transform.SetParent(targetTransform, false);
            display.Prime(action);
        }
    }
}
