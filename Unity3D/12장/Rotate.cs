using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	public float Speed = 100.0f;
	
	// Update is called once per frame
	void Update () {
		
		rigidbody.AddTorque(Vector3.forward * Speed,ForceMode.Impulse);
		
	}
}
