using UnityEngine;
using System.Collections;

public class Mushroom_animatorChecker : MonoBehaviour {

	mushroom_BulletBehavior mushroomBehavior;

	// Use this for initialization
	void Start () {
		mushroomBehavior = GetComponent<mushroom_BulletBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle")){
			mushroomBehavior.enabled = true;
		}
	
	}
}
