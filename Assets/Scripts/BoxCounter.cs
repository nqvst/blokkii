using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoxCounter : MonoBehaviour {

	public Build buildScript;
	Text text;
	public const string times = "X";
	void Start () {
		GameObject player = GameObject.Find("Player");
		buildScript = player.GetComponent<Build>();
		text = GetComponent<Text>();
	}
	

	void LateUpdate () {
		if(!buildScript ){
			buildScript = GameObject.Find("Player").GetComponent<Build>();
		}

		text.text = buildScript.budget.ToString();
	}
}
