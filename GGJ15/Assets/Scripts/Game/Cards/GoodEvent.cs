using UnityEngine;
using System.Collections;

public class GoodEvent : MonoBehaviour {

	public static int Num;

	public static void Played () {
		GameObject.Find("GameManager").GetComponent<GameManager>().Entertainment += Num;
		//Destroy(this);
	}
}
