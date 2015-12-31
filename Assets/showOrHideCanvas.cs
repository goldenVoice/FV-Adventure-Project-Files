using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class showOrHideCanvas : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowCanvas(Canvas canvasToShow){
		canvasToShow.enabled = true;
	}

	public void HideCanvas(Canvas canvasToHide){
		canvasToHide.enabled = false;
	}

}
