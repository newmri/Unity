using UnityEngine;
using System.Collections;

public class PlayerSprite : SpriteBase {
	
	public GameObject Bullet;
	public float Speed = 5.0f;
	
	float LastShootTime;
	public float ShootDelayTime = 0.2f;
	
	//new Code
	public NetworkPlayer Owner;
	public TextMesh NameMesh;
	[RPC]
	void SetPlayer(NetworkPlayer player){
		this.Owner = player;
		gameObject.AddComponent("PlayerRemote");
		string[] ip = this.Owner.ipAddress.Split('.');
		NameMesh.text = ip[ip.Length-1];
		NameMesh.renderer.material.color = Color.red;
		
	}
	void Start () {
		LastShootTime = Time.time;
	}
	void Update () {
		//new code
		if(Network.peerType != NetworkPeerType.Disconnected &&
			Network.player == Owner)
		{
			float moveAmount = Input.GetAxis("Horizontal")*Speed*Time.deltaTime;
			transform.Translate(Vector3.right * moveAmount);
			if(Time.time > LastShootTime + ShootDelayTime)
			{
				LastShootTime = Time.time;
				Network.Instantiate(Bullet,transform.position,transform.rotation,0);
			}
		}
	}
}
