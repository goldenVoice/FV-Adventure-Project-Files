using UnityEngine;
using System.Collections;

public class Tutorial_forPothole : MonoBehaviour {

	private TutorialManager tutorialManager;

	bool heroPlaced;

	// Use this for initialization
	void Start () {
		heroPlaced = false;
		tutorialManager = (TutorialManager) GameObject.FindObjectOfType(typeof(TutorialManager));
	}
	
	// Update is called once per frame
	void Update () {
		if(tutorialManager.tutorial){ 	// if the tutorial is still playing
			if(gameObject.GetComponent<PotholeManager>().hero != null && gameObject.GetComponent<PotholeManager>().hero.name != "dummy_object" // check if the hero is not the dummy object then add it to the list
			   && !heroPlaced){
				tutorialManager.hero_forTutorial.Add( gameObject.GetComponent<PotholeManager>().hero);	// add this pothole's hero to the list of hero_forTutorial
				gameObject.GetComponent<PotholeManager>().hero.GetComponent<BoxCollider2D>().enabled = false;
				heroPlaced = true;
			}
			// tutorial: sa part ng removing the hero, if wala ng hero sa pothole and pag heroPlaced returns true. meaning ni remove to ng user
			else if(gameObject.GetComponent<PotholeManager>().hero  == null && heroPlaced){
				tutorialManager.hero_forTutorial.Remove( gameObject.GetComponent<PotholeManager>().hero);	// so remove the hero from the list
			}

			// check if the user 
			if(gameObject.GetComponent<PotholeManager>().hero != null){

			}

		}
	}
}
