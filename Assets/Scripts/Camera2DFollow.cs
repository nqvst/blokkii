using UnityEngine;
using System.Collections;

public class Camera2DFollow : MonoBehaviour {
	
	Transform target;
	float damping = 0.4f;
	float lookAheadFactor = 0f;
	float lookAheadReturnSpeed = 0f;
	float lookAheadMoveThreshold = 0f;
	
	float offsetZ;
	Vector3 lastTargetPosition;
	Vector3 currentVelocity;
	Vector3 lookAheadPos;

	void Start () {

		Cursor.visible = false;

		setFinishAsTarget();
		StartCoroutine(ShowFinishTimer(2.0F));

		if( !target ) { enabled = false; return;}
		lastTargetPosition = target.position;
		offsetZ = (transform.position - target.position).z;
		transform.parent = null;
	}

	
	void FixedUpdate () {

		if (Input.anyKeyDown){
			SetPlayerAsTarget();
		}
		// only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (target.position - lastTargetPosition).x;

	    bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

		if (updateLookAheadTarget) {
			lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
		} else {
			lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);	
		}
		
		Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
		Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);
		
		transform.position = newPos;
		
		lastTargetPosition = target.position;		
	}

	void setFinishAsTarget() {
		GameObject finish = GameObject.Find("Finish");
		
		if( finish ) 
		{
			target = finish.transform;
		}
	}

	void SetPlayerAsTarget() {
		GameObject player = GameObject.Find("Player");
		
		if( player ) 
		{
			target = player.transform;
		}
	}

	IEnumerator ShowFinishTimer(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		SetPlayerAsTarget();
	}
}
