/*This script is the function as the controller for your whole turn 
 * Activating specific parts (mulligan, draw a card, place a card, activate a card, etc.)*/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	//Reference to the deck builder
	private DeckBuilder Deck;

	//List of cards in hand
	public List<Card> Hand = new List<Card>();

	//temp
	private List<bool> mulligans = new List<bool>();

	//Are you in mulligan phase
	public bool Mulligan;

	// Use this for initialization
	void Start () {
		Debug.Log("YOU ARE NOW IN THE GAME SCENE");
		Deck = GetComponent<DeckBuilder>();
		Debug.Log(GetComponent<DeckBuilder>());
	}
	
	// Begin Turn
	public void StartTurn () {

	}

	//Draw a card
	public void DrawCard(){
		Debug.Log(Hand);
		Debug.Log(Deck);
		Hand.Add(Deck.Shuffled[0]);
		Deck.Shuffled.RemoveAt(0);
		//TODO: Animation and visual of card being drawn into hand
	}

	//Draw cards for mulligan phase (num = number of cards to draw, mulligan = has mulligan started or ended)
	public IEnumerator DrawForMulligan(int num, bool mulligan){
		//Loop through and draw the correct number of cards
		for(int i = 0; i < num; i++){
			DrawCard();
			mulligans.Add(false);
			yield return new WaitForSeconds(1);
		}
		Mulligan = mulligan;
	}

	void OnGUI(){
		//Set the total number of mulliganed cards
		int Total = 0;
		foreach(bool mull in mulligans){
			if(mull)
				Total++;
		}

		//begin a GUI group for the cards in your hand
		GUI.BeginGroup(new Rect((Screen.width-(Hand.Count*110))/2,Screen.height-200,Hand.Count*110,150));
		for(int i = 0; i < Hand.Count; i++){
			GUI.color = Color.white;
			//Turn them red if they are to be mulliganed
			if(mulligans[i])
				GUI.color = Color.red;

			//Make sure the total mulligans does not exceed 3
			if(Total < 3){
				//Alternate the mulligan of this button (bool)
				if(GUI.Button(new Rect(i*110,0,100,150),Hand[i].Title)){
					mulligans[i] = !mulligans[i];
				}
			}
			else{
				//Set the mulligan to false for this button
				if(GUI.Button(new Rect(i*110,0,100,150),Hand[i].Title)){
					mulligans[i] = false;
				}
			}
		}
		GUI.EndGroup();

		//Display the Discard button if you are able to mulligan
		GUI.color = Color.white;
		if(Mulligan){
			if(GUI.Button(new Rect((Screen.width-80)/2,Screen.height-50,100,50),"Discard")){
				Total = 0;
				List<int> nums = new List<int>();
				//add the numbers of the cards to be mulliganed
				for(int i = 0; i < mulligans.Count; i++){
					if(mulligans[i]){
						nums.Add(i);
					}
				}
				//Reverse the list of mulliganed card numbers
				nums.Reverse();

				//sort through mulliganed numbers to start removing/replacing the cards (back to the deck).
				foreach(int n in nums){
					mulligans[n] = false;
					Deck.ReplaceCard(Hand[n]);
					Hand.RemoveAt(n);
					Total++;
				}
				//Draw for this stage of the mulligan
				StartCoroutine(DrawForMulligan(Total,false));
			}
		}
	}
}
