using UnityEngine;
using System.Collections;

public class CarrotCannon_particleBehavior : MonoBehaviour {

	private ParticleSystem thisParticleSystem;
	[HideInInspector]
	public GameObject CC_areaImpact;		// carrot cannon impacct

	AudioSource audio;
	public AudioClip explodeSound;

	// Use this for initialization
	void Start () {
		// get the current gameObjects particle system then place it to the variable thisParticleSystem
		thisParticleSystem = (ParticleSystem)gameObject.GetComponent(typeof(ParticleSystem));
		audio = (AudioSource)GameObject.FindObjectOfType(typeof(AudioSource));

		if(PlayerPrefs.GetInt("sounds") == 1){		// sounds: ON
			audio.PlayOneShot(explodeSound, 0.8f);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		if (thisParticleSystem.isPlaying){  
			return;     // if the particle is still playing, do nothing
		}

		Destroy (CC_areaImpact.gameObject);		
		Destroy(gameObject);   // destroy the particle gameObject that is attached to the script
	}
}
