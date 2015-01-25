﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Person))]
public class SluttyChick : MonoBehaviour {
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
			//SLUT GOES TO ENEMY;
		}else{
			//SLUT STAYS HERE;
		}
	}
}