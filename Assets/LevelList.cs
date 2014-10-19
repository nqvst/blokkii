using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Parse;

public class LevelList : MonoBehaviour {

	[SerializeField] GameObject itemPrefab;
	[SerializeField] RectTransform listPanel;
	
	GameManager gameManager;

	public int itemCount = 10, columnCount = 1;

	float offset = 40;
	IList<ParseObject> levels = new List<ParseObject>();
	bool listPopulated = false;
	void Start () {

		var query = ParseObject.GetQuery("Level");
		query.Limit(10); 
//		query = query.Skip(10);
		query.FindAsync().ContinueWith(t =>	{
			IEnumerable<ParseObject> r = t.Result;
			foreach (ParseObject obj in r){
				levels.Add (obj);
			}
		});

		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(levels.Count != 0 && !listPopulated){
			listPopulated = true;
			PopulateLsíst();
		}
	}

	void PopulateLsíst() {
		itemCount = levels.Count;

		RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
		RectTransform containerRectTransform = listPanel.GetComponent<RectTransform>();
		
		//calculate the width and height of each child item.
		float width = containerRectTransform.rect.width / columnCount;
		float ratio = width / rowRectTransform.rect.width;
		float height = rowRectTransform.rect.height * ratio;
		int rowCount = itemCount / columnCount;
		if (itemCount % rowCount > 0)
			rowCount++;
		
		//adjust the height of the container so that it will just barely fit all its children
		float scrollHeight = height * rowCount;
		containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
		containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);
		
		int j = 0;
		int i = 0;

		foreach( ParseObject obj in levels)
		{
			//this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
			i++;
			if (i % columnCount == 0)
				j++;

			string levelId = obj.ObjectId;
			string levelName = obj.Get<string>("name");

			//create a new item, name it, and set the parent
			GameObject newItem = Instantiate(itemPrefab) as GameObject;
			newItem.name = "item at (" + i + "," + j + ")";
			Button newItemButton = newItem.GetComponentInChildren<Button>();
			newItemButton.onClick.AddListener(() => { LoadParseLevel(levelId); });
			Text newItemText = newItem.transform.FindChild("Title").GetComponent<Text>();
			newItemText.text = levelName;
			newItem.transform.parent = listPanel.transform;
			
			//move and size the new item
			RectTransform rectTransform = newItem.GetComponent<RectTransform>();
			
			float x = -containerRectTransform.rect.width / 2 + width * (i % columnCount);
			float y = containerRectTransform.rect.height / 2 - height * j;
			rectTransform.offsetMin = new Vector2(x, y);
			
			x = rectTransform.offsetMin.x + width;
			y = rectTransform.offsetMin.y + height;
			rectTransform.offsetMax = new Vector2(x, y);
		}
	}

	public void LoadParseLevel(string levelID) {
		Debug.Log("clicked " + levelID);
		gameManager.setLevelId(levelID);
	}

}
