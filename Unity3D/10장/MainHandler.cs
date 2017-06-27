using UnityEngine;
using System.Collections;

public class MainHandler : MonoBehaviour {
	
	public int Lives = 3;
	public int Score = 0;
	private float MovingSpeed = 50.0f;
	
	public GameObject DistanceLabel;
	public GameObject LifeLabel;
	public GameObject ScoreLabel;

	// Update is called once per frame
	void Update () {
		//add new code
		if(GameObject.FindWithTag("Player") == null)
			return;
		
		DistanceLabel.SendMessage("SetText",
			(Time.time*MovingSpeed).ToString("N1")+" m");
		LifeLabel.SendMessage("SetText","X "+Lives.ToString());
		ScoreLabel.SendMessage("SetText","Score:"+Score.ToString());
	}
	void Bomb(){
		Debug.Log("Bomb Button Click");
	}
}
