using UnityEngine;
using System.Collections;

public class KillScriptForge : MonoBehaviour {


	ForgeManager fManager;

	void Start () {
		SetForgeManager();
	}
	
	void Update () {
		if(! fManager){
			Debug.Log("screeammmm");
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if( other.CompareTag("Player") ) {
			Kill();
			Debug.Log("Kill now");
		}
	}

	void Kill(){
		fManager.TogglePlayMode();
	}

	void SetForgeManager(){
		fManager = GameObject.FindObjectOfType<ForgeManager>();
	}
}
