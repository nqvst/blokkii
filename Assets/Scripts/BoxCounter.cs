using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoxCounter : MonoBehaviour {

	Build buildScript;
	Text text;

	void Start () {
		text = GetComponent<Text>();
		SetBuildScript();
	}
	

	void LateUpdate () {
		if( !buildScript ){
			SetBuildScript();
			return;
		}

		text.text = buildScript.budget.ToString();
	}

	void SetBuildScript(){
		GameObject player = GameObject.Find("Player");
		if( player ) {
			buildScript = player.GetComponent<Build>();
		}
	}
}
