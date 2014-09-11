using UnityEngine;
using System.Collections;

public class PlaceHolder : MonoBehaviour {

	public bool canBuild = false;
	public bool canRemove = false;

	public bool overlapping = false;
	public Transform currentBox;

	SpriteRenderer spriteRenderer;

	OverlapCheck innerCheck;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		innerCheck = transform.FindChild("InnerOverlapCheck").GetComponent<OverlapCheck>();
	}
	
	void Update () {

		canBuild = !innerCheck.overlaping && overlapping;
		Color targetColor = Color.red;
		targetColor = canBuild ? Color.green : Color.red;

		spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, 0.1f);
		canRemove = innerCheck.overlaping;

	}

	void OnTriggerEnter2D( Collider2D other ){
		if (!transform.CompareTag("Player"))
			overlapping = true;
	}

	void OnTriggerStay2D( Collider2D other ){
		if (!transform.CompareTag("Player"))
			overlapping = true;
	}
	void OnTriggerExit2D( Collider2D other ) {
		if (!transform.CompareTag("Player"))
			overlapping = false;
	}
}
