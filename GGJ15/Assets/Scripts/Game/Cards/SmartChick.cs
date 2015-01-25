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
		Manager.SmartChickEff = 2;
	}
}
