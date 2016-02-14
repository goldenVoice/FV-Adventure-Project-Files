using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class InsectInfo{
	public GameObject insectPrefab;
	public bool unlockInLibrary;
}

public class LibraryChecker : MonoBehaviour {

	public InsectInfo[] insects;
	string currentProfile;
	
	void Awake(){
		
		currentProfile = PlayerPrefs.GetString ("currentProfile");
	}
	// Use this for initialization
	void Start () {
		// loop through all the insects then check if its already seen in the library
		foreach (InsectInfo insect in insects) {							// playerprefs.getint(key, defaultvalue)  it goes to the default value if the key is non-existent
			if(PlayerPrefs.GetInt(currentProfile + insect.insectPrefab.name, 0) == 1){		// get the name of the insect, check usin playerprefs. if its 1 then means mkkta na tong insect na to sa library
				insect.unlockInLibrary = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void unlockInsect(string insectName){
		PlayerPrefs.SetInt (currentProfile + insectName, 1);
	}
}
