using UnityEngine;
using System.Collections;

public class LockOverlap : MonoBehaviour {

	[SerializeField] LayerMask whatToOverlap;
	[SerializeField] Transform overlapTr;
	[SerializeField] float radius = 0.1f;

	private bool overlaping = false;
	
	void FixedUpdate () {

		overlaping = false;
		
		Collider2D[] coll = new Collider2D[10];

		if( Physics2D.OverlapCircleNonAlloc( overlapTr.position, radius , coll, whatToOverlap ) > 0)
		{
			foreach(Collider2D c in coll)
			{
				if(c) 
				{
					overlaping = true;
				}
			}
		}

		GetComponent<SpriteRenderer>().enabled = overlaping && transform.parent.rigidbody2D.isKinematic;
	}
}
