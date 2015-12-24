using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pauseScene : MonoBehaviour {

  public bool paused;
  GameObject canvas_pauseMenu;
  GameObject _2xbutton;
  public bool ifFastFoward;

	// Use this for initialization
	void Start () {
		paused = false;
		// look for the canvas with the pause menu
		canvas_pauseMenu = GameObject.Find("Canvas_PauseMenu");
		canvas_pauseMenu.GetComponent<Canvas>().enabled = false;	// hide the pause menu
		ifFastFoward = false;		// sa start naka false to
		_2xbutton = GameObject.Find("2xSpeedButton");

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

	// public void resumeTheScene(){

	// 	Debug.Log("Time.timeScale (resumeTheScene Script): " + Time.timeScale);

	// 	// kung pinindot ng user yung pause ng naka fastfoward yung time, 
	// 	if(ifFastFoward == true){
	// 		Time.timeScale = 2.0f;		// set it back to 2.0f, which is 2x faster
	// 		canvas_pauseMenu.GetComponent<Canvas>().enabled = false;	// hide the pause menu
	// 		paused = false;
	// 	}
	// 	// else, kung pinindot, in a regular speed
	// 	else{
	// 		Time.timeScale = 1.0f;
	// 		canvas_pauseMenu.GetComponent<Canvas>().enabled = false;	// hide the pause menu
	// 		paused = false;
	// 	}
	// }

	public void fastForward(){
		
//		Debug.Log("Time.timeScale (fastforward script): " + Time.timeScale);

		// fast forward the time,
		if(Time.timeScale == 1.0f || Time.timeScale == 0.0f){		// if normal time, or nakapause
			_2xbutton.GetComponent<Image>().color = Color.grey;
			Time.timeScale = 2.0f;	// 2x faster

			ifFastFoward = true;

		}
		//normalize the time
		else if(Time.timeScale == 2.0f){	// if naka fast fowrard
			Time.timeScale = 1.0f;	// normalize
			_2xbutton.GetComponent<Image>().color = Color.white;
			ifFastFoward = false;
		}

	}
}
