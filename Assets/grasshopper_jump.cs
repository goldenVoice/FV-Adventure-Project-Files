using UnityEngine;
using System.Collections;

public class grasshopper_jump : MonoBehaviour {

	public bool jump;
	public float durationOfJump = 3f;
	bool notJump;

	public bool cannotAttack;
	public bool grasshopperCanAttack;	// para sa hardmode
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
		cannotAttack = false;
		grasshopperCanAttack = true;	// para sa hard mode
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
			//GetComponent<BoxCollider2D>().enabled = false;
			cannotAttack = true;		// para di sya ma atake ng heroes
			grasshopperCanAttack = false;
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
				//GetComponent<BoxCollider2D>().enabled = true;
				cannotAttack = false;		// nasa lupa na ule sya. pwede na atakihin
			grasshopperCanAttack = true;
				moveEnemy.SwitchIntoMoveDirection();
			
		}

		//Debug.Log (timeInterval - lastJumptime);
	}
}
