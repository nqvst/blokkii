using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	[SerializeField] Transform overlapGround;
	[SerializeField] LockOverlap up;
	[SerializeField] LockOverlap left;
	[SerializeField] LockOverlap right;

	[SerializeField] LayerMask whatIsGround;

	[SerializeField] float radius = 0.01f;

	public bool overlaping = false;
	public bool set = false;

	public bool hardcoreMode = false;
	private Vector2 targetPos;

	private Vector2 pos;
	void Start()
	{
		hardcoreMode = true;
		pos = transform.position;
	}

	void FixedUpdate()
	{
		if(set){return;}
		overlaping = false;

		Collider2D[] coll = new Collider2D[10];
		if( Physics2D.OverlapCircleNonAlloc( overlapGround.position, radius , coll, whatIsGround ) > 0)
		{
			foreach(Collider2D c in coll)
			{
				if(c && c != collider2D ) 
				{
					overlaping = true;
//					if(!set){
//						SetKinematic();
//					}
				}
			}
		}

		if(!rigidbody2D.isKinematic && (up.overlaping || left.overlaping || right.overlaping || overlaping) ){
			SetKinematic();
		}

		rigidbody2D.isKinematic = up.overlaping || left.overlaping || right.overlaping || overlaping ;
	}

	void Update()
	{

		if( set && (Vector2) transform.position != targetPos){
			transform.position = Vector2.Lerp ( transform.position, targetPos, 10 * Time.deltaTime );
		}
	}

	void SetKinematic()
	{
		rigidbody2D.isKinematic = true;
		targetPos = new Vector2(Mathf.Floor (transform.position.x + rigidbody2D.velocity.magnitude) , Mathf.Floor( transform.position.y ));
		set = true;
	}

	void Respawn()
	{
		Destroy(gameObject);
	}
}
