using UnityEngine;
using System.Collections;

public class MoveCharacter : MonoBehaviour {



	public Vector2 targetPosition;

	[SerializeField] LayerMask whatIsGround;
	[SerializeField] float jumpSpeed = 10;
	[SerializeField] float speed = 8;

	Transform groundCheck;
	float groundedRadius = .2f;
	public bool grounded = false;

	bool jump = false;

	float targetX = 0f;

	void Start () 
	{
		groundCheck = transform.Find("GroundCheck");
	}

	void Update () 
	{
		
		if(Input.GetKeyDown(KeyCode.A)){
			targetX --;
//			targetPosition = new Vector2 (Mathf.CeilToInt(transform.position.x) - 1, transform.position.y);
			Debug.Log(targetPosition);
		}
		
		if(Input.GetKeyDown(KeyCode.D)){
			targetX ++;
			Debug.Log(targetPosition);
		}
		
		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)){
		
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);

		}


		float dist = Vector2.Distance(transform.position, targetPosition);



		Vector3 dir = Vector2.zero; 
		if(dist > .1f){
			dir = ((Vector3)targetPosition - (Vector3)transform.position ).normalized * speed;
			Debug.DrawRay(transform.position, dir);
		}
		GetComponent<Rigidbody2D>().velocity = Vector2.Lerp( GetComponent<Rigidbody2D>().velocity, (Vector2)dir, Time.deltaTime * 10);
//		rigidbody2D.velocity = dir;
		
	}
	
	void FixedUpdate ()
	{ 
		targetPosition = new Vector2 (targetX , transform.position.y);
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
	}
	
}
