using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExitManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape)){
			GetComponent<Canvas>().enabled = true;
		}
	}

	public void CloseApplication(){
		Application.Quit();
	}
}
