using UnityEngine;
using System.Collections;

public class AnimatorChecker : MonoBehaviour {
  Animator anim;
  
	// Use this for initialization
	void Start () {
	     anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
      
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("finished") ){
              // destroy the nextWaveButton gameObject if the animation is finished      
              //Destroy(gameObject);
			  gameObject.SetActive(false);
        }	
	}

}
