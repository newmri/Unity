using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
    static public int Score = 0;
    static public int Lives = 3;

    public int FinishSocre = 1000;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Score >= FinishSocre){
            Score = 0;
            Application.LoadLevel("Win");
        }
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10.0f, 10.0f, 200.0f, 20.0f), Main.Score.ToString());
        GUI.Label(new Rect(10.0f, 30.0f, 200.0f, 20.0f), Main.Lives.ToString());
    }
}
