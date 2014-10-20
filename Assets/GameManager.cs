using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class GameManager : MonoBehaviour {

	public static bool forgeMode = false;
	public static string LEVEL_ID = "";

	public const string PARSE_LOADED_LEVEL = "ComunityLevel";
	public const string COMUNITY_LEVEL_MENU = "CustomLevelMenu";
	public const string FORGE_LEVEL = "Forge";

	static GameManager _instance;

	private GameManager () {}

	public static bool isActive { 
		get { 
			return _instance != null; 
		} 
	}
	
	public static GameManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(GameManager)) as GameManager;
				
				if (_instance == null)
				{
					GameObject go = new GameObject("GameManager");
					DontDestroyOnLoad(go);
					_instance = go.AddComponent<GameManager>();
				}
			}
			return _instance;
		}
	}

	public static void setLevelId (string objectId) {
		LEVEL_ID = objectId;
	}

	void StartLevel() {
		Application.LoadLevel(PARSE_LOADED_LEVEL);
	}

	public void RestartLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}

	public void LoadLevel(string levelToLoad) 
	{
		Application.LoadLevel( levelToLoad );
	}

	public void LoadLevel(int levelToLoad) 
	{
		Application.LoadLevel( levelToLoad );
	}

	public void LoadParseLevel(string levelID) 
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
