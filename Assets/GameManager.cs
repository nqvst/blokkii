using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class GameManager : MonoBehaviour {

	public static bool forgeMode = false;
	public static string LEVEL_ID = "";

	const string PARSE_LOADED_LEVEL = "ComunityLevel";
	const string FORGE_LEVEL = "Forge";
	void Start(){
		DontDestroyOnLoad(gameObject);
	}

	public static void setLevelId (string objectId) {
		LEVEL_ID = objectId;
		//Invoke ("StartLevel", 0.1f);
	}

	void StartLevel() {
		Application.LoadLevel(PARSE_LOADED_LEVEL);
	}

	public static void RestartLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}

	public static void LoadLevel(string levelToLoad) 
	{
		Application.LoadLevel( levelToLoad );
	}

	public static void LoadLevel(int levelToLoad) 
	{
		Application.LoadLevel( levelToLoad );
	}

	public static void LoadParseLevel(string levelID) 
	{
		LEVEL_ID = levelID;
		Application.LoadLevel(PARSE_LOADED_LEVEL);
	}

	public static void ForgeParseLevel(string levelID) 
	{
		LEVEL_ID = levelID;
		Application.LoadLevel(FORGE_LEVEL);
	}
}
