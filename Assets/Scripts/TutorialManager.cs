using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;	


public class TutorialManager : MonoBehaviour {

	[HideInInspector]
	public bool tutorial;

	public Animator anim_kingGuava;
	private int counter;
	public GameObject[] buttons;	// sa start ng tutorial dapat naka disable/ not interactable to. para di makapag laro ang user, at makinig sa tutorial :P

	public Text tutorialText;

	public List<string> tutorial_dialog;

	private GameManagerBehavior gameManager;
	private Potholes_list list_potholes;

	private PotholeManager potholeManager;

	private GameObject pothole_hero1;	// pothole where the hero is planted
	private GameObject pothole_hero2;	// para sa tutorial	

	private GameObject hero1;
	private GameObject hero2;

	public List<GameObject> hero_forTutorial;
	
	void Awake(){
		gameManager = (GameManagerBehavior) GameObject.FindObjectOfType(typeof(GameManagerBehavior));
		list_potholes = (Potholes_list) GameObject.FindObjectOfType(typeof(Potholes_list));
		potholeManager = (PotholeManager) GameObject.FindObjectOfType(typeof(PotholeManager));

		tutorial_dialog.Add("The insects have arrived! Defend our place!"); 
		tutorial_dialog.Add("I will give you my Veggie hero, the Carrot.");	
		tutorial_dialog.Add("As shown below the Carrot's image, this hero costs 80 water.");	
		tutorial_dialog.Add("Click on the carrot then click any pothole.");	
		tutorial_dialog.Add("You can also drag the hero to plant. Try it!");	
		tutorial_dialog.Add("Click any hero to show its range and element. Try to remove a hero");	
		tutorial_dialog.Add("Every hero removed gives you a water refund.");	
//		tutorial_dialog.Add();	
//		tutorial_dialog.Add();	

	}
	// Use this for initialization
	void Start () {
		tutorial = true;
		counter = 0;
		tutorialText.text = tutorial_dialog[counter];	// display the first tutorial text from the array tutorial_dialog
		foreach(GameObject button in buttons){
			button.SetActive(false);	// hide first the buttons
		}
		counter++;
	}
	
	// Update is called once per frame
	void Update () {
		if(tutorial){
			if(anim_kingGuava.GetCurrentAnimatorStateInfo(0).IsName("king guava idle")
		   		&& tutorialText.text != "Click on the carrot then click any pothole."){
				tutorialText.transform.parent.gameObject.SetActive(true);	// show the tutorial text;
			}
			if(counter < 4){		// true if bawal pa mag tanim yung user. kase wala pa sinasabe si king guava
				gameManager.currentSelectedHero = null;	// so set this to null :D
			}
			// use this to know if the user is on the part of clicking the hero to plant
			if(counter == 5){
				// if this returns true ibig sabihin may isang nakatanim na hero na.
				if(hero_forTutorial.Count == 1){
						anim_kingGuava.gameObject.GetComponent<Image>().enabled = true;	 // show king guava 
						buttons[0].GetComponent<DragManager>().enabled = true;			 // enable dragging to teach the user another way of planting heroes.
						buttons[0].transform.GetChild(1).gameObject.SetActive(true);     // show the highlight of the carrot
						counter++;
						foreach(GameObject potholes in list_potholes.potholesList){
							potholes.transform.GetChild(1).gameObject.SetActive(false);	// this is the highlight gameObject, referenced then setactive (false)
						}
				}
			}
			// check if the hero is dragged, if so hide king guava and the caption cloud again
			if(counter == 6 && buttons[0].GetComponent<DragManager>().dragBegin == true){
				gameManager.currentSelectedHero = buttons[0].GetComponent<DragManager>().heroPrefab;
				//Debug.Log("KEMEEEEEEEE");
				hideTutorialStuffs();
				foreach(GameObject potholes1 in list_potholes.potholesList){
					if(potholes1.GetComponent<PotholeManager>().hero == null){
						// do nothing. since nasa update tong code na to, at ginagawa mong null yung .hero dadating talaga sa point na mag e error saying unassigned yung hero na variable.
						// so do nothing. para di lang mag error
						potholes1.transform.GetChild(1).gameObject.SetActive(true);
					}
					else if(potholes1.GetComponent<PotholeManager>().hero.name == "dummy_object"){
						potholes1.GetComponent<PotholeManager>().hero = null;		// disable the dummy object, so the user can plant on the empty potholes. its okay, the drag has started
//						potholes1.transform.GetChild(1).gameObject.SetActive(true);	// this is the highlight gameObject, referenced then setactive (true)
					}
					else if(potholes1.GetComponent<PotholeManager>().hero.name == "carrotHero 1(Clone)"){
						potholes1.transform.GetChild(1).gameObject.SetActive(false); // if a hero is already planted. dont show the highlight
					}
				}
			}
			// if the drag ended and ..
			else if(counter == 6 && buttons[0].GetComponent<DragManager>().dragEnd == true){
				if(hero_forTutorial.Count >= 2){
						Debug.Log ("Hero count : " + hero_forTutorial.Count);
					
					foreach(GameObject hero in hero_forTutorial){
								hero.GetComponent<BoxCollider2D>().enabled = true;
					}
					gameManager.currentSelectedHero = buttons[0].GetComponent<DragManager>().heroPrefab;
					buttons[0].transform.GetChild(1).gameObject.SetActive(false);     	 // hide the highlight of the carrot
					anim_kingGuava.gameObject.GetComponent<Image>().enabled = true;	 // show king guava 
					tutorialText.text = tutorial_dialog[counter - 1];	// hero planted, proceed to the next tutorial
					counter++;	
				}
				else {
					showTutorialStuffs();
					foreach(GameObject potholes1 in list_potholes.potholesList){
						potholes1.transform.GetChild(1).gameObject.SetActive(false);	// hide the pothole highlight
					}
				}
			}
			else if(counter == 7){
				// NEXT TUTORIAL, REMOVING THE HERO.
				if(hero_forTutorial.Count == 1){	// returns true, meaning nakapag tanggal na ng isang hero yung user
					tutorialText.text = tutorial_dialog[counter - 1];
					counter++;
				}
			}
		}
		
	}

	// this is attached to the invisible button
	public void tutorialNext(){
		// if the tutorial is still on going
		if(tutorial){
			if(tutorialText.text == "The insects have arrived! Defend our place!" && counter == 1
			   && (tutorialText.transform.parent.gameObject.activeSelf == true)){	// this check lets the user click anywhere ng hindi nasisira yung pattern ng tutorial. kase pag wala to, mag po proceed ang counter++, masisira yung tutorial, di pa tapos si king guava pumasok. pwede na mag plant
				//buttons[0].SetActive(true);
				tutorialText.text = tutorial_dialog[counter];
				//show the carrot
				buttons[0].SetActive(true);
				buttons[0].transform.GetChild(1).gameObject.SetActive(false);

				// SHOW THE WATER BAR
				buttons[5].SetActive(true);

   				GameObject waterText = GameObject.Find("WaterBarText");
				foreach(GameObject potholes in list_potholes.potholesList){					
					potholes.GetComponent<PotholeManager>().waterText = waterText;
				}

				counter++;
			}
			// dialog: "I will give you my warrior, the carrot" 1
			else if(tutorialText.text == "I will give you my Veggie hero, the Carrot." /* && counter == 1*/){
			
				tutorialText.text = tutorial_dialog[counter];
				counter++;
			}
			else if(tutorialText.text == "As shown below the Carrot's image, this hero costs 80 water."/* || counter == 2*/){
				tutorialText.text = tutorial_dialog[counter];
				buttons[0].transform.GetChild(1).gameObject.SetActive(true);
				counter++;				
			}
			else if(tutorialText.text == "Click on the carrot then click any pothole." && counter == 3){
				buttons[0].transform.GetChild(1).gameObject.SetActive(true);
			}
			// if the user didnt click on the pothole, he will see the instruction again
			else if(tutorialText.text == "Click on the carrot then click any pothole." 
			        && gameManager.currentSelectedHero == null && counter == 5){		
				// disable the highlights of potholes
				foreach(GameObject potholes in list_potholes.potholesList){
					potholes.transform.GetChild(1).gameObject.SetActive(false);	// this is the highlight gameObject, referenced then setactive (true)
				}
				buttons[0].transform.GetChild(1).gameObject.SetActive(true);   // show the highlight of the carrot
				anim_kingGuava.gameObject.GetComponent<Image>().enabled = true;					    // show king guava 
				tutorialText.transform.parent.gameObject.SetActive(true);	    // show the tutorial text
			}
		}
	}

	// called when the hero is clicked, highlight the potholes
	public void showInstruction(){
		if(tutorial){
			if(tutorialText.text == "I will give you my Veggie hero, the Carrot." /* && counter == 1*/){

				// SHOW THE WATER BAR
				buttons[5].SetActive(true);

				tutorialText.text = tutorial_dialog[counter];
				counter++;
			}
			else if(tutorialText.text == "As shown below the Carrot's image, this hero costs 80 water."  /*|| counter == 2*/){
				tutorialText.text = tutorial_dialog[counter];
				buttons[0].transform.GetChild(1).gameObject.SetActive(true);
				// highlight the circle carrot
				buttons[0].transform.GetChild(1).gameObject.SetActive(true);
				counter++;	
			}
			else if(tutorialText.text ==  "Click on the carrot then click any pothole." && counter == 4){
//				tutorialText.text = tutorial_dialog[counter];
				counter++;
			}
			 if(tutorialText.text == "Click on the carrot then click any pothole." 
			        && gameManager.currentSelectedHero != null && counter == 5 ){
				// show the highlights of potholes
				foreach(GameObject potholes in list_potholes.potholesList){					
					potholes.transform.GetChild(1).gameObject.SetActive(true);	  // this is the highlight gameObject, referenced then setactive (true)
				}
				hideTutorialStuffs();
//				Debug.Log (tutorialText.transform.parent.gameObject.name);
			}
			// if the hero was not dragged/ the user failed to drag the hero to the pothole
			else if(counter == 6){
				showTutorialStuffs();
				foreach(GameObject potholes1 in list_potholes.potholesList){
					if(potholes1.GetComponent<PotholeManager>().hero == null){
						potholes1.transform.GetChild(1).gameObject.SetActive(false);	// this is the highlight gameObject, referenced then setactive (false)
						potholes1.GetComponent<PotholeManager>().hero = buttons[9];		// if the user clicked the carrot. set the pothole's hero to the dummy_object para di sila makapag plant. tatanggalin naman mamaya yung dummy object pag nagsimula na yung drag
					}
				}
			}
		}
	}

	void hideTutorialStuffs(){
		buttons[0].transform.GetChild(1).gameObject.SetActive(false);     // disable the highlight of the carrot
		anim_kingGuava.gameObject.GetComponent<Image>().enabled = false;  // hide king guava 
		tutorialText.transform.parent.gameObject.SetActive(false);	      // hide the tutorial text
	}

	void showTutorialStuffs(){
		anim_kingGuava.gameObject.GetComponent<Image>().enabled = true;	 	 // show king guava 
		buttons[0].transform.GetChild(1).gameObject.SetActive(true);     	 // show the highlight of the carrot
		tutorialText.transform.parent.gameObject.SetActive(true);	    	 // show the tutorial text
	}
}
