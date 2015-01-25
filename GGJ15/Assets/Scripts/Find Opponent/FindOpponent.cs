using UnityEngine;
using System.Collections;

public class FindOpponent : Photon.MonoBehaviour {

	public Texture2D BG;

	// Use this for initialization
	void Start () {
		Debug.Log("FIND OPPONENT");
		Debug.Log(PhotonNetwork.connected);
		Debug.Log(PhotonNetwork.countOfPlayers);
		if(PhotonNetwork.countOfPlayers > 1){
			PhotonNetwork.JoinRandomRoom();
			photonView.RPC("FoundOpponent",PhotonNetwork.otherPlayers[0]);
		}
		else{
			PhotonNetwork.JoinOrCreateRoom(PhotonNetwork.playerName,new RoomOptions(){maxPlayers = 2},null);
			Debug.Log("No other players");
		}
	}

	void OnGUI(){
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),BG);
	}

	// We have two options here: we either joined(by title, list or random) or created a room.
	public void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom: " + PhotonNetwork.player.isMasterClient);
		if(!PhotonNetwork.player.isMasterClient){
			photonView.RPC("EnterGameScene",PhotonTargets.MasterClient);
		}
	}
	
	
	public void OnPhotonCreateRoomFailed()
	{
		Debug.Log("OnPhotonCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
	}
	
	public void OnPhotonJoinRoomFailed()
	{
		Debug.Log("OnPhotonJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
	}
	public void OnPhotonRandomJoinFailed()
	{
		Debug.Log("OnPhotonRandomJoinFailed got called. Happens if no room is available (or all full or invisible or closed). JoinrRandom filter-options can limit available rooms.");
	}
	
	public void OnCreatedRoom()
	{
		Debug.Log("OnCreatedRoom");
	}
	
	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("Disconnected from Photon.");
	}
	
	public void OnFailedToConnectToPhoton(object parameters)
	{
		Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + parameters + " ServerAddress: " + PhotonNetwork.networkingPeer.ServerAddress);
	}

	[RPC]
	public void EnterGameScene(PhotonMessageInfo info){
		PhotonNetwork.LoadLevel("Game");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
