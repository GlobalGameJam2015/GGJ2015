/*This script is specifically for the card Deck Creating, Organizing, Shuffeling etc.*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

//ENUM for different card types
[System.Serializable]
public enum CardTypes{
	People = 0,
	Entertainment = 1,
	Foul = 2,
	Resource = 3,
	Party = 4,
	PartyLevel = 5,
}

//Class for holding card data
[System.Serializable]
public class Card{
	public CardTypes Type;
	public string Title;
	public string Gender;
	public Texture2D Image;
	public int Resource;
	public int People;
	public int Entertainment;
	public string Effect;
	public int Amount;
	public GameObject CardObj;
}

public class DeckBuilder : Photon.MonoBehaviour {

	//Reference to game manager
	private GameManager Manager;

	//Variables for all cards, deck, and shuffled deck
	public List<Card> Cards = new List<Card>();
	public List<Card> Shuffled = new List<Card>();
	private List<int> CardCounts = new List<int>();
	private string DeckString = "";
	private int TotalCards;

	//XML
	private TextAsset Xml;

	// Use this for initialization
	void Start () {
		//Set reference for game manager
		Manager = GetComponent<GameManager>();

		//Begin XML parse
		Xml = (TextAsset)Resources.Load ("cards");
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (Xml.text);
		XmlNodeList cardList = xmlDoc.GetElementsByTagName("populate");

		//Search through each node within populate (types of cards)
		foreach (XmlNode populate in cardList){

			XmlNodeList poptrim = populate.ChildNodes;

			//Search through each node within type of card (each card)
			foreach (XmlNode cardTypes in poptrim){

				//Search through each card
				foreach (XmlNode cardInfo in cardTypes){
					Cards.Add(new Card());
					//Search through inner data of each card
					foreach (XmlNode cardTitle in cardInfo) {
						if(cardTitle.Name == "type"){
							if(int.Parse(cardTitle.InnerText) == 0)
								Cards[Cards.Count-1].Type = CardTypes.People;
							if(int.Parse(cardTitle.InnerText) == 1)
								Cards[Cards.Count-1].Type = CardTypes.Entertainment;
							if(int.Parse(cardTitle.InnerText) == 2)
								Cards[Cards.Count-1].Type = CardTypes.Foul;
							if(int.Parse(cardTitle.InnerText) == 3)
								Cards[Cards.Count-1].Type = CardTypes.Resource;
							if(int.Parse(cardTitle.InnerText) == 4)
								Cards[Cards.Count-1].Type = CardTypes.Party;
							if(int.Parse(cardTitle.InnerText) == 5)
								Cards[Cards.Count-1].Type = CardTypes.PartyLevel;
						}
						if (cardTitle.Name == "title")
							Cards[Cards.Count-1].Title = cardTitle.InnerText;
						if (cardTitle.Name == "Gender")
							Cards[Cards.Count-1].Gender = cardTitle.InnerText;
						if (cardTitle.Name == "img")
							Cards[Cards.Count-1].Image = Resources.Load(cardTitle.InnerText) as Texture2D;
						if (cardTitle.Name == "resource")
							Cards[Cards.Count-1].Resource = int.Parse(cardTitle.InnerText);
						if (cardTitle.Name == "people")
							Cards[Cards.Count-1].People = int.Parse(cardTitle.InnerText);
						if (cardTitle.Name == "entertainment")
							Cards[Cards.Count-1].Entertainment = int.Parse(cardTitle.InnerText);
						if (cardTitle.Name == "effect")
							Cards[Cards.Count-1].Effect = cardTitle.InnerText;
						if (cardTitle.Name == "amount")
							Cards[Cards.Count-1].Amount = int.Parse(cardTitle.InnerText);
					}

					//Add to list the number of each card
					CardCounts.Add(Cards[Cards.Count-1].Amount);
					TotalCards += Cards[Cards.Count-1].Amount;
				}
			}
		}
		Debug.Log(TotalCards);
		//Shuffle deck if you are the host
		if(PhotonNetwork.isMasterClient || Manager.isSinglePlayer)
			ShuffleDeck();
	}

	//Handles deck shuffling
	void ShuffleDeck(){
		int RandomCard;
		//Places a random card from total count into shuffled deck list
		for(int i = 0; i < TotalCards; i++){
			RandomCard = GetRandomCard();
			Shuffled.Add(Cards[RandomCard]);
			CardCounts[RandomCard]--;
		}

		for(int i = 0; i < Cards[Cards.Count-1].Amount; i++){
			int num = Random.Range(9,Shuffled.Count);
			Shuffled.Insert(num,Cards[Cards.Count-1]);
		}

		//Begin creating deck string to send to second player
		for(int i = 0; i < Shuffled.Count; i++){
			DeckString += GetCardNumber(Shuffled[i]).ToString();
			if(i < Shuffled.Count-1)
				DeckString += ",";
		}
		Debug.Log(DeckString);
		//Send RPC of shuffled deck string to second player
		photonView.RPC("SendShuffledDeck",PhotonTargets.Others,DeckString);

		//Start Player 1's turn
		StartCoroutine(Manager.InitialDraw());
		//StartCoroutine(GetComponent<DebugManager>().InitialDraw());
	}

	//Replace a card back into the deck at a random spot
	public void ReplaceCard(Card card){
		Shuffled.Insert(Random.Range(0,Shuffled.Count),card);
	}

	//Selects a random card (keeps looking if a card is out)
	int GetRandomCard(){
		int num = Random.Range(0,Cards.Count-1);
		if(CardCounts[num] < 1){
			num = GetRandomCard();
		}
		return num;
	}

	//RPC function for recieving shuffled deck string parses and sets the deck for 2nd player
	[RPC]
	void SendShuffledDeck(string Deck, PhotonMessageInfo info){
		string[] CutDeck = Deck.Split(new char[] {','});
		Debug.Log(Deck);
		foreach(string card in CutDeck){
			//Debug.Log(card);
			if(card != "")
				Shuffled.Add(Cards[int.Parse(card)]);
		}
	}

	int GetCardNumber(Card card){
		for(int i = 0; i < Cards.Count; i++){
			if(card == Cards[i]){
				return i;
			}
		}
		Debug.Log("COULD NOT FIND CARD");
		return 0;
	}
}
