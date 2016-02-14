using UnityEngine;
using System.Collections;

public class moleCricket_burrow : MonoBehaviour {

	public bool burrow;
	public float durationOfBurrow = 3f;
	bool notBurrow;

	public bool cannotAttack;
	public bool moleCricketCanAttack;	// para sa hardmode
	//bool jumpOnce;

	Animator anim;

	float timeInterval;
	float lastburrowtime;

	HealthBar healthBar;

	private MoveEnemy moveEnemy;

	public ParticleSystem burrowParticle;
	public Transform point;
	// Use this for initialization
	void Start () {
		anim = GetComponent < Animator> ();
		notBurrow = true;
		lastburrowtime = Time.time;
		moveEnemy = transform.parent.GetComponent<MoveEnemy> ();
		healthBar = transform.parent.GetChild(2).GetComponent<HealthBar>();
		cannotAttack = false;
		moleCricketCanAttack = true;	// para sa hard mode
	}
	
	// Update is called once per frame
	void Update () {

		timeInterval = Time.time;

		if (healthBar.currentHealth < healthBar.maxHealth && notBurrow) {
			burrow = true;
			notBurrow = false;	// para isang bes na lang dumaaan tong statement na to
		}

		if (burrow) {
			lastburrowtime = Time.time;
			//GetComponent<BoxCollider2D>().enabled = false;
			cannotAttack = true;		// para di sya ma atake ng heroes
			moleCricketCanAttack = false;
			anim.SetBool("burrow", true);
//			Debug.Log(transform.parent.FindChild("point").);
			Instantiate(burrowParticle, point.position, point.rotation);
			
			
		//	if(anim.GetCurrentAnimatorStateInfo(0).){
		//	}
			burrow = false;

		//	burrow = false;
		//	notBurrow = false;
		}
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("stay on ground") ){
			anim.SetBool("burrow", false);
			anim.SetBool("stay on ground", true);

			if(timeInterval - lastburrowtime >= durationOfBurrow){
				anim.SetBool("burrow finish", true);
			//	Instantiate(burrowParticle, transform.parent.FindChild("point").transform.position, transform.FindChild("point").transform.rotation);
				Instantiate(burrowParticle, point.position, point.rotation);
				anim.SetBool("stay on ground", false);
			}
		}
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("has landed") ){
				anim.SetBool("burrow finish", false);
				// switch into move direction
				//GetComponent<BoxCollider2D>().enabled = true;
				cannotAttack = false;		// nasa lupa na ule sya. pwede na atakihin
				moleCricketCanAttack = true;
				moveEnemy.SwitchIntoMoveDirection();
			
		}

		//Debug.Log (timeInterval - lastburrowtime);
	}
}
