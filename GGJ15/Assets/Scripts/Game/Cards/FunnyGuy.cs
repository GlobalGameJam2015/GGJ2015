using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Person))]
public class FunnyGuy : MonoBehaviour {
	
	
	private GameObject Target;
	private GameManager Manager;
	
	private Ray ray;
	private RaycastHit hit;
	
	private Person person;
	private bool isDoneGet = false;
	private int addedEntertainment = 0;
	
	void Start(){
		Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	void Update(){
		Ray ray;
		
	}
	
	void Played () {
		person = this.GetComponent<Person>();
		addedEntertainment = 1;
		foreach(Card card in Manager.Field.Self){
			if(card.Title == "Funny Guy"){
				addedEntertainment+=3;
				return;
			}
			isDoneGet = true;
		}
		if (isDoneGet)
			person.SetEntertainment (addedEntertainment);
	}
}
