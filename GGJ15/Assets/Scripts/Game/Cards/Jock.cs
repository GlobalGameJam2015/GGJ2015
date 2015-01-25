using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Person))]
public class Jock : MonoBehaviour {


	private GameObject Target;
	private GameManager Manager;

	private Ray ray;
	private RaycastHit hit;

	private Person person;

	void Played () {
		Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
		person = this.GetComponent<Person>();
		person.SetResourceNeed(2);
		foreach(Card card in Manager.Field.Self){
			if(card.Title == "Fat Guy"){
				Manager.Field.Self.Remove(card);
				return;
			}
		}
	}
}
