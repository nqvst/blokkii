using UnityEngine;
using System.Collections;

public class OverlapCheck : MonoBehaviour {

	public bool overlaping = false;
	PlaceHolder placeHolder;

	void Start () {
		placeHolder = transform.parent.GetComponent<PlaceHolder>();
	}

	void OnTriggerEnter2D( Collider2D other ) {
		HandleOverlap(other.transform);
	}
	
	void OnTriggerStay2D( Collider2D other ) {
		HandleOverlap(other.transform);
	}

	void OnTriggerExit2D( Collider2D other ) { 
		if(other.transform != transform.parent)
			overlaping = false;

		if( other.transform.CompareTag("BuildBox") ) {
			placeHolder.currentBox = null;
			Debug.Log(placeHolder.currentBox);
		}
	}
		
	void HandleOverlap(Transform tr) {
		if( tr != transform.parent )
			overlaping = true;

		if( tr.CompareTag("BuildBox") )
			placeHolder.currentBox = tr;
	}

}
