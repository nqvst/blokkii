using UnityEngine;
using System.Collections;

public class EyeSize : MonoBehaviour {

	Transform body;

	Vector3 scale = new Vector3(1,1,1);
	void Start () {
		body = transform.parent.parent;
	}
	

	void Update () {
		float dist = Vector2.Distance( transform.position , body.position);
		transform.localScale = new Vector3( 1 , 1.2f - dist, 1.2f - dist );
	}
}
