using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Person))]
public class SmartChick : MonoBehaviour {


	private GameObject Target;
	private GameManager Manager;

	private Ray ray;
	private RaycastHit hit;

	private Person person;

	void Played () {
		Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
		person = this.GetComponent<Person>();
		if (GameManager.YourTurn) {
			Debug.Log("smart chick on your turn");
			Manager.SmartChickEff = 2;
		} else {
			//no effect on opponent side.
			Debug.Log ("smart chick on their turn");
		}
	}
}
