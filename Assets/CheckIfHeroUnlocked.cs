using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckIfHeroUnlocked : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt (gameObject.name) == 1) {


		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
