﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using UnityEngine.UI;

public class DialogManager_ending : MonoBehaviour {

	public string sceneToLoad;

	public bool secondStoryline;	// check in the inspector kung the story to play is on 
	public bool thirdStoryline;	
	public bool endingStoryline;
	public string[] dialogs;

	public Text story_text;
	public Canvas canvas;

	private FadeCanvas fadeCanvas;
	private fade_panelDialog fade_panel;
	
	public CanvasGroup story_1;
	public CanvasGroup story_2;
	public CanvasGroup story_3;
	public CanvasGroup story_4;
	public CanvasGroup story_5;
	public CanvasGroup story_6;

	public CanvasGroup panel_dialog;
	public int counter = 0;	

	void Awake(){
		if(secondStoryline){
			dialogs[0] = "\"Is that the queen? Hurry! We must rescue her!\" King Guava proclaimed.";
			dialogs[1] = "\"Wait... Something's wrong with her.\"";
			dialogs[2] = "\"Grrr...\"";
			dialogs[3] = "\"My insects! ATTACK THEM!\" Queen Bitter Gourd yelled!";
			dialogs[4] = "\"What? Wait... We have no other choice! We have to fight her!\" King Guava exclaimed.";
			story_text.text = dialogs[0];
		}

		else if(thirdStoryline){	// natalo na si queen
			dialogs[0] = "Because of the damage the queen has taken, she fell hard on the ground.";
			dialogs[1] = "King Guava went near to her and said \"My Queen! What happened to you?\"";
			dialogs[2] = "\"I'm sorry... I was blinded by my desire to give you children. I hope you can forgiv...\"";
			dialogs[3] = "Suddenly, the queen was zapped by lightning! The King was shocked";
			dialogs[4] = "to see his wife die right in front of his eyes";
			dialogs[5] = "\"HAHAHA! You're too weak. I'll get Fruity-Veggie World myself.\"";
			dialogs[6] = "It's the Insect Queen! She killed Queen Bitter Gourd!";
			dialogs[7] = "\"This is too much! I can't let you do this any longer!\" King Guava opposed.";
			dialogs[8] = "\"If you can stop me.\" The Insect Queen mocked.";
			dialogs[9] = "\"I'll wait at the third place of my chamber! Prove me your worth King Guava!\"";
			dialogs[10] = "After that, The Insect Queen vanished. Leaving King Guava with a challenge";
			dialogs[11] = "to avenge his fallen wife, Queen Bitter Gourd, whose now lying cold on the ground.";
			story_text.text = dialogs[0];
		}
		else if(endingStoryline){

		}
	}

	// Use this for initialization
	void Start () {

		fadeCanvas = gameObject.GetComponent<FadeCanvas>();
		fade_panel = (fade_panelDialog) GameObject.FindObjectOfType(typeof(fade_panelDialog));

		// print the first line in the dialog, counter should be equal to 0
		counter++;	// then iterate the counter variable to proceed to the next line
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void nextLine(){

		if(secondStoryline){
			if(story_text.text == "\"Is that the queen? Hurry! We must rescue her!\" King Guava proclaimed."){
				panel_dialog.GetComponentInChildren<Button>().interactable = false;

				story_text.text = dialogs[counter];
				counter++;

			}
			else if(story_text.text == "\"Wait... Something's wrong with her.\""){
				fade_panel.hidePanel(panel_dialog);
				fadeCanvas.fadeOutPanel(story_1);	// hide the panel story_1
				fadeCanvas.fadeInPanel(story_2);	// show the panel story_2
				fade_panel.fadeInPanel(panel_dialog);

				story_text.text = dialogs[counter];
				counter++;

			}
			else if(story_text.text == "\"Grrr...\""){

				story_text.text = dialogs[counter];
				counter++;

			}
			else if(story_text.text == "\"My insects! ATTACK THEM!\" Queen Bitter Gourd yelled!"){
				story_text.text = dialogs[counter];
				counter++;

			}
			else if(story_text.text == "\"What? Wait... We have no other choice! We have to fight her!\" King Guava exclaimed."){
				story_2.GetComponent<CanvasGroup>().alpha = 0;		// hide current scenario
				panel_dialog.alpha = 0;								// hide panel
				LoadingScreen1 loadingScreen = (LoadingScreen1) GameObject.FindObjectOfType(typeof(LoadingScreen1));
				loadingScreen.LoadScene(sceneToLoad);

			}

		} 
		else if(thirdStoryline){
			if(story_text.text == "Because of the damage the queen has taken, she fell hard on the ground."){	
				fade_panel.hidePanel(panel_dialog);
				fadeCanvas.fadeOutPanel(story_1);	// hide the panel story_1
				fadeCanvas.fadeInPanel(story_2);	// show the panel story_2
				fade_panel.fadeInPanel(panel_dialog);
				
				story_text.text = dialogs[counter];
				counter++;
			}
			else if(story_text.text == "King Guava went near to her and said \"My Queen! What happened to you?\""){	

				story_text.text = dialogs[counter];
				counter++;	
			}
			else if(story_text.text == "\"I'm sorry... I was blinded by my desire to give you children. I hope you can forgiv...\""){	
				fade_panel.hidePanel(panel_dialog);
				fadeCanvas.fadeOutPanel(story_2);	// hide the panel story_1
				fadeCanvas.fadeInPanel(story_3);	// show the panel story_2
				fade_panel.fadeInPanel(panel_dialog);

				story_text.text = dialogs[counter];
				counter++;	
			}

			else if(story_text.text == "Suddenly, the queen was zapped by lightning! The King was shocked"){	

				story_text.text = dialogs[counter];
				counter++;	
			}
			else if(story_text.text == "to see his wife die right in front of his eyes"){	
				fade_panel.hidePanel(panel_dialog);
				fadeCanvas.fadeOutPanel(story_3);	// hide the panel story_1
				fadeCanvas.fadeInPanel(story_4);	// show the panel story_2
				fade_panel.fadeInPanel(panel_dialog);

				story_text.text = dialogs[counter];
				counter++;	
			}
			else if(story_text.text == "\"HAHAHA! You're too weak. I'll get Fruity-Veggie World myself.\""){	

				story_text.text = dialogs[counter];
				counter++;	
			}
			else if(story_text.text == "It's the Insect Queen! She killed Queen Bitter Gourd!"){	
				fade_panel.hidePanel(panel_dialog);
				fadeCanvas.fadeOutPanel(story_4);	// hide the panel story_1
				fadeCanvas.fadeInPanel(story_5);	// show the panel story_2
				fade_panel.fadeInPanel(panel_dialog);

				story_text.text = dialogs[counter];
				counter++;	
			}
			else if(story_text.text == "\"This is too much! I can't let you do this any longer!\" King Guava opposed."){	

				story_text.text = dialogs[counter];
				counter++;	
			}
			else if(story_text.text == "\"If you can stop me.\" The Insect Queen mocked."){	

				story_text.text = dialogs[counter];
				counter++;	
			}
			else if(story_text.text == "\"I'll wait at the third place of my chamber! Prove me your worth King Guava!\""){	
				fade_panel.hidePanel(panel_dialog);
				fadeCanvas.fadeOutPanel(story_5);	// hide the panel story_1
				fadeCanvas.fadeInPanel(story_6);	// show the panel story_2
				fade_panel.fadeInPanel(panel_dialog);

				story_text.text = dialogs[counter];
				counter++;	
			}
			else if(story_text.text == "After that, The Insect Queen vanished. Leaving King Guava with a challenge"){	
				
				story_text.text = dialogs[counter];
				counter++;	
			}
			else if(story_text.text == "to avenge his fallen wife, Queen Bitter Gourd, whose now lying cold on the ground."){	
				story_6.GetComponent<CanvasGroup>().alpha = 0;		// hide current scenario
				panel_dialog.alpha = 0;								// hide panel
				LoadingScreen1 loadingScreen = (LoadingScreen1) GameObject.FindObjectOfType(typeof(LoadingScreen1));
				loadingScreen.LoadScene(sceneToLoad);


			}

		}
//
//
//		if(counter == 3){
//			panel_dialog.GetComponentInChildren<Button>().interactable = false;
//			fade_panel.hidePanel(panel_dialog);
//			fadeCanvas.fadeOutPanel(story_1);	// hide the panel story_1
//			fadeCanvas.fadeInPanel(story_2);	// show the panel story_2
//			fade_panel.fadeInPanel(panel_dialog);
//
//		}
//
//		// Line #6: because of this, she ran away from the King and left the palace.
//		else if(counter == 5){
//			panel_dialog.GetComponentInChildren<Button>().interactable = false;
//			fade_panel.hidePanel(panel_dialog);
//			fadeCanvas.fadeOutPanel(story_2);
//			fadeCanvas.fadeInPanel(story_3);
//			fade_panel.fadeInPanel(panel_dialog);
//		}
//
//		else if(counter == 6){
//			fade_panel.hidePanel(panel_dialog);
//			fadeCanvas.fadeOutPanel(story_3);
//			fade_panel.fadeInPanel(panel_dialog);
//			fadeCanvas.fadeInPanel(story_4);
//		}
//
//		else if(counter == 8){
//			fade_panel.hidePanel(panel_dialog);
//			fadeCanvas.fadeOutPanel(story_4);
//			fade_panel.fadeInPanel(panel_dialog);
//			fadeCanvas.fadeInPanel(story_5);
//		}
//
//		else if(counter == 9){
//			fade_panel.hidePanel(panel_dialog);
//			fadeCanvas.fadeOutPanel(story_5);
//			fade_panel.fadeInPanel(panel_dialog);
//			fadeCanvas.fadeInPanel(story_6);
//		}
//
//		else if(counter  == 10){
////			Application.LoadLevel ("Level_1-1");
//			story_6.GetComponent<CanvasGroup>().alpha = 0;		// hide current scenario
//			panel_dialog.alpha = 0;								// hide panel
//			LoadingScreen1 loadingScreen = (LoadingScreen1) GameObject.FindObjectOfType(typeof(LoadingScreen1));
//			loadingScreen.LoadScene(sceneToLoad);
//			PlayerPrefs.SetInt("Tutorial", 1); 						// this will be used later para malaman na kailangan ng user mag tutorial pag dating ng level 1
//
//		}
//
//		story_text.text = dialogs[counter];
//		counter++;
	}

	IEnumerator wait(){
		yield return new WaitForSeconds(2);
	}
}
