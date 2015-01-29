using UnityEngine;
using System.Collections;
using Parse;

public class Parse_Init : MonoBehaviour {

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
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
