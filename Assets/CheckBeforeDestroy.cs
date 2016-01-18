using UnityEngine;
using System.Collections;

public class CheckBeforeDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// check the hero selected panel if wala ng child then destroy

		for (int i = 0; i < 5; i++){
			// loop through all the circles
			if (gameObject.transform.GetChild(0).childCount == 0) {
				Destroy(gameObject);
			}

		}

	}

	public void ActivateScript(){
		this.enabled = true;
	}
}
