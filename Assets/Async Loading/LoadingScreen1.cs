using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoadingScreen1 : MonoBehaviour {
	
	public string levelToLoad;

	public GameObject background;
	public GameObject progressBar_bg;
	public GameObject progressBar;
	public Text text;

	private float origScale;
	private float newProgress;

	private int loadProgress = 0;
	
	private float progress = 0;
	
	AsyncOperation asyncop;
	
	// Use this for initialization
	void Start () {
		//	StartCoroutine(DisplayLoadingScreen(levelToLoad));
		asyncop = Application.LoadLevelAsync (levelToLoad);
		origScale = progressBar.transform.localScale.x;
		background.SetActive(true);
		progressBar_bg.SetActive(true);
		progressBar.SetActive(true);

	//	progressBar.transform.localScale = new Vector3 (0f, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.A)){
			asyncop = 	Application.LoadLevelAsync("Level_1-3");
			asyncop.allowSceneActivation = false;
		}

		else if(Input.GetKeyDown (KeyCode.S)){
			asyncop.allowSceneActivation = true;
		}
		newProgress = asyncop.progress * origScale;
		text.text = "LOADING: " + ((int) (asyncop.progress * 100)) + "%";		// show current percentage, kaya cinast ko sa int para whole num lang
		progressBar.transform.localScale = new Vector3 (newProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

		if(asyncop.progress == 0.9f){
			newProgress = (asyncop.progress + 0.09f) * origScale;		// para 99 percent :D
			text.text = "LOADING: " + Mathf.RoundToInt((( (asyncop.progress + 0.1f) * 100))) + "%";		// show current percentage, kaya cinast ko sa int para whole num lang
			progressBar.transform.localScale = new Vector3 (newProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
		}
		//print (asyncop.progress);
	}
	
	
	//	IEnumerator DisplayLoadingScreen(string level)
	//	{
	//		background.SetActive (true);
	//	//	text.SetActive (true);
	//		progressBar.SetActive (true);
	//		
	//		progressBar.transform.localScale = new Vector3 (loadProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
	//		text.text = "Loading Progress " + loadProgress + "%";
	//		
	//
	////		while (!asyncop.isDone) {
	//	}
	
	public void StartLoading_scene(string sceneName){
		levelToLoad = sceneName;
		this.enabled = true;
	}
}
