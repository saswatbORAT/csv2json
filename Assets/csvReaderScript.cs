using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class wordList{
	public List<string> words;
	wordList()	{
		words = new List<string> ();
	}
}

[System.Serializable]
public class optionsData{
	public string text;
	public int nextIndex;
	public float economyChange;
}

[System.Serializable]
public class stateData{
	public string text;
	public List<optionsData> options;
	public stateData(){
		options = new List<optionsData> ();
	}
}

public class csvReaderScript : MonoBehaviour {

	public TextAsset csvFile;
	string[,] grid;
	int i, j;
	[SerializeField] 
	public wordList[] list;
	public List<stateData> stateDataList;
	stateData currentData;

	void Start () {
		list = new wordList[9];
		stateDataList = new List<stateData> ();
		currentData = new stateData ();
	}

	public void readCSV(){
		grid = CSVReader.SplitCsvGrid(csvFile.text);
		int x = grid.GetUpperBound (0);
		int y = grid.GetUpperBound (1);

		for (i = 0; i < x; i++) {
			for (j = 0; j < y; j++) {
				if (grid [i, j] != null && grid [i, j].Length != 0) {
					grid [i, j] = grid [i, j].ToUpper();
					list [grid [i, j].Length - 1].words.Add (grid [i, j]);
				}
			}
		}

		for (i = 0; i < list.Length; i++) {
			list [i].words.Sort ();
		}
	}

	public void readCamperCSV(){
		grid = CSVReader.SplitCsvGrid(csvFile.text);
		int x = grid.GetUpperBound (0);
		int y = grid.GetUpperBound (1);
		int size = 0;
		optionsData od = new optionsData();
		for (i = 0; i < x; i++) {
			if (grid [i, 0] != null && grid [i, 0].Length != 0) {
				currentData.text = grid [i, 0];
				size =  Int32.Parse(grid[i,1]);
				for (j = 0; j < size; j++) {
					od.text = grid [i, j + 2];
					od.nextIndex = Int32.Parse(grid [i, j + 2 + size]);
					od.economyChange = Int32.Parse(grid [i, j + 2 + size * 2]);
					currentData.options.Add (od);
					od = new optionsData ();
				}
				stateDataList.Add (currentData);
				currentData = new stateData ();

				//currentData.text = grid [i, 0];
			}

		}
		
	}
}
