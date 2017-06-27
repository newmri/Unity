using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CSVDemo : MonoBehaviour {

	public TextAsset csv; 
	
	void Start () {
		csv = (TextAsset)Resources.Load<TextAsset>("ExampleCSV");
		CSVReader.DebugOutputGrid( CSVReader.SplitCsvGrid(csv.text) ); 

		PlayerPrefs.SetString("Player Name", "Foobar");
	}

}
