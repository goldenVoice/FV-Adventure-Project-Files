using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoButtonChecker : MonoBehaviour {
	GameObject selectedHeroPanel;
	bool hasChild;
	// Use this for initialization
	void Start () {
		selectedHeroPanel = transform.parent.GetComponent<HeroSelectionManager> ().selectedHeroPanel;
	}
	
	// Update is called once per frame
	void Update () {

		// check if wala pang laman yung  selected hero panel.
		for (int i = 0; i < 5; i++) {
			if (selectedHeroPanel.transform.GetChild (i).childCount == 1) {	// meaning wala pang child	
				hasChild = true;
				break;
			}
			hasChild = false;
		}

		if (hasChild) {
			GetComponent<Button> ().interactable = true;
		} 
		else {
			GetComponent<Button>().interactable = false;
		}
	}
}
