using UnityEngine;
using System.Collections;

public class PlaceHolder : MonoBehaviour {

	public bool canBuild = false;
	public bool canRemove = false;

	public bool overlaping = false;
	public Transform currentBox;
	[SerializeField] LayerMask whatIsGround;
	[SerializeField] SpriteRenderer removeIndicator;

	SpriteRenderer spriteRenderer;

	OverlapCheck innerCheck;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		innerCheck = transform.FindChild("InnerOverlapCheck").GetComponent<OverlapCheck>();
	}

	void FixedUpdate() {
		overlaping = Physics2D.OverlapCircle(transform.position, 0.6f , whatIsGround);
	}
	
	void Update () {

		canBuild = !innerCheck.overlaping && overlaping;
		Color targetColor = Color.red;
		targetColor = canBuild ? Color.green : Color.red;

		spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, 0.1f);
		canRemove = innerCheck.overlaping;

		Color removeIndicatorColor;

		if(canRemove && currentBox) {
			removeIndicatorColor = new Color(1,1,1,0.7f);
		} else {
			removeIndicatorColor = new Color(1,1,1,0);
		}

		removeIndicator.color = Color.Lerp(removeIndicator.color, removeIndicatorColor, 0.1f);

	}

//	void OnTriggerEnter2D( Collider2D other ){
//		if (!transform.CompareTag("Player"))
//			overlapping = true;
//	}
//
//	void OnTriggerStay2D( Collider2D other ){
//		if (!transform.CompareTag("Player"))
//			overlapping = true;
//	}
//	void OnTriggerExit2D( Collider2D other ) {
//		if (!transform.CompareTag("Player"))
//			overlapping = false;
//	}
}
