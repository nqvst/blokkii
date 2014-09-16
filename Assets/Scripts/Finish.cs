using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Finish : MonoBehaviour {

	bool finished = false;
	Transform player;
	bool open = false;

	public Trigger[] triggers;

	public Transform lockSprite;
	
	void Update () {
		if(finished){
			player.position = Vector2.Lerp(player.position, transform.position, 0.05f);

			Invoke("ChangeMap", 2);
		}

		bool shouldOpen = true;

		if(triggers.Length > 0){

			foreach (Trigger t in triggers) {
				if(!t.active){
					shouldOpen = false;
				}
			}
			open = shouldOpen;

		} else {
			open = true;
		}

		lockSprite.gameObject.SetActive(!open);
		collider2D.enabled = open;
	}

	void OnTriggerEnter2D( Collider2D other ){
		if (!transform.CompareTag("Player")){

			finished = true;
			player = other.transform;
			other.rigidbody2D.isKinematic = true;
			other.GetComponent<Platformer2DUserControl>().enabled = false;
			other.GetComponent<Character2D>().enabled = false;


		}
	}
	
	void OnTriggerStay2D( Collider2D other ){
		if (!transform.CompareTag("Player"))
			finished = true;
	}

	void ChangeMap() {
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
