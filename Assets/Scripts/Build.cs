using UnityEngine;
using System.Collections;

public class Build : MonoBehaviour {

	public Transform boxPrefab;
	public Transform placeHolderPrefab;
	public Transform raycastOrigin;
	public Transform placeHolderGuide;

	private Transform _transform;
	private Transform _placeHolder;
	public bool canBuild = false;
	public bool canRemove = false;
	PlaceHolder placeholderScript;

	public int budget = 0;

	void Start() {
		_transform = transform;
		_placeHolder = Instantiate(placeHolderPrefab, transform.position , Quaternion.identity) as Transform;
		_placeHolder.GetComponent<SpriteRenderer>().enabled = true;
		placeholderScript = _placeHolder.GetComponent<PlaceHolder>();
	}

	void Update () {

		Vector2 placeholderTargetPosition = new Vector2 (Mathf.RoundToInt(placeHolderGuide.position.x) , Mathf.RoundToInt(placeHolderGuide.position.y) ); 
		_placeHolder.position = Vector2.Lerp(_placeHolder.position, placeholderTargetPosition, 0.2f);



		if(Input.GetMouseButtonUp(1)) {
			canRemove = placeholderScript.canRemove;

			DeleteBox(placeholderScript.currentBox);
		}

		if(Input.GetMouseButtonUp(0)) {
			canBuild = placeholderScript.canBuild;
			BuildBox();
		}


	}


	void BuildBox() {
		if(canBuild && budget > 0) {
			Instantiate( boxPrefab, _placeHolder.position, Quaternion.identity);
			budget--;
		}
	}

	void DeleteBox(Transform boxToRemove) {
		if(canRemove && boxToRemove.CompareTag("BuildBox") && boxToRemove){
			Destroy(boxToRemove.gameObject);
			budget++;
		}
	}

//	void Raycasting() {
//		RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, raycastOrigin.forward, 4);
//		Debug.DrawRay(raycastOrigin.position, raycastOrigin.forward * 1000);
//		
//		Debug.DrawRay(hit.point, hit.normal, Color.red);
//		
//		
//		
//		
//	}
}
