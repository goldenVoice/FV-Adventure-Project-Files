using UnityEngine;
using System.Collections;

public class SEquipChecker : MonoBehaviour {

	// this script checks if the user gets to medium mode, if so, unlocks the SpecialEquipment button

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("Level 4-3") == 1) {		// tapos na last stage ng easy mode
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
