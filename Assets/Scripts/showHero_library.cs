using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class showHero_library: MonoBehaviour {

	void Awake(){
		string currentProfile;
		currentProfile = PlayerPrefs.GetString ("currentProfile");

		if(PlayerPrefs.GetInt(currentProfile + gameObject.name) == 1){
			Debug.Log("active watermelon");
			gameObject.GetComponent<Image>().enabled = true;
			gameObject.GetComponent<Button>().interactable = true;
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
		}
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
