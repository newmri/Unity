using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetChat : MonoBehaviour {
    public string connectToIp = "127.0.0.1";
    public int connectPort = 12345;
    public ArrayList clientsScripts = new ArrayList();
    NetworkView networkViews;
    // chat var
    private string chatmessage = "";

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Application.runInBackground = true;
    }

    void Start()
    {
        networkViews = new NetworkView();
    }

    void OnServerInitialized()
    {
        Debug.Log("Server Started~!");
    }

    void OnPlayerConnected(NetworkPlayer newPlayer)
    {
        Debug.Log("A Clients connected to me(the server)!");
    }
    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Clean up after player " + player);
    }
    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        Debug.Log("Resetting the scene the easy way.");
    }

    void OnGUI()
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 128, 200, 256));
            // We are currently disconnected: Not a client or host
            GUILayout.Label("Connection status: Disconnected");
            connectToIp = GUILayout.TextField(connectToIp, GUILayout.MinWidth(100));
            connectPort = int.Parse(GUILayout.TextField(connectPort.ToString()));
            if (GUILayout.Button("Connect as client", GUILayout.MinHeight(40)))
            {
                // Connect to the "connectToIP" and "connectPort"
                // as entered via the GUI
                // Ignore the NAT for now
                Debug.Log("Click Connect as client");
                Network.Connect(connectToIp, connectPort);
            }

            if (GUILayout.Button("Start Server", GUILayout.MinHeight(40)))
            {
                // Start a server for 32 clients using the "connectPort"
                // given via the GUI
                // Ignore the nat for now
                Debug.Log("Click Start Server");
                Network.InitializeServer(32, connectPort, false);
                GUILayout.EndArea();
            }
        }
        else
        {
            // We've got a connections
            GUILayout.BeginArea(new Rect(Screen.width - 150, 20, 100, 200));
            if (Network.peerType == NetworkPeerType.Connecting) GUILayout.Label("Connection status: Connecting");
            else if (Network.peerType == NetworkPeerType.Client)
            {
                GUILayout.Label("Connection status: Client!");
                GUILayout.Label("Ping to server: " + Network.GetAveragePing(Network.connections[0]));
            }

            else if (Network.peerType == NetworkPeerType.Server)
            {
                GUILayout.Label("Connection status: Server!");
                GUILayout.Label("Connections: " + Network.connections.Length);

                if (Network.connections.Length >= 1) GUILayout.Label("Ping fp: " + Network.GetAveragePing(Network.connections[0]));
            }


            if (GUILayout.Button("Disconnect"))
            {
                Debug.Log("click DisConnect");
                Network.Disconnect(200);
            }

            GUILayout.EndArea();

            Event e = Event.current;

            chatmessage = GUI.TextField(new Rect(Screen.width / 2 - 200, Screen.height - 30, 400, 30), chatmessage);

            if (GUI.Button(new Rect(Screen.width / 2 + 220, Screen.height - 30, 100, 30), "Send") || e.keyCode == KeyCode.Return)
            {
                if (chatmessage != " ")
                {
                    networkViews.RPC("SendMsg", RPCMode.Server, Network.player.ipAddress + ":" + chatmessage);
                    chatmessage = " ";
                }
            }
        }
    }
	[RPC]
    public void SendMsg(string msg)
    {
        Debug.Log("Called SendMsg");
        networkViews.RPC("ScreenLog", RPCMode.All, msg);
    }
}
