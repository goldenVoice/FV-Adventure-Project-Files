using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckIfHeroUnlocked : MonoBehaviour {

	string currentProfile;
	void Awake(){
		
		currentProfile = PlayerPrefs.GetString ("currentProfile");
	}
	// Use this for initialization
	void Start () {

		if (PlayerPrefs.GetInt (currentProfile + gameObject.name) == 1) {


		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
