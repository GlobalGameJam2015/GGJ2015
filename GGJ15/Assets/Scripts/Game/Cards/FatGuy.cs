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
		scopeOutSluts ();
		
	}

	void scopeOutSluts() {
			//Debug.Log (slutty.Title);
		int playerFatGuys;
		int enemyFatGuys;
		if (GameManager.YourTurn) {
			playerFatGuys = 0;
			enemyFatGuys = 1;
		} else {
			playerFatGuys = 1;
			enemyFatGuys = 0;
		}
			foreach(Card card in Manager.Field.Self){
				if(card.Title == "Fat Guy"){
					playerFatGuys++;
				}
			}
			foreach(Card card in Manager.Field.Opponets){
				if(card.Title == "Fat Guy"){
					enemyFatGuys++;
				}
			}
		if(GameManager.YourTurn){
			foreach(Card slutty in Manager.Field.Opponets){
				if (slutty.Title == "Slutty Chick") {
					if (enemyFatGuys>playerFatGuys) {
						Manager.Field.Opponets.Remove(slutty);
						Manager.Field.Self.Add(slutty);
					}
				}
			}
		} else {
			foreach(Card slutty2 in Manager.Field.Self){
				if (slutty2.Title == "Slutty Chick") {
					if (playerFatGuys>enemyFatGuys) {
						Manager.Field.Self.Remove(slutty2);
						Manager.Field.Opponets.Add(slutty2);
					}
				}
			}
		}
	}

}
