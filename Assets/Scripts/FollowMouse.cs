	using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {

	[SerializeField] Transform placeHolderGuide;

	private Vector2 placeholderTargetPosition;
	private Vector2 mousePosition;
	Transform player;
	void Start () {
		player = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {

		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		if( Vector2.Distance(player.position, (Vector2) mousePosition) < 1.65f ) 
		{
					
			placeholderTargetPosition = mousePosition;
		}
		else 
		{
//			placeholderTargetPosition = new Vector2 (Mathf.RoundToInt( placeHolderGuide.position.x ) , Mathf.RoundToInt( placeHolderGuide.position.y ) ); 
			placeholderTargetPosition = placeHolderGuide.position;
		}

		transform.position = placeholderTargetPosition;
	
	}
}
