using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour {
    public GUISkin skin;
    public int ScrollSize;

    private int index = 0;
    private Vector2 scrollPosition = Vector2.zero;
    private string[] logtext;

    public static Log logInstance;
    public bool ison = false;
    public Texture textrue;

    void Awake()
    {
        logInstance = this;
        DontDestroyOnLoad(transform.gameObject);
    }
	// Use this for initialization
	void Start () {
        if (ScrollSize == 0) ScrollSize = 1000;
        logtext = new string[ScrollSize];
        for (int i = 0; i < ScrollSize; ++i) logtext[i] = " ";
	}
	
    void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 48, 16), "X"))
        {
            index = 0;
            for (int i = 0; i < ScrollSize; ++i) logtext[i] = " ";
            scrollPosition = Vector2.zero;
        }
        ison = GUI.Toggle(new Rect(100, 0, 100, 24), ison, "On/Off");
        if(ison.Equals(true))
        {
            GUI.skin = skin;
            scrollPosition = GUI.BeginScrollView(new Rect(10, 50, Screen.width - 10, Screen.height - 100), 
                scrollPosition, new Rect(0, 0, 1000, ScrollSize * 24));
            if(index > 15)
            {
                Vector2 sPos = scrollPosition;
                scrollPosition = new Vector2(sPos.x, (index - 15) * 30.0f);
            }
            GUI.color = Color.white;
            for(int i=0; i< ScrollSize; ++i)
            {
                if (logtext[i].ToString().Contains("picture")) GUI.Label(new Rect(0, i * 28, 700, 28), this.textrue);
                else GUI.Label(new Rect(0, i * 28, 700, 28), logtext[i]);
  
            }
            GUI.EndScrollView();
        }
    }

    [RPC]
    public void ScreenLog(string text)
    {
        if (logtext == null) return;
        logtext[index] = text;
        index++;
        if (index > ScrollSize - 1)
        {
            for (int i = 0; i < ScrollSize - 1; ++i) logtext[i] = logtext[i + 1];
            index = ScrollSize - 1;
        }
    }
	
    public void SetOnOff(bool onoff)
    {
        this.ison = onoff;
    }
}
