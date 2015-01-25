using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Person))]
public class Jock : MonoBehaviour {


	private GameObject Target;
	private GameManager Manager;

	private Ray ray;
	private RaycastHit hit;

	private Person person;

	void Start(){
		Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void Update(){
		Ray ray;

	}

	void Played () {
		person = this.GetComponent<Person>();
		foreach(Card card in Manager.Field.Self){
			if(card.Title == "Fat Guy"){
				_modifier = 1;
				Manager.Field.Self.Remove(card);
				break;
			}
		}
	}
}
