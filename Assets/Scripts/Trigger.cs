using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	public bool active = false;
	SpriteRenderer renderer;
	void Start () {
		renderer = GetComponent<SpriteRenderer>();
	}
	
	void FixedUpdate () {
		active = Physics2D.OverlapCircle(transform.position, 0.2f);

		renderer.color = active ? Color.green : Color.red;

	}
}
