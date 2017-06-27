using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {

	
	
	void OnGUI(){
		if(GUI.Button(new Rect(Screen.width/2-100.0f,Screen.height/2-100.0f,200.0f,200.0f),"You Win~!"))
		{
			
			Application.LoadLevel("game");
		}
	}
}
