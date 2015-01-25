/*This script is the function as the controller for your whole turn 
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

	public bool MultiPlayer = true;

	//Reference to the deck builder
	public DeckBuilder Deck;

	//List of cards in hand
	public List<Card> Hand = new List<Card>();
	private Rect HandRect;
	private float HandYMovement;
	private string PlayedCardsString;
	private int OpponentHandCount = 5;

	//Game Variables
	public int People;
	public int TotalResources;
	public int _Resources;
	public int Entertainment;
	public FieldCards Field;
	public int CardMax = 5;
	public bool YourTurn;
	public int SmartChickEff = 1;

	//GUISTYLES
	public Texture2D BG;
	public Texture2D CardBack;
	public Texture2D PartyCard;
	public GUIStyle EndTurn;
	public GUIStyle Title;
	public GUIStyle ResourceText;
	public GUIStyle Numbers;
	public GUIStyle Description;
	public GUIStyle TitlePlayed;
	public GUIStyle NumbersPlayed;
	public GUIStyle DescriptionPlayed;
	public GUIStyle Points;
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
			DrawCard();
			yield return new WaitForSeconds(1);
		}
		//photonView.RPC("SendInitialDraw",PhotonTargets.Others);
		StartCoroutine(StartTurn());
	}

	public IEnumerator StartTurn(){
		if(Hand.Count < CardMax){
			DrawCard();
			yield return new WaitForSeconds(1);
		}
		PlayedCardsString = "";
		YourTurn = true;
	}

	//Draw a card
	public void DrawCard(){
		Hand.Add(Deck.Shuffled[0]);
		Deck.Shuffled.RemoveAt(0);
		//TODO: Animation and visual of card being drawn into hand
	}

	void OnGUI(){
		if(MultiPlayer){
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
				GUI.Label(new Rect(117,178,20,20),Field.Self[self].Value.ToString(),NumbersPlayed);
				GUI.Label(new Rect(10,140,125,50),Field.Self[self].Effect,DescriptionPlayed);
				
				GUI.EndGroup();
				GUI.EndGroup();
			}
			for(int opponent = 0; opponent < Field.Opponets.Count; opponent++){
				GUI.BeginGroup(new Rect((Screen.width-(Field.Opponets.Count*142))/2,144,Field.Opponets.Count*142,206));
				GUI.Box(new Rect(0,0,142,206),"");
				GUI.BeginGroup(new Rect(opponent*142,0,142,206));
				
				GUI.DrawTexture(new Rect(0,0,142,206),Field.Self[opponent].Image);
				GUI.Label(new Rect(5,3,104,20),Field.Self[opponent].Title,TitlePlayed);
				GUI.Label(new Rect(6,178,20,20),Field.Self[opponent].Resource.ToString(),NumbersPlayed);
				GUI.Label(new Rect(117,178,20,20),Field.Self[opponent].Value.ToString(),NumbersPlayed);
				GUI.Label(new Rect(10,140,125,50),Field.Self[opponent].Effect,DescriptionPlayed);
				
				GUI.EndGroup();
				GUI.EndGroup();
			}

			//Other HUD
			GUI.BeginGroup(new Rect(0,(Screen.height-225)/2-110,220,105));
			GUI.Label(new Rect(10,0,100,20),"Resources",Title);
			GUI.Box(new Rect(20,20,70,85),TotalResources.ToString(),Points);
			GUI.Label(new Rect(100,0,120,20),"Entertainment",Title);
			GUI.Box(new Rect(122,20,70,85),Entertainment.ToString(),Points);
			GUI.EndGroup();
			
			if(GUI.Button(new Rect(21,290,179,43),"",EndTurn)){
				photonView.RPC("PassTurn",PhotonTargets.Others);
			}
			
			GUI.BeginGroup(new Rect(39,343,142,206));
			GUI.DrawTexture(new Rect(0,0,142,206),PartyCard);
			GUI.Label(new Rect(5,4,104,20),"Goal Party Level",TitlePlayed);
			GUI.Label(new Rect(4,179,20,20),PeopleLevel[PartyLevel].ToString(),NumbersPlayed);
			GUI.Label(new Rect(115,179,20,20),EntertainmentLevel[PartyLevel].ToString(),NumbersPlayed);
			GUI.Label(new Rect(15,135,110,50),"Reach your Party Level before your Opponent.",DescriptionPlayed);
			GUI.EndGroup();

			GUI.BeginGroup(new Rect((Screen.width-(Hand.Count*190))/2,Screen.height-135-HandYMovement,Hand.Count*190,275));
			for(int i = 0; i < Hand.Count; i++){
				if(Hand[i].Type == CardTypes.People){
					GUI.BeginGroup(new Rect(i*190,0,190,275));
					
					GUI.DrawTexture(new Rect(0,0,190,275),Hand[i].Image);
					GUI.Label(new Rect(5,8,185,20),Hand[i].Title,Title);
					GUI.Label(new Rect(10,243,20,20),Hand[i].Resource.ToString(),Numbers);
					GUI.Label(new Rect(160,243,20,20),Hand[i].Value.ToString(),Numbers);
					GUI.Label(new Rect(30,190,130,50),Hand[i].Effect,Description);

					if(GUI.Button(new Rect(0,0,190,275),"",Invisible)){
						if(_Resources >= Hand[i].Resource){
							_Resources -= Hand[i].Resource;

							Hand[i].CardObj = new GameObject();
							Hand[i].CardObj.name = Hand[i].Title;
							Hand[i].CardObj.transform.parent = transform.GetChild(0);
							Hand[i].CardObj.AddComponent(Hand[i].Title.Replace(" ",""));
							Hand[i].CardObj.SendMessage("Played",SendMessageOptions.DontRequireReceiver);

							Field.Self.Add(Hand[i]);
							PlayedCardsString += i+",";
							Hand.RemoveAt(i);
							Debug.Log("SELECT CARD");
							
						}
						else{
							Debug.Log("Not enough Resources");
						}
					}

					GUI.EndGroup();
				}
				else if(Hand[i].Type == CardTypes.Entertainment || Hand[i].Type == CardTypes.Foul){
					GUI.BeginGroup(new Rect(i*190,0,190,275));
					
					GUI.DrawTexture(new Rect(0,0,190,275),Hand[i].Image);
					GUI.Label(new Rect(5,8,185,20),Hand[i].Title,Title);
					GUI.Label(new Rect(10,243,20,20),(Hand[i].Resource/SmartChickEff).ToString(),Numbers);
					//GUI.Label(new Rect(160,243,20,20),Hand[i].Value.ToString(),Numbers);
					GUI.Label(new Rect(30,190,130,50),Hand[i].Effect,Description);

					if(GUI.Button(new Rect(0,0,190,275),"",Invisible)){
						if(TotalResources >= Hand[i].Resource){
							TotalResources -= Hand[i].Resource/SmartChickEff;
							_Resources = TotalResources;
							if(Hand[i].Type == CardTypes.Entertainment){
								Entertainment += Hand[i].Amount;
							}
							else{
								//DO NEGATIVE EVENTS EFFECT
							}
							
							Hand.RemoveAt(i);
						}
						else{
							Debug.Log("Not enough Resources");
						}
					}
					
					GUI.EndGroup();
				}
				else if(Hand[i].Type == CardTypes.Resource){
					GUI.BeginGroup(new Rect(i*190,0,190,275));
					
					GUI.DrawTexture(new Rect(0,0,190,275),Hand[i].Image);
					GUI.Label(new Rect(5,8,185,20),Hand[i].Title,Title);
					GUI.Label(new Rect(30,190,130,50),Hand[i].Effect,Description);
					
					GUI.Box(new Rect(30,40,130,130),Hand[i].Resource.ToString(),ResourceText);

					if(GUI.Button(new Rect(0,0,190,275),"",Invisible)){
						TotalResources += Hand[i].Resource;
						_Resources = TotalResources;
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
		if(PhotonNetwork.isMasterClient)
			StartCoroutine(StartTurn());
		else
			StartCoroutine(InitialDraw());
	}

	[RPC]
	void PassTurn(){
		StartCoroutine(StartTurn());
	}

	[RPC]
	void PassEffect(int field, string name, int resources, int entertrainment){
		if(field >= 0)
			Field.Self.RemoveAt(field);
		if(name != ""){
			foreach(Card card in Field.Self){
				if(card.Title == name){
					People -= card.Value;
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
