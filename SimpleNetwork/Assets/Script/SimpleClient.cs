using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
public class SimpleClient : MonoBehaviour {
    public string m_IPAdress = "127.0.0.1";
    public const int kPort = 10253;
    private static SimpleClient singleton;
    private Socket m_Socket;

    void Awake()
    {
        m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        System.Net.IPAddress remoteIPAddress = System.Net.IPAddress.Parse(m_IPAdress);
        System.Net.IPEndPoint remoteEndPoint = new System.Net.IPEndPoint(remoteIPAddress, kPort);
        singleton = this;
        m_Socket.Connect(remoteEndPoint);
        Debug.Log("Connecting");
    }
    void OnAplicationQuit()
    {
        m_Socket.Close();
        m_Socket = null;
    }
	
	// Update is called once per frame
	void Update () {
        // Send Hello string and mouse location when clicked left mouse
        if (Input.GetMouseButtonUp(0))
        {
            MessageData newmsg = new MessageData();
            newmsg.stringData = "hello";
            newmsg.mousex = Input.mousePosition.x;
            newmsg.mousey = Input.mousePosition.y;
            newmsg.type = 0;
            SimpleClient.Send(newmsg);
        }
	}


    static public void Send(MessageData msgData)
    {
        if (singleton.m_Socket == null) return;
        byte[] sendData = MessageData.ToByteArray(msgData);
        byte[] prefix = new byte[1];
        prefix[0] = (byte)sendData.Length;
        singleton.m_Socket.Send(prefix);
        singleton.m_Socket.Send(sendData);
        // Log
        Debug.Log(msgData.stringData + " " + msgData.mousex.ToString() + " " + msgData.mousey.ToString());
    }
}
