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
		person.isOnWrongSide = true;
		scopeOutSlutty ();
		
	}

	void scopeOutSlutty() {
		foreach(Card slutty in Manager.Field.Opponets){
			if (slutty.Title == "Slutty Chick") {
				slutty.CardObj.GetComponent<SluttyChick>().CheckSluttiness();
				if (slutty.CardObj.GetComponent<Person>().isOnWrongSide) {
					if(GameManager.YourTurn){
						Manager.Field.Self.Remove(slutty);
						Manager.Field.Opponets.Add(slutty);
					} else {
						Manager.Field.Opponets.Remove(slutty);
						Manager.Field.Self.Add(slutty);
					}
				}
			}
		}
	}

}
