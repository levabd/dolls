using UnityEngine;
using System.Collections;

public class miHeadLook : MonoBehaviour {
	
	public HeadLookController headLook;

	// Update is called once per frame
	void LateUpdate () {
		
		
		headLook.target = transform.position;
	
	}
}
