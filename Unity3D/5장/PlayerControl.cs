using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	public float Speed = 1.0f;
	public GameObject bullet;
	
	// Update is called once per frame
	void Update () {
		
		float moveAmt = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
		float moveAmt2 = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
		Vector3 moveVector = Vector3.right * moveAmt + Vector3.up * moveAmt2;
		transform.Translate(moveVector);
		if(Input.GetKeyDown(KeyCode.Space))
		{
			audio.Play();
			Instantiate(bullet,transform.position,transform.rotation);
		}
	}
}
