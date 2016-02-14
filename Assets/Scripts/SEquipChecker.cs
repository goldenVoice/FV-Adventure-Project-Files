using UnityEngine;
using System.Collections;

public class SEquipChecker : MonoBehaviour {

	// this script checks if the user gets to medium mode, if so, unlocks the SpecialEquipment button
	string currentProfile;
	void Awake(){
		
		currentProfile = PlayerPrefs.GetString ("currentProfile");
	}
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt (currentProfile + "Level 7-3") == 1) {		// tapos na last stage ng medium mode
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
