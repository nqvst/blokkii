using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Parse;

public class Profile : MonoBehaviour 
{

	[SerializeField] CanvasGroup profile;
	[SerializeField] CanvasGroup notLoggedIn;
	GameManager gameManager;

	[SerializeField] Text usernameText;
	[SerializeField] Text starsText;
	[SerializeField] Text levelCount;

	[SerializeField] LevelList levelList;

	float targetAlpha = 0;
	float targetAlphaLoggedOut = 0;

	bool auth = false;

	bool isProfilePopulated = false;

	void Start () 
	{
		gameManager = GameManager.instance;

	}
	
	void Update () 
	{

		targetAlpha = auth ? 1 : 0;
		targetAlphaLoggedOut = !auth ? 1 : 0;

		profile.alpha = Mathf.Lerp(profile.alpha, targetAlpha, Time.deltaTime * 5);
		notLoggedIn.alpha = Mathf.Lerp(notLoggedIn.alpha, targetAlphaLoggedOut, Time.deltaTime * 10);
		profile.blocksRaycasts = auth;
		profile.interactable = auth;
		notLoggedIn.blocksRaycasts = !auth;
		notLoggedIn.interactable = !auth;





	}

	void PopulateProfile(){
		usernameText.text = ParseUser.CurrentUser.Username;
	}

	void OnLoginSuccess() 
	{
		auth = true;
		PopulateProfile();
	}

	void OnLogOut() 
	{
		auth = false;
	}
}
