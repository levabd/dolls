using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PositionPieceBody : MonoBehaviour {


    public GameObject cameraPosition;
    public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    private bool step1 = true, step2 = false;
    private MockExam Check;
    private bool CheckPosition;
    public ToolItem tool;
    private string errorMessage;
    

    void Start () {
        Check = new MockExam();
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
	

	void Update () {
        if (Input.GetMouseButtonDown(0) && step1 == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                CheckPosition = Check.Move(ref tool, hit.transform.gameObject.tag, out errorMessage);
                
                Cursor.SetCursor(null, hotSpot, cursorMode);
                
                if (CheckPosition)
                {
                    if (cameraPosition.transform.position != Camera.main.transform.position)
                    {
                        step2 = true;
                    }
                }
                else
                {
                    print(errorMessage);

                }

                step1 = false;
            }
        }
        else if (step2 == true)
        {

            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, cameraPosition.transform.position, 1f);
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(Camera.main.transform.eulerAngles, cameraPosition.transform.eulerAngles, 1f);
            if (Camera.main.transform.position == cameraPosition.transform.position && Camera.main.transform.eulerAngles == cameraPosition.transform.eulerAngles)
            {
                step2 = false;
            }
            
        }

    }
}
