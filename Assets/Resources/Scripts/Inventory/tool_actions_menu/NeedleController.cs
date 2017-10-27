using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NeedleController : MonoBehaviour
{
    public ToolItemActionResponder GetToolItemActionResponder;
    public ToolItem Needle;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FingerСovering()
    {
        CurrentTool.Instance.Tool = Needle;
        GetToolItemActionResponder.HandleonClick("finger_covering");
    }

    public void NeedleRemoving()
    {
        CurrentTool.Instance.Tool = Needle;
        GetToolItemActionResponder.HandleonClick("needle_removing");
        GetToolItemActionResponder.CtrlStat.NeedlePanel.SetActive(false);
    }
}





   
