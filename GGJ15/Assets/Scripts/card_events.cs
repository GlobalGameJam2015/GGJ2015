using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class card_events : MonoBehaviour {
	public Vector3 getCurrent;
	public bool draggable;
	public bool dragging;
	//Vector3 screenPos;
	//public List<string> cardList = new List<string>();
	

	void OnMouseEnter() {
		getCurrent = gameObject.transform.position;
		iTween.MoveTo (gameObject, new Vector3(getCurrent.x, -7.5f, 1), 0.5f);
		iTween.ScaleTo (gameObject, new Vector3(1.5f,1.5f,1f), 1);
		draggable = true;
	}

	void OnMouseOver() {
		//Debug.Log (this.tag);

	}

	void OnMouseExit() {
		makeSmall ();
		draggable = false;
	}
	
	void OnMouseDown() {
		Debug.Log ("clicked");
	}
	
	void makeSmall() {
		iTween.MoveTo (gameObject, new Vector3(getCurrent.x, -11.75f, 2), 0.5f);
		iTween.ScaleTo (gameObject, new Vector3(1f,1f,1f), 0.5f);
	}
}
