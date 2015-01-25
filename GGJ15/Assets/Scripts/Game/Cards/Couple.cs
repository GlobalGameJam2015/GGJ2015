using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Person))]
public class Couple : MonoBehaviour {


	private GameObject Target;
	private GameManager Manager;

	private Person person;

	void Start(){
		Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
		person = this.GetComponent<Person>();
	}

	private int turnCount = 2;
	void OnTurn(){
		turnCount--;
		if(turnCount==0){
			person.SetPersonCount(4);
		}
	}

	void Played (Card card) {
	}
}
