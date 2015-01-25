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
	private int OpponentHandCount;

	//temp
	private List<bool> mulligans = new List<bool>();

	//Game Variables
	public int People;
	public int TotalResources;
	public int _Resources;
	public int Entertainment;
	public FieldCards Field;
	public int CardMax = 5;
	public bool YourTurn;

	//GUISTYLES
	public Texture2D BG;
	public Texture2D CardBack;
	public GUIStyle EndTurn;
	public GUIStyle Title;
	public GUIStyle ResourceText;
	public GUIStyle Numbers;
	public GUIStyle Description;
	public GUIStyle Points;
	public GUIStyle Invisible = new GUIStyle();

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
		StartCoroutine(StartTurn());
	}

	public IEnumerator StartTurn(){
		if(Hand.Count < CardMax){
			DrawCard();
			yield return new WaitForSeconds(1);
		}
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
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),BG);

			GUI.BeginGroup(new Rect((Screen.width-(Hand.Count*190))/2,-130,Hand.Count*190,275));
			for(int i = 0; i < Hand.Count; i++){
				GUI.DrawTexture(new Rect(i*190,0,190,275),CardBack);
			}
			GUI.EndGroup();

			//begin a GUI group for the cards in your hand
			GUI.BeginGroup(new Rect((Screen.width-(Hand.Count*190))/2,Screen.height-295,Hand.Count*190,275));
			for(int i = 0; i < Hand.Count; i++){

				if(Hand[i].Type == CardTypes.People){
					GUI.BeginGroup(new Rect(i*190,0,190,275));
					
					GUI.DrawTexture(new Rect(0,0,190,275),Hand[i].Image);
					GUI.Label(new Rect(5,8,185,20),Hand[i].Title,Title);
					GUI.Label(new Rect(10,243,20,20),Hand[i].Resource.ToString(),Numbers);
					GUI.Label(new Rect(160,243,20,20),Hand[i].Value.ToString(),Numbers);
					GUI.Label(new Rect(30,190,130,50),Hand[i].Effect,Description);

					GUI.EndGroup();

					if(GUI.Button(new Rect(i*190,0,190,275),"",Invisible)){
						if(_Resources >= Hand[i].Resource){
							Debug.Log("SELECT CARD");
						}
						else{
							Debug.Log("Not enough Resources");
						}
					}
				}
				else if(Hand[i].Type == CardTypes.Entertainment || Hand[i].Type == CardTypes.Foul){
					GUI.BeginGroup(new Rect(i*190,0,190,275));
					
					GUI.DrawTexture(new Rect(0,0,190,275),Hand[i].Image);
					GUI.Label(new Rect(5,8,185,20),Hand[i].Title,Title);
					GUI.Label(new Rect(10,243,20,20),Hand[i].Resource.ToString(),Numbers);
					//GUI.Label(new Rect(160,243,20,20),Hand[i].Value.ToString(),Numbers);
					GUI.Label(new Rect(30,190,130,50),Hand[i].Effect,Description);
					
					GUI.EndGroup();

					if(GUI.Button(new Rect(i*190,0,190,275),"",Invisible)){
						if(TotalResources >= Hand[i].Resource){
							TotalResources -= Hand[i].Resource;
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
				}
				else if(Hand[i].Type == CardTypes.Resource){
					GUI.BeginGroup(new Rect(i*190,0,190,275));
					
					GUI.DrawTexture(new Rect(0,0,190,275),Hand[i].Image);
					GUI.Label(new Rect(5,8,185,20),Hand[i].Title,Title);
					
					GUI.Box(new Rect(30,40,130,130),Hand[i].Resource.ToString(),ResourceText);
					
					GUI.EndGroup();

					if(GUI.Button(new Rect(i*190,0,190,275),"",Invisible)){
						TotalResources += Hand[i].Resource;
						_Resources = TotalResources;
						Hand.RemoveAt(i);
						Debug.Log("SELECT CARD");
					}
				}				
			}
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(40,(Screen.height-225)/2-110,120,225));
			GUI.Label(new Rect(10,0,100,20),"Resources",Title);
			GUI.Box(new Rect(20,20,70,85),TotalResources.ToString(),Points);
			GUI.Label(new Rect(0,120,120,20),"Entertainment",Title);
			GUI.Box(new Rect(25,140,70,85),Entertainment.ToString(),Points);
			GUI.EndGroup();

			if(GUI.Button(new Rect(10,400,179,43),"",EndTurn)){
				photonView.RPC("PassTurn",PhotonTargets.Others);
			}
		}
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
