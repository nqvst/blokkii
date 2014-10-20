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
	
	private Build buildScript;

	void Start () 
	{
		GameObject player = GameObject.Find("Player");
		buildScript = player.GetComponent<Build>();

		spriteRenderer = GetComponent<SpriteRenderer>();
		innerCheck = transform.FindChild("InnerOverlapCheck").GetComponent<OverlapCheck>();
	}

	void FixedUpdate() 
	{
		overlaping = Physics2D.OverlapCircle(transform.position, 0.6f , whatIsGround);
	}
	
	void Update () 
	{
		canBuild = !innerCheck.overlaping && buildScript.budget > 0;

		canRemove = innerCheck.overlaping;

		Color removeIndicatorColor;

		if(canRemove && currentBox) {
			removeIndicatorColor = new Color(1,0,0,1f);
		} else {
			removeIndicatorColor = new Color(1,0,0,0);
		}

		removeIndicator.color = Color.Lerp(removeIndicator.color, removeIndicatorColor, 0.1f);

	}
	
}
