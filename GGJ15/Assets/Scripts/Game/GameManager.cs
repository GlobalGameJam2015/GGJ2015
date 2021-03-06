﻿/*This script is the function as the controller for your whole turn 
 * Activating specific parts (mulligan, draw a card, place a card, activate a card, etc.)*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class FieldCards{
	public List<Card> Opponets = new List<Card>();
	public List<Card> Self = new List<Card>();
}

public class GameManager : Photon.MonoBehaviour {

	public bool isSinglePlayer;

	//Reference to the deck builder
	public DeckBuilder Deck;

	//List of cards in hand
	public List<Card> Hand = new List<Card>();
	public List<Card> OtherHand = new List<Card>();
	private Rect HandRect;
	private float HandYMovement;
	private string PlayedCardsString;
	private int OpponentHandCount = 5;

	//Game Variables
	public int People;
	public int TotalResources = 2;
	public int _Resources = 2;
	public int Entertainment;
	private int PeopleEntertainment;
	public FieldCards Field;
	public int CardMax = 5;
	public static bool YourTurn = false;
	public int SmartChickEff = 1;

	//GUISTYLES
	public Texture2D BG;
	public Texture2D CardBack;
	public Texture2D PartyCard;
	public GUIStyle EndTurnBtn;
	public GUIStyle Title;
	public GUIStyle ResourceText;
	public GUIStyle Numbers;
	public GUIStyle Description;
	public GUIStyle TitlePlayed;
	public GUIStyle NumbersPlayed;
	public GUIStyle DescriptionPlayed;
	public GUIStyle ResourcePoints;
	public GUIStyle EntertainmentPoints;
	public GUIStyle PeoplePoints;
	public GUIStyle Invisible = new GUIStyle();

	//Party Level Hash
	public int PartyLevel = 0;
	public int[] PeopleLevel = new int[]{6,9,12,15,18,21};
	public int[] EntertainmentLevel = new int[]{7,9,11,13,15,17};

	// Use this for initialization
	void Start () {
		Debug.Log("YOU ARE NOW IN THE GAME SCENE");
		Deck = GetComponent<DeckBuilder>();
	}

	public IEnumerator InitialDraw(){
		for(int i = 0; i < 4; i++){
			DrawCard(0);
			yield return new WaitForSeconds(1);
		}
		photonView.RPC("SendInitialDraw",PhotonTargets.Others);
	}

	public IEnumerator StartTurn(){
		if(Hand.Count < CardMax){
			DrawCard(0);
			yield return new WaitForSeconds(1);
		}
		PlayedCardsString = "";
		YourTurn = true;
	}

	public void EndTurn(){
		People = 0;
		Entertainment -= PeopleEntertainment;
		PeopleEntertainment = 0;
		foreach(Card card in Field.Self){
			Person tempPerson = card.CardObj.GetComponent<Person>();
			People += tempPerson.PersonCount;
			PeopleEntertainment += tempPerson.Entertainment;
		}
		Entertainment += PeopleEntertainment;
		photonView.RPC("PassTurn",PhotonTargets.Others);
		YourTurn = false;
	}



	//Draw a card
	public void DrawCard(int num){
		if(num == 0){
			Hand.Add(Deck.Shuffled[0]);
			photonView.RPC("PlayerDrew",PhotonTargets.Others);
		}
		else{
			OtherHand.Add(Deck.Shuffled[0]);
		}
		Deck.Shuffled.RemoveAt(0);
		//TODO: Animation and visual of card being drawn into hand
	}

	void OnGUI(){
		if(!isSinglePlayer){
			//GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),BG);
		
			for(int i = 0; i < OpponentHandCount; i++){
				GUI.BeginGroup(new Rect((Screen.width-(i*190))/2,-130,i*190,275));
				GUI.DrawTexture(new Rect(i*190,0,190,275),CardBack);
				GUI.EndGroup();
			}

			//begin a GUI group for the cards in your hand
			HandRect = new Rect((Screen.width-(Hand.Count*190))/2,Screen.height-285,190*Hand.Count,275);
			if(HandRect.Contains(new Vector3(Input.mousePosition.x,-1*(Input.mousePosition.y - Screen.height),0))){
				HandYMovement = 150;
			}
			else{
				HandYMovement = 0;
			}

			//On Field
			for(int self = 0; self < Field.Self.Count; self++){
				GUI.BeginGroup(new Rect((Screen.width-(Field.Self.Count*142))/2,Screen.height-350,Field.Self.Count*142,206));
				GUI.BeginGroup(new Rect(self*142,0,142,206));
				
				GUI.DrawTexture(new Rect(0,0,142,206),Field.Self[self].Image);
				GUI.Label(new Rect(5,3,104,20),Field.Self[self].Title,TitlePlayed);
				GUI.Label(new Rect(6,178,20,20),Field.Self[self].Resource.ToString(),NumbersPlayed);
				NumbersPlayed.normal.textColor = Color.white;
				GUI.Label(new Rect(117,178,20,20),Field.Self[self].People.ToString(),NumbersPlayed);
				NumbersPlayed.normal.textColor = Color.black;
				GUI.Label(new Rect(10,140,125,50),Field.Self[self].Effect,DescriptionPlayed);
				
				GUI.EndGroup();
				GUI.EndGroup();
			}
			for(int opponent = 0; opponent < Field.Opponets.Count; opponent++){
				GUI.BeginGroup(new Rect((Screen.width-(Field.Opponets.Count*142))/2,144,Field.Opponets.Count*142,206));
				GUI.BeginGroup(new Rect(opponent*142,0,142,206));
				
				GUI.DrawTexture(new Rect(0,0,142,206),Field.Opponets[opponent].Image);
				GUI.Label(new Rect(5,3,104,20),Field.Opponets[opponent].Title,TitlePlayed);
				GUI.Label(new Rect(6,178,20,20),Field.Opponets[opponent].Resource.ToString(),NumbersPlayed);
				NumbersPlayed.normal.textColor = Color.white;
				GUI.Label(new Rect(117,178,20,20),Field.Opponets[opponent].People.ToString(),NumbersPlayed);
				NumbersPlayed.normal.textColor = Color.black;
				GUI.Label(new Rect(10,140,125,50),Field.Opponets[opponent].Effect,DescriptionPlayed);
				
				GUI.EndGroup();
				GUI.EndGroup();
			}

			//Other HUD
			GUI.BeginGroup(new Rect(880,490,100,105));
			GUI.Label(new Rect(0,0,100,20),"Resources",Title);
			GUI.Box(new Rect(14,20,70,85),_Resources.ToString(),ResourcePoints);
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(0,490,220,105));
			GUI.Label(new Rect(20,0,60,20),"People",Title);
			GUI.Box(new Rect(20,20,70,85),People.ToString(),PeoplePoints);
			GUI.Label(new Rect(100,0,120,20),"Entertainment",Title);
			GUI.Box(new Rect(122,20,70,85),Entertainment.ToString(),EntertainmentPoints);
			GUI.EndGroup();

			if(YourTurn){
				if(GUI.Button(new Rect(21,230,179,43),"",EndTurnBtn)){
					EndTurn();
				}
			}
			
			GUI.BeginGroup(new Rect(39,278,142,206));
			GUI.DrawTexture(new Rect(0,0,142,206),PartyCard);
			GUI.Label(new Rect(5,4,104,20),"Goal Party Level",TitlePlayed);
			NumbersPlayed.normal.textColor = Color.white;
			GUI.Label(new Rect(4,179,20,20),PeopleLevel[PartyLevel].ToString(),NumbersPlayed);
			GUI.Label(new Rect(115,179,20,20),EntertainmentLevel[PartyLevel].ToString(),NumbersPlayed);
			NumbersPlayed.normal.textColor = Color.black;
			GUI.Label(new Rect(15,135,110,50),"Reach your Party Level before your Opponent.",DescriptionPlayed);
			GUI.EndGroup();

			GUI.BeginGroup(new Rect((Screen.width-(OtherHand.Count*190))/2,-135,OtherHand.Count*190,275));
				for(int i = 0; i < OtherHand.Count; i++){
				GUI.DrawTexture(new Rect(i*190,0,190,275),CardBack);
				}
			GUI.EndGroup();

			//HAND
			GUI.BeginGroup(new Rect((Screen.width-(Hand.Count*190))/2,Screen.height-135-HandYMovement,Hand.Count*190,275));
			for(int i = 0; i < Hand.Count; i++){
				if(Hand[i].Type == CardTypes.People){
					GUI.BeginGroup(new Rect(i*190,0,190,275));
					
					GUI.DrawTexture(new Rect(0,0,190,275),Hand[i].Image);
					GUI.Label(new Rect(5,8,185,20),Hand[i].Title,Title);
					GUI.Label(new Rect(10,243,20,20),Hand[i].Resource.ToString(),Numbers);
					Numbers.normal.textColor = Color.white;
					GUI.Label(new Rect(160,243,20,20),Hand[i].People.ToString(),Numbers);
					Numbers.normal.textColor = Color.black;
					GUI.Label(new Rect(30,190,130,50),Hand[i].Effect,Description);

					if(GUI.Button(new Rect(0,0,190,275),"",Invisible)){
						if(YourTurn){
							if(_Resources >= Hand[i].Resource){

								_Resources -= Hand[i].Resource;

								Hand[i].CardObj = new GameObject();
								Hand[i].CardObj.name = Hand[i].Title;
								Hand[i].CardObj.transform.parent = transform.GetChild(0);
								Hand[i].CardObj.AddComponent(Hand[i].Title.Replace(" ",""));
								Hand[i].CardObj.SendMessage("Played",Hand[i],SendMessageOptions.DontRequireReceiver);

								if (!Hand[i].CardObj.GetComponent<Person>().isOnWrongSide)
									Field.Self.Add(Hand[i]);
								else
									Field.Opponets.Add(Hand[i]);
								PlayedCardsString += i+",";
								photonView.RPC("PlayerPlayedACard",PhotonTargets.Others,0,i);
								Hand.RemoveAt(i);
								Debug.Log("SELECT CARD");
								
							}
							else{
								Debug.Log("Not enough Resources");
							}
						}
						else{
							Debug.Log("Don't Cheat It's Not Your Turn");
						}
					}

					GUI.EndGroup();
				}
				else if(Hand[i].Type == CardTypes.Entertainment || Hand[i].Type == CardTypes.Foul){
					GUI.BeginGroup(new Rect(i*190,0,190,275));
					
					GUI.DrawTexture(new Rect(0,0,190,275),Hand[i].Image);
					GUI.Label(new Rect(5,8,185,20),Hand[i].Title,Title);
					GUI.Label(new Rect(10,243,20,20),(Hand[i].Resource/SmartChickEff).ToString(),Numbers);
					Numbers.normal.textColor = Color.white;
					GUI.Label(new Rect(160,243,20,20),Hand[i].Amount.ToString(),Numbers);
					Numbers.normal.textColor = Color.black;
					GUI.Label(new Rect(30,190,130,50),Hand[i].Effect,Description);

					if(GUI.Button(new Rect(0,0,190,275),"",Invisible)){
						if(YourTurn){
							if(TotalResources >= Hand[i].Resource){
								TotalResources -= Hand[i].Resource/SmartChickEff;
								_Resources = TotalResources;
								if(Hand[i].Type == CardTypes.Entertainment){
									Entertainment += Hand[i].Amount;
									photonView.RPC("PlayerPlayedACard",PhotonTargets.Others,1,i);
								}
								else{
									//DO NEGATIVE EVENTS EFFECT
									photonView.RPC("PlayerPlayedACard",PhotonTargets.Others,2,i);
								}
								
								Hand.RemoveAt(i);
							}
							else{
								Debug.Log("Not enough Resources");
							}
						}
						else{
							Debug.Log("Don't Cheat It's Not Your Turn");
						}
					}
					
					GUI.EndGroup();
				}
				else if(Hand[i].Type == CardTypes.Resource){
					GUI.BeginGroup(new Rect(i*190,0,190,275));
					
					GUI.DrawTexture(new Rect(0,0,190,275),Hand[i].Image);
					GUI.Label(new Rect(5,8,185,20),Hand[i].Title,Title);
					GUI.Label(new Rect(30,230,130,50),Hand[i].Effect,Description);
					
					GUI.Box(new Rect(30,70,130,130),Hand[i].Resource.ToString(),ResourceText);

					if(GUI.Button(new Rect(0,0,190,275),"",Invisible)){
						if(YourTurn){
							TotalResources += Hand[i].Resource;
							_Resources = TotalResources;
							photonView.RPC("PlayerPlayedACard",PhotonTargets.Others,3,i);
							Hand.RemoveAt(i);
						}
						else{
							Debug.Log("Don't Cheat It's Not Your Turn");
						}
					}
					
					GUI.EndGroup();
				}	
				else if(Hand[i].Type == CardTypes.Party){
					GUI.BeginGroup(new Rect(i*190,0,190,275));
					
					GUI.DrawTexture(new Rect(0,0,190,275),Hand[i].Image);
					GUI.Label(new Rect(5,8,185,20),Hand[i].Title,Title);
					Numbers.normal.textColor = Color.white;
					GUI.Label(new Rect(10,243,20,20),Hand[i].Resource.ToString(),Numbers);
					GUI.Label(new Rect(160,243,20,20),Hand[i].People.ToString(),Numbers);
					Numbers.normal.textColor = Color.black;
					GUI.Label(new Rect(30,190,130,50),Hand[i].Effect,Description);

					if(GUI.Button(new Rect(0,0,190,275),"",Invisible)){
						PartyLevel++;
						Hand.RemoveAt(i);
					}

					GUI.EndGroup();
				}				
			}
			GUI.EndGroup();
		}
	}
	
	[RPC]
	void SendInitialDraw(){
		if(PhotonNetwork.isMasterClient){
			StartCoroutine(StartTurn());
		}
		else{
			StartCoroutine(InitialDraw());
		}
	}

	[RPC]
	void PlayerDrew(){
		Debug.Log("Player Drew a Card");
		DrawCard(1);
	}

	[RPC]
	void PlayerPlayedACard(int type, int card){
		Debug.Log("Player Played A Card");
		if (YourTurn) {
			Debug.Log("is your turn");
		} else {
			Debug.Log ("is thier turn");
		}
		if(type == 0){
			OtherHand[card].CardObj = new GameObject();
			OtherHand[card].CardObj.name = OtherHand[card].Title;
			OtherHand[card].CardObj.transform.parent = transform.GetChild(1);
			OtherHand[card].CardObj.AddComponent(OtherHand[card].Title.Replace(" ",""));
			OtherHand[card].CardObj.SendMessage("Played",OtherHand[card],SendMessageOptions.DontRequireReceiver);
			if (!OtherHand[card].CardObj.GetComponent<Person>().isOnWrongSide)
				Field.Opponets.Add(OtherHand[card]);
			else
				Field.Self.Add(OtherHand[card]);
			OtherHand.RemoveAt(card);
		}
		if(type == 1){
			OtherHand.RemoveAt(card);
		}
		if(type == 2){
			OtherHand.RemoveAt(card);
		}
		if(type == 3){
			//TotalResources += Hand[i].Resource;
			//_Resources = TotalResources;

			OtherHand.RemoveAt(card);
		}
	}

	[RPC]
	void PassTurn(){
		Debug.Log("RECIEVED END TURN FROM OPPONENT");
		StartCoroutine(StartTurn());
	}

	[RPC]
	void PassEffect(int field, string name, int resources, int entertrainment){
		if(field >= 0)
			Field.Self.RemoveAt(field);
		if(name != ""){
			foreach(Card card in Field.Self){
				if(card.Title == name){
					People -= card.People;
					Field.Self.Remove(card);
				}
			}
		}
		if(resources != 0)
			_Resources += resources;
		if(entertrainment != 0)
			Entertainment += entertrainment;
	}
}
