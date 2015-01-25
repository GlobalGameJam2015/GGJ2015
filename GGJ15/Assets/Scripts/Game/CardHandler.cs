using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*[System.Serializable]
public class FieldCards{
	public List<Card> Opponets = new List<Card>();
	public List<Card> Self = new List<Card>();
}*/

public class CardHandler : MonoBehaviour {
	private GameManager Manager;

	private Card PlayedCard;

	private FieldCards Field = new FieldCards();

	void Start(){
		Manager = GetComponent<GameManager>();
	}

	/*public CardTypes Type;
	public string Title;
	public string Gender;
	public Texture2D Image;
	public int Resource;
	public int Value;
	public int Entertainment;
	public string Effect;
	public string Player;
	public string Target;
	public string Action;
	public int Amount;
	public string ActionTarget;
	public int Count;*/

	public void PlayCard(Card card){
		PlayedCard = card;
		DetectType();
	}

	private void DetectType(){
		if(PlayedCard.Type == CardTypes.People){
			DetectPlayer();
		}
		//else if(PlayedCard.Type == CardTypes.Events){
			
		//}
		else if(PlayedCard.Type == CardTypes.Resource){
			
		}
		else if(PlayedCard.Type == CardTypes.Party){
			
		}
		else{
			
		}
	}

	private void DetectPlayer(){
		if(PlayedCard.Player == "Self"){
			//Put on your feild
			ConductAction();
		}
		else if(PlayedCard.Player == "Opponent"){
			//put on opponets field
		}
		else{
			//Switches between fields
		}
	}

	private void ConductAction(){

		if(PlayedCard.TargetType == "All"){

		}
		else if(PlayedCard.TargetType == "Opponent"){
			foreach(Card card in Field.Opponets){
				if(PlayedCard.Target == card.Title){
					if(PlayedCard.TargetCount > 0){
						//Select Specific amount
					}
					else if(PlayedCard.TargetCount < 0){
						//Select all
					}
				}
			}
		}
		else if(PlayedCard.TargetType == "Self"){
			foreach(Card card in Field.Self){
				
			}
		}
		else if(PlayedCard.TargetType == "Hand"){
			foreach(Card card in Manager.Hand){
				
			}
		}
		else{

		}
	}

	private void DetectTarget(){
		if(PlayedCard.Target == "All"){

		}
		else if(PlayedCard.Target == "Self"){
			
		}
		else if(PlayedCard.Target == "Opponent"){
			
		}
		else if(PlayedCard.Target == "Card"){
			
		}
		else if(PlayedCard.Target == "Guy"){
			
		}
		else if(PlayedCard.Target == "Chick"){
			
		}
		else if(PlayedCard.Target == "Jocks"){
			
		}
		else if(PlayedCard.Target == "Fat Guy"){
			//DetectAction();
		}
		else if(PlayedCard.Target == "Slutty Chick"){
			
		}
		else if(PlayedCard.Target == "Popular Chick"){
			
		}
		else if(PlayedCard.Target == "Funny Guy"){
			
		}
		else if(PlayedCard.Target == "Couple"){
			
		}
		else if(PlayedCard.Target == "Playa"){
			
		}
		else if(PlayedCard.Target == "Smart Chick"){
			
		}
		else{
			
		}

	}
}
