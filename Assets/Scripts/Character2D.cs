using UnityEngine;

public class Character2D : MonoBehaviour 
{
	bool facingRight = true;							// For determining which way the player is currently facing.

	[SerializeField] float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
	[SerializeField] float jumpForce = 400f;			// Amount of force added when the player jumps.	
	[SerializeField] float moveForce = 400f;	

	[Range(0, 1)]
	[SerializeField] float crouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	
	[SerializeField] bool airControl = false;			// Whether or not a player can steer while jumping;
	[SerializeField] float airControlRatio = 0.5f;		// how much a player can steer while jumping;
	[SerializeField] LayerMask whatIsGround;			// A mask determining what is ground to the character
	
	Transform groundCheck;								// A position marking where to check if the player is grounded.
	float groundedRadius = .2f;							// Radius of the overlap circle to determine if grounded
	public bool grounded = false;						// Whether or not the player is grounded.
	Transform ceilingCheck;								// A position marking where to check for ceilings
	float ceilingRadius = .01f;							// Radius of the overlap circle to determine if the player can stand up
	Animator anim;										// Reference to the player's animator component.
	Transform body;
    void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
	}


	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
	}


	public void Move(float move, bool crouch, bool jump)
	{
		if(grounded || airControl)
		{

			if(rigidbody2D.velocity.x > 0 ) {
				// going left
				if( rigidbody2D.velocity.x < maxSpeed ){
					rigidbody2D.velocity = new Vector2 ( move * maxSpeed, rigidbody2D.velocity.y );
				}

			} else {
				//going right
				if( rigidbody2D.velocity.x > maxSpeed * -1 ){
					rigidbody2D.velocity = new Vector2 ( move * maxSpeed, rigidbody2D.velocity.y );
				}
			}

			if(move > 0 && !facingRight)
				Flip();
			else if(move < 0 && facingRight)
				Flip();
		}

        if (grounded && jump) {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
        }
	}

	
	void Flip ()
	{
		facingRight = !facingRight;
//		
//		Vector3 theScale = transform.localScale;
//		theScale.x *= -1;
//		body.transform.localScale = theScale;
	}
}
