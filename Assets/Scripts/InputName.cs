using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputName : MonoBehaviour {

	public InputField inputField;
	public Text profileNameText;

	string userName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void enterNameToProfile(){
		profileNameText.text = inputField.text;
		string profileNumber = gameObject.transform.parent.name;	// gets the name of the profile ex: 'profile 1'
		Debug.Log(profileNumber);
		
		PlayerPrefs.SetString (profileNumber, profileNumber);
		PlayerPrefs.SetString(profileNumber + " name", profileNameText.text);				// 'profile 1 name', ex: Jeymow
		
		inputField.text = "";
	}
}
