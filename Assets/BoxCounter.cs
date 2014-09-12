using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoxCounter : MonoBehaviour {

	[SerializeField] Build buildScript;
	Text text;
	public const string times = "X";
	void Start () {
		text = GetComponent<Text>();
	}
	

	void Update () {
		text.text = times + " " + buildScript.budget.ToString();
	}
}
