using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Person))]
public class PopularChick : MonoBehaviour {
	private GameManager Manager;
	
	private Person person;
	
	void Played () {
		Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
		person = this.GetComponent<Person>();

		if (GameManager.YourTurn) {
			Debug.Log("popular chick on your tur");
			foreach(Card card in Manager.Field.Self){
				if(card.Title == "Jock"){
					person.SetPersonCount(2);
					return;
				}
			}
		} else {
			//currently no effect on opposing side.
			Debug.Log ("Popular Chick on their turn");
		}
	}
}
