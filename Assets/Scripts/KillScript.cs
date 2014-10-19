using UnityEngine;
using System.Collections;

public class KillScript : MonoBehaviour {

	Build buildScript;
	void Start () {
		SetBuildScript();
	}
	
	void Update () {
		if(!buildScript){
			SetBuildScript();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if( other.CompareTag("Player") ) {
		   Kill(other.transform);
		}

		if( other.CompareTag("BuildBox") ) {
			buildScript.budget ++;
			other.transform.SendMessage("Respawn", SendMessageOptions.DontRequireReceiver);
		}
	}


	void Kill(Transform toKill){
		Application.LoadLevel(Application.loadedLevelName);
	}

	void SetBuildScript(){
		GameObject player = GameObject.Find("Player");
		if( player ) {
			buildScript = player.GetComponent<Build>();
		}
	}
	
}
