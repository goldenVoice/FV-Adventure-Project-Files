using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class showHero : MonoBehaviour {

	void Awake(){
		if(PlayerPrefs.GetInt(gameObject.name) == 1){
			Debug.Log("active watermelon");
			gameObject.GetComponent<Image>().enabled = true;
			gameObject.GetComponent<Button>().interactable = true;
		}
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
