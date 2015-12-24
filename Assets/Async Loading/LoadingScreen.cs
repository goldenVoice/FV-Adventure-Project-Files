using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

	public string levelToLoad;
	
	public GameObject background;
	public GameObject text;
	public GameObject progressBar;
	
	private int loadProgress = 0;

	// Use this for initialization
	void Start () {
		StartCoroutine(DisplayLoadingScreen(levelToLoad));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator DisplayLoadingScreen(string level)
	{
		background.SetActive (true);
		text.SetActive (true);
		progressBar.SetActive (true);
		
		progressBar.transform.localScale = new Vector3 (loadProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
		text.guiText.text = "Loading Progress " + loadProgress + "%";
		
		AsyncOperation async = Application.LoadLevelAsync (level);
		while (!async.isDone) {
			loadProgress = (int)(async.progress * 100);
			text.guiText.text = "Loading Progress " + loadProgress + "%";
			progressBar.transform.localScale = new Vector3 (async.progress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
			yield return null;
		}
	}

	public void StartLoading_scene(string sceneName){
		levelToLoad = sceneName;
		this.enabled = true;
	}
}
