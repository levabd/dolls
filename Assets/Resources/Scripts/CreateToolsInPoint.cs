using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateToolsInPoint : MonoBehaviour {
    public GameObject myPrefab;   
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.name == "body")
                    Instantiate(myPrefab, hit.point, Quaternion.identity);
                myPrefab.transform.LookAt(hit.transform);
                
            }
        }
    }
}
