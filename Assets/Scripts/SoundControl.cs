using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{

	AudioSource musicManager;
	bool mute = false;
	float fadeOutTime = 2f;
	Toggle musicToggle;

	void Start ()
	{
		mute = PlayerPrefs.GetString("Music") == "off";

		musicManager = GameObject.Find ("Music").GetComponent<AudioSource> ();	

		musicToggle = transform.FindChild("Mute").GetComponent<Toggle>();
		musicToggle.isOn = mute;
		
		if ( !mute ){
			musicManager.Play();
		}

	}

	void Update ()
	{
		float targetVolume = mute ? 0f : 1f;
		if (musicManager.volume != targetVolume) {
			musicManager.volume = Mathf.Lerp (musicManager.volume, targetVolume, Time.deltaTime * fadeOutTime);
		}


	}

	void StopMusic ()
	{
		musicManager.Pause ();
		PlayerPrefs.SetString("Music", "off");
	}

	void ResumeMusic ()
	{
		musicManager.Play ();
		PlayerPrefs.SetString("Music", "on");
	}

	public void ToggleMute ()
	{
		mute = musicToggle.isOn;
		Debug.Log(mute);
		if(!mute){
			ResumeMusic();
		}else{
			Invoke("StopMusic", 1);
		}

	}
}
