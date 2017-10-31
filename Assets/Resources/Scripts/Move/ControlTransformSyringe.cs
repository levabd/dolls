using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTransformSyringe : MonoBehaviour {

    // Use this for initialization

    private GameObject Syringe;
    private GameObject SyringeModel;
    public Text ToolAngels;
    [Header("Поворот в градусах")]
    public int stateParamRotate;
    [Header("Условная еденица поворота в Unity")]
    public float unitRotate;
    [Header("Ось координат поворота инструмента (x, y, z)")]
    public string coordinateAxis;
    private int toolAngel = 0;
    private int stepCounter = 0;
    public bool EndNeedleInCollider = false;
    public ToolItemActionResponder TIAR;

    void Start ()
    {
        Syringe = this.gameObject;
        SyringeModel = this.gameObject.transform.GetChild(0).transform.gameObject;
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A) && stepCounter == 0 && toolAngel != 80)
        {
            IncreaseAngle();
            print($"{CurrentTool.Instance.Tool.StateParams["entry_angle"]}");
            TIAR.CreateLogEntry();
        }
        if (Input.GetKeyDown(KeyCode.D) && stepCounter == 0 && toolAngel != -10)
        {
            ReduceAngle();
            print($"{CurrentTool.Instance.Tool.StateParams["entry_angle"]}");
            TIAR.CreateLogEntry();
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
        switch (coordinateAxis)
        {
            case "x":
                Syringe.transform.localEulerAngles = new Vector3(Syringe.transform.localEulerAngles.x + unitRotate, Syringe.transform.localEulerAngles.y, Syringe.transform.localEulerAngles.z);
                break;
            case "y":
                Syringe.transform.localEulerAngles = new Vector3(Syringe.transform.localEulerAngles.x, Syringe.transform.localEulerAngles.y + unitRotate, Syringe.transform.localEulerAngles.z);
                break;
            case "z":
                Syringe.transform.localEulerAngles = new Vector3(Syringe.transform.localEulerAngles.x, Syringe.transform.localEulerAngles.y, Syringe.transform.localEulerAngles.z + unitRotate);
                break;
            default:
                break;
        }
        //Syringe.transform.localEulerAngles = new Vector3(Syringe.transform.localEulerAngles.x, Syringe.transform.localEulerAngles.y + 5f, Syringe.transform.localEulerAngles.z);
        toolAngel += stateParamRotate;
        CurrentTool.Instance.Tool.StateParams["entry_angle"] = System.Convert.ToString(toolAngel);
    }

    public void ReduceAngle()
    {
        switch (coordinateAxis)
        {
            case "x":
                Syringe.transform.localEulerAngles = new Vector3(Syringe.transform.localEulerAngles.x - unitRotate, Syringe.transform.localEulerAngles.y, Syringe.transform.localEulerAngles.z);
                break;
            case "y":
                Syringe.transform.localEulerAngles = new Vector3(Syringe.transform.localEulerAngles.x, Syringe.transform.localEulerAngles.y - unitRotate, Syringe.transform.localEulerAngles.z);
                break;
            case "z":
                Syringe.transform.localEulerAngles = new Vector3(Syringe.transform.localEulerAngles.x, Syringe.transform.localEulerAngles.y, Syringe.transform.localEulerAngles.z - unitRotate);
                break;
            default:
                break;
        }
        //Syringe.transform.localEulerAngles = new Vector3(Syringe.transform.localEulerAngles.x, Syringe.transform.localEulerAngles.y - 5f, Syringe.transform.localEulerAngles.z);
        toolAngel -= stateParamRotate;
        CurrentTool.Instance.Tool.StateParams["entry_angle"] = System.Convert.ToString(toolAngel);

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
