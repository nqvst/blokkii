using UnityEngine;
using System.Collections;

public class Build : MonoBehaviour {

	public Transform boxPrefab;
	public Transform placeHolderPrefab;
	public Transform raycastOrigin;
	public Transform placeHolderGuide;

	private Transform _transform;
	private Transform _placeHolder;
	private Vector2 placeholderTargetPosition;
	private Vector2 mousePosition;
	public bool canBuild = false;
	public bool canRemove = false;
	public bool canStick = false;

	PlaceHolder placeholderScript;

	public int budget = 0;

	Vector2 placeholderOffset = Vector2.zero;
	Vector2 lastOffset = Vector2.zero;

	void Start() 
	{
		_transform = transform;
		_placeHolder = Instantiate(placeHolderPrefab, transform.position , Quaternion.identity) as Transform;
		_placeHolder.GetComponent<SpriteRenderer>().enabled = true;
		placeholderScript = _placeHolder.GetComponent<PlaceHolder>();
	}

	void Update () 
	{

		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if( Vector3.Distance(_transform.position, mousePosition) < Vector2.Distance(transform.position, placeHolderGuide.position) ) 
		{
			placeholderTargetPosition = new Vector2 ( Mathf.RoundToInt( mousePosition.x ) , Mathf.RoundToInt( mousePosition.y ) ); 
//			placeholderTargetPosition = mousePosition;
		}
		else 
		{
			placeholderTargetPosition = new Vector2 (Mathf.RoundToInt( placeHolderGuide.position.x ) , Mathf.RoundToInt( placeHolderGuide.position.y ) ); 
			//placeholderTargetPosition = placeHolderGuide.position;
		}


//
//
//
//	
//		// just toggle now that i think about it just set the offset and use in transform pint.
//		if(Input.GetKeyDown(KeyCode.J)){
//			placeholderOffset = -Vector2.right * 1;
//			if (lastOffset == placeholderOffset){
//				placeholderOffset *= 2;
//			}
//			lastOffset = placeholderOffset;
//
//
//		}
//		if(Input.GetKeyDown(KeyCode.L)){
//			placeholderOffset = Vector2.right * 1;
//			if (lastOffset == placeholderOffset){
//				placeholderOffset *= 2;
//			}
//			lastOffset = placeholderOffset;
//		}
//		if(Input.GetKeyDown(KeyCode.I)){
//			placeholderOffset = Vector2.up * 1;
//			if (lastOffset == placeholderOffset){
//				placeholderOffset *= 2;
//			}
//			lastOffset = placeholderOffset;
//		}
//		if(Input.GetKeyDown(KeyCode.K)){
//			placeholderOffset = -Vector2.up * 1;
//			if (lastOffset == placeholderOffset){
//				placeholderOffset *= 2;
//			}
//			lastOffset = placeholderOffset;
//		}
//
//		placeholderTargetPosition = transform.TransformPoint(placeholderOffset);

		_placeHolder.position = Vector2.Lerp( _placeHolder.position, placeholderTargetPosition, 0.2f );


		if( Input.GetMouseButtonUp(0) ) 
		{
			canStick = placeholderScript.overlaping;
			canBuild = placeholderScript.canBuild;
			canRemove = placeholderScript.canRemove;

			if( canBuild )
			{
				if(Input.GetKey(KeyCode.LeftShift)) {
					BuildBox(true);
				}
				else if (canStick) {
					BuildBox(false);
				}
			}
			else if( canRemove && placeholderScript.currentBox )
			{
				DeleteBox(placeholderScript.currentBox);
			}
		}
	}


	void BuildBox( bool willFall ) 
	{
		if(canBuild && budget > 0) {
			Vector2 buildPosition = new Vector2 (Mathf.RoundToInt(_placeHolder.position.x) , Mathf.RoundToInt(_placeHolder.position.y) );
			Transform box = Instantiate( boxPrefab, buildPosition, Quaternion.identity) as Transform;

			if(willFall){
				box.rigidbody2D.isKinematic = false;
			}

			budget--;
		}
	}

	void DeleteBox(Transform boxToRemove) 
	{
		if(canRemove && boxToRemove.CompareTag("BuildBox")){
			Destroy(boxToRemove.gameObject);
			budget++;
		}
	}
}
