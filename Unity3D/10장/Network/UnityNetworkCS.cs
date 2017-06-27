using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class UnityNetworkCS : MonoBehaviour {
	
	public IPAddress ipaddress;
	public string connectToIP = "127.0.0.1";
	public int connectPort = 12345;
	public Transform PlayerPrefab;
	public ArrayList playerScripts = new ArrayList();
	public Vector3 spawnPosition;
	
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		Application.runInBackground = true;
	}
	
	// Use this for initialization
	void Start () {
		string name =  Dns.GetHostName();
		IPAddress[] ips = Dns.GetHostAddresses(name);
		foreach(IPAddress ip in ips)
		{
			Debug.Log(string.Format("{0}={1}({2})",name,ip,ip.AddressFamily));
			ipaddress = ip;
		}
	}
	
	void OnGUI(){
		if(Network.peerType == NetworkPeerType.Disconnected)
		{
			GUILayout.BeginArea( new Rect(Screen.width/2 - 100,
				Screen.height/2 - 128,200,256));
			
			GUILayout.Label("Connection Status:Disconnected");
			
			connectToIP = GUILayout.TextField(connectToIP,GUILayout.MinWidth(100));
			connectPort = int.Parse(GUILayout.TextField(connectPort.ToString()));
			
			if(GUILayout.Button("Connect as Client",GUILayout.MinHeight(40)))
			{
				Debug.Log("Click connect as client");
				Network.Connect(connectToIP,connectPort);
			}
			if(GUILayout.Button("Start Server",GUILayout.MinHeight(40)))
			{
				Debug.Log("Click Start Server");
				Network.InitializeServer(32,connectPort,false);
			}
			
			GUILayout.EndArea();
		}else{
			GUILayout.BeginArea( new Rect(Screen.width - 150, 20.0f,150.0f,200));
			GUILayout.Label("IP: "+this.ipaddress.ToString());
			if(Network.peerType == NetworkPeerType.Connecting){
				GUILayout.Label("Connection Status : Connecting");
			}else if(Network.peerType == NetworkPeerType.Client){
				GUILayout.Label("Connection Status : Client~!");
				GUILayout.Label("Ping To server : "+
					Network.GetAveragePing(Network.connections[0]));
			}else if(Network.peerType == NetworkPeerType.Server){
				GUILayout.Label("Connection status : Sevrer~!");
				GUILayout.Label("Connections : "+Network.connections.Length);
				if(Network.connections.Length >=1)
				{
					GUILayout.Label("Ping fp: "+
						Network.GetAveragePing(Network.connections[0]));
				}
			}
			if(GUILayout.Button("Disconnect"))
			{
				Debug.Log("Click Disconnect");
				Network.Disconnect(200);
			}
			GUILayout.EndArea();
		}
	}
	
	void SpawnPlayer( NetworkPlayer newPlayer)
	{
		int playerNumber = int.Parse(newPlayer.ToString());
		Transform myNewTrans = null;
		Debug.Log("SpawnPlayer Called");
		
		if(Network.player == newPlayer)
		{
			myNewTrans = Network.Instantiate(PlayerPrefab,
				spawnPosition,transform.rotation,playerNumber) as Transform;
		}
		if(myNewTrans != null)
		{
			NetworkView newObjectsNetworkView = myNewTrans.networkView;
			playerScripts.Add(myNewTrans.GetComponent<PlayerSprite>());
			Debug.Log("Player Spawn :" + playerNumber);
			newObjectsNetworkView.RPC("SetPlayer",RPCMode.AllBuffered,newPlayer);
		}else
		{
			Debug.LogError("Cant Instantiated the Player Prefab");
		}
	}
	
	void OnServerInitialized(){
		Debug.Log("Server Initialze finish");
		SpawnPlayer(Network.player);
	}
	void OnConnectedToServer(){
		Debug.Log("Connected to Server");
		SpawnPlayer(Network.player);
	}
	void OnPlayerConnected(NetworkPlayer newPlayer){
		Debug.Log("A player connected to me(the server)");
	}
	void OnPlayerDisconnected( NetworkPlayer player)
	{
		
		int playerNumber = int.Parse(player.ToString());
		Network.RemoveRPCs(Network.player,playerNumber);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
		
	}
	void OnDisconnectedFromServer( NetworkDisconnection infor)
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}
