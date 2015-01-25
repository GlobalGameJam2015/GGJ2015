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
		CheckSluttiness();

	}

	void CheckSluttiness(){
		int playerFatGuys = 0;
		int enemyFatGuys = 0;
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