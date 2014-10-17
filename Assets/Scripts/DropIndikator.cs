using UnityEngine;
using System.Collections;

public class DropIndikator : MonoBehaviour {

	SpriteRenderer spr;

	bool show = false;

	Color hideColor = new Color (1,1,1,0);
	[SerializeField] Color showColor;

	void Start () {
		spr = GetComponent<SpriteRenderer>();
	}

	void Update () {



		show = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S);

		spr.color = Color.Lerp (spr.color, (show ? showColor: hideColor), .2f);
	}
}
