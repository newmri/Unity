using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody.AddForce(Vector3.up * 10.0f,ForceMode.Impulse);
	}

}
