using UnityEngine;
using System.Collections;

public class SwitchBus : MonoBehaviour {

	public bool active = false;
	
	public Trigger[] triggers;

	[SerializeField] bool invertedBehaviour = false;

	void Start () {
	
	}
	
	void Update () {


		if ( invertedBehaviour ) {
			bool shouldActivate = true;
			if(triggers.Length > 0){
				
				foreach (Trigger t in triggers) {
					if(t.active){
						shouldActivate = false;
					}
				}
				active = shouldActivate;
				
			} else {
				active = true;
			}

		} else {

			bool shouldActivate = true;
			if(triggers.Length > 0){
				
				foreach (Trigger t in triggers) {
					if(!t.active){
						shouldActivate = false;
					}
				}
				active = shouldActivate;
				
			} else {
				active = true;
			}
		}
	
	}
}
