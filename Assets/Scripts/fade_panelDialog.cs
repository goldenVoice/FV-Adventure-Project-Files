using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fade_panelDialog : MonoBehaviour
{
	
	//	void Awake()
	//	{
	//		canvasGroup = GetComponent<CanvasGroup>();
	//	}
	public Button nextButton;
	
	public CanvasGroup currentCanvasGroup;
	
	void Start ()
	{	
		nextButton.GetComponent<Button>().interactable = false;
		fadeInPanel(currentCanvasGroup);
	}

	void Update(){

		if(currentCanvasGroup != null){
			if(currentCanvasGroup.alpha == 1){
				showNextButton();
			}
		}
	}

	public void fadeInPanel(CanvasGroup canvasGroup){
		StartCoroutine("FadeIn", canvasGroup);
	}
	
	public void fadeOutPanel(CanvasGroup canvasGroup){
		StartCoroutine("FadeOut", canvasGroup);
	}
	
	IEnumerator FadeOut(CanvasGroup canvasGroup)
	{	
		hideNextButton();

		float time = 1f;
		while(canvasGroup.alpha > 0)
		{
			canvasGroup.alpha -= Time.deltaTime / time;
			currentCanvasGroup.alpha += Time.deltaTime / time;
			yield return null;
		}
		yield return new WaitForSeconds(1);
		
	}
	
	IEnumerator FadeIn(CanvasGroup canvasGroup)	
	{	
		currentCanvasGroup = canvasGroup;

		yield return new WaitForSeconds(2);
		float time = 1f;
		while(canvasGroup.alpha < 1)
		{
			canvasGroup.alpha += Time.deltaTime / time;
			currentCanvasGroup.alpha += Time.deltaTime / time;
			
			yield return null;
		}
	//	yield return new WaitForSeconds(2);

	//	showNextButton();
	}

	public void hidePanel(CanvasGroup canvasGroup){		// without the fade effect
		canvasGroup.alpha = 0;
	}

	public void showPanel(CanvasGroup canvasGroup){		// without the fade effect
		canvasGroup.alpha = 1;
	}
	void hideNextButton(){
		nextButton.GetComponent<Button>().interactable = false;				// dont let the user touch the button para di masira yung sequence ng storyline
		nextButton.GetComponent<Image>().enabled = false;					// hide the button image
		nextButton.transform.GetChild(0).GetComponent<Text>().text = "";	// empty the text string

	}

	void showNextButton(){
		nextButton.GetComponent<Button>().enabled = true;						// show button
		nextButton.GetComponent<Button>().interactable = true;						// show button
		nextButton.GetComponent<Image>().enabled = true;							// show the button image
		nextButton.transform.GetChild(0).GetComponent<Text>().text = "Next";

	}
	
}