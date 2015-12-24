using UnityEngine;
using System.Collections;

public class DestroyFinishedParticle : MonoBehaviour {


  private ParticleSystem thisParticleSystem;

	// Use this for initialization
	void Start () {
        // get the current gameObjects particle system then place it to the variable thisParticleSystem
	     thisParticleSystem = (ParticleSystem)gameObject.GetComponent(typeof(ParticleSystem));
	}
	
	// Update is called once per frame
	void Update () {

	     if (thisParticleSystem.isPlaying){  
            return;     // if the particle is still playing, do nothing
       }

       Destroy(gameObject);   // destroy the particle gameObject that is attached to the script
	}


}
