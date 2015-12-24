using UnityEngine;
using System.Collections;

// enable the hero attack script of the gameObject hero when it is finished planted

public class finishedPlanted_carrot : MonoBehaviour {
  
  public bool hero_isPlanted;
  Animator anim;

  
  // Use this for initialization
  void Start () {
       anim = gameObject.GetComponent<Animator>();
       hero_isPlanted = false;
  }
  
  // Update is called once per frame
  void Update () {
        

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("carrot_idle") ){
//              Debug.Log("Carrot hero is now ready to attack");
              hero_isPlanted = true;
              Destroy(this); // destroys this script instance
        } 
  }
}
