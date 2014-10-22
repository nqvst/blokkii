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
	[SerializeField] Button showSignInButton;
	[SerializeField] Text submitButtonText;
	[SerializeField] Transform feedbackMessagePanel;
	[SerializeField] Text feedbackMessage;
	[SerializeField] Transform target;

	bool auth = false;

	public const string SIGN_UP_TITLE = "please Sign Up";
	public const string SIGN_IN_TITLE = "please Sign In";
	public const string SIGN_UP_BUTTON_LABEL = "Sign Up!";
	public const string SIGN_IN_BUTTON_LABEL = "Sign In!";
	public const string RESET_TITLE = "please enter your E-mail";
	public const string RESET_BUTTON_LABEL = "Reset";

	Task loginTask;
	Task signUpTask;

	float targetAlpha = 0f;

	public CanvasGroup canvasGroup;

	void Start () 
	{
		canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0f;
		CheckAuth();
		if( auth ) {
			transform.SendMessageUpwards( "OnLoginSuccess" , SendMessageOptions.RequireReceiver);
		}else {
			HideFeedbackMessage();
			ShowSignIn();
		}

	}

	void CheckAuth() {
		if(ParseUser.CurrentUser != null){
			Debug.Log ("user is logged in as " + ParseUser.CurrentUser.Username);
			auth = true;
			ShowFeedbackMessage("Welcome " + ParseUser.CurrentUser.Username);
			StartCoroutine("AuthPanelTimer");
		}
	}

	public void HideAuthWindow() {
		targetAlpha = 0;
		canvasGroup.blocksRaycasts = false;
	}

	public void Show(){
		targetAlpha = 1;
		canvasGroup.blocksRaycasts = true;
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

	public void ShowPasswordReset()
	{

		titleText.text = RESET_TITLE;
		submitButtonText.text = RESET_BUTTON_LABEL;

		emailInput.gameObject.SetActive(true);
		passwordInput.gameObject.SetActive(false);
		usernameInput.gameObject.SetActive(false);

		showSignUpButton.gameObject.SetActive(true);
		showSignInButton.gameObject.SetActive(true);
		forgotPasswordButton.gameObject.SetActive(false);

		submitButton.onClick.RemoveAllListeners();
		submitButton.onClick.AddListener(() => { ResetPassword(); });
	}

	public void ShowSignUp () 
	{

		titleText.text = SIGN_UP_TITLE;
		submitButtonText.text = SIGN_UP_BUTTON_LABEL;

		usernameInput.gameObject.SetActive(true);
		passwordInput.gameObject.SetActive(true);
		emailInput.gameObject.SetActive(true);


		showSignInButton.gameObject.SetActive(true);
		showSignUpButton.gameObject.SetActive(false);
		forgotPasswordButton.gameObject.SetActive(true);

		submitButton.onClick.RemoveAllListeners();
		submitButton.onClick.AddListener(() => { SignUp(); });
	}

	public void ShowSignIn () 
	{

		titleText.text = SIGN_IN_TITLE;
		submitButtonText.text = SIGN_IN_BUTTON_LABEL;

		usernameInput.gameObject.SetActive(true);
		passwordInput.gameObject.SetActive(true);
		emailInput.gameObject.SetActive(false);

		showSignInButton.gameObject.SetActive(false);
		showSignUpButton.gameObject.SetActive(true);
		forgotPasswordButton.gameObject.SetActive(true);


		submitButton.onClick.RemoveAllListeners();
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

		auth = (ParseUser.CurrentUser != null );
		targetAlpha = auth ? 0 : 1;
		canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, Time.deltaTime * 4);
		canvasGroup.blocksRaycasts = !auth;

		if(loginTask != null){
			if (loginTask.IsCompleted && ParseUser.CurrentUser != null){
				LoginSuccess();
				loginTask = null;
			}else if (loginTask.IsFaulted) {
				Debug.Log("Fault");
				ShowFeedbackMessage("Login Faild");
				loginTask = null;
			}
		}

		if(signUpTask != null){
			if (signUpTask.IsCompleted && ParseUser.CurrentUser != null){
				LoginSuccess();
				signUpTask = null;
			}else if (signUpTask.IsFaulted){
				ShowFeedbackMessage("Something went wrong during the registration, try again later");
				Debug.Log("Fault");
				signUpTask = null;
			}

		}
	}

	void LoginSuccess()
	{
		ShowFeedbackMessage("Welcome " + ParseUser.CurrentUser.Username);
		Debug.Log("success");
		target.SendMessage( "OnLoginSuccess" , SendMessageOptions.RequireReceiver);
		ResetInputFields();
	}
	
	void ResetInputFields()
	{
		passwordInput.value = "";
		usernameInput.value = "";
		emailInput.value = "";
	}

	public void LogOut()
	{
		Debug.Log("Loging out...");
		ParseUser.LogOut();
		ShowSignIn();
		target.SendMessage( "OnLogOut" , SendMessageOptions.RequireReceiver);

	}

	public IEnumerator FeedbackMessageTimer(){
		yield return new WaitForSeconds(3);
		HideFeedbackMessage();
	}

	public IEnumerator AuthPanelTimer(){
		yield return new WaitForSeconds(2);
		HideAuthWindow();
	}
}
