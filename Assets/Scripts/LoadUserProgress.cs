using UnityEngine;
using System.Collections;

public class LoadUserProgress : MonoBehaviour {

	
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("sounds", 1);		// i put this here para at the start of the game sounds is on
		PlayerPrefs.SetInt("vibr", 1);			// i put this here para at the start of the game vibration is on
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void checkProgress(){
		Debug.Log(PlayerPrefs.HasKey("Tutorial"));
		if( !(PlayerPrefs.HasKey("Tutorial")) ){	// true if di pa tapos yung storyline, bigla nyang ni exit yung game
			Application.LoadLevel("start_storyline");
		}
		else if( PlayerPrefs.GetInt("Tutorial") == 1){		// nakarating yung user sa tutorial of level 1, pero di nya tinapos, so rekta storyline ulet sya :P 
			Application.LoadLevel("start_storyline");
		}
		else{
			Application.LoadLevel("map");
			
		}
	}
}
