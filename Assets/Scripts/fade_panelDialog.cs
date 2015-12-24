using UnityEngine;
using System.Collections;

public class fade_panelDialog : MonoBehaviour
{
	
	//	void Awake()
	//	{
	//		canvasGroup = GetComponent<CanvasGroup>();
	//	}
	
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
		float time = 0.2f;
		while(canvasGroup.alpha > 0)
		{
			canvasGroup.alpha -= Time.deltaTime / time;
			yield return null;
		}
	}
	
	IEnumerator FadeIn(CanvasGroup canvasGroup)	
	{	
		yield return new WaitForSeconds(2);
		Debug.Log(gameObject.name);
		
		float time = 1f;
		while(canvasGroup.alpha < 1)
		{
			canvasGroup.alpha += Time.deltaTime / time;
			yield return null;
		}
	}
	
}