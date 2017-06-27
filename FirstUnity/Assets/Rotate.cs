using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 5 * Time.deltaTime, 0);
        transform.Translate(0, 0, 2 * Time.deltaTime);
	}
}
