using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;	


public class TutorialManager1 : MonoBehaviour {
	[HideInInspector]
	public bool tutorial;

	bool kingGuavaEntered;
	bool activeNa;	// temporary variable. lalagay ko to sa update para isang bes lang mag true yung conditions na nag a activate ng hidden objects like pothole highlight ganon
	bool activeNaPotholes;		// check if active na yung lahat ng potholes, same lang den sa taas temporary variable
	public Animator anim_kingGuava;
	private int counter;
	public GameObject[] buttons;	// sa start ng tutorial dapat naka disable/ not interactable to. para di makapag laro ang user, at makinig sa tutorial :P
	
	public Text tutorialText;
	
	public List<string> tutorial_dialog;
	
	private GameManagerBehavior gameManager;
	private Potholes_list list_potholes;
	
	private PotholeManager potholeManager;

	public GameObject[]	potholes_array;
	private BoxCollider2D[] colliders_pothole1;

	void Awake(){
		gameManager = (GameManagerBehavior) GameObject.FindObjectOfType(typeof(GameManagerBehavior));
		list_potholes = (Potholes_list) GameObject.FindObjectOfType(typeof(Potholes_list));
		potholeManager = (PotholeManager) GameObject.FindObjectOfType(typeof(PotholeManager));
		
		tutorial_dialog.Add("The insects have arrived! Defend our place!"); 
		tutorial_dialog.Add("I will give you my Veggie hero, the Carrot.");	
		tutorial_dialog.Add("As shown below the Carrot's image, this hero costs 80 water.");	
		tutorial_dialog.Add("Tap on the Carrot then tap on the pothole.");	
		tutorial_dialog.Add("Here's another pothole, now drag the Carrot hero to plant. Try it!");	
		tutorial_dialog.Add("Tap a Carrot to show its range and element. Tap X to remove a Carrot.");	
		tutorial_dialog.Add("Every hero removed gives you a water refund.");	
		tutorial_dialog.Add("I added more potholes. Feel free to plant Carrots as long as you have enough water.");	
		//		tutorial_dialog.Add();	
		
	}
	// Use this for initialization
	void Start () {
		tutorial = true;
		kingGuavaEntered = false;
		activeNa = false;
		activeNaPotholes = false;
		counter = 0;
		tutorialText.text = tutorial_dialog[counter];	// display the first tutorial text from the array tutorial_dialog
		foreach(GameObject button in buttons){
			button.SetActive(false);	// hide first the buttons
		}
		buttons[11].gameObject.SetActive(true);	// show first pothole
		counter++;
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
			else if(tutorialText.text == "Here's another pothole, now drag the Carrot hero to plant. Try it!" && !activeNa){
				showTutorialStuffs();
				buttons[10].SetActive(false);									// hide NextButton
				buttons[0].GetComponent<DragManager>().enabled = true;			// enable dragging
				potholes_array[1].SetActive(true);								// show the 2nd pothole
				colliders_pothole1[0].enabled = false;							// disable both para di muna makapag tanim yung user :D
				colliders_pothole1[1].enabled = false;							// 2 kase yung collider kaya nilagay ko sa array
//				colliders_pothole1 = buttons[12].GetComponents<BoxCollider2D>();// get the 2 colliders of the 2nd pothole
				activeNa = true;												// use this variable para isang bes lang mag execute tong expression na to, showing ng stuffs lang kase to kaya 1 bes lang dapat
			}
			else if(tutorialText.text == "Here's another pothole, now drag the Carrot hero to plant. Try it!"){
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
			else if(tutorialText.text == "Tap a Carrot to show its range and element. Tap X to remove a Carrot." ){
				// just check if one of the 2 potholes got their hero removed
				if(potholes_array[0].GetComponent<PotholeManager>().hero == null){			// true if nakapag remove na ang user ng hero
					nextMessage();
					showTutorialStuffs();
					buttons[0].transform.GetChild(1).gameObject.SetActive(false);     // disable the highlight of the carrot
				}
				else if(potholes_array[1].GetComponent<PotholeManager>().hero == null){
					nextMessage();
					showTutorialStuffs();
					buttons[0].transform.GetChild(1).gameObject.SetActive(false);     // disable the highlight of the carrot
				}
			}
			else if(tutorialText.text == "I added more potholes. Feel free to plant Carrots as long as you have enough water." && !activeNaPotholes){
				// loop through the pothole array and show all hidden potholes. 
				for (int i = 0; i < potholes_array.Length; i++){
					if(potholes_array[i].activeSelf){
						// do nothing. this pothole is already active
					}
					else{
						potholes_array[i].SetActive(true);	// show hidden pothole
						BoxCollider2D[] tempCollider = potholes_array[i].GetComponents<BoxCollider2D>();
						tempCollider[0].enabled = false;	// disable the collider para di muna makapag tanim yung user
						tempCollider[1].enabled = false;
					}
				}
				activeNaPotholes = true;
			}
			else if(tutorialText.text == "I added more potholes. Feel free to plant Carrots as long as you have enough water." && activeNaPotholes){
				Debug.Log ("This should show when all the potholes collider are disabled");

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
		if(tutorialText.text == "Here's another pothole, now drag the Carrot hero to plant. Try it!"){
			Debug.Log("You shouldnt be clickin' start dragging!");
			gameManager.currentSelectedHero = null;
		}
	}
}