using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetHero : MonoBehaviour {

	private GameManagerBehavior gameManager;
	// Use this for initialization
	void Start () {
		gameManager = (GameManagerBehavior) GameObject.FindObjectOfType(typeof(GameManagerBehavior));

	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager == null) {
			gameManager = (GameManagerBehavior) GameObject.FindObjectOfType(typeof(GameManagerBehavior));	
			Debug.Log("how many times");
		}
	
	}

	public void ActivateScript(){
		this.enabled = true;
	}

	public void GetTheHero(GameObject hero){
		gameManager.currentSelectedHero = hero;

		// SET THE WATER cost of the cur selected hero, by getting the text on the circle_hero's child. the waterCostText
		int cost = int.Parse (gameObject.transform.GetChild (0).GetComponent<Text> ().text);
		gameManager.curHeroWatercost = cost;

		//hero.GetComponent<HeroData> ().cost = cost;
	}
	
}
