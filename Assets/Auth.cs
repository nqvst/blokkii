using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Parse;


public class Auth : MonoBehaviour 
{

	[SerializeField] Text titleText;
	[SerializeField] InputField usernameInput;
	[SerializeField] InputField passwordInput;
	[SerializeField] InputField confirmPasswordInput;
	[SerializeField] InputField emailInput;

	[SerializeField] Text feedbackMessage;

	bool auth = false;

	public const string SIGN_UP_TITLE = "please Sign Up";
	public const string SIGN_IN_TITLE = "please Sign In";



	void Start () 
	{
		CheckAuth();
		gameObject.SetActive(auth);
		GameManager.instance.playMode = !auth;
		ShowSignIn();
	}

	void CheckAuth(){
		if(ParseUser.CurrentUser != null){
			Debug.Log ("user is logged in as " + ParseUser.CurrentUser);
			auth = true;
		}
	}

	public void ShowSignUp () 
	{

		titleText.text = SIGN_UP_TITLE;
		usernameInput.ActivateInputField();
		passwordInput.ActivateInputField();
		confirmPasswordInput.ActivateInputField();
		emailInput.ActivateInputField();

	}

	public void ShowSignIn () 
	{
		titleText.text = SIGN_IN_TITLE;
		usernameInput.ActivateInputField();
		passwordInput.ActivateInputField();
		confirmPasswordInput.DeactivateInputField();
		emailInput.DeactivateInputField();

	}

	public void  SignUp(){

	}

	public void SignIn(){

	}

}
