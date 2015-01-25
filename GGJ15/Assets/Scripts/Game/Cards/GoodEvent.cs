using UnityEngine;
using System.Collections;

public class GoodEvent : MonoBehaviour {

	public int Num;

	void Played () {
		GameObject.Find("GameManager").GetComponent<GameManager>().Entertainment += Num;
		Destroy(this);
	}
}
