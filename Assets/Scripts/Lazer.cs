using UnityEngine;
using System.Collections;

public class Lazer : MonoBehaviour {


	LineRenderer lineRenderer;
	public Transform sparks;

	[SerializeField] bool isOn = false;
	[SerializeField] float max_thickness;
	[SerializeField] float min_thickness;
	[SerializeField] LayerMask whatToHit;

	SwitchBus bus;

	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		bus = GetComponent<SwitchBus>();

	}

	void Update () {

		isOn = bus.active;

		if( isOn ) {
			Activate ();

			RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);	
			Vector2 endpoint = transform.up * 1000;

			if ( hit ) {
				endpoint = hit.point;
			}

			lineRenderer.SetPosition(0, transform.position);
			lineRenderer.SetPosition(1, endpoint);

			float ran1 = Random.Range(min_thickness, max_thickness);
			float ran2 = Random.Range(min_thickness, max_thickness);
			lineRenderer.SetWidth(ran1, ran2);

//			Color startColor = new Color(Random.Range(0.3f,1), Random.Range(0,0.3f), 0);
//			Color endColor = new Color(Random.Range(0.3f,1), Random.Range(0,0.3f), 0);
//
//			lineRenderer.SetColors(startColor, endColor);

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
		lineRenderer.enabled = false;
	}

	void Activate () {
		sparks.gameObject.SetActive(true);
		lineRenderer.enabled = true;
	}

	void Restart() {
		Application.LoadLevel(Application.loadedLevel);
	}
}
