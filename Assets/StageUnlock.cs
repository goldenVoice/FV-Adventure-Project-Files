using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageUnlock : MonoBehaviour {
	public GameObject[] stages;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("Level 1-3") == 1){		// tapos na stage 1
			// show stage unlock animation
			stages[1].transform.GetChild(0).GetComponent<Button>().interactable = true;		// stage 2
			stages[1].transform.GetChild(2).gameObject.SetActive(false);					// hide locked image
		}
		if(PlayerPrefs.GetInt("Level 2-3") == 1){		// tapos na stage 2
			// show stage unlock animation
			stages[2].transform.GetChild(0).GetComponent<Button>().interactable = true;		// stage 3
			stages[2].transform.GetChild(2).gameObject.SetActive(false);					// hide locked image
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
