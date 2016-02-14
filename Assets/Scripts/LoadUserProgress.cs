using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadUserProgress : MonoBehaviour {

	LoadingScreen1 loadingScreen;
	
	// Use this for initialization
	void Start () {
		//		PlayerPrefs.SetInt("sounds", 1);		// i put this here para at the start of the game sounds is on
		//		PlayerPrefs.SetInt("vibr", 1);			// i put this here para at the start of the game vibration is on
		loadingScreen = (LoadingScreen1) GameObject.FindObjectOfType(typeof(LoadingScreen1));	
		
		// returns true if first time mag laro. or bakante yung space ng profile
		if( !(PlayerPrefs.HasKey(gameObject.name + "Tutorial")) ){	// true if di pa tapos yung storyline, or first time mag laro ng user, bigla nyang ni exit yung game
			// hide overwrite and erase button
			gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);	//erase button
			gameObject.transform.parent.GetChild(2).gameObject.SetActive(false);	//replace
			
			// the level text
			transform.GetChild(1).GetComponent<Text>().text = "New Profile";
		}
		
		// nasa tutorial na ang user kaso umalis sya sa game. so balik storyline ule.
		else if( PlayerPrefs.GetInt(gameObject.name + "Tutorial") == 1){		// nakarating yung user sa tutorial of level 1, pero di nya tinapos, so rekta storyline ulet sya :P 
			gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);	//erase button
			gameObject.transform.parent.GetChild(2).gameObject.SetActive(false);	//replace
			
			// the level text
			transform.GetChild(1).GetComponent<Text>().text = "New Profile";
			
		}
		
		// THIS PROFILE HAS A SAVED GAME/DATA IN IT.
		else{
			// if tutorial = 0 edi level 1-1 ang latest?
			if( PlayerPrefs.GetInt(gameObject.name + "Tutorial") == 0){
				transform.GetChild(1).GetComponent<Text>().text = "Level 1-1";
			}
			
			// PROCEED TO CHECK LATEST UNLOCK NA LEVEL
			// then output sa level text status
			
			// level 1 finished. level 2 na ang present level na lalaruiin nya
			if( PlayerPrefs.GetInt(gameObject.name + "Level 1-1") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 1-2";
				
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 1-2") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 1-3";
				
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 1-3") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 2-1";
				
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 2-1") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 2-2";
				
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 2-2") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 2-3";
				
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 2-3") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 3-1";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 3-1") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 3-2";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 3-2") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 3-3";
				
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 3-3") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 4-1";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 4-1") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 4-2";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 4-2") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 4-3";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 4-3") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 5-1";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 5-1") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 5-2";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 5-2") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 5-3";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 5-3") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 6-1";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 6-1") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 6-2";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 6-2") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 6-3";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 6-3") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 7-1";
			}
			
			if( PlayerPrefs.GetInt(gameObject.name + "Level 7-1") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 7-2";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 7-2") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 7-3";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 7-3") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 8-1";
			}
			
			if( PlayerPrefs.GetInt(gameObject.name + "Level 8-1") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 8-2";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 8-2") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 8-3";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 8-3") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 9-1";
			}
			
			if( PlayerPrefs.GetInt(gameObject.name + "Level 9-1") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 9-2";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 9-2") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 9-3";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 9-3") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 10-1";
			}
			
			if( PlayerPrefs.GetInt(gameObject.name + "Level 10-1") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 10-2";
			}
			if( PlayerPrefs.GetInt(gameObject.name + "Level 10-2") == 1){
				transform.GetChild(1).GetComponent<Text>().text = "Level 10-3";
			}
			
			
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void checkProgress(){
		//Debug.Log(PlayerPrefs.HasKey(gameObject.name + "Tutorial"));
		
		// set this profile to be used for all the player prefs
		PlayerPrefs.SetString ("currentProfile", gameObject.name);
		Debug.Log ("The current profile: " + gameObject.name);
		
		if( !(PlayerPrefs.HasKey(gameObject.name + "Tutorial")) ){	// true if di pa tapos yung storyline, bigla nyang ni exit yung game
			loadingScreen.LoadScene("start_storyline");
			//			Application.LoadLevel("start_storyline");
		}
		else if( PlayerPrefs.GetInt(gameObject.name + "Tutorial") == 1){		// nakarating yung user sa tutorial of level 1, pero di nya tinapos, so rekta storyline ulet sya :P 
			loadingScreen.LoadScene("start_storyline");
			//			Application.LoadLevel("start_storyline");
		}
		else{
			loadingScreen.LoadScene("map");
			//Application.LoadLevel("map");
			
		}
	}
}
