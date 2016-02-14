using UnityEngine;
using System.Collections;

public class MediumHeroesChecker : MonoBehaviour {

	string currentProfile;
	void Awake(){
		
		currentProfile = PlayerPrefs.GetString ("currentProfile");
	}

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt (currentProfile + "Level 4-3") == 1) {		// tapos na last stage ng easy mode
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
