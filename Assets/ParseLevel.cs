using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
public class ParseLevel : MonoBehaviour {

	[SerializeField] Transform lazerPrefab;
	[SerializeField] Transform groundPrefab;
	[SerializeField] Transform boxPrefab;
	[SerializeField] Transform playerPrefab;
	[SerializeField] Transform hudPrefab;
	[SerializeField] Transform musicPrefab;
	[SerializeField] Transform finishPrefab;


	Transform nextPrefab;

	ParseObject level;

	bool built = false;

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
			if(prefabName == "Spawnpoint"){
				Transform player =  Instantiate(playerPrefab, spawnPos, Quaternion.identity) as Transform; 
				player.name = "Player";
			}else{
				if(prefabName == "BuildBox"){
					nextPrefab = boxPrefab;
				}
				if(prefabName == "Lazer"){
					nextPrefab = lazerPrefab;
				}
				if(prefabName == "SolidBox"){
					nextPrefab = groundPrefab;
				}
				if(prefabName == "Finish"){
					nextPrefab = finishPrefab;
				}
				Instantiate(nextPrefab, spawnPos, q); 
			}
		}

	}

	void Start () {
		ParseQuery<ParseObject> query = ParseObject.GetQuery("Level");
		query.GetAsync("SGaxVIYSuD").ContinueWith(t => {
			level = t.Result;
		});

	}
	
	void Update () {
		if(level != null && !built) {
			built = true;
			PopulateLevel(level);
		}
	}
}
