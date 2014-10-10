using UnityEngine;
using System.Collections;

public class Breathe : MonoBehaviour 
{

	[SerializeField] Color color1;
	[SerializeField] Color color2;
	[SerializeField] float pace = 5;
	[SerializeField] Color targetColor;
	private float timer;
	private bool dir = false;

	void Start () 
	{
		Flip ();
	}

	void Update () 
	{
		timer -= Time.deltaTime;

		if (timer <= 0) {
			Flip();
		}

		particleSystem.startColor = Color.Lerp(particleSystem.startColor, targetColor, Time.deltaTime * pace * pace);

		Debug.Log(particleSystem.startColor.a);

	}

	void Flip () 
	{
		timer = pace;
		Debug.Log("Flipped!");
		dir = !dir;
		targetColor = dir ? color1 : color2;
	}
}
