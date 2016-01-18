using UnityEngine;
using System.Collections;

public class CheckBeforeDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// check the hero selected panel if wala ng child then destroy

//		if(gameObject.transform.GetChild(0).childCount != 0){
		for (int i = 0; i < gameObject.transform.GetChild(0).childCount; i++) {
			// loop through all the circles
			if (gameObject.transform.GetChild (0).GetChild (i).childCount == 0) {		// kapag walang laman na hero tong slot na to.
				Destroy (gameObject.transform.GetChild (0).GetChild (i).gameObject);	// destroy
			}
		}
//	}
		// if wala ng laman ang selected hero panel at naiwan na lang ang nag iisang 'selected hero panel para remove'
		if (gameObject.transform.GetChild(0).childCount == 0) {
			Destroy(gameObject);
		}
		

	}
	
	public void ActivateScript(){
		this.enabled = true;
	}
}
