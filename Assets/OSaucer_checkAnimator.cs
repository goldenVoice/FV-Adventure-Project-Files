using UnityEngine;
using System.Collections;

// this script checks if the orange saucer is no longeer attacking, hence 'idle' na sya.
public class OSaucer_checkAnimator : MonoBehaviour {

	public GameObject OSaucer_hero;
	Animator OSaucerHero_anim;

	Animator saucerAnim;

	// Use this for initialization
	void Start () {
		OSaucerHero_anim = OSaucer_hero.GetComponent<Animator> ();
		saucerAnim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (OSaucerHero_anim.GetCurrentAnimatorStateInfo (0).IsName ("idle") || OSaucerHero_anim.GetCurrentAnimatorStateInfo (0).IsName ("idle_leftSide")) {
			saucerAnim.enabled = false;

	//		gameObject.transform.Rotate( new Vector3(0f,0f,9f) );
			transform.eulerAngles = new Vector3(0f,0f,OSaucer_hero.transform.rotation.z);
		}
	
	}
}
