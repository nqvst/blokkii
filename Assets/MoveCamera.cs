using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	GameManager gameManager;
	void Start () {
		gameManager = GameManager.instance;
	}

	[SerializeField] float speed = 0.2f;	

	void FixedUpdate () {
		if(gameManager.playMode){
			if( Input.GetAxis("Horizontal") != 0 ) {
				transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0));
			}
			if( Input.GetAxis("Vertical") != 0 ) {
				transform.Translate(new Vector3(0, Input.GetAxis("Vertical") * speed, 0));
			}
		}
	}
}
