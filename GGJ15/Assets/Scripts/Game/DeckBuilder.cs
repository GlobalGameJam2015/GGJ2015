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
	PartyFoul = 2,
	Resource = 3,
	EventLevel = 4,
}

//Class for holding card data
[System.Serializable]
public class Card{
	public CardTypes Type;
	public string Title;
	public Texture2D Image;
	public int Resource;
	public int Value;
	public int Entertainment;
	public string Effect;
	public int Count;
}

public class DeckBuilder : Photon.MonoBehaviour {

	//Reference to game manager
	private GameManager Manager;

	//Variables for all cards, deck, and shuffled deck
	private List<Card> Cards = new List<Card>();
	public List<Card> Shuffled = new List<Card>();
	private List<int> CardCounts = new List<int>();
	private string DeckString = "";
	private int TotalCards = 145;


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
						if (cardTitle.Name == "title")
							Cards[Cards.Count-1].Title = cardTitle.InnerText;
						//if (cardTitle.Name == "img")
						//	Cards[Cards.Count-1].Image = cardTitle.InnerText;
						if (cardTitle.Name == "resource")
							Cards[Cards.Count-1].Resource = int.Parse(cardTitle.InnerText);
						if (cardTitle.Name == "value")
							Cards[Cards.Count-1].Value = int.Parse(cardTitle.InnerText);
						if (cardTitle.Name == "entertainment")
							Cards[Cards.Count-1].Entertainment = int.Parse(cardTitle.InnerText);
						if (cardTitle.Name == "effect")
							Cards[Cards.Count-1].Effect = cardTitle.InnerText;
						if (cardTitle.Name == "number")
							Cards[Cards.Count-1].Count = int.Parse(cardTitle.InnerText);
					}
					//Add to list the number of each card
					CardCounts.Add(Cards[Cards.Count-1].Count);
				}
			}
		}

		//Shuffle deck if you are the host
		//if(PhotonNetwork.isMasterClient)
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
		
			//Begin creating deck string to send to second player
			DeckString += RandomCard.ToString();
			if(i < TotalCards-1)
				DeckString += ",";
		}

		//Send RPC of shuffled deck string to second player
		//photonView.RPC("SendShuffledDeck",PhotonTargets.Others,DeckString);

		//Start Player 1's turn
		StartCoroutine(Manager.DrawForMulligan(5,true));
	}

	//Replace a card back into the deck at a random spot
	public void ReplaceCard(Card card){
		Shuffled.Insert(Random.Range(0,Shuffled.Count),card);
	}

	//Selects a random card (keeps looking if a card is out)
	int GetRandomCard(){
		int num = Random.Range(0,Cards.Count);
		if(CardCounts[num] < 1){
			num = GetRandomCard();
		}
		return num;
	}

	//RPC function for recieving shuffled deck string parses and sets the deck for 2nd player
	[RPC]
	void SendShuffledDeck(PhotonMessageInfo info, string Deck){
		string[] CutDeck = Deck.Split(new char[] {','});
		foreach(string card in CutDeck){
			Shuffled.Add(Cards[int.Parse(card)]);
		}
	}
}
