using UnityEngine;
using System.Collections;

public class SoundControl : MonoBehaviour
{

	AudioSource audioSource;
	bool mute = false;
	float fadeOutTime = 2f;

	void Start ()
	{
		audioSource = GameObject.Find ("Music").GetComponent<AudioSource> ();	
	}

	void Update ()
	{
		float targetVolume = mute ? 0f : 1f;
		if (audioSource.volume != targetVolume) {
			audioSource.volume = Mathf.Lerp (audioSource.volume, targetVolume, Time.deltaTime * fadeOutTime);
		}

		if(Mathf.Abs(targetVolume - audioSource.volume) < 0.1 && targetVolume == 0) {
			StopMusic();
		}
		Debug.Log(targetVolume);


	}

	void StopMusic ()
	{
		audioSource.Pause ();
	}

	void ResumeMusic ()
	{
		audioSource.Play ();
	}

	public void ToggleMute ()
	{
		mute = !mute;
		Debug.Log(mute);
		if(!mute){
			ResumeMusic();
		}

	}
}
