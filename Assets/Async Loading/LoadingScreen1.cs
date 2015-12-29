using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoadingScreen1 : MonoBehaviour {
	
	private string levelToLoad;

	bool isLoading = false;

	public GameObject background;
	public GameObject progressBar_bg;
	public GameObject progressBar;
	public Text textObj;

	private float origScale;
	private float newProgress;

	private int loadProgress = 0;
	
	private float progress = 0;
	
	public AsyncOperation asyncop;
	
	// Use this for initialization
	void Start () {

		isLoading = true;
		asyncop = Application.LoadLevelAsync (levelToLoad);
		
		origScale = progressBar.transform.localScale.x;

		// show the progress bar, dark bg
		background.SetActive(true);
		progressBar_bg.SetActive(true);
		progressBar.SetActive(true);
		textObj.gameObject.SetActive(true);

		// start with the size of the progress bar as 0
		progressBar.transform.localScale = new Vector3 (0f, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
		Debug.Log(isLoading);
		Debug.Log(asyncop);
		
		asyncop.allowSceneActivation = false;		
	}
	
	// Update is called once per frame
	void Update () {
		if(isLoading){		// if nag lo load na. 
			Debug.Log ("LOAAAAAAAAAADING");
			newProgress = asyncop.progress * origScale;		
			textObj.text = "LOADING: " + ((int) (asyncop.progress * 100)) + "%";		// show current percentage, kaya cinast ko sa int para whole num lang
			progressBar.transform.localScale = new Vector3 (newProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

			if(asyncop.progress == 0.9f){
				newProgress = (asyncop.progress + 0.09f) * origScale;		// para 99 percent :D
				textObj.text = "LOADING: " + Mathf.RoundToInt((( (asyncop.progress + 0.1f) * 100))) + "%";		// show current percentage, kaya cinast ko sa int para whole num lang
				progressBar.transform.localScale = new Vector3 (newProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

				asyncop.allowSceneActivation = true;
			}
		}
	}

	public void LoadScene(string levelName){
		levelToLoad = levelName; 	// levelName typed in the inspector
		this.enabled = true;	// enable the script to start loading
		//isLoading = true;

	}

}
