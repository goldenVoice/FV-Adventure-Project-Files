using UnityEngine;
using System.Collections;

public class musicPlayer : MonoBehaviour {

  public GameObject music;

  void Awake(){
       DontDestroyOnLoad(music);
    }
    
  
	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
