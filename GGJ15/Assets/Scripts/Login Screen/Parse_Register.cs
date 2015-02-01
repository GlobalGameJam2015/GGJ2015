using UnityEngine;
using System.Collections;
using Parse;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class Parse_Register : MonoBehaviour {
	private string username;
	private string password;
	private string confirm_pass;
	private string email_var;
	// Use this for initialization
	void Start () {
		//nameInputField.OnSubmit.AddListener ((value) => SubmitName(value));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Click() {

	}

	public void start_register() {
		GameObject regBox = GameObject.Find("Register_box");
		if (validate_registeration (username, password, confirm_pass, email_var)){
			register_user (username, password, email_var);
			iTween.MoveTo(regBox, new Vector3(-1000, 0, 0), 0);
			open_close_register.is_open = false;
			Debug.Log ("cool");
		}else {
			Debug.Log("not cool");
		}
	}

	private bool validate_registeration(string user, string pass, string conPass, string email){
		//below are forbidden characters
		if (user.IndexOfAny ("*&#!@$%^&()-+=/\\[]{}|':;>?<.,\"_~`".ToCharArray ()) != -1) {
			Debug.Log ("Special characters are not allowed.");
			return false;
		} else {
			if (user.Length > 5){
				Debug.Log ("You can use this user name.");
			}else {
				return false;
				Debug.Log("Username must be at least 6 charaters.");
			}
		}


		//password must have at least 6 characters.
		if (pass == conPass) {
			if (pass.Length > 5) {
				Debug.Log("this is a valid password length");
			} else {
				Debug.Log("You need at least 6 characters for your password");
				return false;
			}
		} else {
			Debug.Log("Make sure your passwords match.");
			return false;
		}
		//email pattern
		string patemail = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" 
			+ @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" 
				+ @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
		//is email a valid email
		if (Regex.IsMatch (email, patemail,RegexOptions.IgnoreCase)) {
			Debug.Log("that is a valid email");
		} else {
			Debug.Log("that is not a valid email");
			return false;
		}

		return true;
	}

	public void setUsername(string users) {
		username = users;
	}

	public void setPass(string passes) {
		password = passes;
	}

	public void setConPass(string conPass) {
		confirm_pass = conPass;
	}

	public void setEmail(string em) {
		email_var = em;
	}

	private void register_user(string user, string pass, string email) {
		ParseUser newuser = new ParseUser(){
			Username = user,
			Password = pass,
			Email = email
		};
		
		newuser.SignUpAsync();
		Debug.Log ("new user " + user + " added");
	}

	
}
