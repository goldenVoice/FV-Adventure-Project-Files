using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundButtonManager : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Debug.Log(PlayerPrefs.GetInt("sounds"));
		if(PlayerPrefs.GetInt("sounds") == 1){
			gameObject.transform.GetChild(0).GetComponent<Text>().text = "Sounds: ON";
		}
		else{
			gameObject.transform.GetChild(0).GetComponent<Text>().text = "Sounds: OFF";
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
