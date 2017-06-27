using UnityEngine;
using UnityEditor;
using System.Collections;

public class MyWindow : EditorWindow {
	string myString = "Hello World";
	bool groupEnabled;
	bool myBool = true;
	float myFloat = 1.23f;
	Color myColor = Color.white;
	GameObject myGameObject = null;
	[MenuItem ("My Window/Hello World")]
	static void Init () {
		MyWindow window = (MyWindow)EditorWindow.GetWindow (typeof (MyWindow));
	}
	
	void OnGUI () {
		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField ("Text Field", myString);

		myBool = EditorGUILayout.Foldout(myBool,"Fold Out");
		if(myBool)
		{
			myFloat = EditorGUILayout.Slider(myFloat,0.0f,10.0f);
			myColor = EditorGUILayout.ColorField(myColor);
			myGameObject = EditorGUILayout.ObjectField(myGameObject,typeof(GameObject)) as GameObject;
		}

	}
}
