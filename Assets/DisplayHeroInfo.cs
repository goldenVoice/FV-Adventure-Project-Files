using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayHeroInfo : MonoBehaviour {
	public string charName;
	public string attackText;
	public string speedText;
	public string HPtext;
	public string descText;
	public Sprite heroImage; 

	public string path;
	public string ability;
	public GameObject heroDescPanel;


//	// MEDIUM DESCR
//	public string attackText_med;
//	public string speedTextattackText_med;
//	public string HPtextattackText_med;
//	public string pathattackText_med;
//	public string abilityattackText_med;
//
//	// HARD DESC
//	public string attackText_hard;
//	public string speedTextattackText_hard;
//	public string HPtextattackText_hard;
//	public string pathattackText_hard;
//	public string abilityattackText_hard;
//
//
//	bool mediumUnlocked;
//	bool hardUnlocked;

	//string toRemove = "circle_";
	// Use this for initialization
	void Start () {

//		mediumUnlocked = false;
//		hardUnlocked = false;
//		string whatMode = GameObject.Find ("MODE TEXT").GetComponent<Text>().text ;
//
//		if(whatMode == "MEDIUM"){
//			mediumUnlocked = true;
//		}
//		if(whatMode == "HARD"){
//			hardUnlocked = true;
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void displayInfo(){

//		if(){
//
//		}


		if (Application.loadedLevelName == "library-heroes"){
			heroDescPanel.transform.GetChild (5).GetComponent<Text> ().text = "Target: " + path;	
		}
		if (Application.loadedLevelName == "library-heroes alfie") {
			heroDescPanel.transform.GetChild (5).GetComponent<Text> ().text = "Target: " + path;	
		}
		else {
			heroDescPanel.transform.GetChild (5).GetComponent<Text> ().text = "Path: " + path;	
			heroDescPanel.transform.GetChild (6).GetComponent<Text> ().text = "Ability: " + ability;	
		}

		heroDescPanel.transform.GetChild (0).GetComponent<Image>().sprite = heroImage;
		heroDescPanel.transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ().text = charName;	// insect name
		heroDescPanel.transform.GetChild (1).GetComponent<Text>().text = "Strength: " + attackText;			// attack
		heroDescPanel.transform.GetChild (2).GetComponent<Text>().text = "Speed: " + speedText; // speed
		heroDescPanel.transform.GetChild (3).GetComponent<Text>().text = "HP: " + HPtext;// HP
		heroDescPanel.transform.GetChild (4).GetChild(0).GetComponent<Text> ().text = descText;
		//}
	}


}
