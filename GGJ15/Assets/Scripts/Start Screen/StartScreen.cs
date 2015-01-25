using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class StartScreen : MonoBehaviour {

	public GUISkin Skin;
	public Vector2 WidthAndHeight = new Vector2(600,400);
	private string roomName = "FindOpponent";
	
	private bool connectFailed = false;
	
	public static readonly string SceneNameGame = "FindOpponent";

	public Texture2D BG;
	public GUIStyle FindOpponent;
	public GUIStyle Text;
	
	public void Awake()
	{
		// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
		PhotonNetwork.automaticallySyncScene = true;
		
		// the following line checks if this client was just created (and not yet online). if so, we connect
		if (PhotonNetwork.connectionStateDetailed == PeerState.PeerCreated)
		{
			// Connect to the photon master-server. We use the settings saved in PhotonServerSettings (a .asset file in this project)
			PhotonNetwork.ConnectUsingSettings("0.9");
		}
		
		// generate a name for this player, if none is assigned yet
		if (String.IsNullOrEmpty(PhotonNetwork.playerName))
		{
			PhotonNetwork.playerName = "Guest" + Random.Range(1, 9999);
		}
		
		// if you wanted more debug out, turn this on:
		// PhotonNetwork.logLevel = NetworkLogLevel.Full;
	}
	
	public void OnGUI()
	{
		if (this.Skin != null)
		{
			GUI.skin = this.Skin;
		}
		
		if (!PhotonNetwork.connected)
		{
			if (PhotonNetwork.connecting)
			{
				GUILayout.Label("Connecting to: " + PhotonNetwork.ServerAddress);
			}
			else
			{
				GUILayout.Label("Not connected. Check console output. Detailed connection state: " + PhotonNetwork.connectionStateDetailed + " Server: " + PhotonNetwork.ServerAddress);
			}
			
			if (this.connectFailed)
			{
				GUILayout.Label("Connection failed. Check setup and use Setup Wizard to fix configuration.");
				GUILayout.Label(String.Format("Server: {0}", new object[] {PhotonNetwork.ServerAddress}));
				GUILayout.Label("AppId: " + PhotonNetwork.PhotonServerSettings.AppID);
				
				if (GUILayout.Button("Try Again", GUILayout.Width(100)))
				{
					this.connectFailed = false;
					PhotonNetwork.ConnectUsingSettings("0.9");
				}
			}
			
			return;
		}

		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),BG);

		GUI.Label(new Rect((Screen.width-200)/2-140,(Screen.height-30)/2+90,100,30),"Player Name: ",Text); 
		PhotonNetwork.playerName = GUI.TextField(new Rect((Screen.width-200)/2-30,(Screen.height-30)/2+90,200,30),PhotonNetwork.playerName,Text);

		if(GUI.Button(new Rect((Screen.width-267)/2-30,(Screen.height-64)/2+155,267,64),"",FindOpponent)){
			PhotonNetwork.JoinLobby();
			GetComponent<FindOpponent>().enabled = true;
			enabled = false;
		}
		
		/*Rect content = new Rect((Screen.width - WidthAndHeight.x)/2, (Screen.height - WidthAndHeight.y)/2, WidthAndHeight.x, WidthAndHeight.y);
		GUI.Box(content,"Party Foul");
		GUILayout.BeginArea(content);
		
		GUILayout.Space(40);
		
		// Player name
		GUILayout.BeginHorizontal();
		GUILayout.Label("Player name:", GUILayout.Width(150));
		PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName);
		GUILayout.Space(158);
		if (GUI.changed)
		{
			// Save name
			PlayerPrefs.SetString("playerName", PhotonNetwork.playerName);
		}
		GUILayout.EndHorizontal();
		
		GUILayout.Space(15);
		
		// Join room by title
		GUILayout.BeginHorizontal();
		
		if (GUILayout.Button("Find Opponent", GUILayout.Width(150)))
		{
			PhotonNetwork.JoinLobby();
			GetComponent<FindOpponent>().enabled = true;
			enabled = false;
		}
		
		GUILayout.EndHorizontal();
		
		GUILayout.EndArea();*/
	}
	
	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("Disconnected from Photon.");
	}
	
	public void OnFailedToConnectToPhoton(object parameters)
	{
		this.connectFailed = true;
		Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + parameters + " ServerAddress: " + PhotonNetwork.networkingPeer.ServerAddress);
	}
}
