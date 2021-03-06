using UnityEngine;

[RequireComponent(typeof(Character2D))]
public class Platformer2DUserControl : MonoBehaviour 
{
	private Character2D character;
	private MoveCharacter character2;
    private bool jump;


	void Awake()
	{
		character = GetComponent<Character2D>();
	}

    void Update ()
    {
        // Read the jump input in Update so button presses aren't missed.
#if CROSS_PLATFORM_INPUT
        if (CrossPlatformInput.GetButtonDown("Jump")) jump = true;
#else
		if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) jump = true;

#endif

    }

	void FixedUpdate()
	{
		// Read the inputs.
		bool crouch = Input.GetKey(KeyCode.LeftControl);
		#if CROSS_PLATFORM_INPUT
		float h = CrossPlatformInput.GetAxis("Horizontal");
		#else
		float h = Input.GetAxis("Horizontal");
		#endif

		// Pass all parameters to the character control script.
		character.Move( h, crouch , jump );

        // Reset the jump input once it has been used.
	    jump = false;
	}
}
