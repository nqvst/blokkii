using UnityEngine;
using System.Collections;

public class SoundControl : MonoBehaviour {

	AudioSource audioSource;
	bool mute = false;

	float fadeOutTime = 2f;

	void Start () {
		audioSource = GameObject.Find("Music").GetComponent<AudioSource>();	
	}
	
	void Update () {
		float targetVolume = mute ? 0f : 1f;
		audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime * fadeOutTime);
	}

	public void ToggleMute(){
		mute = !mute;

	}
}
