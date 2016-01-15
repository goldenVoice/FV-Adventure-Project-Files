using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
    public AudioSource audio;
//    public Text text;

	public Text soundsText;
	public Text vibrText;

	void Awake(){
		if( !(PlayerPrefs.HasKey("sounds")) ){	// if wala pang sounds na key ibig sabihin wala 1st time mag laro ng user
			PlayerPrefs.SetInt("sounds", 1);
			PlayerPrefs.SetInt("vibr", 1);		// malamang pate vibr wala pa kaya set mo na den
		}

	}

    void Start() {
      	audio = GetComponent<AudioSource>();
		Debug.Log(PlayerPrefs.GetInt("sounds"));

		if (soundsText != null && vibrText != null) {
			if (PlayerPrefs.GetInt ("sounds") == 1) {
				soundsText.text = "Sounds: ON";
				audio.Play ();
			} else {
				soundsText.text = "Sounds: OFF";
				audio.Play ();
				audio.mute = true;
			}

			if (PlayerPrefs.GetInt ("vibr") == 1) {		// vibr: On
				vibrText.text = "Vibration: ON";
			} else {
				vibrText.text = "Vibration: OFF";
			}
		} 
    }
    
    void Update() {
    

    }

    public void pauseSound(){
        if (audio.mute){
                audio.mute = false;
//				audio.Play();
                soundsText.text = "Sounds: ON";
				PlayerPrefs.SetInt("sounds", 1); 	// meaning on
        }
            else{
                audio.mute = true;
                soundsText.text = "Sounds: OFF";
				PlayerPrefs.SetInt("sounds", 0); 	// meaning off
            }    
    }
}
