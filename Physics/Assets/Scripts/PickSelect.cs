using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSelect : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            hit.collider.gameObject.GetComponent<HingeJoint>().useMotor = true;
            hit.collider.gameObject.GetComponent<HingeJoint>().anchor = new Vector3(-0.5f, 0.0f, 0.0f);
            hit.collider.gameObject.GetComponent<HingeJoint>().axis = new Vector3(0.0f, 0.0f, 1.0f);
            hit.collider.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
	}
}
