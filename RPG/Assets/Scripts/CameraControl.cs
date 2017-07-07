using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float distance = 10.0f;
    public float height = 5.0f;

    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    public GameObject target;

    void LateUpdate()
    {
        ThirdView();
    }

    void ThirdView()
    {
        if (target == null) target = GameObject.FindWithTag("Player");
        else
        {
            float wantedRotationAngle = target.transform.eulerAngles.y;
            float wantedHeight = target.transform.position.y + height;

            float currentRotationAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y;

            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            transform.position = target.transform.position; // player position
            transform.position -= currentRotation * Vector3.forward * distance; // move back

            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
            transform.LookAt(target.transform);
        }
    }

}
