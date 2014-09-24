using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Parse;

[RequireComponent (typeof (ReadSceneNames))]
public class LevelSelect : MonoBehaviour 
{

	[SerializeField] float fadeTime;
	[SerializeField] AudioSource audioSource;
	[SerializeField] Image fadeImg;

	public string currentLevel;

	private string levelToLoad = "";
	private bool fade = false;
	private float volume = 1;
	private float alpha = 0;
	private float targetVolume = 1;

	private string [] levels;

	Color targetColor = Color.black;

	void Start ()
	{
//		Screen.showCursor = false;
		levels = GetComponent<ReadSceneNames>().scenes;
	
		fadeTime = fadeTime <= 0 ? 1 : fadeTime;  

		if( fadeImg ) 
		{
			fadeImg.color  = new Color(0,0,0,1);
		}

		targetColor = new Color(0,0,0,0);

		if( audioSource ) 
		{
			audioSource.volume = 0;
		}

		targetVolume = 1;

		fade = true;

		currentLevel = Application.loadedLevelName;
	}

	void Update () 
	{
		if( fade && audioSource) 
		{
			volume = Mathf.Lerp( volume, targetVolume, Time.deltaTime * fadeTime );
			audioSource.volume = volume;
		}

		if( fade && fadeImg) 
		{
			fadeImg.color = Color.Lerp(fadeImg.color, targetColor, Time.deltaTime * fadeTime);

			if(fadeImg.color.a == targetColor.a){
				fade = false;
				targetColor = Color.black;
			}
		}

	}
	
	public void SetLevel( string levelName ) 
	{
		levelToLoad = levelName;
		targetColor = Color.black;
		targetVolume = 0;
		fade = true;

		Invoke( "LoadLevel", fadeTime );

	}

	public void SetLevel( int levelIndex ) 
	{
		levelToLoad = levels[levelIndex];
		targetColor = Color.black;
		targetVolume = 0;
		fade = true;

		
		Invoke( "LoadLevel", fadeTime );
		
	}

	private void LoadLevel() 
	{

		Application.LoadLevel( levelToLoad );
	}

	void OnLevelWasLoaded( int level )
	{

		fade = true;
		currentLevel = Application.loadedLevelName;
	}
}
