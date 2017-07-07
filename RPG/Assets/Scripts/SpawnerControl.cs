using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControl : MonoBehaviour {
    public float SpawnTime = 1.0f;
    float LastSpawnTime;
    public GameObject monster;
	// Use this for initialization
	void Start () {
        LastSpawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > LastSpawnTime + SpawnTime)
        {
            LastSpawnTime = Time.time;
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-5.0f, 5.0f), transform.position.y, transform.position.z + Random.Range(-5.0f, 5.0f));

            Instantiate(monster, pos, transform.rotation);
        }
	}
}
