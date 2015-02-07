using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Person))]
public class FatGuy : MonoBehaviour {
	private GameManager Manager;
	private Person person;
	private Card selfCard;

	void Played (Card card) {
		Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
		person = this.GetComponent<Person>();

		//person.MoveToOpponent();
	}

	void Moved(){
		//
	}
}
