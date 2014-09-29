using UnityEngine;
using System.Collections;

public class KillScript : MonoBehaviour {

	Build player;
	void Start () {
		player = GameObject.Find("Player").GetComponent<Build>();
	}
	
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if( other.CompareTag("Player") ) {
		   Kill(other.transform);
		}

		if( other.CompareTag("BuildBox") ) {
			player.budget ++;
			other.transform.SendMessage("Respawn", SendMessageOptions.DontRequireReceiver);
		}
	}

	void OnTriggerStay2D(){
		
	}

	void Kill(Transform toKill){
		Application.LoadLevel(Application.loadedLevelName);
	}
	
}
