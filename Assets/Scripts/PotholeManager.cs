using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PotholeManager : MonoBehaviour {

	public AudioClip noWaterSound;
  	public GameObject heroPrefab_parent; // ilalagay sa inspector

	private GameObject heroPrefab;	// the actual hero


  	public GameObject hero;      // para mamaya, check kung may carrot na sa pothole

	[HideInInspector]
  	public GameObject waterText;
	private string layerName;

  	GameManagerBehavior GameManager;
  	HeroData heroData;

  	private List_hero list_hero;
  	public GameObject plantedParticle;

	public Remove_hero remove_hero;

	public bool drop;

	public DragManager dragManager;

	// Use this for initialization
	void Start () {

      GameManager = (GameManagerBehavior)FindObjectOfType(typeof(GameManagerBehavior));
      heroData = (HeroData)FindObjectOfType(typeof(HeroData));
      waterText = GameObject.Find("WaterBarText");
      list_hero = (List_hero)GameObject.Find("list_hero").GetComponent<List_hero>();
	}
	
	// Update is called once per frame
	void Update () {
      

  	}  
  
  	public bool canPlaceHero(){
      	if(hero == null){
          	return true;
      	}
      	return false;
  	}

  	// kapag pinindot ng user yung pothole, 
  	void OnMouseUp (){
		PlantHero();
    
  	}

	void OnMouseOver(){

		if(dragManager != null){
			dragManager.currentPothole = this; // ibig sabihin, set THIS pothole, as the current pothole, kase nga dito naka mouseOver yung user
		}
		if(drop){
			PlantHero();
			drop = false;
		}
	}
	void OnMouseExit(){
		if(dragManager != null){					// dont forget to check this para di mag error, when the mouse leaves the pothole,
			if(dragManager.currentPothole != null)  // set the drag manager's pothole to null,
				dragManager.currentPothole = null;  // para di mag set yung variable drop ng recently left na pothole into true.
		}
	}

	void PlantHero(){
		// titignan kung may selected na hero, this means di na null yung value.
		if(GameManager.currentSelectedHero != null){    				// by looking at currentSelected hero at gameManager
			
			heroPrefab_parent = GameManager.currentSelectedHero;              			// set the currentSelected hero as the heroPrefab_parent for the pothole 
//			Debug.Log(heroPrefab_parent.transform.GetChild(1).gameObject);
			heroPrefab = heroPrefab_parent.transform.GetChild(1).gameObject;	// the actual heroPrefab. not the empty parent gameObject (heroPrefab_parent)
			int cost = GameManager.curHeroWatercost;       					
			// pag wala pang hero at may sapat na water pa yung user      
			if( canPlaceHero() && (GameManager.water >= cost)) {
				
				// show the soil image
				gameObject.transform.GetChild(0).renderer.enabled = true;
				Instantiate(plantedParticle, gameObject.transform.position, gameObject.transform.rotation);
				hero =  (GameObject) Instantiate(heroPrefab_parent, transform.position, gameObject.transform.rotation);
				hero.layer = LayerMask.NameToLayer("heroes");
				// set the heroes sorting layer depending on what pothole it was planted
				//				print (gameObject.renderer.sortingLayerName);
				if(gameObject.renderer.sortingLayerName == "lower potholes"){
					hero.transform.GetChild(0).renderer.sortingLayerName = "hero_lowerSide";	// the x mark
					hero.transform.GetChild(1).renderer.sortingLayerName = "hero_lowerSide";	// carrotHero
					hero.transform.GetChild(2).renderer.sortingLayerName = "hero_lowerSide";	// range
					hero.transform.GetChild(4).renderer.sortingLayerName = "hero_lowerSide";	// element icon
					hero.transform.GetChild(5).renderer.sortingLayerName = "hero_lowerSide";	// HealthBar BG
					hero.transform.GetChild(6).renderer.sortingLayerName = "hero_lowerSide";	// HealthBar
				}
				else if(gameObject.renderer.sortingLayerName == "upper potholes"){
					hero.transform.GetChild(0).renderer.sortingLayerName = "hero_upperSide";
					hero.transform.GetChild(1).renderer.sortingLayerName = "hero_upperSide";
					hero.transform.GetChild(2).renderer.sortingLayerName = "hero_upperSide";
					hero.transform.GetChild(4).renderer.sortingLayerName = "hero_upperSide";
					hero.transform.GetChild(5).renderer.sortingLayerName = "hero_lowerSide";	// HealthBar BG
					hero.transform.GetChild(6).renderer.sortingLayerName = "hero_lowerSide";	// HealthBar
				}
				
				// add the hero to the current lists of planted heroes
				list_hero.addHero(hero);
				
				GameManager.deductWater(cost);
				waterText.GetComponent<Animator>().SetTrigger("water_deducted");

				// 				gameobject.sendMessage("MethodName")
				hero.transform.GetChild(2).SendMessage("ActivateScript");	// looks through all the script attached to a gameObject, and finds and executes the method if it exists in the script

				gameObject.GetComponent<BoxCollider2D>().enabled = false;						// disable the potholes 2D collider, for now, para clickable yung hero,		
				hero.transform.GetChild(0).GetComponent<Remove_hero>().potholeManager = this;	// make this script instance the potholeManager script to use it to remove the hero later

				// play sound
				if(PlayerPrefs.GetInt("sounds") == 1){		// if sounds: ON
					AudioSource audio = GetComponent<AudioSource>();
					audio.PlayOneShot(audio.clip, 0.8f);			// audio.clip is yung pinaka sound na naka attach sa audio source component of the object
				}

			}
			// kung yung water ng user di na kaya i-afford yung cost nung hero :'(
			else if(GameManager.water < cost){
				waterText.GetComponent<Animator>().SetTrigger("no_water");
				GameManager.deductWater(0); // tinawag pa ren yung deduct water, para lang 
				// ma-display yung kung ilang water pa meron yung user.
				// 0 yung pinasang parameter kase walang ibabawas sa water
				// kahet na wala naman talagang laman.

				// play sound no water
				if(PlayerPrefs.GetInt("sounds") == 1){		// if sounds: ON
					AudioSource audio = GetComponent<AudioSource>();
					//Debug.Log("play sound");
					audio.PlayOneShot(noWaterSound, 0.8f);			// audio.clip is yung pinaka sound na naka attach sa audio source component of the object
				}

			}
			// kapag na touch nya na yung pothole, i DESELECT mo yung currentSelected hero para 1 is to 1 yung pagtatanim, kailangan click ule sa button para makapag tanim
			GameManager.currentSelectedHero = null;
		}
		// when a pothole is touched na walang selected hero. disable the range of all the heroes (if ever na may naka show na range)
		list_hero.deselectAllHeroes();

	}
}
