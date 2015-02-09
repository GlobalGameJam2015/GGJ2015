using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Person))]
public class Playa : MonoBehaviour {

	private GameObject Target;
	private GameManager Manager;

	private Ray ray;
	private RaycastHit hit;

	private Person person;

	void Played () {
		Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
		person = this.GetComponent<Person>();
		if (GameManager.YourTurn) {
			int chickCount = 2;
			foreach(Card card in Manager.Field.Opponets){
				if(chickCount <= 0){
					return;
				}
				if(card.Gender == "Chick"){
					Manager.Field.Opponets.Remove(card);
					Manager.Field.Self.Add(card);
					chickCount--;
				}
			}
		} else {
			int chickCount = 2;
			foreach(Card card in Manager.Field.Self){
				if(chickCount <= 0){
					return;
				}
				if(card.Gender == "Chick"){
					Manager.Field.Self.Remove(card);
					Manager.Field.Opponets.Add(card);
					chickCount--;
				}
			}
		}
	}
}
