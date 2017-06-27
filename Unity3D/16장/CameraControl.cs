using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	//third viwe point var
	public float distance = 10.0f;
	public float height = 5.0f;
	
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	
	public GameObject target; //player
	
	public enum CameraViewPoint {FIRST = 0,SECOND = 1,THIRD = 2,SIZE}
	public CameraViewPoint current = CameraViewPoint.THIRD;
	
	void LateUpdate()
	{
		switch(current)
		{
		case CameraViewPoint.THIRD:
			ThirdView();
			break;
		case CameraViewPoint.SECOND:
			break;
		case CameraViewPoint.FIRST:
			break;
		}
	}
	void ThirdView()
	{
		if(target == null)
		{
			target = GameObject.FindWithTag("Player");
		}else
		{
			float wantedRotationAngle = target.transform.eulerAngles.y;
			float wantedHeight = target.transform.position.y+height;
			
			float currentRotationAngle = transform.eulerAngles.y;
			float currentHeight = transform.position.y;
			
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle,
			                                       wantedRotationAngle,rotationDamping * Time.deltaTime);
			currentHeight = Mathf.Lerp(currentHeight,wantedHeight,
			                           heightDamping * Time.deltaTime);
			
			Quaternion currentRotation = Quaternion.Euler(0,
			                                              currentRotationAngle,0);
			
			//player position
			transform.position = target.transform.position; 
			// move back
			transform.position -= currentRotation * Vector3.forward * distance;
			
			transform.position = new Vector3(transform.position.x,
			                                 currentHeight,
			                                 transform.position.z);
			transform.LookAt(target.transform);
			
		}
	}
}
