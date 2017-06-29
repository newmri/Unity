using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHandler : MonoBehaviour {
    public int Lives = 3;
    public int Score = 0;
    private float MovingSpeed = 50.0f;

    public GameObject DistanceLabel;
    public GameObject LifeLabel;
    public GameObject ScoreLabel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        DistanceLabel.SendMessage("SetText", (Time.time * MovingSpeed).ToString("N1") + " m");
        LifeLabel.SendMessage("SetText", "X " + Lives.ToString());
        ScoreLabel.SendMessage("SetText", "Score: " + Score.ToString());
	}

    void Bomb()
    {
        Debug.Log("Bomb Button Click");
    }
}
