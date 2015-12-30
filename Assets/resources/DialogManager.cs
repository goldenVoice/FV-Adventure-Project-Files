using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	public string sceneToLoad;
	public List<string> dialog_lines;
	public TextAsset _xml;

	public Text story_text;

	public Canvas canvas;

	private FadeCanvas fadeCanvas;
	private fade_panelDialog fade_panel;

	Animator anim_fv;
	Animator anim_insect;

	public CanvasGroup story_1;
	public CanvasGroup story_2;
	public CanvasGroup story_3;
	public CanvasGroup story_4;
	public CanvasGroup story_5;
	public CanvasGroup story_6;

	public CanvasGroup panel_dialog;
	public int counter = 0;

	// Use this for initialization
	void Start () {

		fadeCanvas = gameObject.GetComponent<FadeCanvas>();
		fade_panel = (fade_panelDialog) GameObject.FindObjectOfType(typeof(fade_panelDialog));

		_xml = Resources.Load<TextAsset> ("storyline_dialog") as TextAsset; // hanapin yung .xml mula sa path tapos i load
		XmlDocument xmlDoc = new XmlDocument (); 
		xmlDoc.LoadXml(_xml.text);
		XmlNodeList itemsList = xmlDoc.GetElementsByTagName ("Dialog_line"); // array of the level nodes.

		anim_fv = gameObject.transform.GetChild(0).GetChild(1).GetComponent<Animator>();
		anim_insect = gameObject.transform.GetChild(0).GetChild(2).GetComponent<Animator>();

		foreach(XmlNode dialog_line in itemsList){
			// add the dialog lines you got from the xml into a list
			dialog_lines.Add (dialog_line.SelectSingleNode("line").InnerText);
		}

		// print the first line in the dialog, counter should be equal to 0
		story_text.text = dialog_lines[counter];
		counter++;	// then iterate the counter variable to proceed to the next line
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void nextLine(){

		// Line #2: This is Fruity-Veggie world and Insect world 
		if(counter == 1){
			// show the fruity veggie world
			anim_fv.enabled = true;
			anim_insect.enabled = true;
		}

		// Line #4: King Guava had a beautiful wife named Queen Bitter Gourd 
		else if(counter == 3){
			panel_dialog.GetComponentInChildren<Button>().interactable = false;
			fade_panel.hidePanel(panel_dialog);
			fadeCanvas.fadeOutPanel(story_1);	// hide the panel story_1
			fadeCanvas.fadeInPanel(story_2);	// show the panel story_2
			fade_panel.fadeInPanel(panel_dialog);

		}

		// Line #6: because of this, she ran away from the King and left the palace.
		else if(counter == 5){
			fade_panel.hidePanel(panel_dialog);
			fadeCanvas.fadeOutPanel(story_2);
			fade_panel.fadeInPanel(panel_dialog);

			fadeCanvas.fadeInPanel(story_3);
		}

		else if(counter == 6){
			fade_panel.hidePanel(panel_dialog);
			fadeCanvas.fadeOutPanel(story_3);
			fade_panel.fadeInPanel(panel_dialog);
			fadeCanvas.fadeInPanel(story_4);
		}

		else if(counter == 8){
			fade_panel.hidePanel(panel_dialog);
			fadeCanvas.fadeOutPanel(story_4);
			fade_panel.fadeInPanel(panel_dialog);
			fadeCanvas.fadeInPanel(story_5);
		}

		else if(counter == 9){
			fade_panel.hidePanel(panel_dialog);
			fadeCanvas.fadeOutPanel(story_5);
			fade_panel.fadeInPanel(panel_dialog);
			fadeCanvas.fadeInPanel(story_6);
		}

		else if(counter  == 10){
//			Application.LoadLevel ("Level_1-1");
			story_6.GetComponent<CanvasGroup>().alpha = 0;		// hide current scenario
			panel_dialog.alpha = 0;								// hide panel
			LoadingScreen1 loadingScreen = (LoadingScreen1) GameObject.FindObjectOfType(typeof(LoadingScreen1));
			loadingScreen.LoadScene(sceneToLoad);
			PlayerPrefs.SetInt("Tutorial", 1); 						// this will be used later para malaman na kailangan ng user mag tutorial pag dating ng level 1

		}
		story_text.text = dialog_lines[counter];
		counter++;
	}

	IEnumerator wait(){
		yield return new WaitForSeconds(2);
	}
}
