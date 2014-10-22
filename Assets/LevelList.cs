﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Parse;

public class LevelList : MonoBehaviour 
{

	[SerializeField] GameObject allItemPrefab;
	[SerializeField] GameObject myItemPrefab;
	[SerializeField] RectTransform allLevelsListPanel;
	[SerializeField] RectTransform myLevelsListPanel;
	[SerializeField] Transform[] loadingPanels;

	float offset = 5;

	IList<ParseObject> allLevels = new List<ParseObject>();
	IList<ParseObject> myLevels = new List<ParseObject>();

	bool allLevelsAreRendered = false;
	bool myLevelsAreRendered = false;

	bool allLevelsAreLoaded = false;
	bool myLevelsAreLoaded = false;

	void Start () 
	{
		FetchAllLevels();
//		FetchMyLevels();
	}

	public void Reload()
	{
		ReloadMyLevels();
		ReloadOtherLevels();
	}
	public void ReloadMyLevels()
	{
		ClearPanel(myLevelsListPanel);
		FetchMyLevels();
	}
	public void ReloadOtherLevels()
	{
		ClearPanel(allLevelsListPanel);
		FetchAllLevels();
	}

	void ClearPanel(Transform panel){
		foreach(Transform child in myLevelsListPanel.transform){
			Destroy(child.gameObject);
		}
	}
	void FetchMyLevels()
	{
		myLevelsAreLoaded = false;
		myLevelsAreRendered = false;
		myLevels.Clear();
		if(ParseUser.CurrentUser != null) {
			Debug.Log("logged in as " + ParseUser.CurrentUser.Username);
			var query = ParseObject.GetQuery("Level").WhereEqualTo("creator", ParseUser.CurrentUser);
			query.FindAsync().ContinueWith(t =>	{
				IEnumerable<ParseObject> r = t.Result;
				foreach (ParseObject obj in r){
					myLevels.Add (obj);
				}
				myLevelsAreLoaded = true;
			});
		}
	}

	void FetchAllLevels()
	{
		allLevelsAreRendered = false;
		allLevelsAreLoaded = false;
		allLevels.Clear();
		var query = ParseObject.GetQuery("Level").Limit(100); 
		query.FindAsync().ContinueWith(t =>	{
			IEnumerable<ParseObject> r = t.Result;
			foreach (ParseObject obj in r){
				allLevels.Add (obj);
			}
			allLevelsAreLoaded = true;
		});
	}

	void RemoveLoadingPanel ()
	{
		Debug.Log("stop loading");
		foreach (Transform loader in loadingPanels){
			loader.gameObject.SetActive(false);
		}
	}
	
	void Update () 
	{
		if(allLevelsAreLoaded && !allLevelsAreRendered){
			allLevelsAreRendered = true;
			RemoveLoadingPanel();
			if(allLevels.Count != 0){
				Debug.Log("more than zero all stuff");
				PopulateLsíst(allLevels, allLevelsListPanel, allItemPrefab, false, 2);
			}
		}

		if(myLevelsAreLoaded && !myLevelsAreRendered){
			myLevelsAreRendered = true;
			RemoveLoadingPanel();
			if(myLevels.Count != 0 ){
				Debug.Log("more than zero my stuff");
				PopulateLsíst(myLevels, myLevelsListPanel, myItemPrefab, true, 1);
			}
		}
	}

	void PopulateLsíst( IList<ParseObject> levelList , Transform panel, GameObject itemPrefab, bool forge, int collumns ) 
	{
		int itemCount = levelList.Count;
		Debug.Log (itemCount);

		RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
		RectTransform containerRectTransform = panel.GetComponent<RectTransform>();

		float width = containerRectTransform.rect.width / collumns;
		float ratio = width / rowRectTransform.rect.width;
		float height = rowRectTransform.rect.height * ratio;
		int rowCount = itemCount / collumns;
		if (itemCount % rowCount > 0)
			rowCount++;

		Debug.Log(rowCount);
		//adjust the height of the container so that it will just barely fit all its children
		float scrollHeight = height * rowCount;
		containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
		containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);
		
		int j = 0;
		int i = 0;

		foreach( ParseObject obj in levelList)
		{
			if (i % collumns == 0)
				j++;

			string levelId = obj.ObjectId;
			string levelName = obj.Get<string>("name");
			string creatorName = obj.Get<string>("creatorName");

			GameObject newItem = Instantiate(itemPrefab) as GameObject;
			newItem.name = "item at (" + i + "," + j + ")";

			Button newItemButton = newItem.GetComponentInChildren<Button>();
			if ( forge ) {
				newItemButton.onClick.AddListener(() => { ForgeParseLevel(levelId); });
			} else {
				newItemButton.onClick.AddListener(() => { LoadParseLevel(levelId); });
			}

			Text newItemCreatedBy = newItem.transform.FindChild("CreatedBy").GetComponent<Text>();
			newItemCreatedBy.text = "By: " + creatorName;

			Text newItemText = newItem.transform.FindChild("Title").GetComponent<Text>();
			newItemText.text = levelName;

			newItem.transform.parent = panel.transform;

			RectTransform rectTransform = newItem.GetComponent<RectTransform>();
			
			float x = -containerRectTransform.rect.width / 2 + width * (i % collumns);
			float y = containerRectTransform.rect.height / 2 - height * j;
			rectTransform.offsetMin = new Vector2(x, y);
			
			x = rectTransform.offsetMin.x + width - (offset / 2);
			y = rectTransform.offsetMin.y + height - offset;
			rectTransform.offsetMax = new Vector2(x, y);

			i++;
		}
	}

	public void LoadParseLevel(string levelID) {
		Debug.Log("clicked " + levelID);
		GameManager.instance.LoadParseLevel(levelID);
	}

	public void ForgeParseLevel(string levelID) {
		Debug.Log("clicked " + levelID);
		GameManager.instance.ForgeParseLevel(levelID);
	}

	public void LoadForge()
	{
		GameManager.instance.LEVEL_ID = "";
		GameManager.instance.LoadLevel("Forge");
	}

	public void BackToMain(){
		GameManager.instance.LoadLevel("main");
	}


}
