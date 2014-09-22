using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	float lastVelocity = 0;
	[SerializeField] Transform overlapCheck;
	public bool overlaping = false;
	[SerializeField] LayerMask whatIsGround;
	public bool set = false;


	void FixedUpdate()
	{
		//Vector2 overlapPos = new Vector2(transform.position.x , transform.position.y - 0.5f);
		Collider2D coll = Physics2D.OverlapCircle(overlapCheck.position, 0.05f , whatIsGround);

		if(coll != collider2D && coll != null) {
			overlaping = true;
		}
	}

	void Update ()
	{
		if ( overlaping && !set) 
		{
			SetKinematic();
		}
	}

	void SetKinematic()
	{
		rigidbody2D.isKinematic = true;
		set = true;
		enabled = false;
	}
}
