using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
public class ParseLevel : MonoBehaviour {

	[SerializeField] Transform lazerPrefab;
	[SerializeField] Transform groundPrefab;
	[SerializeField] Transform boxPrefab;
	[SerializeField] Transform playerPrefab;
	[SerializeField] Transform finishPrefab;

	GameManager gameManager;
	Transform nextPrefab;
	string levelId = "";
	ParseObject level;

	bool built = false;
	bool startBuilt = false;
	bool finishBuilt = false;

	Vector2 GetVector2 (IDictionary vector)
	{
		float x = (float)vector["x"];
		float y = (float)vector["y"];
		return new Vector2(x, y);
	}


	void PopulateLevel (ParseObject lev)
	{
		IList< object > levelObjects = lev.Get<List<object>>("objects");
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

			Debug.Log(spawnPos);
			Debug.Log(spawnRot);
			Debug.Log(dict["type"]);

			string prefabName = dict["type"].ToString();
			if (prefabName == "Spawnpoint" && !startBuilt){
				Transform player =  Instantiate(playerPrefab, spawnPos, Quaternion.identity) as Transform; 
				player.name = "Player";
				startBuilt = true;
			} else if (prefabName == "Finish"){
				if (!finishBuilt) {
					Instantiate(finishPrefab, spawnPos, q); 
					finishBuilt = true;
				}
			} else {
				if(prefabName == "BuildBox"){
					nextPrefab = boxPrefab;
				}
				if(prefabName == "Lazer"){
					nextPrefab = lazerPrefab;
				}
				if(prefabName == "SolidBox"){
					nextPrefab = groundPrefab;
				}
				Instantiate(nextPrefab, spawnPos, q); 
			}
		}

	}
	void Start () {
		//gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		levelId = GameManager.instance.LEVEL_ID;
		if(levelId != ""){
			LoadLevel(GameManager.instance.LEVEL_ID);
		}
	}

	public void LoadLevel (string objectId) {
		ParseQuery<ParseObject> query = ParseObject.GetQuery("Level");
		Debug.Log("before GetAsync");
		query.GetAsync(objectId).ContinueWith(t => {
			Debug.Log("inside GetAsync");
			level = t.Result;
			built = false;
		});
		Debug.Log("after GetAsync");

	}
	
	void Update () {
		if(level != null && !built) {
			built = true;
			PopulateLevel(level);
		}
	}
}
