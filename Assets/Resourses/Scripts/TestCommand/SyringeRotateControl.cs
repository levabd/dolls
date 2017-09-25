using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyringeRotateControl : MonoBehaviour {




    public GameObject SyringeCoordinateSystem;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

    }
    public void IncreaseAngle()
    {
        SyringeCoordinateSystem.transform.localEulerAngles = new Vector3(SyringeCoordinateSystem.transform.localEulerAngles.x, SyringeCoordinateSystem.transform.localEulerAngles.y + 5f, SyringeCoordinateSystem.transform.localEulerAngles.z);
    }
    public void ReduceAngle()
    {
        SyringeCoordinateSystem.transform.localEulerAngles = new Vector3(SyringeCoordinateSystem.transform.localEulerAngles.x, SyringeCoordinateSystem.transform.localEulerAngles.y - 5f, SyringeCoordinateSystem.transform.localEulerAngles.z);
    }
}
