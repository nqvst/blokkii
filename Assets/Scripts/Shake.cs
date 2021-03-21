using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {


	Vector3 originalPosition;
	Quaternion originalRotation;

	public float shakeDecay = 0.002f;
	public float shakeIntensity = 0.1f;
	float currentShakeIntensity;

	public bool shakeNow = false;

	void Update () {
		if (currentShakeIntensity > 0) {
			transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;
			if( !GetComponent<Camera>().orthographic ) { // only modify the angle if the camera is in perspective mode.
				transform.rotation = new Quaternion(originalRotation.x * Random.Range(-currentShakeIntensity, currentShakeIntensity) * Time.deltaTime,
				                                    originalRotation.y * Random.Range(-currentShakeIntensity, currentShakeIntensity) * Time.deltaTime,
				                                    originalRotation.z * Random.Range(-currentShakeIntensity, currentShakeIntensity) * Time.deltaTime,
				                                    originalRotation.w * Random.Range(-currentShakeIntensity, currentShakeIntensity) * Time.deltaTime);
			}
			currentShakeIntensity -= shakeDecay;
		}
		if(shakeNow) {
			ShakeNow();
		}
	}

	void ShakeNow() {
		shakeNow = false;
		originalPosition = transform.position;
		originalRotation = originalRotation;
		currentShakeIntensity = shakeIntensity;
	}
}
