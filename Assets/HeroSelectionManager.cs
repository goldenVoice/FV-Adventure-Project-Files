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
				hero.transform.SetParent(selectedHeroPanel.transform.GetChild(i).transform, false);

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
