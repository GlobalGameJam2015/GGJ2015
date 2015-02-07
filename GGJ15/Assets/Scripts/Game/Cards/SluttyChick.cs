using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Person))]
public class SluttyChick : MonoBehaviour {
	private GameManager Manager;
	
	private Person person;
	private Card selfCard;
	
	void Played (Card card) {
		Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
		person = this.GetComponent<Person>();
		person.SetResourceNeed(0);
		//CheckSluttiness();
		if (GameManager.YourTurn) {
			Debug.Log("Slutty Chick on your tur");
		} else {
			Debug.Log ("Slutty Chick on their turn");
		}

	}

	void CheckSluttiness(){
		int playerFatGuys = 0;
		int enemyFatGuys = 0;
		if (GameManager.YourTurn) {
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
			if(playerFatGuys>enemyFatGuys){
				person.MoveToOpponent();
			}
		}
	}
}