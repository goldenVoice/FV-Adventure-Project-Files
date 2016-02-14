﻿using UnityEngine;
using System.Collections;

public class LoadUserProgress : MonoBehaviour {

	LoadingScreen1 loadingScreen;

	// Use this for initialization
	void Start () {
//		PlayerPrefs.SetInt("sounds", 1);		// i put this here para at the start of the game sounds is on
//		PlayerPrefs.SetInt("vibr", 1);			// i put this here para at the start of the game vibration is on
		loadingScreen = (LoadingScreen1) GameObject.FindObjectOfType(typeof(LoadingScreen1));	
		PlayerPrefs.GetString ("");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void checkProgress(){
		PlayerPrefs.SetString ("currentProfile", gameObject.name);	// currentProfile = Profile 1 example
		string currentProfile = gameObject.name;

		Debug.Log(PlayerPrefs.HasKey(currentProfile + "Tutorial"));
		if( !(PlayerPrefs.HasKey(currentProfile + "Tutorial")) ){	// true if di pa tapos yung storyline, bigla nyang ni exit yung game
			loadingScreen.LoadScene("start_storyline");
//			Application.LoadLevel("start_storyline");
		}
		else if( PlayerPrefs.GetInt(currentProfile + "Tutorial") == 1){		// nakarating yung user sa tutorial of level 1, pero di nya tinapos, so rekta storyline ulet sya :P 
			loadingScreen.LoadScene("start_storyline");
//			Application.LoadLevel("start_storyline");
		}
		else{
			loadingScreen.LoadScene("map");
			//Application.LoadLevel("map");
			
		}
	}
}
