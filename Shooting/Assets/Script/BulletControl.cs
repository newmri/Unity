using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
    public float Speed = 20.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float moveAmt = Time.deltaTime * Speed;
        transform.Translate(Vector3.forward * moveAmt);

        if (transform.position.z > 35.0f) Destroy(gameObject);
	}
}
