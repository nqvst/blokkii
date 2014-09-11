using UnityEngine;
using System.Collections;

public class KillScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if( other.CompareTag("Player") )
		   Kill(other.transform);
	}

	void OnTriggerStay2D(){
		
	}

	void Kill(Transform toKill){
		Application.LoadLevel(Application.loadedLevelName);
	}
}
