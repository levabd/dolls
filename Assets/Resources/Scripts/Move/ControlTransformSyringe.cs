using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTransformSyringe : MonoBehaviour {

    // Use this for initialization


    private GameObject Syringe;
    private GameObject SyringeModel;
    public Text ToolAngels;
    private int toolAngel = 0;
    private int stepCounter = 0;
    public bool EndNeedleInCollider = false;
    void Start () {
        Syringe = this.gameObject;
        SyringeModel = this.gameObject.transform.GetChild(0).transform.gameObject;

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.D) && stepCounter == 0 && toolAngel != 80)
        {
            IncreaseAngle();
            print($"{CurrentTool.Instance.Tool.StateParams["entry_age"]}");
        }
        if (Input.GetKeyDown(KeyCode.A) && stepCounter == 0 && toolAngel != -10)
        {
            ReduceAngle();
            print($"{CurrentTool.Instance.Tool.StateParams["entry_age"]}");
        }
        if (Input.GetKeyDown(KeyCode.W) && EndNeedleInCollider == false)
        {
            TransformIn();
        }
        if (Input.GetKeyDown(KeyCode.S)&& stepCounter > 0)
        {
            TransformOut();
        }

    }
    public void IncreaseAngle()
    {
        Syringe.transform.localEulerAngles = new Vector3(Syringe.transform.localEulerAngles.x, Syringe.transform.localEulerAngles.y + 5f, Syringe.transform.localEulerAngles.z);
        toolAngel += 10;
        CurrentTool.Instance.Tool.StateParams["entry_age"] = System.Convert.ToString(toolAngel);

    }
    public void ReduceAngle()
    {
        Syringe.transform.localEulerAngles = new Vector3(Syringe.transform.localEulerAngles.x, Syringe.transform.localEulerAngles.y - 5f, Syringe.transform.localEulerAngles.z);
        toolAngel -= 10;
        CurrentTool.Instance.Tool.StateParams["entry_age"] = System.Convert.ToString(toolAngel);

    }
    public void TransformIn()
    {
        SyringeModel.transform.localPosition = new Vector3(SyringeModel.transform.localPosition.x, SyringeModel.transform.localPosition.y, SyringeModel.transform.localPosition.z + 0.005f);
        stepCounter++;

    }
    public void TransformOut()
    {
        SyringeModel.transform.localPosition = new Vector3(SyringeModel.transform.localPosition.x, SyringeModel.transform.localPosition.y, SyringeModel.transform.localPosition.z - 0.005f);
        stepCounter--;
    }
}
