using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

	[SerializeField] string currentLevel;

	void Start () {
		currentLevel = Application.loadedLevelName;
	}
	
	public void SetLevel(string levelName) {
		Application.LoadLevel(levelName);
	}

	void OnLevelWasLoaded(int level) {
		currentLevel = Application.loadedLevelName;
	}
}
