using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;	


public class TutorialManager1 : MonoBehaviour {
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
		counter = 0;
		tutorialText.text = tutorial_dialog[counter];	// display the first tutorial text from the array tutorial_dialog
		foreach(GameObject button in buttons){
			button.SetActive(false);	// hide first the buttons
		}
		counter++;
	}

	void Update(){
		if(tutorial){
			if(anim_kingGuava.GetCurrentAnimatorStateInfo(0).IsName("king guava idle")
			   && tutorialText.text != "Click on the carrot then click any pothole."){
				tutorialText.transform.parent.gameObject.SetActive(true);	// show the tutorial text;
				buttons[10].SetActive(true);
			}

		}
	}

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

}