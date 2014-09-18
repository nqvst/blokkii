using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour 
{

	[SerializeField] float fadeOutTime;
	[SerializeField] AudioSource audioSource;

	public string currentLevel;

	private string levelToLoad = "";
	private bool fadeOut = false;
	private float volume = 1;

	void Start ()
	{
		fadeOut = false;
		currentLevel = Application.loadedLevelName;
	}

	void Update () 
	{
		if( fadeOut && audioSource ) 
		{
			volume = Mathf.Lerp( volume, 0, Time.deltaTime * fadeOutTime );
			audioSource.volume = volume;
		}
	}
	
	public void SetLevel( string levelName ) 
	{
		levelToLoad = levelName;
		fadeOut = true;

		Invoke( "LoadLevel", fadeOutTime );

	}

	private void LoadLevel() 
	{
		Application.LoadLevel( levelToLoad );
	}

	void OnLevelWasLoaded( int level )
	{
		currentLevel = Application.loadedLevelName;
	}
}
