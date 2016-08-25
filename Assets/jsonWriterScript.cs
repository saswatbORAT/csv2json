using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEditor;
using System.IO;

public class jsonWriterScript : MonoBehaviour {

	public csvReaderScript csv;
	string jsonString;
	int i,j;
	void Start () {
		csv = GetComponent<csvReaderScript> ();
		StartCoroutine ("delay");
	}

	IEnumerator delay()	{
		yield return new WaitForSeconds (0.5f);
		csv.readCamperCSV ();
	//	csv.readCSV ();
	//	createJSON ();
	}

	void createJSON(){
		JSONNode mainNode = new JSONClass();
		JSONArray mainArray = new JSONArray ();
		JSONArray array = new JSONArray ();

		for (i = 0; i < csv.list.Length; i++) {
			for (j = 0; j < csv.list [i].words.Count; j++) {
				array.Add (csv.list [i].words [j]);
			}
			mainArray.Add (array);
			array = new JSONArray ();
		}
		mainNode.Add ("Root", mainArray);
		//print (mainNode.ToString ());
		writeJSON(mainNode.ToString());
	}

	void writeJSON(string data){
		string path = "Assets/BOLLYWOOD.json";
		using (FileStream fs = new FileStream (path, FileMode.Create)) {
			using (StreamWriter writer = new StreamWriter (fs)) {
				writer.Write (data);
			}
		}
		UnityEditor.AssetDatabase.Refresh ();
	}
}
