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
		if (GameManager.YourTurn) {
			Debug.Log("jock on your tur");
			foreach(Card card in Manager.Field.Self){
				if(card.Title == "Fat Guy"){
					Manager.Field.Self.Remove(card);
					return;
				}
			}
		} else {
			Debug.Log ("Jock on their turn");
			foreach(Card card in Manager.Field.Opponets){
				if(card.Title == "Fat Guy"){
					Manager.Field.Opponets.Remove(card);
					return;
				}
			}
		}
	}
}
