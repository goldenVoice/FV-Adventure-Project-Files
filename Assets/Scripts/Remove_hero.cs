using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Remove_hero : MonoBehaviour {

	public PotholeManager potholeManager;
	private List_hero list_hero;
	public GameManagerBehavior gameManager;
	private GameObject waterBarText;

	// Use this for initialization
	void Start () {
       gameObject.SetActive(true);
	   list_hero = (List_hero)GameObject.Find("list_hero").GetComponent<List_hero>();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManagerBehavior>();
		waterBarText = GameObject.Find("WaterBarText");
//	     gameObject.GetComponent<Collider2D>().enabled = true;
//      gameObject.renderer.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnMouseDown(){
		RemoveTheHero ();
    //  GameObject parentTransform = GetComponentInParent<GameObject>();
    //  Destroy(parentTransform);
  }

	void RemoveTheHero(){
		GameObject hero_parent = transform.parent.gameObject;
		gameManager.water += hero_parent.transform.GetChild(1).GetComponent<HeroData>().waterRefund;
	//	gameManager.water += gameObject.GetComponentInParent<HeroData>().waterRefund; // before destroying the parent gameObject na hero, refund muna ng water.
		gameManager.displayWater();
		//gameManager.GetComponent<Animator>().Play("waterText_pulsate");	// DEBUG. ANIMATION WONT PLAY
		waterBarText.GetComponent<Animator>().Play("waterText_pulsate");
//		print (gameObject.GetComponentInParent<HeroData>().waterRefund);
		potholeManager.heroPrefab_parent = null;
		potholeManager.transform.GetChild(0).renderer.enabled = false;    // hide the soil image 
		potholeManager.GetComponent<BoxCollider2D>().enabled = true;	  // enable the collider so the user can plant heroes again
		
		Destroy (gameObject.transform.parent.gameObject); // destroy the parent gameObject
		list_hero.removeHeroFromList(gameObject.transform.parent.gameObject);	// iterate through the list of heroes. then find the hero to remove
	}

}
