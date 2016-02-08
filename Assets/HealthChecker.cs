using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthChecker : MonoBehaviour {

	// Use this for initialization

	GameManagerBehavior gm;
	GameObject qtyText;
	void Start () {
		gm = (GameManagerBehavior)FindObjectOfType (typeof(GameManagerBehavior));
		qtyText = transform.GetChild (0).GetChild (0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (gm.health == gm.maxhealth || qtyText.GetComponent<Text>().text == "0") {
			Debug.Log("gm.health == gm.maxhealth");
			gameObject.GetComponent<Button> ().interactable = false;
		} else if(gm.health < gm.maxhealth) {
			Debug.Log(gm.health + " = gm.health " + ", " + gm.maxhealth + " = gm.maxhealth");
			gameObject.GetComponent<Button> ().interactable = true;
		}
	}
}
