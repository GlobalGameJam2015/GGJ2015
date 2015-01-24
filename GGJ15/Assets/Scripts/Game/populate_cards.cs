using UnityEngine;
using System.Collections;

public class populate_cards : MonoBehaviour {
	public TextAsset theXml;
	// Use this for initialization
	void Start () {
		theXml = (TextAsset)Resources.Load ("cards");
		System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument ();
		xmlDoc.LoadXml (theXml.text);
		System.Xml.XmlNodeList cardList = xmlDoc.GetElementsByTagName("populate");
		Debug.Log (cardList.Count);
		foreach (System.Xml.XmlNode populate in cardList)
		{
			System.Xml.XmlNodeList poptrim = populate.ChildNodes;

			foreach (System.Xml.XmlNode cardTypes in poptrim) // levels itens nodes.
			{
				if(cardTypes.Name == "peopleCards")
				{
					foreach (System.Xml.XmlNode cardInfo in cardTypes)
					{
						if (cardInfo.Name == "card") {
							foreach (System.Xml.XmlNode cardTitle in cardInfo) {
								if (cardTitle.Name == "title")
									Debug.Log(cardTitle.InnerText); // put this in the dictionary.
								if (cardTitle.Name == "img")
									Debug.Log(cardTitle.InnerText);
								if (cardTitle.Name == "resource")
									Debug.Log(cardTitle.InnerText);
								if (cardTitle.Name == "value")
									Debug.Log(cardTitle.InnerText);
								if (cardTitle.Name == "entertainment")
									Debug.Log(cardTitle.InnerText);
								if (cardTitle.Name == "effect")
									Debug.Log(cardTitle.InnerText);
								if (cardTitle.Name == "number")
									Debug.Log(cardTitle.InnerText);
							}

						}
					
					}
				} else if(cardTypes.Name == "entertainmentCards")
				{
					foreach (System.Xml.XmlNode cardInfo in cardTypes)
					{
						if (cardInfo.Name == "card") {
							foreach (System.Xml.XmlNode cardTitle in cardInfo) {
								if (cardTitle.Name == "title")
									Debug.Log(cardTitle.InnerText); // put this in the dictionary.
								if (cardTitle.Name == "img")
									Debug.Log(cardTitle.InnerText);
								if (cardTitle.Name == "resource")
									Debug.Log(cardTitle.InnerText);
								if (cardTitle.Name == "value")
									Debug.Log(cardTitle.InnerText);
								if (cardTitle.Name == "entertainment")
									Debug.Log(cardTitle.InnerText);
								if (cardTitle.Name == "effect")
									Debug.Log(cardTitle.InnerText);
								if (cardTitle.Name == "number")
									Debug.Log(cardTitle.InnerText);
							}
							
						}
						
					}
				} else if(cardTypes.Name == "partyFouls")
				{
					foreach (System.Xml.XmlNode cardInfo in cardTypes)
					{
						if (cardInfo.Name == "card") {
							foreach (System.Xml.XmlNode cardTitle in cardInfo) {
								if (cardTitle.Name == "title")
									Debug.Log(cardTitle.InnerText); // put this in the dictionary.
								if (cardTitle.Name == "img")
									Debug.Log(cardTitle.InnerText);
								if (cardTitle.Name == "resource")
									Debug.Log(cardTitle.InnerText);
								if (cardTitle.Name == "value")
									Debug.Log(cardTitle.InnerText);
								if (cardTitle.Name == "entertainment")
									Debug.Log(cardTitle.InnerText);
								if (cardTitle.Name == "effect")
									Debug.Log(cardTitle.InnerText);
								if (cardTitle.Name == "number")
									Debug.Log(cardTitle.InnerText);
							}
							
						}
						
					}
				} else if(cardTypes.Name == "resourceCards")
				{
					foreach (System.Xml.XmlNode cardInfo in cardTypes)
					{
						if (cardInfo.Name == "card") {
							foreach (System.Xml.XmlNode cardTitle in cardInfo) {
								if (cardTitle.Name == "bonus")
									Debug.Log(cardTitle.InnerText); // put this in the dictionary.
								if (cardTitle.Name == "number")
									Debug.Log(cardTitle.InnerText);
							}
							
						}
						
					}
				}
			}
		}
				
	}
	
	// Update is called once per frame
	void Update () {
	}


}
