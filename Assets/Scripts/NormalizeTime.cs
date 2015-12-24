using UnityEngine;
using System.Collections;

public class NormalizeTime : MonoBehaviour {
	// script attached to the main camera
	// so that when any scene loads, the time.timeScale is not paused or fast forwarded

	// Use this for initialization
	void Start () {

		Time.timeScale = 1f;	// set the time to realtime
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
