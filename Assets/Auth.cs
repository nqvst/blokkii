﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Parse;
using System.Threading.Tasks;


public class Auth : MonoBehaviour 
{

	[SerializeField] Text titleText;
	[SerializeField] InputField usernameInput;
	[SerializeField] InputField passwordInput;
	[SerializeField] InputField emailInput;
	[SerializeField] Button submitButton;
	[SerializeField] Button forgotPasswordButton;
	[SerializeField] Button showSignUpButton;
	[SerializeField] Text submitButtonText;
	[SerializeField] Transform feedbackMessagePanel;
	[SerializeField] Text feedbackMessage;

	bool auth = false;

	public const string SIGN_UP_TITLE = "please Sign Up";
	public const string SIGN_IN_TITLE = "please Sign In";
	public const string SIGN_UP_BUTTON_LABEL = "Sign Up!";
	public const string SIGN_IN_BUTTON_LABEL = "Sign In!";
	public const string RESET_TITLE = "please enter your E-mail";
	public const string RESET_BUTTON_LABEL = "Reset";

	Task loginTask;
	Task signUpTask;

	void Start () 
	{
		CheckAuth();
		gameObject.SetActive(auth);
		GameManager.instance.playMode = auth;
		HideFeedbackMessage();
		ShowSignIn();
	}

	void CheckAuth() {
		if(ParseUser.CurrentUser != null){
			Debug.Log ("user is logged in as " + ParseUser.CurrentUser.Username);
			auth = true;
			ShowFeedbackMessage("Welcome " + ParseUser.CurrentUser.Username);
			HideAuthWindow();
		}
	}

	public void HideAuthWindow() {
		transform.gameObject.SetActive(false);
	}

	void ShowFeedbackMessage(string msg){
		feedbackMessage.text = msg;
		StartCoroutine("FeedbackMessageTimer");
		feedbackMessagePanel.gameObject.SetActive(true);
	}

	void HideFeedbackMessage ()
	{
		feedbackMessage.text = "";
		feedbackMessagePanel.gameObject.SetActive(false);
	}

	public void ShowPasswordReset(){
		titleText.text = RESET_TITLE;
		submitButtonText.text = RESET_BUTTON_LABEL;
		submitButton.onClick.AddListener(() => { ResetPassword(); });
		emailInput.gameObject.SetActive(true);
		showSignUpButton.gameObject.SetActive(true);
		forgotPasswordButton.gameObject.SetActive(false);
		passwordInput.gameObject.SetActive(false);
		usernameInput.gameObject.SetActive(false);
	}

	public void ShowSignUp () 
	{
		titleText.text = SIGN_UP_TITLE;
		emailInput.gameObject.SetActive(true);
		submitButtonText.text = SIGN_UP_BUTTON_LABEL;

		showSignUpButton.gameObject.SetActive(false);
		forgotPasswordButton.gameObject.SetActive(false);

		submitButton.onClick.AddListener(() => { SignUp(); });
	}

	public void ShowSignIn () 
	{
		titleText.text = SIGN_IN_TITLE;
		emailInput.gameObject.SetActive(false);
		showSignUpButton.gameObject.SetActive(true);
		forgotPasswordButton.gameObject.SetActive(true);
		submitButtonText.text = SIGN_IN_BUTTON_LABEL;

		submitButton.onClick.AddListener(() => { SignIn(); });
	}

	void ResetPassword ()
	{
		if(emailInput.value.Equals("")){
			ShowFeedbackMessage("E-mail can't be empty!");
			return;
		}
		Task requestPasswordTask = ParseUser.RequestPasswordResetAsync(emailInput.value);
	}

	public void  SignUp(){
		Debug.Log("Sign up mothod");
		if(emailInput.value.Equals("") || passwordInput.value.Equals("") || usernameInput.value.Equals("")){
			Debug.Log("missing");
			ShowFeedbackMessage("All fields are required");
			return;
		}

		var user = new ParseUser()
		{
			Username = usernameInput.value,
			Password = passwordInput.value,
			Email = emailInput.value
		};
		
		signUpTask = user.SignUpAsync();
	}

	public void SignIn(){
		Debug.Log("Sign in mothod");
		if(passwordInput.value.Equals("") || usernameInput.value.Equals("")){
			Debug.Log("missing");
			ShowFeedbackMessage("All fields are required");
			return;
		}
		loginTask = ParseUser.LogInAsync(usernameInput.value, passwordInput.value);
	}

	void Update(){

		if(ParseUser.CurrentUser != null){
			HideAuthWindow();
		}

		if(loginTask != null){
			if (loginTask.IsCompleted && ParseUser.CurrentUser != null){
				Debug.Log("success");
				ShowFeedbackMessage("Welcome " + ParseUser.CurrentUser.Username);
				loginTask = null;
			}
			if (loginTask.IsFaulted){
				Debug.Log("Fault");
				ShowFeedbackMessage("Login Faild");
				loginTask = null;
			}
		}

		if(signUpTask != null){
			if (signUpTask.IsCompleted && ParseUser.CurrentUser != null){
				ShowFeedbackMessage("Welcome " + ParseUser.CurrentUser.Username);
				Debug.Log("success");
				signUpTask = null;
			}
			if (signUpTask.IsFaulted){
				ShowFeedbackMessage("Something went wrong during the registration, try again later");
				Debug.Log("Fault");
				signUpTask = null;
			}

		}
	}

	public IEnumerator FeedbackMessageTimer(){
		yield return new WaitForSeconds(3);
		HideFeedbackMessage();
	}

	public IEnumerator AuthPanelTimer(){
		yield return new WaitForSeconds(2);
		HideFeedbackMessage();
	}
}
