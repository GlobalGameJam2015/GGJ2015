using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugManager : MonoBehaviour {

	//Reference to the deck builder
	public DeckBuilder Deck;
	
	//List of cards in hand
	public List<Card> Hand = new List<Card>();
	public List<Card> OtherHand = new List<Card>();

	//Card Prefab
	public GameObject CardPrefab;

	void Start () {
		Debug.Log("YOU ARE NOW IN THE GAME SCENE");
		Deck = GetComponent<DeckBuilder>();
	}

	public IEnumerator InitialDraw(){
		for(int i = 0; i < 1; i++){
			DrawCard(0);
			yield return new WaitForSeconds(3);
		}
	}

	//Draw a card
	public void DrawCard(int num){
		if(num == 0){
			Hand.Add(Deck.Shuffled[0]);
		}
		else{
			OtherHand.Add(Deck.Shuffled[0]);
		}
		Deck.Shuffled.RemoveAt(0);

		DefineCard(Hand[Hand.Count-1]);
		//TODO: Animation and visual of card being drawn into hand
	}

	//Define the card variables
	public void DefineCard(Card card){
		GameObject go = Instantiate(CardPrefab,Vector3.zero,Quaternion.identity) as GameObject;
		CardReference reference = go.GetComponent<CardReference>();
		go.transform.parent = transform.GetChild(0);
		go.transform.localEulerAngles = new Vector3(0,0,0);
		go.transform.localPosition = Vector3.zero;
		reference.Type = card.Type;
		reference.Name = card.Title;
		reference.Description = card.Effect;
		reference.Gender = card.Gender;
		reference.Description = card.Effect;
		reference.Image = card.Image;
		reference.Resource = card.Resource;
		reference.People = card.Value;
		reference.Entertainment = card.Entertainment;
	}
}
