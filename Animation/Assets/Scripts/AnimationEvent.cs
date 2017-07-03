using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour {

	public void EventTest(int i)
    {
        Debug.Log("Animation Event Call" + i.ToString());
    }
}
