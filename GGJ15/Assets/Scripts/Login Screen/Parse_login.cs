using UnityEngine;
using System.Collections;
using Parse;

public class Parse_login : MonoBehaviour {
	private string log_user;
	private string log_pass;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setUser(string user) {
		log_user = user;
	}

	public void setPass(string pass) {
		log_pass = pass;
	}


	public void parse_login() {
		ParseUser.LogInAsync(log_user, log_pass).ContinueWith(t =>
		                                                      {
			if (t.IsFaulted || t.IsCanceled)
			{
				// The login failed. Check the error to see why.
				Debug.Log ("you have failed to log in.");
			}
			else
			{
				// Login was successful.
				Debug.Log ("you have successfully logged in.");
				has_Logged_In();
			}
		});
	}

	public void has_Logged_In() {
		if (ParseUser.CurrentUser != null)
		{
			// do stuff with the user
		}
		else
		{
			// show the signup or login screen
		}
	}
}
