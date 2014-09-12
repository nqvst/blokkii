using UnityEngine;
using System.Collections;

public class OverlapCheck : MonoBehaviour {

	public bool overlaping = false;
	PlaceHolder placeHolder;
	[SerializeField] LayerMask whatIsGround;

	void Start () {

		placeHolder = transform.parent.GetComponent<PlaceHolder>();
	}

	void FixedUpdate() {
		Collider2D overlap = Physics2D.OverlapCircle(transform.position, 0.3f , whatIsGround);

		overlaping = overlap != null;

		if(overlaping && overlap.transform.CompareTag("BuildBox")) {
			placeHolder.currentBox = overlap.transform;
		} else {
			placeHolder.currentBox = null;

		}
	}
			
	void HandleOverlap(Transform tr) {

		if( tr != transform.parent )
			overlaping = true;

		if( tr.CompareTag("BuildBox") )
			placeHolder.currentBox = tr;
	}

	void OnBoxRemoved(){
		overlaping = false;
		placeHolder.currentBox = null;
	}
}
