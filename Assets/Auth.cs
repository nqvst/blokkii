using UnityEngine;
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

	[SerializeField] Text feedbackMessage;

	bool auth = false;

	public const string SIGN_UP_TITLE = "please Sign Up";
	public const string SIGN_IN_TITLE = "please Sign In";
	public const string SIGN_UP_BUTTON_LABEL = "Sign Up!";
	public const string SIGN_IN_BUTTON_LABEL = "Sign In!";

	Task loginTask;
	Task signUpTask;
	void Start () 
	{
		CheckAuth();
		gameObject.SetActive(auth);
		GameManager.instance.playMode = !auth;
		ShowSignIn();
	}

	void CheckAuth(){
		if(ParseUser.CurrentUser != null){
			Debug.Log ("user is logged in as " + ParseUser.CurrentUser.Username);
			auth = true;
		}
	}

	public void HideAuthWindow() {

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

	public void  SignUp(){
		Debug.Log("Sign up mothod");
		if(emailInput.value.Equals("") || passwordInput.value.Equals("") || usernameInput.value.Equals("")){
			Debug.Log("missing");
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
			return;
		}
		loginTask = ParseUser.LogInAsync(usernameInput.value, passwordInput.value);
	}

	void Update(){
		if(loginTask != null){
			if (loginTask.IsCompleted){
				Debug.Log("success");
			}
			if (loginTask.IsFaulted){
				Debug.Log("Fault");
			}
		}

		if(signUpTask != null){
			if (signUpTask.IsCompleted){
				Debug.Log("success");
				Debug.Log(ParseUser.CurrentUser.Username);
			}
			if (signUpTask.IsFaulted){
				Debug.Log("Fault");
			}
		}
	}
}
