using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;	


public class TutorialManager1 : MonoBehaviour {
	[HideInInspector]
	public bool tutorial;

	bool kingGuavaEntered;

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
		tutorial_dialog.Add("Click on the carrot then click any pothole.");	
		tutorial_dialog.Add("Here's another pothole, now drag the hero to plant. Try it!");	
		tutorial_dialog.Add("Click any hero to show its range and element. Try to remove a hero");	
		tutorial_dialog.Add("Every hero removed gives you a water refund.");	
		//		tutorial_dialog.Add();	
		//		tutorial_dialog.Add();	
		
	}
	// Use this for initialization
	void Start () {
		tutorial = true;
		kingGuavaEntered = false;
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
		//		tutorial_dialog.Add();	
			else if(tutorialText.text ==  "I will give you my Veggie hero, the Carrot."){
				colliders_pothole1 = buttons[11].GetComponents<BoxCollider2D>();
				colliders_pothole1[0].enabled = false;		// disable both para di muna makapag tanim yung user :D
				colliders_pothole1[1].enabled = false;		// 2 kase yung collider kaya nilagay ko sa array
			}
			else if(tutorialText.text == "Click on the carrot then click any pothole."){
				buttons[10].SetActive(false);										// hide NextButton
				if(gameManager.currentSelectedHero != null){							// if may hero dito (hindi null) means na click ng user, yung carrot hero
					hideTutorialStuffs();
					potholes_array[0].transform.GetChild(1).gameObject.SetActive(true);	// show pothole highlight
				}
			}

		}
	}

	public void tutorialNext(){
		// if the tutorial is still on going
		if(tutorial){
			if(tutorialText.text ==  "The insects have arrived! Defend our place!"){
				nextMessage();
				buttons[0].SetActive(true);										// show the carrot
				buttons[0].transform.GetChild(1).gameObject.SetActive(false);	// hide the highlight
			}
			else if(tutorialText.text ==  "I will give you my Veggie hero, the Carrot."){
				nextMessage();
			}
			else if(tutorialText.text ==  "As shown below the Carrot's image, this hero costs 80 water."){
				nextMessage();
				showTutorialStuffs();
			}

		}
	}
	
	void hideTutorialStuffs(){
		buttons[0].transform.GetChild(1).gameObject.SetActive(false);     // disable the highlight of the carrot
		anim_kingGuava.gameObject.GetComponent<Image>().enabled = false;  // hide king guava 
		tutorialText.transform.parent.gameObject.SetActive(false);	      // hide the tutorial text
		buttons[10].SetActive(true);
		
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
}