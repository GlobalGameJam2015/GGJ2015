using UnityEngine;
using System.Collections;

public class BadEvent : MonoBehaviour {

	public int Num;

	void Played () {
		GameManager Manager = GameObject.Find("GameManager").GetComponent<GameManager>();

		switch(Num){
		case 1:
			//Remove from party
			break;
		case 2:
			int count = 0;
			foreach(Card card in Manager.Field.Opponets){
				if(card.Title == "Jock"){
					Manager.Field.Opponets.Remove(card);
					count++;
				}
			}
			count *= 2;
			Manager.photonView.RPC("PassEffect",PhotonTargets.Others,-1,"Jock",0,count);
			break;
		case 3:
			Manager.photonView.RPC("PassEffect",PhotonTargets.Others,-1,"",-4,0);
			break;
		case 4:
			
			break;
		case 5:
			
			break;
		case 6:
			Manager.photonView.RPC("PassEffect",PhotonTargets.Others,-1,"",0,-3);
			break;
		}
	}
}
