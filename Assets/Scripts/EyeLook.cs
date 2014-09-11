using UnityEngine;
using System.Collections;

public class EyeLook : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
		Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mPos.z = 0;
		transform.LookAt(mPos);
	}
}
