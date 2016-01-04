using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
    public AudioSource audio;
    public Text text;

    void Start() {
      	audio = GetComponent<AudioSource>();
      
		if(PlayerPrefs.GetInt("sounds") == 1){

		}
    }
    
    void Update() {
    

    }

    public void pauseSound(){
        if (audio.mute){
                audio.mute = false;
                text.text = "Sounds: ON";
				PlayerPrefs.SetInt("sounds", 1); 	// meaning on
        }
            else{
                audio.mute = true;
                text.text = "Sounds: OFF";
				PlayerPrefs.SetInt("sounds", 0); 	// meaning off
            }
                
    
    }
}
