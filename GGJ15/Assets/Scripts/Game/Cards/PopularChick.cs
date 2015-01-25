using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Person))]
public class PopularChick : MonoBehaviour {
	private GameManager Manager;
	
	private Person person;
	
	void Start(){
		Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
		person = this.GetComponent<Person>();
	}
	
	void Update(){
		Ray ray;
		
	}
	
	void Played () {
		foreach(Card card in Manager.Field.Self){
			if(card.Title == "Jock"){
				person.SetPersonCount(2);
				return;
			}
		}
	}
}
