using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClearData(){		// reset ALL current user data
		PlayerPrefs.DeleteAll();
	}
}
