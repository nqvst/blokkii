using UnityEngine;
using System.Collections;

public class EyeFollow : MonoBehaviour {

	[SerializeField] Transform target;
	[SerializeField] float damping = 1;

	void Update () {
		if ( target.position != transform.position )
		{
			transform.position = Vector2.Lerp(transform.position, target.position, Time.deltaTime * damping);
		}
	}
}
