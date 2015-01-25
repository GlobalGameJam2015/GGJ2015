using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class card_events : MonoBehaviour {
	public Vector3 getCurrent;
	public bool dragging;
	public GameObject[] listObjects;


	void OnMouseEnter() {
		int pos = gameObject.name.LastIndexOf("d") + 1;
		//cardExpandHandle(gameObject.name.Substring(pos, gameObject.name.Length - pos));
		//Debug.Log (gameObject.name.LastIndexOf("d"));
		getCurrent = gameObject.transform.position;
		iTween.MoveTo (gameObject, new Vector3(getCurrent.x, -7.5f, 1), 0.5f);
		iTween.ScaleTo (gameObject, new Vector3(1.5f,1.5f,1f), 1);
	}
	/*
	void cardExpandHandle(string cardNum) {
		listObjects = GameObject.FindGameObjectsWithTag ("playerCard");
		foreach (Object obj in listObjects) {
			int newpos = obj.name.LastIndexOf("d") + 1;
			string num = obj.name.Substring(newpos, gameObject.name.Length - newpos);// prints "world"
			//Debug.Log (obj.name);
			if (num != cardNum) {
				Debug.Log (obj.name);
			}

		}
		Debug.Log (testint);
		switch (cardNum) {
		case "1":
			//Debug.Log(1);
			break;
		case "2":
			//Debug.Log(2);
			break;
		case "3":
			//Debug.Log(3);
			break;
		case "4":
			//Debug.Log(4);
			break;
		case "5":
			//Debug.Log(5);
			break;
		default:
			//Debug.Log ("none");
			break;
		}

	}
*/

	void OnMouseExit() {
		makeSmall ();
	}
	
	void OnMouseDownAsButton() {
		Debug.Log ("clicked");
	}
	
	void makeSmall() {
		iTween.MoveTo (gameObject, new Vector3(getCurrent.x, -11.75f, 2), 0.5f);
		iTween.ScaleTo (gameObject, new Vector3(1f,1f,1f), 0.5f);
	}
}
