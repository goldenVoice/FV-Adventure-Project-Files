using UnityEngine;
using System.Collections;

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
	}
	
}
