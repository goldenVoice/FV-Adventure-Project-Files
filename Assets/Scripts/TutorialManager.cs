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
		//		buttons[10].SetActive(true);	// show the nextButton for the tutorial
			}
			if(counter < 4){		// true if bawal pa mag tanim yung user. kase wala pa sinasabe si king guava
				gameManager.currentSelectedHero = null;	// so set this to null :D
			}
			// use this to know if the user is on the part of clicking the hero to plant
			if(counter == 5){
//				int count =0;
//				foreach(GameObject potholes in list_potholes.potholesList){

//					Debug.Log (potholes.gameObject);

					// check if the user has planted a hero, if yes move to the next dialog of king guava
//					if(potholes.GetComponent<PotholeManager>().hero != null ){		 // if this canPlaceHero returns false, meaning may hero na sa isang pothole, kaya may negator akong nilagay sa dulo (!)
//						tutorialText.text = tutorial_dialog[counter - 1];

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
						// get the newly planted hero, and disable its box collider, para di sya ma delete ng user
						//pothole_hero1 = potholes;
						//Debug.Log("This should show: " + potholes.transform.GetComponent<PotholeManager>().hero.GetComponent<BoxCollider2D>());
						// potholes.transform.GetComponent<PotholeManager>().hero.GetComponent<BoxCollider2D>().enabled = false;
						//hero1 = potholes.transform.GetComponent<PotholeManager>().hero.gameObject;

//						break;
//					}
//				}
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
//				foreach(GameObject potholes1 in list_potholes.potholesList){
					// .. the user is over a pothole
//				Debug.Log (list_potholes.potholesList[0].GetComponent<PotholeManager>().dragManager != null);
//					if(list_potholes.potholesList[0].GetComponent<PotholeManager>().dragManager != null){
//				int heroCount = 0;
					
//				Debug.Log ("SAGLET LANG DAPAT TO LALABAS");
//				foreach(GameObject potholes1 in list_potholes.potholesList){
//					if((potholes1.GetComponent<PotholeManager>().hero == null)){
//						// do nothing
//					}
//					else if((potholes1.GetComponent<PotholeManager>().hero.name == "carrotHero 1(Clone)")){
//						heroCount++;
//					//	potholes1.transform.GetComponent<PotholeManager>().hero.GetComponent<BoxCollider2D>().enabled = true;
//						if(heroCount >= 2){
//							break;
//						}
//					}
//				}
				// if the drag ended and the hero_forTutorial has 2 or more numbers. meaning the user planted a new hero using drag and drop
				if(hero_forTutorial.Count >= 2){
						Debug.Log ("Hero count : " + hero_forTutorial.Count);
					
					foreach(GameObject hero in hero_forTutorial){
						// enable the box collider of the heroes, para sa susunod na instruction ni king guava
//								hero1 = potholes1.transform.GetComponent<PotholeManager>().hero.gameObject;	// get the hero object na nasa scene mismo
								hero.GetComponent<BoxCollider2D>().enabled = true;
//								pothole_hero1 = potholes1;	// get the pothole kung nasan yung hero1 na nakaplant
//							}
//							else if(pothole_hero2 == null){
//								hero2 = potholes1.transform.GetComponent<PotholeManager>().hero.gameObject;
//								hero2.GetComponent<BoxCollider2D>().enabled = true;
//								pothole_hero2 = potholes1;
//							}
//						}
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
//				if(pothole_hero1.GetComponent<PotholeManager>().hero == null){
//					tutorialText.text = tutorial_dialog[counter - 1];
//					counter++;
//				}
//				else if(pothole_hero2.GetComponent<PotholeManager>().hero == null){
//					tutorialText.text = tutorial_dialog[counter - 1];
//					counter++;
//				}
			}
		}
		
	}

	// this is attached to the invisible button
	public void tutorialNext(){
		// if the tutorial is still on going
		if(tutorial){
//			Debug.Log ("HOYYYYYY");
//			if(anim_kingGuava.GetCurrentAnimatorStateInfo(0).IsName("king guava idle")
//			   && tutorialText.text != "Click on the carrot then click any pothole."){
//				tutorialText.transform.parent.gameObject.SetActive(true);	// show the tutorial text;
//			}

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
				// highlight the circle carrot
			//	buttons[0].transform.GetChild(1).gameObject.SetActive(true);

				counter++;
				
			}
			else if(tutorialText.text == "Click on the carrot then click any pothole." && counter == 3){
				buttons[0].transform.GetChild(1).gameObject.SetActive(true);

				//			tutorialText.text = tutorial_dialog[counter];
	//			counter++;
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
			else if(tutorialText.text == "You can also drag the hero to plant. Try it!" && counter == 6){
				Debug.Log ("Hey");
			}
		}

	}

	// called when the hero is clicked, highlight the potholes
	public void showInstruction(){
		if(tutorial){
//			Debug.Log(tutorialText.text);
//			Debug.Log(tutorialText.text== "Click on the carrot then click any pothole." 
//			          && gameManager.currentSelectedHero != null);
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
//				Debug.Log("KEMEEEEEEEEEEEEEE");
				// show the highlights of potholes
				foreach(GameObject potholes in list_potholes.potholesList){					
					potholes.transform.GetChild(1).gameObject.SetActive(true);	  // this is the highlight gameObject, referenced then setactive (true)
				}
				hideTutorialStuffs();
//				Debug.Log (tutorialText.transform.parent.gameObject.name);
			}

			// if the hero was not dragged/ the user failed to drag the hero to the pothole
			else if(counter == 6){
				Debug.Log("counter 6");

				showTutorialStuffs();

				foreach(GameObject potholes1 in list_potholes.potholesList){
					potholes1.transform.GetChild(1).gameObject.SetActive(false);	// this is the highlight gameObject, referenced then setactive (false)
					potholes1.GetComponent<PotholeManager>().hero = buttons[9];		// if the user clicked the carrot. set the pothole's hero to the dummy_object para di sila makapag plant. tatanggalin naman mamaya yung dummy object pag nagsimula na yung drag
				}
				// this is to restrict the user na mag plant by clicking. Pag ang instruction ni King guava ay drag to plant :)
				gameManager.currentSelectedHero = null;
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
