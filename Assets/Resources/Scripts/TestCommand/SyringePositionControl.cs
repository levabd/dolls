using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyringePositionControl : MonoBehaviour {




    public GameObject cameraPositions2;
    public GameObject Syringe;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public GameObject colliderTrue;
    private GameObject clickCollider;
    private bool step1 = true, step2 = false;
	void Start () {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}
	
	
    
	void Update () {

        if (Input.GetMouseButtonDown(0) && step1 == true)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject.name == colliderTrue.name)
                {
                    step2 = true;
                }

            }
            if (step2 == false)
            {
                print("это провал");
            }
            Cursor.SetCursor(null, hotSpot, cursorMode);
            step1 = false;
        }
        else if (step2 == true)
        {

            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, cameraPositions2.transform.position, 1f);
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(Camera.main.transform.eulerAngles, cameraPositions2.transform.eulerAngles, 1f);
            Syringe.SetActive(true);
        }
        
	}

    
}
