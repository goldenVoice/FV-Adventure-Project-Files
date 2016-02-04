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

	public GameObject heroDescPanel;

	//string toRemove = "circle_";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void displayInfo(){

//		if (Application.loadedLevelName == "library-heroes") {
//			string newName = gameObject.name.Substring (toRemove.Length);	
//			heroDescPanel.transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ().text = newName;	// hero name
//		}
//		else {
//			heroDescPanel.transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ().text = gameObject.name;	// insect name
//		}

		heroDescPanel.transform.GetChild (0).GetComponent<Image>().sprite = heroImage;
		heroDescPanel.transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ().text = charName;	// insect name
		heroDescPanel.transform.GetChild (1).GetComponent<Text>().text = "Attack: " + attackText;			// attack
		heroDescPanel.transform.GetChild (2).GetComponent<Text>().text = "Speed: " + speedText; // speed
		heroDescPanel.transform.GetChild (3).GetComponent<Text>().text = "HP: " + HPtext;// HP
		heroDescPanel.transform.GetChild (4).GetChild(0).GetComponent<Text> ().text = descText;

	}


}
