﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSprite : SpriteBase {
    public float Speed = 10.0f;
	// Use this for initialization
	void Start () {
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.AddComponent<BoxCollider>();
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }
	
	// Update is called once per frame
	void Update () {
        float moveAmount = Speed * Time.deltaTime;
        transform.Translate(Vector3.up * moveAmount);
        if (transform.position.y > 10.0f) Destroy(gameObject);
	}
}
