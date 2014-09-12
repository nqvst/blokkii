using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

	bool finished = false;
	Transform player;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(finished){
			player.position = Vector2.Lerp(player.position, transform.position, 0.05f);
			Invoke("ChangeMap", 2);
		}
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
