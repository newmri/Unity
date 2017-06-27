using UnityEngine;
using System.Collections;

public class Usage : MonoBehaviour {

	private string text = "";
	public GUISkin skin;
	
	void OnGUI()
	{
		GUILayout.Label(text, skin.label);
	}

	void Start()
	{
		string xmlText = System.IO.File.ReadAllText(Application.dataPath + "/Rifleman.xml");
		TinyXmlReader reader = new TinyXmlReader(xmlText);
		while (reader.Read())
		{
			if (reader.isOpeningTag)
			{
				text += (reader.tagName + " \"" + reader.content + "\"\n");
			}
			if (reader.tagName == "Skills" && reader.isOpeningTag)
			{
				while(reader.Read("Skills")) // read as long as not encountering the closing tag for Skills
				{
					
					if (reader.isOpeningTag)
					{
						text += ("Skill: " + reader.tagName + " \"" + reader.content + "\"\n");
					}
				}
			}
		}
	}
}
