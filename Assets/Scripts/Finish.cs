using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class Finish : MonoBehaviour {

	bool finished = false;
	Transform player;
	bool open = false;
	
	public Transform lockSprite;
	public LevelSelect levelSelect;

	SwitchBus bus;

	void Start () {
		levelSelect = GameObject.Find("HUD").GetComponent<LevelSelect>();
		bus = GetComponent<SwitchBus>();
	}
	
	void Update () 
	{
		if(finished)
		{
			player.position = Vector2.Lerp(player.position, transform.position, 0.05f);

			Invoke("ChangeMap", 2);
		}

		open = bus.active;

		lockSprite.gameObject.SetActive(!open);
		collider2D.enabled = open;
	}

	void OnTriggerEnter2D( Collider2D other ){
		if (other.transform.CompareTag("Player")){

			finished = true;
			player = other.transform;
			other.rigidbody2D.isKinematic = true;
			other.GetComponent<Platformer2DUserControl>().enabled = false;
			other.GetComponent<Character2D>().enabled = false;


		}
	}
	
	void OnTriggerStay2D( Collider2D other ){
		if (other.transform.CompareTag("Player"))
			finished = true;
	}

	void ChangeMap() {
		levelSelect.SetLevel(Application.loadedLevel + 1);

//		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
