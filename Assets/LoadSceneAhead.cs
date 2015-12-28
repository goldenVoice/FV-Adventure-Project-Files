﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadSceneAhead : MonoBehaviour {


	public string levelToLoad;
	
	bool isLoading;
	
	public GameObject background;
	public GameObject progressBar_bg;
	public GameObject progressBar;
	public Text textObj;
	
	private float origScale;
	private float newProgress;
	
	private int loadProgress = 0;
	
	private float progress = 0;

	AsyncOperation asyncop;
	
	// Use this for initialization
	void Start () {

		origScale = progressBar.transform.localScale.x;
		background.SetActive(true);
		progressBar_bg.SetActive(true);
		progressBar.SetActive(true);
		textObj.gameObject.SetActive(true);

		DisplayLoadingScreen(levelToLoad);


	}
	
	// Update is called once per frame
	void Update () {
		if(isLoading){		// if nag lo load na. 
			
			newProgress = asyncop.progress * origScale;
			textObj.text = "LOADING: " + ((int) (asyncop.progress * 100)) + "%";		// show current percentage, kaya cinast ko sa int para whole num lang
			progressBar.transform.localScale = new Vector3 (newProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
			
			if(asyncop.progress == 0.9f){
				newProgress = (asyncop.progress + 0.09f) * origScale;		// para 99 percent :D
				textObj.text = "LOADING: " + Mathf.RoundToInt((( (asyncop.progress + 0.1f) * 100))) + "%";		// show current percentage, kaya cinast ko sa int para whole num lang
				progressBar.transform.localScale = new Vector3 (newProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

				background.SetActive(false);
				progressBar_bg.SetActive(false);
				progressBar.SetActive(false);
				textObj.gameObject.SetActive(false);
			}
		}

	}

	public void DisplayLoadingScreen(string sceneName){
		asyncop = Application.LoadLevelAsync(sceneName);
		asyncop.allowSceneActivation = false;			// wag muna ipakita yung scene
		isLoading = true;
		
	}
	public void ActivateScene(){
		asyncop.allowSceneActivation = true;			// dito palang ipapakita yung scene
	}

}
