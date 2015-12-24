using UnityEngine;
using System.Collections;

public class pauseMenuManager : MonoBehaviour {
 
  public bool paused;
  GameObject canvas_pauseMenu;
  GameObject canvas_restartDialog; 
  GameObject canvas_MapDialog;
  public pauseScene pauseScene;

	// Use this for initialization
	void Start () {
		paused = false;
		// look for the canvas with the pause menu
		canvas_pauseMenu = GameObject.Find("Canvas_PauseMenu");
		canvas_pauseMenu.GetComponent<Canvas>().enabled = false;	// hide the pause menu

		canvas_restartDialog = GameObject.Find("Canvas_RestartDialog");
		canvas_restartDialog.GetComponent<Canvas>().enabled = false;	// hide the confirmation dialog

		canvas_MapDialog = GameObject.Find("Canvas_BackToMapDialog");
		canvas_MapDialog.GetComponent<Canvas>().enabled = false;	// hide the confirmation dialog

		// get the pauseScene script
 		pauseScene = (pauseScene)FindObjectOfType(typeof(pauseScene));

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// note: tinanggal ko yung mga if statements, di kase nag se save yung boolean value ng paused, kase tong script na to
	// naka attach sa mag kaibang canvas, Canvas_pauseMenu and Canvas (pinaka main).

	public void pauseTheScene(){
	//	if (paused == false){
			Time.timeScale = 0.0f;		// when set to 0.0f, parang yung Update function ng lahat ng nasa scene, naka tigil, IN SHORT PAUSE.
			canvas_pauseMenu.GetComponent<Canvas>().enabled = true;  // show the pause menu
			paused = true;
	//	}
	}

	public void resumeTheScene(){
		
		if(pauseScene.ifFastFoward == true){
			Debug.Log("HOYYYYYYYY");
			Time.timeScale = 2.0f;		// set it back to 2.0f, which is 2x faster
			canvas_pauseMenu.GetComponent<Canvas>().enabled = false;	// hide the pause menu
			paused = false;
		}
		// else, kung pinindot yung pause, while in a regular speed
		else{
			Time.timeScale = 1.0f;
			canvas_pauseMenu.GetComponent<Canvas>().enabled = false;	// hide the pause menu
			paused = false;
		}
		
		canvas_pauseMenu.GetComponent<Canvas>().enabled = false;	// hide the pause menu
		paused = false;
	}

	public void RestartDialog(){
		canvas_restartDialog.GetComponent<Canvas>().enabled = true;	// show the confirmation dialog
		canvas_pauseMenu.GetComponent<Canvas>().enabled = false;	// hide the pause menu
		
		// 2 kaseng button meron, eh restart lang to, kaya kailangan i disable yung NoButton_backToMap 
		GameObject NoButton_backToMap = GameObject.Find("NoButton_backToMap");
      	GameObject NoButton_restart = GameObject.Find("NoButton_restart");

      	NoButton_backToMap.SetActive(false);
      	NoButton_restart.SetActive(true);

	}

	public void BackToMapDialog(){
		canvas_MapDialog.GetComponent<Canvas>().enabled = true;	// show the confirmation dialog
		canvas_pauseMenu.GetComponent<Canvas>().enabled = false;	// hide the pause menu

  	}

	public void closeDialog(){
		canvas_restartDialog.GetComponent<Canvas>().enabled = false;	// hide the restart confirmation dialog
		canvas_MapDialog.GetComponent<Canvas>().enabled = false;	// hide the map confirmation dialog
		canvas_pauseMenu.GetComponent<Canvas>().enabled = true;	// show the pause menu again

	}

	public void normalizeTime(){
		Time.timeScale = 1f;
	}
}

