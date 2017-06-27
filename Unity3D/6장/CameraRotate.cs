using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {
	
	public float Speed = 0.5f;
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(Vector3.up,Time.deltaTime * Speed);
	}
}
