using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
		Button hero = (Button)Instantiate (heroButton);
		hero.transform.SetParent(selectedHeroPanel.transform, false);
	}

	public void NextScene_dontDestroy(){
		DontDestroyOnLoad (obj.gameObject);
		DontDestroyOnLoad (selectHeroPanelCanvas.gameObject);
	}
}
