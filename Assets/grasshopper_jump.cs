using UnityEngine;
using System.Collections;

public class grasshopper_jump : MonoBehaviour {

	public bool jump;
	public float durationOfJump = 3f;
	bool notJump;

	//bool jumpOnce;

	Animator anim;

	float timeInterval;
	float lastJumptime;

	HealthBar healthBar;

	private MoveEnemy moveEnemy;

	// Use this for initialization
	void Start () {
		anim = GetComponent < Animator> ();
		notJump = true;
		lastJumptime = Time.time;
		moveEnemy = transform.parent.GetComponent<MoveEnemy> ();
		healthBar = transform.parent.GetChild(2).GetComponent<HealthBar>();
		
	}
	
	// Update is called once per frame
	void Update () {

		timeInterval = Time.time;

		if (healthBar.currentHealth < healthBar.maxHealth && notJump) {
			jump = true;
			notJump = false;	// para isang bes na lang dumaaan tong statement na to
		}

		if (jump) {
			lastJumptime = Time.time;
			anim.SetBool("jump", true);
		//	if(anim.GetCurrentAnimatorStateInfo(0).){
		//	}
			jump = false;
		//	jump = false;
		//	notJump = false;
		}
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("stay on air") ){
			anim.SetBool("jump", false);
			anim.SetBool("stay on air", true);

			if(timeInterval - lastJumptime >= durationOfJump){
				anim.SetBool("jump finish", true);
				anim.SetBool("stay on air", false);
			}
		}
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("has landed") ){
				anim.SetBool("jump finish", false);
				// switch into move direction
				moveEnemy.SwitchIntoMoveDirection();
			
		}

		//Debug.Log (timeInterval - lastJumptime);
	}
}
