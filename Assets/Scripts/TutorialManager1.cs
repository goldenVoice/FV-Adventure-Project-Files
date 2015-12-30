using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;	


public class TutorialManager1 : MonoBehaviour {
	[HideInInspector]
	public bool tutorial;

	bool shopTutorial;

	bool kingGuavaEntered;
	bool activeNa;	// temporary variable. lalagay ko to sa update para isang bes lang mag true yung conditions na nag a activate ng hidden objects like pothole highlight ganon
	bool activeNaPotholes;		// check if active na yung lahat ng potholes, same lang den sa taas temporary variable
	public Animator anim_kingGuava;
//	public Animator anim_captionCloud;

	private int counter;
	public GameObject[] buttons;	// sa start ng tutorial dapat naka disable/ not interactable to. para di makapag laro ang user, at makinig sa tutorial :P
	
	public Text tutorialText;
	
	public List<string> tutorial_dialog;
	
	private GameManagerBehavior gameManager;
	private Potholes_list list_potholes;
	private List_hero list_hero;
	
	private PotholeManager potholeManager;

	public GameObject[]	potholes_array;
	private BoxCollider2D[] colliders_pothole1;

	private pauseMenuManager pauseMenuManagerScript;

	void Awake(){

		gameManager = (GameManagerBehavior) GameObject.FindObjectOfType(typeof(GameManagerBehavior));
		list_potholes = (Potholes_list) GameObject.FindObjectOfType(typeof(Potholes_list));
		potholeManager = (PotholeManager) GameObject.FindObjectOfType(typeof(PotholeManager));
		list_hero = (List_hero) GameObject.FindObjectOfType(typeof(List_hero));
		
		pauseMenuManagerScript = (pauseMenuManager) GameObject.FindObjectOfType(typeof(pauseMenuManager));

		if(PlayerPrefs.GetInt("Tutorial") == 1){		// tutorial enabled
			ElementManager elementManager = (ElementManager) GameObject.FindObjectOfType(typeof(ElementManager));
			elementManager.GetComponent<ElementManager>().enabled = false;								// ng wag mag random ang mga element
			
			SpawnEnemy spawnEnemy = (SpawnEnemy) GameObject.FindObjectOfType(typeof(SpawnEnemy));
			spawnEnemy.GetComponent<SpawnEnemy>().waves[0].WaveElement = ElementManager.Element.Air;	// kailangan gantong order yung elements para
			spawnEnemy.GetComponent<SpawnEnemy>().waves[1].WaveElement = ElementManager.Element.Water;	// makita ng user yung pagkakaiba ng damgages
			spawnEnemy.GetComponent<SpawnEnemy>().waves[2].WaveElement = ElementManager.Element.Fire;	// per element

			tutorial_dialog.Add("The insects have arrived! Defend our place!"); 
			tutorial_dialog.Add("I will give you my Veggie hero, the Carrot.");	
			tutorial_dialog.Add("As shown below the Carrot's image, this hero costs 80 water.");	
			tutorial_dialog.Add("Tap on the Carrot then tap on the pothole.");	
			tutorial_dialog.Add("Here's another pothole, now DRAG the Carrot hero to plant. Try it!");	
			tutorial_dialog.Add("Tap a Carrot to see an X mark, the Carrot's range, and element.");
			tutorial_dialog.Add("Remove a hero by tapping the X mark.");	
			tutorial_dialog.Add("Every hero removed gives you a water refund.");	
			tutorial_dialog.Add("I added more potholes. Plant Carrots as long as you have enough water and");	
			tutorial_dialog.Add("Click on the Start wave button to start fighting! But before that..");	
			tutorial_dialog.Add("The Next Wave indicator shows the element of the approaching enemies");	
			tutorial_dialog.Add("and if they are a walking or flying type of insect.");	
			tutorial_dialog.Add("Here, you can see that the insects approaching are");
			tutorial_dialog.Add("walking insects possessing Air element.");
			tutorial_dialog.Add("Here is a list of the precedence of the elements");
			tutorial_dialog.Add("When a hero attacks an element stronger than him. The damage is lesser, and vice versa.");
			tutorial_dialog.Add("If an element is attacked by the same element the damage is normal.");
			tutorial_dialog.Add("As you go on, you'll acquire hero with different elements. Use them well!");
			tutorial_dialog.Add("GOOD LUCK!");
			
			tutorial_dialog.Add("Another wave of enemy is coming!");
			tutorial_dialog.Add("You may tap the Next Wave Button if you want to fight them right away");	
			tutorial_dialog.Add("Notice that the Next Wave indicator changed. Water element is next");	
		}

		if(PlayerPrefs.GetInt("ShopTutorial") == 1){		// shop tutorial enabled
			buttons[7].SetActive(true);						// tutorial parent gameObject
			shopTutorial = true;
			tutorial_dialog.Add("This is the Map, you can scroll sideways to navigate.");	
			tutorial_dialog.Add("Upgrade and buy heroes at the shop!");	
			
			tutorial_dialog.Add("You can also buy certain boosters for your battle.");	

			tutorial_dialog.Add("Claim rewards in the 'Achievements'.");	
			tutorial_dialog.Add("Browse the 'Library' for references about enemies, etc.");	

			buttons[1].SetActive(false);
			buttons[2].SetActive(false);
			buttons[3].SetActive(false);
			buttons[5].SetActive(false);
			buttons[6].GetComponent<Button>().enabled = false;	// stage 1 button
			//		tutorial_dialog.Add("");	

			counter = 0;
			counter++;

			tutorialText.text = tutorial_dialog[0];
		}

//		tutorial_dialog.Add("");	
//		tutorial_dialog.Add("");	
//		tutorial_dialog.Add("");	
//		tutorial_dialog.Add("");	
//		tutorial_dialog.Add("");	
//		tutorial_dialog.Add("");	
	}
	// Use this for initialization
	void Start () {
//		Debug.Log ("HEYYY");
		if(PlayerPrefs.HasKey("Tutorial")){		 // this key is set from the storyline.
			if(PlayerPrefs.GetInt("Tutorial") == 1){
				tutorial = true;							// if true, 1st time mag laro ng user & he needs to undergo tutorial
				
				kingGuavaEntered = false;
				activeNa = false;
				activeNaPotholes = false;
				counter = 0;
				tutorialText.text = tutorial_dialog[counter];	// display the first tutorial text from the array tutorial_dialog
				foreach(GameObject button in buttons){
					button.SetActive(false);	// hide first the buttons
				}
				
				buttons[15].SetActive(true);						// tutorial parent gameObject
				
				// hide potholes
				for (int i = 0; i < list_potholes.potholesList.Length; i++){
					if(list_potholes.potholesList[i].activeSelf && list_potholes.potholesList[i].renderer.enabled == true){
						list_potholes.potholesList[i].renderer.enabled = false;	// show hidden pothole
						BoxCollider2D[] tempCollider = list_potholes.potholesList[i].GetComponents<BoxCollider2D>();
						tempCollider[0].enabled = false;	// disable the collider para di muna makapag tanim yung user
						tempCollider[1].enabled = false;
					}
					else{
						
					}
				}
				
				BoxCollider2D[] tempCollider1 =buttons[11].GetComponents<BoxCollider2D>();
				tempCollider1[0].enabled = false;	// disable the collider para di muna makapag tanim yung user
				tempCollider1[1].enabled = false;
				
				buttons[11].gameObject.SetActive(true);	// show first pothole
				buttons[11].renderer.enabled = true;
				counter++;
			}

			else if(PlayerPrefs.GetInt("Tutorial") == 0 && PlayerPrefs.GetInt("ShopTutorial") == 0){
				tutorial = false;							// this means pwedeng na fail ni user yung level 1 tas kelangan nya laruin ule
				buttons[0].transform.GetChild(1).gameObject.SetActive(false);	// hide carrot circle highlight gameObject
				buttons[8].transform.GetChild(1).gameObject.SetActive(false);	// hide highlight of next Wave indicator
				buttons[1].transform.GetChild(0).gameObject.SetActive(false);	// hide the highlight of startWave button
				buttons[0].GetComponent<DragManager>().enabled = true;			// enable dragging
				
			}
		}
	}

	void Update(){
		if(tutorial){
			if(anim_kingGuava.GetCurrentAnimatorStateInfo(0).IsName("king guava idle") && !kingGuavaEntered){
				tutorialText.transform.parent.gameObject.SetActive(true);	// show the tutorial text;
				buttons[10].SetActive(true);
				kingGuavaEntered = true;	// set to true para di na daanan tong condition na to
			}
		//	tutorial_dialog.Add();	
			else if(tutorialText.text ==  "I will give you my Veggie hero, the Carrot."){
				colliders_pothole1 = buttons[11].GetComponents<BoxCollider2D>();
				colliders_pothole1[0].enabled = false;		// disable both para di muna makapag tanim yung user :D
				colliders_pothole1[1].enabled = false;		// 2 kase yung collider kaya nilagay ko sa array
			}
			else if(tutorialText.text == "Tap on the Carrot then tap on the pothole."){
				buttons[10].SetActive(false);										// hide NextButton
				buttons[0].GetComponent<Button>().interactable = true;				// the user can now click the button
				if(gameManager.currentSelectedHero != null){							// if may hero dito (hindi null) means na click ng user, yung carrot hero
					hideTutorialStuffs();
					potholes_array[0].transform.GetChild(1).gameObject.SetActive(true);	// show pothole highlight
					colliders_pothole1[0].enabled = true;		// disable both para di muna makapag tanim yung user :D
					colliders_pothole1[1].enabled = true;		// 2 kase yung collider kaya nilagay ko sa array
				}
				else if(potholes_array[0].GetComponent<PotholeManager>().hero != null ){	// may nakatanim na
					potholes_array[0].GetComponent<PotholeManager>().hero.GetComponent<BoxCollider2D>().enabled = false;	// disable para di muna ma remove ng user tong carrot nato
					potholes_array[0].transform.GetChild(1).gameObject.SetActive(false);	// hide pothole highlight
					nextMessage();
				}
			}
			else if(tutorialText.text == "Here's another pothole, now DRAG the Carrot hero to plant. Try it!" && !activeNa){
				showTutorialStuffs();
				buttons[10].SetActive(false);									// hide NextButton
				buttons[0].GetComponent<DragManager>().enabled = true;			// enable dragging
				potholes_array[1].SetActive(true);								// show the 2nd pothole
				colliders_pothole1[0].enabled = false;							// disable both para di muna makapag tanim yung user :D
				colliders_pothole1[1].enabled = false;							// 2 kase yung collider kaya nilagay ko sa array
//				colliders_pothole1 = buttons[12].GetComponents<BoxCollider2D>();// get the 2 colliders of the 2nd pothole
				activeNa = true;												// use this variable para isang bes lang mag execute tong expression na to, showing ng stuffs lang kase to kaya 1 bes lang dapat
			}
			else if(tutorialText.text == "Here's another pothole, now DRAG the Carrot hero to plant. Try it!"){
				if(potholes_array[1].GetComponent<PotholeManager>().dragManager != null){	// if may laman. the user is currently dragging
					hideTutorialStuffs();
//					colliders_pothole1[0].enabled = true;		// enable both para di muna makapag tanim yung user :D
//					colliders_pothole1[1].enabled = true;		// 2 kase yung collider kaya nilagay ko sa array
					potholes_array[1].transform.GetChild(1).gameObject.SetActive(true);	// show pothole 2 highlight
				}
				else if(potholes_array[1].GetComponent<PotholeManager>().hero != null){	// hero is now planted! using dragging
					nextMessage();
					potholes_array[0].GetComponent<PotholeManager>().hero.GetComponent<BoxCollider2D>().enabled = true;	// enabled para pwede na i remove
					buttons[0].transform.GetChild(1).gameObject.SetActive(false);     		// disable the highlight of the carrot
				}
				else if(potholes_array[1].GetComponent<PotholeManager>().dragManager == null){	// if walang laman. the user is ended dragging/ has not dragged
					showTutorialStuffs();
//					colliders_pothole1[0].enabled = false;									// disable both para di muna makapag tanim yung user :D
//					colliders_pothole1[1].enabled = false;									// 2 kase yung collider kaya nilagay ko sa array
					buttons[0].transform.GetChild(1).gameObject.SetActive(true);     		// show the highlight of the carrot
					buttons[10].SetActive(false);											// hide NextButton
					potholes_array[1].transform.GetChild(1).gameObject.SetActive(false);	// hide pothole 2 highlight
				}
			}
			else if(tutorialText.text == "Tap a Carrot to see an X mark, the Carrot's range, and element."){
				if(list_hero.plantedHeroes[0].transform.GetChild(2).renderer.enabled == true){			// true if na tap na yung hero
					nextMessage();
					showTutorialStuffs();
					buttons[10].gameObject.SetActive(false);						  // hide next button
					buttons[0].transform.GetChild(1).gameObject.SetActive(false);     // disable the highlight of the carrot
				}
				else if(list_hero.plantedHeroes[1].transform.GetChild(2).renderer.enabled == true){			// true if na tap na ng user ng hero
					nextMessage();
					showTutorialStuffs();
					buttons[0].transform.GetChild(1).gameObject.SetActive(false);     // disable the highlight of the carrot
					buttons[10].gameObject.SetActive(false);						  // hide next button
				}
			}
			else if(tutorialText.text == "Remove a hero by tapping the X mark."){
				// just check if one of the 2 potholes got their hero removed
				if(potholes_array[0].GetComponent<PotholeManager>().hero == null){			// true if nakapag remove na ang user ng hero
					nextMessage();
					showTutorialStuffs();
					buttons[0].transform.GetChild(1).gameObject.SetActive(false);     // disable the highlight of the carrot
					buttons[0].GetComponent<Button>().interactable = false;			// para di muna makapag tanim yung user
					buttons[0].GetComponent<DragManager>().enabled = false;			// para di muna makapag tanim yung user
				}
				else if(potholes_array[1].GetComponent<PotholeManager>().hero == null){
					nextMessage();
					showTutorialStuffs();
					buttons[0].transform.GetChild(1).gameObject.SetActive(false);     // disable the highlight of the carrot
					buttons[0].GetComponent<Button>().interactable = false;			// para di muna makapag tanim yung user
					buttons[0].GetComponent<DragManager>().enabled = false;			// para di muna makapag tanim yung user
				}
			}
			else if(tutorialText.text == "I added more potholes. Plant Carrots as long as you have enough water and" && !activeNaPotholes){
				// loop through the pothole array and show all hidden potholes. 
				for (int i = 0; i < list_potholes.potholesList.Length; i++){
					if(list_potholes.potholesList[i].activeSelf && list_potholes.potholesList[i].renderer.enabled == true){
						// do nothing. this pothole is already active
					}
					else{
						list_potholes.potholesList[i].renderer.enabled = true;	// show hidden pothole
						BoxCollider2D[] tempCollider = list_potholes.potholesList[i].GetComponents<BoxCollider2D>();
						tempCollider[0].enabled = false;	// disable the collider para di muna makapag tanim yung user
						tempCollider[1].enabled = false;

					}
				}
				activeNaPotholes = true;
			}
			// wait  for king guava to go to the right side before showing the next message
			else if(anim_kingGuava.GetCurrentAnimatorStateInfo(0).IsName("king guava on right side") && 
			       tutorialText.text == "I added more potholes. Plant Carrots as long as you have enough water and"){
				nextMessage();
				tutorialText.transform.parent.transform.localScale = new Vector3(-1f, 1f, 1f);	  // set the localScale to 1 para di bumaliktad yung text
				tutorialText.gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);	  // set the localScale to 1 para di bumaliktad yung caption cloud
				tutorialText.transform.parent.gameObject.SetActive(true);	      			  // show the tutorial text
				Debug.Log("hoy magpakita ka");
				tutorialText.transform.parent.gameObject.GetComponent<Image>().enabled = true;	      // show the caption cloud image
				tutorialText.GetComponent<Text>().enabled = true;	     							  // show the tutorial text object
				buttons[1].SetActive(true);							// show the startWave button
				buttons[1].GetComponent<Button>().enabled = false;	// dont let the user interact with the button. di pa tapos mag salita si king guava
				buttons[10].SetActive(true);						// show NextButton	

			}
			else if(tutorialText.text == "GOOD LUCK!"){
				if(buttons[14].activeSelf == true){
					buttons[14].transform.GetChild(1).gameObject.SetActive(false);	// hide the next wave button highlight gameObject
					if(buttons[14].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("next_wave_idle") ){
						nextMessage();
						showTutorialStuffs();
						buttons[0].transform.GetChild(1).gameObject.SetActive(false);	// hide carrot circle highlight gameObject
						buttons[0].GetComponent<Button>().interactable = false;			// para di muna makapag tanim yung user
						buttons[0].GetComponent<DragManager>().enabled = false;			// para di muna makapag tanim yung user
						Time.timeScale = 0f;
							// enable dragging
						if(buttons[0].GetComponent<DragManager>().heroPreview != null){
							Destroy(buttons[0].GetComponent<DragManager>().heroPreview.gameObject);
						}
					}
				}
			}
	
		}
		else if(shopTutorial){
			 if(PlayerPrefs.GetInt("ShopTutorial") == 1 ){		// shop tutorial starts
				anim_kingGuava.SetBool("shop_moveRight", true);
				
				// shop tutorial has diff set of buttons than gameplay tutorial WAG MALILITO :d
				buttons[4].SetActive(true);														  // show caption cloud
				
				//				buttons[2].SetActive(true);			// achievements
				//				buttons[3].SetActive(true);			 // library
				//				buttons[5].SetActive(true);			// back button
				
				tutorialText.transform.parent.transform.localScale = new Vector3(-1f, 1f, 1f);	  // set the localScale to 1 para di bumaliktad yung text
				tutorialText.gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);	  	  // set the localScale to 1 para di bumaliktad yung caption cloud
 			}

		}
	}

	public void tutorialNext(){
		// if the tutorial is still on going
		if(tutorial){
			if(tutorialText.text ==  "The insects have arrived! Defend our place!"){
				nextMessage();
				buttons[0].SetActive(true);										// show the carrot
				buttons[0].GetComponent<Button>().interactable = false;			// so the user cant click the button
				buttons[0].transform.GetChild(1).gameObject.SetActive(false);	// hide the highlight
				// SHOW THE WATER BAR
				buttons[5].SetActive(true);
				GameObject waterText = GameObject.Find("WaterBarText");
				buttons[11].GetComponent<PotholeManager>().waterText = waterText;	// assign water to 1st pothole
			}
			else if(tutorialText.text ==  "I will give you my Veggie hero, the Carrot."){
				nextMessage();
			}
			else if(tutorialText.text ==  "As shown below the Carrot's image, this hero costs 80 water."){
				nextMessage();
				showTutorialStuffs();
			}
			else if(tutorialText.text ==  "Every hero removed gives you a water refund."){
				nextMessage();

			}
			else if(tutorialText.text == "I added more potholes. Plant Carrots as long as you have enough water and" && activeNaPotholes){
				anim_kingGuava.SetBool("moveRight", true);
//				anim_captionCloud.SetBool("flip",true);
				tutorialText.transform.parent.gameObject.GetComponent<Image>().enabled = false;	      // hide the caption cloud image
				tutorialText.GetComponent<Text>().enabled = false;	     							  // hide the tutorial text object
				buttons[10].SetActive(false);					  								      // hide NextButton	
				
			}
			else if(tutorialText.text == "Click on the Start wave button to start fighting! But before that.."){
				nextMessage();
				buttons[1].transform.GetChild(0).gameObject.SetActive(false);							// hide the highlight of startWave button
				buttons[8].SetActive(true);

			}
			else if(tutorialText.text ==  "The Next Wave indicator shows the element of the approaching enemies"){
				nextMessage();
			}
			else if(tutorialText.text == "and if they are a walking or flying type of insect."){
				nextMessage();
			}
			else if(tutorialText.text == "Here, you can see that the insects approaching are"){
				nextMessage();
			}
			else if(tutorialText.text == "walking insects possessing Air element."){
				nextMessage();
			}
			else if(tutorialText.text == "Here is a list of the precedence of the elements" && buttons[13].activeSelf == false){	// precedence not yet shown
				// SHOW PANEL CONTAINING PRECEDENCE OF ELEMENTS.
				buttons[13].SetActive(true);
			}
			else if(buttons[13].activeSelf == true){
				nextMessage();
				// HIDE PANEL CONTAINING PRECEDENCE OF ELEMENTS.
				buttons[13].SetActive(false);
			}
			else if(tutorialText.text == "When a hero attacks an element stronger than him. The damage is lesser, and vice versa."){
				nextMessage();

			}
			else if(tutorialText.text == "If an element is attacked by the same element the damage is normal."){
				nextMessage();
				
			}
			else if(tutorialText.text == "As you go on, you'll acquire hero with different elements. Use them well!"){
				nextMessage();
			}
			else if(tutorialText.text == "GOOD LUCK!"){
				hideTutorialStuffs();
				buttons[8].transform.GetChild(1).gameObject.SetActive(false);
				for (int i = 0; i < list_potholes.potholesList.Length; i++){
					BoxCollider2D[] tempCollider = list_potholes.potholesList[i].GetComponents<BoxCollider2D>();	// enable the colliders of the pothole so the user can plant on them
					if(tempCollider[0].enabled == false && tempCollider[1].enabled == false){
						tempCollider[0].enabled = true;													// disable the collider para di muna makapag tanim yung user
						tempCollider[1].enabled = true;
						GameObject waterText = GameObject.Find("WaterBarText");
						list_potholes.potholesList[i].GetComponent<PotholeManager>().waterText = waterText;			// assign water to pothole
					}
				}
				buttons[1].GetComponent<Button>().enabled = true;

				// SHOW ALL OTHER NECESSARY GAME PANELS: moneybar, etc. except pause. i se set later on
				buttons[4].gameObject.SetActive(true);
				buttons[5].gameObject.SetActive(true);
				buttons[6].gameObject.SetActive(true);
				buttons[7].gameObject.SetActive(true);
				buttons[3].gameObject.SetActive(true);

				buttons[0].GetComponent<Button>().interactable = true;			// makapag tanim yung user
				buttons[0].GetComponent<DragManager>().enabled = true;			// makapag tanim yung user

			}
			else if(tutorialText.text == "Another wave of enemy is coming!"){
				nextMessage ();	
				buttons[14].transform.GetChild(1).gameObject.SetActive(true);	// show next wave button highlight
				buttons[14].transform.GetChild(1).GetComponent<Image>().enabled = true;	// show next wave button highlight
				
			}
			else if(tutorialText.text == "You may tap the Next Wave Button if you want to fight them right away"){
				nextMessage();
				buttons[1].transform.GetChild(0).gameObject.SetActive(false);
				buttons[14].transform.GetChild(1).gameObject.SetActive(false);	// hide next wave button highlight
				buttons[8].transform.GetChild(1).gameObject.SetActive(true);	// show highlight of next Wave indicator
				buttons[10].GetComponentInChildren<Text>().text = "Continue";		// baguhin yung text ng tutorial_nextbutton, para maayos. mahalay naman kung next yung nakalagay.
			}
			else if(tutorialText.text == "Notice that the Next Wave indicator changed. Water element is next"){
				hideTutorialStuffs();
				buttons[0].GetComponent<Button>().interactable = true;			// para makapag tanim na  yung user
				buttons[0].GetComponent<DragManager>().enabled = true;			// para makapag tanim na yung user
				buttons[8].transform.GetChild(1).gameObject.SetActive(false);	// hide highlight of next Wave indicator
				pauseMenuManagerScript.resumeTheScene();						// resume the gameplay

				// show the pause button. tapos na yung tutorial. pwede na mag restart yung user :D
				buttons[2].gameObject.SetActive(true);
				disableTutorial();

				// for the shop tutorial
				PlayerPrefs.SetInt("ShopTutorial", 1);
				nextMessage();
			}

//			else if(tutorialText.text == ""){
//				nextMessage();
//			buttons[0].GetComponent<Button>().interactable = true;			// para makapag tanim na  yung user
//			buttons[0].GetComponent<DragManager>().enabled = true;			// para makapag tanim na yung user
//			}
//			else if(tutorialText.text == ""){
//				nextMessage();
//			}

//
		}
		else if(shopTutorial){

			if(tutorialText.text == "This is the Map, you can scroll sideways to navigate."){
				Debug.Log("i am called");
				nextMessage();
				buttons[1].SetActive(true);			// show the shop button
				buttons[1].GetComponent<Button>().enabled = false;
 			}
			else if(tutorialText.text == "Upgrade and buy heroes at the shop!"){
				nextMessage();
			}
			else if(tutorialText.text == "You can also buy certain boosters for your battle."){
				nextMessage();
				buttons[2].SetActive(true);			// show the achievement button
				buttons[2].GetComponent<Button>().enabled = false;			 // disable clicking achievements
				
			}

				//				buttons[2].SetActive(true);			// achievements
				//				buttons[5].SetActive(true);			// back button
			else if(tutorialText.text == "Claim rewards in the 'Achievements'."){
				nextMessage();
				buttons[3].SetActive(true);			 // library
				buttons[3].GetComponent<Button>().enabled = false;			 // disable clicking library
				buttons[0].transform.GetChild(0).GetComponent<Text>().text = "Okay";
				
			}	
			else if(tutorialText.text == "Browse the 'Library' for references about enemies, etc."){
				//nextMessage();
				//hide everything shop tutorial finished
				PlayerPrefs.SetInt("ShopTutorial", 0);
				buttons[6].SetActive(true);			 // stage 1
				buttons[3].SetActive(true);			 // library

				anim_kingGuava.gameObject.SetActive(false);					 // hide king guava
				buttons[4].SetActive(false);								 // hide caption cloud
				buttons[1].GetComponent<Button>().enabled = true;			 // enable clicking shop
				buttons[2].GetComponent<Button>().enabled = true;			 // enable clicking achievements
				buttons[3].GetComponent<Button>().enabled = true;			 // enable clicking library
				buttons[6].GetComponent<Button>().enabled = true;			 // enable clicking stage 1
				buttons[0].SetActive(false);			 					 // hide tutorial next buttton
				buttons[5].SetActive(true);			 						 // back
			}	

		}

	}
	
	void hideTutorialStuffs(){
		buttons[0].transform.GetChild(1).gameObject.SetActive(false);     // disable the highlight of the carrot
		anim_kingGuava.gameObject.GetComponent<Image>().enabled = false;  // hide king guava 
		tutorialText.transform.parent.gameObject.SetActive(false);	      // hide the tutorial text
		buttons[10].SetActive(false);
		
	}
		
	void showTutorialStuffs(){
		anim_kingGuava.gameObject.GetComponent<Image>().enabled = true;	 	 // show king guava 
		buttons[0].transform.GetChild(1).gameObject.SetActive(true);     	 // show the highlight of the carrot
		tutorialText.transform.parent.gameObject.SetActive(true);	    	 // show the tutorial text
		buttons[10].SetActive(true);
	}
	
	void nextMessage(){
		Debug.Log ("dialog[" + counter + "]: " + tutorial_dialog[counter]);
		tutorialText.text = tutorial_dialog[counter];
		counter++;
	}

	// this function is called when the invisible button is clicked
	public void showStuffsBack(){	// this is to revert back to the original
		if(tutorialText.text == "Tap on the Carrot then tap on the pothole."){
			showTutorialStuffs();
			potholes_array[0].transform.GetChild(1).gameObject.SetActive(false);	// hide pothole 1 highlight		
			buttons[10].SetActive(false);										// hide NextButton	
		}
	}
	public void disableClickToPlant(){
		if(tutorialText.text == "Here's another pothole, now DRAG the Carrot hero to plant. Try it!"){
			Debug.Log("You shouldnt be clickin' start dragging!");
			gameManager.currentSelectedHero = null;
		}
	}

	// called when the resume & back to map button is pressed.
	public void disableTutorial(){
		PlayerPrefs.SetInt("Tutorial", 0);
		PlayerPrefs.SetInt("Tutorial_another", 2);		// basta gagamitin mo to para di mag true yung tutorial na pang gameplay na dapat for shop
		
	}
}