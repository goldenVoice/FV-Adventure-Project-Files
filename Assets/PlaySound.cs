using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = (AudioSource)GameObject.FindObjectOfType(typeof(AudioSource));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play(AudioClip sound){				// checks if the sound settings is set to on then plays sound if yes
		if(PlayerPrefs.GetInt("sounds") == 1){		// sounds: ON
			audio.PlayOneShot(sound, 0.7f);
		}
		else{

		}
	}

	public void DontDestroy(){
		DontDestroyOnLoad (GameObject.Find ("BGM").gameObject );
	}
}
