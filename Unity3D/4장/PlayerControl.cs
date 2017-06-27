using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	public float Speed = 1.0f;
	public GameObject bullet;
	
	// Update is called once per frame
	void Update () {
		
		float moveAmt = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
		
		transform.Translate(Vector3.right * moveAmt);
		if(Input.GetKeyDown(KeyCode.Space))
		{
			audio.Play();
			Instantiate(bullet,transform.position,transform.rotation);
		}
	}
}
