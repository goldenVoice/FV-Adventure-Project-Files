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
		if(PlayerPrefs.GetInt("Level 3-3") == 1){		// tapos na stage 3
			// show stage unlock animation
			stages[3].transform.GetChild(0).GetComponent<Button>().interactable = true;		// stage 4
			stages[3].transform.GetChild(2).gameObject.SetActive(false);					// hide locked image
		}if(PlayerPrefs.GetInt("Level 4-3") == 1){		// tapos na stage 4
			// show stage unlock animation
			stages[4].transform.GetChild(0).GetComponent<Button>().interactable = true;		// stage 5
			stages[4].transform.GetChild(2).gameObject.SetActive(false);					// hide locked image
		}if(PlayerPrefs.GetInt("Level 5-3") == 1){		// tapos na stage 5
			// show stage unlock animation
			stages[5].transform.GetChild(0).GetComponent<Button>().interactable = true;		// stage 6
			stages[5].transform.GetChild(2).gameObject.SetActive(false);					// hide locked image
		}if(PlayerPrefs.GetInt("Level 6-3") == 1){		// tapos na stage 6
			// show stage unlock animation
			stages[6].transform.GetChild(0).GetComponent<Button>().interactable = true;		// stage 7
			stages[6].transform.GetChild(2).gameObject.SetActive(false);					// hide locked image
		}if(PlayerPrefs.GetInt("Level 7-3") == 1){		// tapos na stage 7
			// show stage unlock animation
			stages[7].transform.GetChild(0).GetComponent<Button>().interactable = true;		// stage 8
			stages[7].transform.GetChild(2).gameObject.SetActive(false);					// hide locked image
		}if(PlayerPrefs.GetInt("Level 8-3") == 1){		// tapos na stage 8
			// show stage unlock animation
			stages[8].transform.GetChild(0).GetComponent<Button>().interactable = true;		// stage 9
			stages[8].transform.GetChild(2).gameObject.SetActive(false);					// hide locked image
		}if(PlayerPrefs.GetInt("Level 9-3") == 1){		// tapos na stage 9
			// show stage unlock animation
			stages[9].transform.GetChild(0).GetComponent<Button>().interactable = true;		// stage 10
			stages[9].transform.GetChild(2).gameObject.SetActive(false);					// hide locked image
		}
			//if(PlayerPrefs.GetInt("Level 10-3") == 1){		// tapos na stage 2
//			// show stage unlock animation
//			stages[2].transform.GetChild(0).GetComponent<Button>().interactable = true;		// stage 3
//			stages[2].transform.GetChild(2).gameObject.SetActive(false);					// hide locked image
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
