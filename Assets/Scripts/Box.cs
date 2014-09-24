using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	[SerializeField] Transform overlapGround;
	[SerializeField] LayerMask whatIsGround;

	[SerializeField] float radius = 0.01f;

	public bool overlaping = false;
	public bool set = false;

	private Vector2 pos;
	void Start()
	{
		pos = transform.position;
	}

	void FixedUpdate()
	{
		overlaping = false;

		Collider2D[] coll = new Collider2D[10];

		if( Physics2D.OverlapCircleNonAlloc( overlapGround.position, radius , coll, whatIsGround ) > 0)
		{
			foreach(Collider2D c in coll)
			{
				if(c && c != collider2D ) 
				{
					overlaping = true;
					if(!set){
						SetKinematic();
					}
				}
			}
		}

	}

	void SetKinematic()
	{
		rigidbody2D.isKinematic = true;
		set = true;
	}

	void Respawn()
	{

//		Transform player = GameObject.Find("Player").transform;
//		rigidbody2D.velocity = rigidbody2D.velocity * - 1.03f;
//		rigidbody2D.isKinematic = true;
//		transform.position = new Vector2(Mathf.CeilToInt(player.position.x) , Mathf.CeilToInt(player.position.y) + 2);

	}
}
