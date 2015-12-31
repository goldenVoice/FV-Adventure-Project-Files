using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProfileData : MonoBehaviour {

	public string ProfileName;
	public string LevelProgressText;
	
	// Use this for initialization
	void Start () {
		Debug.Log(gameObject.name + " " + PlayerPrefs.HasKey(gameObject.name));
		if (PlayerPrefs.HasKey(gameObject.name)){			// gameobject.name = 'profile 1'
			Text nameText = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
			Text progressText = gameObject.transform.GetChild(0).GetChild(1).GetComponent<Text>();

			nameText.text = PlayerPrefs.GetString(gameObject.name + " name");
//			progressText.text = PlayerPrefs.GetString(gameObject.name + " name");


		}
		else{			// wala pang data sa profile na ito
			gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "New ";		// name text
			gameObject.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Profile";		// level progress text

			gameObject.transform.GetChild(1).gameObject.SetActive(false);		//delete button hide
			gameObject.transform.GetChild(2).gameObject.SetActive(false);		// overwrite button hide
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
