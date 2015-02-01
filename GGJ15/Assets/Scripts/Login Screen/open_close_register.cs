using UnityEngine;
using System.Collections;

public class open_close_register : MonoBehaviour {
	public static bool is_open = false;
	GameObject regBox;

	// Use this for initialization
	void Start () {
		regBox = GameObject.Find("Register_box");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggle_register() {
		if (!is_open) {
			iTween.MoveTo(regBox, new Vector3(314, 97, 0), 0);
			is_open = true;
		} else {
			iTween.MoveTo(regBox, new Vector3(-1000, 0, 0), 0);
			is_open = false;
		}
	}
}
