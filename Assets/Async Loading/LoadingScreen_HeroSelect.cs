using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadingScreen_HeroSelect : MonoBehaviour {
	
	string levelToLoad;

	bool isLoading = false;

	private GameObject background;
	private GameObject progressBar_bg;
	private GameObject progressBar;
	private GameObject textObj;
	private GameObject loadPanel;

	private float origScale;
	private float newProgress;

	private int loadProgress = 0;
	
	private float progress = 0;
	
	public AsyncOperation asyncop;

	public GameObject selectedHeroPanel;

	void Awake(){
		background = GameObject.Find ("dark BG");
		progressBar_bg = GameObject.Find ("progressBar_BG");
		progressBar = GameObject.Find ("progressBar");
		textObj = GameObject.Find ("LoadText");
		loadPanel = GameObject.Find ("Load panel");
		
//		background.SetActive(false);
//		progressBar_bg.SetActive(false);
//		progressBar.SetActive(false);
//		textObj.SetActive(false);
	
	}
	// Use this for initialization
	void Start () {

		isLoading = true;
		asyncop = Application.LoadLevelAsync (levelToLoad);
//		Debug.Log("Text Object: " + textObj);		
		origScale = progressBar.transform.localScale.x;

		// show the progress bar, dark bg
		background.GetComponent<Image>().enabled = true;
		progressBar_bg.GetComponent<Image>().enabled = true;
		progressBar.GetComponent<Image>().enabled = true;
		textObj.GetComponent<Text>().enabled = true;
		loadPanel.GetComponent<Image>().enabled = true;
		
		// start with the size of the progress bar as 0
		progressBar.transform.localScale = new Vector3 (0f, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
//		Debug.Log(isLoading);
		Debug.Log(asyncop);
		
		asyncop.allowSceneActivation = false;		
	}
	
	// Update is called once per frame
	void Update () {
		if(isLoading){		// if nag lo load na. 
			Debug.Log ("LOAAAAAAAAAADING");
			newProgress = asyncop.progress * origScale;		
			textObj.GetComponent<Text>().text = "LOADING: " + ((int) (asyncop.progress * 100)) + "%";		// show current percentage, kaya cinast ko sa int para whole num lang
			progressBar.transform.localScale = new Vector3 (newProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

			if(asyncop.progress == 0.9f){
				newProgress = (asyncop.progress + 0.09f) * origScale;		// para 99 percent :D
				textObj.GetComponent<Text>().text = "LOADING: " + Mathf.RoundToInt((( (asyncop.progress + 0.1f) * 100))) + "%";		// show current percentage, kaya cinast ko sa int para whole num lang
				progressBar.transform.localScale = new Vector3 (newProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

				asyncop.allowSceneActivation = true;

				// activate all the selected heroes
				for(int i = 0; i < selectedHeroPanel.transform.childCount; i++){
					if(selectedHeroPanel.transform.GetChild(i).childCount != 0 ){
						selectedHeroPanel.transform.GetChild(i).GetChild(0).gameObject.SendMessage("ActivateScript");
					}
				}

				// make it a child of the lower right panel on the level
				//selectedHeroPanel.transform.SetParent (GameObject.Find ("HeroSelectPanel").transform, false );
				selectedHeroPanel.transform.parent.gameObject.SendMessage("ActivateScript");

			}
		}
	}

	public void LoadScene(){
		//levelToLoad = levelName; 	// levelName typed in the inspector
		this.enabled = true;	// enable the script to start loading
		//isLoading = true;
	}

	public void LoadTutorialScene(string levelName){
		PlayerPrefs.SetInt("Tutorial", 1); 						// this will be used later para malaman na kailangan ng user mag tutorial pag dating ng level 1
		PlayerPrefs.SetInt("max health", 5);					// the starting max health of the user is 5
		levelToLoad = levelName; 	// levelName typed in the inspector
		this.enabled = true;	// enable the script to start loading
		//isLoading = true;
	}			

	public void setSceneNameToLoad(){
		print (EventSystem.current.currentSelectedGameObject.name);
		levelToLoad = EventSystem.current.currentSelectedGameObject.name; 	// levelName typed in the inspector
	}

}
