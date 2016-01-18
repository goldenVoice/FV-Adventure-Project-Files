using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		GetComponent<Text> ().text = Application.loadedLevelName;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
