﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Parse;

public class ForgeManager : MonoBehaviour 
{
	
	GameManager gameManager;
	[SerializeField] Transform lazerPrefab;
	[SerializeField] Transform solidBoxPrefab;
	[SerializeField] Transform boxPrefab;
	[SerializeField] Transform startPointPrefab;
	[SerializeField] Transform finishPrefab;

	[SerializeField] InputField levelNameInput;
	Transform nextPrefab;
	Transform currentPrefab;
	Transform selectedPrefab;

	Transform cam;
	Transform player;
	[SerializeField] Transform playerPrefab;
	[SerializeField] Transform startPoint;

	GameObject parentLevelObject;
	[SerializeField] LayerMask whatToHit;

	ParseObject level = new ParseObject("Level");
	IList<object> levelObjects = new List<object>();

	bool levelIsLoaded = false;
	bool levelIsBuilt = false;

	bool showLogin = false;

	bool overMenu = false;

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
		gameManager = GameManager.instance;

		nextPrefab = solidBoxPrefab;
		SpawnPrefab();
		
		level["name"] = "Untitled";
		levelNameInput.value = (string)level["name"];

		cam = Camera.main.transform;
		gameManager.playMode = false;
	
		CreateParentLevelObject();
		LoadLevel();
		
	}

	void LoadLevel ()
	{
		string levelId = gameManager.LEVEL_ID;
		if( levelId == ""){
			levelIsBuilt = true;
		}else{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("Level");
			Debug.Log("before GetAsync");
			query.GetAsync(levelId).ContinueWith(t => {
				Debug.Log("inside GetAsync");
				level = t.Result;
				levelIsLoaded = true;
			});
			Debug.Log("after GetAsync");
		}
	}

	void CreateParentLevelObject(){
		parentLevelObject = new GameObject();
		parentLevelObject.transform.name = "ParentLevelObject";
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
		if(currentPrefab.CompareTag("Spawnpoint")){
			startPoint = currentPrefab;
		}
		currentPrefab.parent = parentLevelObject.transform;
		currentPrefab = null;

		SpawnPrefab();
	}

	void RotateLeft ()
	{
		if(currentPrefab){
			currentPrefab.transform.Rotate(0, 0, 90);
		}
	}

	void RotateRight ()
	{
		if(currentPrefab){
			currentPrefab.transform.Rotate(0, 0, -90);
		}
	}
	
	void Update () 
	{
		Debug.Log(gameManager.playMode);

		if(levelIsLoaded && !levelIsBuilt){
			levelIsBuilt = true;
			RestoreLevelStateFromLevel();
		}

		if(gameManager.playMode || overMenu) return;

		if( currentPrefab ) {
			Vector3 targetPosition = new Vector2 (Mathf.RoundToInt(mousePosition.x) , Mathf.RoundToInt(mousePosition.y) ); 
			currentPrefab.position = Vector2.Lerp(currentPrefab.position, targetPosition, 0.3f);
		}


		if(Input.GetMouseButtonDown(0)) {

			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, whatToHit);
			
			if(hit.transform != null && currentPrefab == null)
			{
				Debug.Log(hit.transform.name);
				selectedPrefab = hit.transform;
				RemoveObjectFromLevel(hit.transform);
			} 
			
			if ( currentPrefab && !gameManager.playMode){
				PlacePrefab();
			}
		}

		if(Input.GetKeyDown(KeyCode.Escape)){
			Cancel();
		}
		if (Input.GetKeyDown(KeyCode.Q) && !gameManager.playMode) {
			RotateLeft();	
		}
		if (Input.GetKeyDown(KeyCode.E) && !gameManager.playMode) {
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
	void RemoveObjectFromLevel(Transform objectToRemove)
	{
		int indexToRemove = -1;
		for(int i = 0; i < levelObjects.Count; i++) {
			IDictionary item = (IDictionary) levelObjects[i];
			if( item ["name"].Equals(objectToRemove.name)){
				indexToRemove = i;
			}
		}
		levelObjects.RemoveAt(indexToRemove);
		Destroy(objectToRemove.gameObject);
		selectedPrefab = null;
	}

	void Cancel()
	{
		if(currentPrefab){
			Destroy(currentPrefab.gameObject);
		}
	}

	void OnLogin(){

	}

	void ShowLogin ()
	{
		throw new System.NotImplementedException ();
	}
	
	public void SaveLevel()
	{
		Debug.Log ("SaveLevel");
		Debug.Log(levelObjects.Count);
		if( ParseUser.CurrentUser == null) {
			ShowLogin();
		} else if( levelObjects.Count > 0 ){
			Debug.Log(ParseUser.CurrentUser.Username.ToString());
			level["creatorName"] = ParseUser.CurrentUser.Username;
			level["creator"] = ParseUser.CurrentUser;
			level["name"] = levelNameInput.value;
			level["objects"] = levelObjects;
			level.SaveAsync();
		}
	}

	public void OnMouseEnterMenu()
	{
		overMenu = true;
	}
	public void OnMouseExitMenu()
	{
		overMenu = false;
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
	
	void ClearLevel ()
	{
		Destroy(parentLevelObject);
		foreach (Box b in FindObjectsOfType<Box>()){
			Destroy(b.gameObject);
		}

		CreateParentLevelObject();
	}

	
	void RestoreLevelStateFromLevel ()
	{
		levelObjects = level.Get<List<object>>("objects");
		RestoreLevelState();
	}


	void RestoreLevelState ()
	{
		Debug.Log(levelObjects.Count.ToString() + " objects in the list" );

		for( int i = 0;  i < levelObjects.Count; i++){
			IDictionary dict = (IDictionary) levelObjects[i];
			
			IDictionary pos = (IDictionary) dict["position"];
			IDictionary rot = (IDictionary) dict["rotation"];
			
			Vector2 spawnPos = new Vector2(float.Parse(pos["x"].ToString()),
			                               float.Parse(pos["y"].ToString()));
			
			Vector3 spawnRot = new Vector3(float.Parse(rot["x"].ToString()),
			                               float.Parse(rot["y"].ToString()),
			                               float.Parse(rot["z"].ToString()));
			Quaternion q = Quaternion.Euler(spawnRot);

			string prefabName = dict["type"].ToString();

			if(prefabName == "BuildBox"){
				nextPrefab = boxPrefab;
			}
			if(prefabName == "Lazer"){
				nextPrefab = lazerPrefab;
			}
			if(prefabName == "SolidBox"){
				nextPrefab = solidBoxPrefab;
			}
			if(prefabName == "Finish"){
				nextPrefab = finishPrefab;
			}
			if(prefabName == "Spawnpoint"){
				nextPrefab = startPointPrefab;
			}
			Transform newObj = Instantiate(nextPrefab, spawnPos, q) as Transform;
			newObj.name = dict["name"].ToString();
			if(newObj.CompareTag("Spawnpoint")){
				startPoint = newObj;
				Debug.Log("Spawnpoint found in restore");
			}
			newObj.parent = parentLevelObject.transform;

		}
	}

	void TogglePlayMode()
	{
		cam.GetComponent<Camera2DFollow>().enabled = gameManager.playMode;
		cam.GetComponent<MoveCamera>().enabled = !gameManager.playMode;

		if( gameManager.playMode ){
			if(currentPrefab){
				Destroy(currentPrefab.gameObject);
			}
			SpawnPlayer();
		} else {
			if( player ){
				GameObject placeHolderGO = GameObject.FindGameObjectWithTag("Placeholder");
				Destroy(player.gameObject);
				Destroy(placeHolderGO);
			}
			ClearLevel();
			RestoreLevelState();
			SolidBoxPrefab();
		}
	}

	public void StopPlay()
	{
		if(!gameManager.playMode) return;
		gameManager.playMode = false;
		TogglePlayMode();
	}

	public void StartPlay()
	{
		if(gameManager.playMode) return;
		gameManager.playMode = true;
		TogglePlayMode();
	}

	void SpawnPlayer()
	{
		if(startPoint){
			player = Instantiate(playerPrefab, startPoint.position, Quaternion.identity) as Transform;
			player.name = "Player";
		}
		else{
			Debug.Log("no spawn point");
			StopPlay();
		}
	}

	public void SolidBoxPrefab()
	{
		nextPrefab = solidBoxPrefab;
		Cancel();
		SpawnPrefab();
	}

	public void BoxPrefab()
	{
		nextPrefab = boxPrefab;
		Cancel();
		SpawnPrefab();
	}

	public void LazerPrefab()
	{
		nextPrefab = lazerPrefab;
		Cancel();
		SpawnPrefab();
	}

	public void StartPointPrefab()
	{
		nextPrefab = startPointPrefab;
		Cancel();
		SpawnPrefab();
	}

	public void FinishPrefab()
	{
		nextPrefab = finishPrefab;
		Cancel();
		SpawnPrefab();
	}

	public void BackToMenu(){
		GameManager.instance.LoadLevel(GameManager.COMUNITY_LEVEL_MENU);
	}
}
