using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Parse;

public class LoginScript : MonoBehaviour {

	[SerializeField] InputField nickname;
	[SerializeField] InputField password;

	public bool logIn = false;

	public ParseUser user = null;

	bool showSpinner = false;
	string message;

	void Start () {
		user = ParseUser.CurrentUser;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(logIn) {
			logIn = false;
			Login();
		}
		if(user != null){
			Debug.Log("Horayyyy! " + user.Username);
			Application.LoadLevel("main");
		}
		if ( message != null) {
			Debug.Log( message );
		}
	
	}

	public void Login()
	{

		showSpinner = true;

		ParseUser.LogInAsync(nickname.value, password.value).ContinueWith(t => {

			if (t.IsFaulted || t.IsCanceled) {
				showSpinner = false;
				message = t.Exception.Message;
			} else {
				showSpinner = false;
				user = t.Result;
			}
		});
	}

	void ResetForm () {
		nickname.value = "";
		password.value = "";
		showSpinner = false;
	}

	public void setLogin(){
		logIn = true;
	}
}
