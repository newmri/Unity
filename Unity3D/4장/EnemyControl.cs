using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {
	
	public float Speed = 8.0f;
	public GameObject explosion;
	
	
	// Update is called once per frame
	void Update () {
		float moveAmt = Speed * Time.deltaTime;
		
		transform.Translate(Vector3.down * moveAmt);
		
		if(transform.position.y < -7.0f)
		{
			InitPosition();
		}
	}
	
	void InitPosition()
	{
		transform.position = new Vector3(Random.Range(-5.0f,5.0f),
			7.0f,0.0f);
		
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
