using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HeroSelectionManager : MonoBehaviour {
	public GameObject selectedHeroPanel;
	public Canvas selectHeroPanelCanvas;

	public GameObject obj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SelectTheHero(Button heroButton){


		for (int i = 0; i < 5; i++){
			if(selectedHeroPanel.transform.GetChild(i).childCount == 0){	// meaning wala pang child	
				Button hero = (Button)Instantiate (heroButton);

				// set the water cost of the newly instantiated hero, by getting the water cost from the last circle hero that was clicked
				hero.transform.GetChild(0).GetComponent<Text>().text = 
					gameObject.transform.GetChild(1).Find(EventSystem.current.currentSelectedGameObject.name).transform.GetChild (0).GetComponent<Text> ().text;

				// put the hero on the selected hero panel
				hero.transform.SetParent(selectedHeroPanel.transform.GetChild(i).transform, false);

//				// set the water of the hero from the existing circle hero that holds the water value from the xml database
//				hero.gameObject.transform.GetChild (0).GetComponent<Text> ().text = gameObject.transform.GetChild (0).GetComponent<Text> ().text;

				// get the last clicked button to disable it para hindi ma click ule ng user, using eventsystem to track the last button clicked
				gameObject.transform.GetChild(1).Find(EventSystem.current.currentSelectedGameObject.name).GetComponent<Button>().interactable = false;
				gameObject.transform.GetChild(1).Find(EventSystem.current.currentSelectedGameObject.name).GetComponent<Image>().color = Color.gray;
				break;	
			}
		}
	}

	public void NextScene_dontDestroy(){
		DontDestroyOnLoad (obj.gameObject);
		DontDestroyOnLoad (selectHeroPanelCanvas.gameObject);
	}
}
