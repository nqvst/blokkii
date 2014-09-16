using UnityEngine;
using System.Collections;

public class Lazer : MonoBehaviour {


	LineRenderer lr;
	public Transform sparks;

	[SerializeField] bool isOn = false;

	void Start () {
		lr = GetComponent<LineRenderer>();
	}

	void Update () {
		if( isOn ) {


			RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);	

			lr.SetPosition(0, transform.position);
			lr.SetPosition(1, hit.point);
			float ran1 = Random.Range(0.05f, 0.1f);
			float ran2 = Random.Range(0.05f, 0.1f);
			lr.SetWidth(ran1, ran2);

			Color startColor = new Color(Random.Range(0.3f,1), Random.Range(0,0.3f), 0);
			Color endColor = new Color(Random.Range(0.3f,1), Random.Range(0,0.3f), 0);

			lr.SetColors(startColor, endColor);

			if( hit ) {
				if(hit.collider.transform.CompareTag("Player") ) {
					//Camera.main.GetComponent<Shake>().shakeNow = true;
					//hit.collider.rigidbody2D.AddForceAtPosition(-hit.normal * 2000, hit.point);
					//Invoke("Restart", 0.5f);
					Restart();
				}

				sparks.position = hit.point;
			}

		} else { // lazer is not on!
			Deactivate();
		}

	}

	void Deactivate() {
		sparks.gameObject.SetActive(false);
		lr.enabled = false;
	}

	void Restart() {
		Application.LoadLevel(Application.loadedLevel);
	}
}
