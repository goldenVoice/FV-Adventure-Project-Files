using UnityEngine;
using System.Collections;

public class HeroPotionChecker : MonoBehaviour {
	// taga check kung nasa hard na. 
	// Use this for initialization
	string currentProfile;
	void Awake(){
		
		currentProfile = PlayerPrefs.GetString ("currentProfile");
	}

	void Start () {

		if (PlayerPrefs.GetInt (currentProfile + "Level 7-3") == 1) {		// tapos na last stage ng med mode
			gameObject.SetActive(true);						// show the special equipment button
		}
		else {
			gameObject.SetActive(false);					// hide the special equipment button
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
