using UnityEngine;
using System.Collections;

public class StorylineSoundManager : MonoBehaviour {
	public AudioSource audio;

	// Use this for initialization
	void Start () {

		if (PlayerPrefs.GetInt ("sounds") == 1) {
			audio.Play ();
		}
		else {
			audio.Play ();
			audio.mute = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
