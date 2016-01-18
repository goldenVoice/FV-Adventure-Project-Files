using UnityEngine;
using System.Collections;

// script checks if nag e exist na yung panel na to. heroSelectpanel if oo, disable na lang to para di dumame ang copies
public class PanelChecker : MonoBehaviour {
	GameObject[] heroPanel;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		heroPanel = GameObject.FindGameObjectsWithTag ("HeroSelect");
		
		// pag mas madame sa isa ibig sabihin galing sa restart ang player. disable this. di kailangan
		if (heroPanel.Length > 1) {
			gameObject.SetActive(false);
		}

	}
}
