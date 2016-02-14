using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class List_ofHardHero : MonoBehaviour {

	public List<GameObject> heroesToUnlock_SE;		// heroes to unlock using special equipments

	string currentProfile;
	
	void Awake(){
		
		currentProfile = PlayerPrefs.GetString ("currentProfile");

		foreach (GameObject circleHero in heroesToUnlock_SE) {
			if(PlayerPrefs.GetInt(currentProfile + circleHero.name) == 1 ){
				circleHero.gameObject.SetActive(true);
			}
			else{
				circleHero.gameObject.SetActive(false);
				
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
