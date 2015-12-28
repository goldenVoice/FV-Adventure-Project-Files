using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{
	
//	void Awake()
//	{
//		canvasGroup = GetComponent<CanvasGroup>();
//	}
	public Button nextButton;
	
	public CanvasGroup canvasGroup;

	void Start ()
	{
		fadeInPanel(canvasGroup);
	}

	public void fadeInPanel(CanvasGroup canvasGroup){
		StartCoroutine("FadeIn", canvasGroup);
	}

	public void fadeOutPanel(CanvasGroup canvasGroup){
		StartCoroutine("FadeOut", canvasGroup);
	}

	IEnumerator FadeOut(CanvasGroup canvasGroup)
	{	
		nextButton.GetComponent<Button>().interactable = false;		// dont let the user touch the button para di masira yung sequence ng storyline
		float time = 1f;
		while(canvasGroup.alpha > 0)
		{
			canvasGroup.alpha -= Time.deltaTime / time;
			yield return null;
		}

	}

	IEnumerator FadeIn(CanvasGroup canvasGroup)	
	{	
		float time = 1f;
		while(canvasGroup.alpha < 1)
		{
			canvasGroup.alpha += Time.deltaTime / time;
			yield return null;
		}
		
		if(canvasGroup.alpha == 1){
//			Debug.Log("lalabas pag tapos mag fade in.");
			nextButton.GetComponent<Button>().interactable = true;
		}
	}

}