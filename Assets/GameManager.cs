using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class GameManager : MonoBehaviour {
	
	public string levelId = "";

	void Start(){
		DontDestroyOnLoad(gameObject);
	}

	public void setLevelId (string objectId) {
		levelId = objectId;
		Invoke ("StartLevel", 0.1f);
	}

	void StartLevel() {
		Application.LoadLevel("ComunityLevel");
	}




}
