using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

	[SerializeField] Transform from, to;

	public bool active = false;
	private LineRenderer _lineRenderer;

	void Start () {
		if(from && to) {
			active = true;
			_lineRenderer = GetComponent<LineRenderer>();
		}

	}
	

	void Update () {
		if(!active) {return;}
		_lineRenderer.SetPosition(0, from.position);
		_lineRenderer.SetPosition(1, new Vector3(to.position.x, from.position.y, 0) );
		_lineRenderer.SetPosition(2, to.position);
	}
}
