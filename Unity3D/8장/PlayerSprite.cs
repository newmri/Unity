using UnityEngine;
using System.Collections;

public class PlayerSprite : SpriteBase {
	
	public GameObject Bullet;
	public float Speed = 5.0f;
	
	float LastShootTime;
	public float ShootDelayTime = 0.2f;
	void Start () {
		LastShootTime = Time.time;
	}
	void Update () {
		float moveAmount = Input.GetAxis("Horizontal")*Speed*Time.deltaTime;
		transform.Translate(Vector3.right * moveAmount);
		if(Time.time > LastShootTime + ShootDelayTime)
		{
			LastShootTime = Time.time;
			Instantiate(Bullet,transform.position,transform.rotation);
		}
	}
}
