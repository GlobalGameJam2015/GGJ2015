using UnityEngine;
using System.Collections;

public class FindOpponent : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("FIND OPPONENT");
		if(PhotonNetwork.otherPlayers.Length > 0){
			PhotonNetwork.JoinOrCreateRoom(PhotonNetwork.playerName,new RoomOptions(){maxPlayers = 2},null);
			photonView.RPC("FoundOpponent",PhotonNetwork.otherPlayers[0]);
		}
		else{
			Debug.Log("No other players");
		}
	}

	public void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom: " + PhotonNetwork.player.isMasterClient);
	}

	public void OnCreatedRoom()
	{
		Debug.Log("OnCreatedRoom");
	}

	[RPC]
	public void FoundOpponent(PhotonMessageInfo info){
		PhotonNetwork.JoinOrCreateRoom(info.sender.name,new RoomOptions(){maxPlayers = 2},null);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
