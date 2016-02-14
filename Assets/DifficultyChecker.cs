using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DifficultyChecker : MonoBehaviour {

	// Use this for initialization
	string currentProfile;
	void Awake(){
		
		currentProfile = PlayerPrefs.GetString ("currentProfile");
	}

	void Start () {
		if (PlayerPrefs.GetInt (currentProfile + "Level 4-3") == 1) {		// tapos na last stage ng med mode
			gameObject.GetComponent<Text>().text = "MEDIUM";	
			gameObject.GetComponent<Text>().color = Color.yellow;
		}
		if (PlayerPrefs.GetInt (currentProfile + "Level 7-3") == 1) {		// tapos na last stage ng med mode
			gameObject.GetComponent<Text>().text = "HARD";	
			gameObject.GetComponent<Text>().color = Color.red;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
