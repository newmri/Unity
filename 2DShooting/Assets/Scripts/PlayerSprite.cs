using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : SpriteBase {
    public GameObject Bullet;
    public float Speed = 5.0f;

    float LastShootTime;
    public float ShootDelayTime = 0.2f;
	// Use this for initialization
	void Start () {
        LastShootTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        float moveAmount = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        transform.Translate(Vector3.right * moveAmount);
        if(Time.time > LastShootTime + ShootDelayTime)
        {
            LastShootTime = Time.time;
            Instantiate(Bullet, transform.position, transform.rotation);
        }
	}
}
