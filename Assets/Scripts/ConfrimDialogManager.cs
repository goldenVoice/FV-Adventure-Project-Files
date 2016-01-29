using UnityEngine;
using System.Collections;

public class ConfrimDialogManager : MonoBehaviour {
 	
 	GameObject canvas_pauseMenu;
	GameObject canvas_restartDialog; 
  GameObject canvas_MapDialog;

  public GameObject NoButton_backToMap;		// kailangan to, para i set pabalik yung noButton_backToMap into true; 
	
	// Use this for initialization
	void Start () {
			// look for the canvas with the pause menu
		canvas_pauseMenu = GameObject.Find("Canvas_PauseMenu");

		canvas_restartDialog = GameObject.Find("Canvas_RestartDialog");
		canvas_restartDialog.GetComponent<Canvas>().enabled = false;	// hide the confirmation dialog

		canvas_MapDialog = GameObject.Find("Canvas_BackToMapDialog");
		canvas_MapDialog.GetComponent<Canvas>().enabled = false;	// hide the confirmation dialog
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RestartCurrentLevel(string sceneName){
		// this method is called when the user clicks yes
		if(Time.timeScale == 0.0f){
		Time.timeScale = 1.0f;		// WAG NA WAG TONG KAKALIMUTAN, KASE DUN SA PauseMenumanager
		}							// nung pinause mo, naka 0.0f yung timeScale which is paused. 
									// tapos pag nag load ka ng scene, naka remain na ganon pa ren yung time scale,
									// kaya set it back to 1.0f yung timeScale kung aales ka sa current scene. NOTE: ANTAGAL KONG DINEBUG TO T.T Glad its finished
		Application.LoadLevel(sceneName);
	}

	public void BackToMap(string sceneName){
		if(Time.timeScale == 0.0f){		// check kung naka pause, pag di mo to ginawa, what if naka 2x gameplay speed sya tapos, na call tong 
		Time.timeScale = 1.0f;			// tong function na to, edi mag no normal ule yung time? kaya naka if to,
		}
		Application.LoadLevel(sceneName);	
	}

	public void closeDialog(){
		canvas_restartDialog.GetComponent<Canvas>().enabled = false;	// hide the restart confirmation dialog
		canvas_MapDialog.GetComponent<Canvas>().enabled = false;	// hide the map confirmation dialog
		canvas_pauseMenu.GetComponent<Canvas>().enabled = true;	// show the pause menu again
		
    GameObject NoButton_restart = GameObject.Find("NoButton_restart");

    NoButton_backToMap.SetActive(true);
    NoButton_restart.SetActive(true);

	}

	public void RestartScene() {
		Debug.Log("Current level: " + Application.loadedLevelName);

		// algorithm, kunin muna current level ngayon
		// ex: curlevel  = "Level 2-1"
		string curLevel = Application.loadedLevelName;

		// kinuha kung anong stage na tayo (example stage 2)
		// kaya nag substring 
		// Level '2'-1
		// yung 2 ang kelangan, eh since pang 6 sya doon sa string na curLevel, kaya ganyan ang format, tapos 1 lang ang length, kase yung isang character na yon lang ang kelangan naten
		string stageNumber = curLevel.Substring (6, 1) ;

//		Debug.Log (stageNumber);
//		Debug.Log (GameObject.Find ("HeroSelectPanel").gameObject);
//		DontDestroyOnLoad (GameObject.Find ("HeroSelectPanel").gameObject);

		// now we load the "Stage 2 - LvlSelect" to apply the restart of the game, by letting them choose the heroes again
		Application.LoadLevel("Stage " + stageNumber + " - LvlSelect") ;
		
		//DontDestroyOnLoad (GameObject.Find ("HeroSelectPanel").gameObject);
	}
}
