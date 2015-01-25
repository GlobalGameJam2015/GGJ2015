using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Person))]
public class Couple : MonoBehaviour {


	private GameObject Target;
	private GameManager Manager;

	private Ray ray;
	private RaycastHit hit;

	private Person person;

	void Start(){
		Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
		person = this.GetComponent<Person>();
	}

	/*private int turnCount = 2;
	void OnTurn(){
		turnCount--;
		if(turn==0){
			SetPersonCount(4);
		}
	}*/

	void Update(){
		Ray ray;

	}

	void Played () {
		person.SetPersonCount(2);
		person.SetResourceNeed(3);
	}
}
