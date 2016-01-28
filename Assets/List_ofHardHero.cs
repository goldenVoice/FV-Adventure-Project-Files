using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class List_ofHardHero : MonoBehaviour {

	public List<GameObject> heroesToUnlock_SE;		// heroes to unlock using special equipments

	// Use this for initialization
	void Start () {

		foreach (GameObject circleHero in heroesToUnlock_SE) {
			if(PlayerPrefs.GetInt(circleHero.name) == 1 ){
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
