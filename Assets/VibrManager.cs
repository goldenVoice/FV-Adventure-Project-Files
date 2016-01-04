using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VibrManager : MonoBehaviour {

	public Text text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void enableDisable(){
		if(PlayerPrefs.GetInt("vibr") == 0){		// vibr: off
			text.text = "Vibration: ON";
			PlayerPrefs.SetInt("vibr", 1); 	// meaning on

		}
		else{
			text.text = "Vibration: OFF";
			PlayerPrefs.SetInt("vibr", 0); 	// meaning off
		}

	}

}
