using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class shopTutorialManager : MonoBehaviour {
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

		if(PlayerPrefs.GetInt("ShopTutorial") == 1){		// shop tutorial enabled
			buttons[7].SetActive(true);						// tutorial parent gameObject
			shopTutorial = true;
			tutorial_dialog.Add("This is the Map, you can scroll sideways to navigate.");	
			tutorial_dialog.Add("Upgrade and buy heroes at the shop!");	
			
			tutorial_dialog.Add("You can also buy certain boosters for your battle.");	
			
		//	tutorial_dialog.Add("Claim rewards in the 'Achievements'.");	
			tutorial_dialog.Add("Browse the 'Library' for references about enemies, etc.");	
			
			buttons[1].SetActive(false);
//			buttons[2].SetActive(false);
			buttons[3].SetActive(false);
			buttons[5].SetActive(false);
			buttons[6].GetComponent<Button>().enabled = false;	// stage 1 button
			//		tutorial_dialog.Add("");	
			
			counter = 0;
			counter++;
			
			tutorialText.text = tutorial_dialog[0];
		}

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(shopTutorial){
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
		if(shopTutorial){
			
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
				PlayerPrefs.SetInt("playerReturns_toLvl_1" , 1);			// so when the user returns to level 1, di na mag tutorial 
				
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
	}
	
	public void restartStageFromTut(){
		PlayerPrefs.SetInt("playerReturns_toLvl_1", 1);		// basta gagamitin mo to para di mag true yung tutorial na pang gameplay na dapat for shop
	}

}
