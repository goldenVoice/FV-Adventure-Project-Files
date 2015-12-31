using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour {

	public Button[] levelButtons;

	// Use this for initialization
	void Start () {
		Debug.Log((levelButtons[0].name));
		if(PlayerPrefs.GetInt(levelButtons[0].name) == 1){											//ex: levelButtons[0].name is Level 1-1, yan yung button na scene. and if = 1 sya, meaning natapos na ng user yung lvl 1

			showStatus(levelButtons[0]);															// show status of level 1: perfect of complete lang
			levelButtons[1].transform.GetChild(2).GetComponent<Image>().enabled = false;			// hide the locked image. 
			levelButtons[1].GetComponent<Button>().interactable = true;								// show/ enable the button. 
			levelButtons[1].transform.GetChild(0).GetComponent<Text>().enabled = true;				// Show level text. 
		}
		if(PlayerPrefs.GetInt(levelButtons[1].name) == 1){											

			showStatus(levelButtons[1]);															// show status of level 2: perfect of complete lang
			levelButtons[2].transform.GetChild(2).GetComponent<Image>().enabled = false;			// hide the locked image. 
			levelButtons[2].GetComponent<Button>().interactable = true;								// show/ enable the button. 
			levelButtons[2].transform.GetChild(0).GetComponent<Text>().enabled = true;				// Show level text. 
		}
		if(PlayerPrefs.GetInt(levelButtons[2].name) == 1){											
			
			showStatus(levelButtons[2]);															// show status of level 2: perfect of complete lang
			levelButtons[2].transform.GetChild(0).GetComponent<Text>().enabled = true;				// Show level text. 
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void showStatus(Button button){

		if(PlayerPrefs.GetFloat(button.name + "_status") == 1){
			button.transform.GetChild(1).GetComponent<Text>().text = "PERFECT!";
			button.transform.GetChild(1).GetComponent<Text>().color = Color.green;
		}
		else if(PlayerPrefs.GetFloat(button.name + "_status") < 1){
			button.transform.GetChild(1).GetComponent<Text>().text = "CLEARED!";
			button.transform.GetChild(1).GetComponent<Text>().color = Color.yellow;
		}
	}
}
