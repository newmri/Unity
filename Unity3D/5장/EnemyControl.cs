using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {
	
	public float Speed = 8.0f;
	public GameObject explosion;
	
	
	// Update is called once per frame
	void Update () {
		float moveAmt = Speed * Time.deltaTime;
		
		transform.Translate(Vector3.back * moveAmt);
		
		if(transform.position.z < -7.0f)
		{
			InitPosition();
		}
	}
	
	void InitPosition()
	{
		transform.position = new Vector3(Random.Range(-10.0f,10.0f),
			Random.Range(-10.0f,10.0f),35.0f);
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "bullet")
		{
			Main.Score += 100;
			audio.Play();
			Instantiate(explosion,transform.position,transform.rotation);
			InitPosition();
			
		}
	}
}
