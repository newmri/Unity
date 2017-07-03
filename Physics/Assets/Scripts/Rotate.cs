using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    public float Speed = 100.0f;

	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().AddTorque(Vector3.forward * Speed, ForceMode.Impulse);
        //GetComponent rigidbody.AddTorque(Vector3.forward * Speed, ForceMode.Impulse);
	}
}
