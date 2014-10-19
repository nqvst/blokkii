using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class ForgeManager : MonoBehaviour 
{

	[SerializeField] Transform [] prefabs;
	Transform nextPrefab;
	Transform currentPrefab;

	bool canBuild = true;

	ParseObject level = new ParseObject("Level");
	IList<object> levelObjects = new List<object>();

	public virtual Vector3 mousePosition 
	{
		get
		{
			Vector3 mp = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			return new Vector3(mp.x, mp.y, 0);
		}
	}

	void Start ()
	{
		nextPrefab = prefabs[0];
		SpawnPrefab();
		level["name"] = "[untitled]";
	}

	void SpawnPrefab ()
	{
		currentPrefab = Instantiate( nextPrefab, mousePosition, Quaternion.identity) as Transform;
		currentPrefab.name = currentPrefab.tag + ":" + Random.Range(10000000, 999999999).ToString() + Time.time.ToString();
	}	

	void PlacePrefab ()
	{
		currentPrefab.position = new Vector2 (Mathf.RoundToInt(currentPrefab.position.x) , Mathf.RoundToInt(currentPrefab.position.y) ); 
		AddObjectToLevel(currentPrefab.name, currentPrefab.position, currentPrefab.rotation.eulerAngles);
		currentPrefab = null;
		SpawnPrefab();
	}

	void RotateLeft ()
	{
		currentPrefab.transform.Rotate(0, 0, 90);
	}

	void RotateRight ()
	{
		currentPrefab.transform.Rotate(0, 0, -90);
	}
	
	void Update () 
	{
		
		if(Input.GetMouseButtonDown(0) && currentPrefab && canBuild) {
			PlacePrefab();
		}
		if( currentPrefab ) {
			Vector3 targetPosition = new Vector2 (Mathf.RoundToInt(mousePosition.x) , Mathf.RoundToInt(mousePosition.y) ); 
			currentPrefab.position = Vector2.Lerp(currentPrefab.position, targetPosition, 0.3f);
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			Cancel();
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			RotateLeft();	
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			RotateRight();	
		}
	}

	void AddObjectToLevel(string objectName, Vector2 pos, Vector3 rot)
	{
		string type = objectName.Split(':')[0];
		IDictionary<string, object> pObject = new Dictionary<string, object>{
			{ "type", type},
			{ "name", objectName },
			{ "position", GetVector2(pos)},
			{ "rotation", GetVector3(rot)}
		};
		levelObjects.Add(pObject);
	}
	void RemoveObjectFromLevel(Transform obj)
	{
		
	}

	void Cancel()
	{
		if(currentPrefab){
				Destroy(currentPrefab.gameObject);
		}
	}

	public void SetNextPrefab(int index)
	{
		nextPrefab = prefabs[index];
		Cancel();
		SpawnPrefab();
	}

	public void SaveLevel()
	{
		Debug.Log ("SaveLevel");
		level["objects"] = levelObjects;
		level.SaveAsync();
	}

	public void OnMouseEnterMenu()
	{
		canBuild = false;
	}
	public void OnMouseExitMenu()
	{
		canBuild = true;
	}

	IDictionary GetVector3(Vector3 vector)
	{
		return new Dictionary<string, string>{
			{ "x", vector.x.ToString() },
			{ "y", vector.y.ToString() },
			{ "z", vector.z.ToString()}
		};
	}

	IDictionary GetVector2(Vector2 vector)
	{
		return new Dictionary<string, string>{
			{ "x", vector.x.ToString() },
			{ "y", vector.y.ToString() }
		};
	}

}
