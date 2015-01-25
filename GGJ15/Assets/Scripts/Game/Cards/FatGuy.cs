using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Person))]
public class FatGuy : MonoBehaviour {
	private GameManager Manager;
	private Person person;

	void Played (Card card) {
		Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
		person = this.GetComponent<Person>();
		//MOVE TO OTHER PLAYER

		//LOSS OF RESOURCES

		//+1 Resource for all womens
	}

	void Moved(){
		//
	}
}
