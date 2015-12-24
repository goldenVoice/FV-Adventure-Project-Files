using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
    public AudioSource audio;
    public Text text;

    void Start() {
      audio = GetComponent<AudioSource>();
      
    }
    
    void Update() {
    

    }

    public void pauseSound(){
        if (audio.mute){
                audio.mute = false;
                text.text = "Sounds: ON";
        }
            else{
                audio.mute = true;
                text.text = "Sounds: OFF";
        
            }
                
    
    }
}
