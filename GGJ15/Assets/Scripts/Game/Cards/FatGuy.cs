using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Person))]
public class Jock : MonoBehaviour {
	private GameManager Manager;
	private Person person;

	void Start(){
		Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
		person = this.GetComponent<Person>();
	}

	void Played () {
		//MOVE TO OTHER PLAYER

		//LOSS OF RESOURCES
		
		person.SetResourceNeed(3);
		//+1 Resource for all womens
	}

	void Moved(){
		//
	}
}
