using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DifficultyChecker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("Level 4-3") == 1) {		// tapos na last stage ng med mode
			gameObject.GetComponent<Text>().text = "MEDIUM";	
			gameObject.GetComponent<Text>().color = Color.yellow;
		}
		if (PlayerPrefs.GetInt ("Level 7-3") == 1) {		// tapos na last stage ng med mode
			gameObject.GetComponent<Text>().text = "HARD";	
			gameObject.GetComponent<Text>().color = Color.red;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
