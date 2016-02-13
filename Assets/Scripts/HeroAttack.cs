﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroAttack : MonoBehaviour {

	public Transform range_center;
	public CircleCollider2D range_radius;
	public LayerMask enemyLayerMask;

	public List<GameObject> enemiesInRange;

	private float lastShotTime;
	private HeroData heroData;

	private GameObject target;

	private GameObject bulletPrefab;

	private AudioSource attackSound;
//	public GameObject bulletPrefab;
	
	private finishedPlanted_carrot planted_carrotScript;

	Animator anim;

	void Awake(){
		attackSound = gameObject.GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		enemiesInRange = new List<GameObject>();
		// at the start, there are no enemies, so you create an empty list
		GameObject parent_hero = gameObject.transform.parent.gameObject;
    	anim = (Animator)parent_hero.transform.GetChild(1).GetComponent<Animator>(); 
		lastShotTime = Time.time;
		heroData = (HeroData)parent_hero.transform.GetChild(1).GetComponent<HeroData>();
	}
	
	void FixedUpdate(){
		// arguments:			     (center of the circle, radius of the circle, layerMask for filtering objects)
		Physics2D.OverlapCircle(range_center.position, range_radius.radius, enemyLayerMask);
//		Debug.Log(Physics2D.OverlapCircle(range_center.position, range_radius.radius, enemyLayerMask) );
		
	}
	// Update is called once per frame
	void Update () {

		// the hero 

			 target = null;
			// 
			float minimalEnemyDistance = float.MaxValue;		// the maximum possible distance
			foreach(GameObject enemy in enemiesInRange){		// iterate through the list of enemies
				float distanceToGoal = enemy.transform.parent.GetComponent<MoveEnemy>().distanceToGoal();		// get the distanceToGoal of the current enemy
				if(distanceToGoal < minimalEnemyDistance){	// kapag yung distance to the end of the stage area (yung goal) ay mas maliit sa minimalEnemyDistance
					target = enemy;
					minimalEnemyDistance = distanceToGoal;		// set as new minimal distance.
				}
			}	

			if(target != null){
				if(target.transform.position.x < gameObject.transform.position.x){
							
							anim.SetBool("enemy_at_leftSide", true);		// make the hero face left
							anim.SetBool("enemy_at_rightSide", false);
							
					if(Time.time - lastShotTime > heroData.fireRate && checkTarget(target) ){
						//	anim.SetTrigger("attack");
						anim.SetTrigger("attack_left");
						playSound();
						Shoot(target.GetComponent<Collider2D>() );	// function shoot, the targets collider2D is used as parameter
						lastShotTime = Time.time;
					}

				}
				else if(target.transform.position.x > gameObject.transform.position.x){
								anim.SetBool("enemy_at_rightSide", true);		// make the hero face right
								anim.SetBool("enemy_at_leftSide", false);

				if(Time.time - lastShotTime > heroData.fireRate && checkTarget(target) ){
						//	anim.SetTrigger("attack");
						anim.SetTrigger("attack");
						playSound();
						Shoot(target.GetComponent<Collider2D>() );	// function shoot, the targets collider2D is used as parameter
						lastShotTime = Time.time;
					}
				}

//				else if(anim.GetTrigger("enemy_at_rightSide")){
//						print("enemy in the right side");
//				}
				
				// rotate the hero, depending where the target is, 
				// in your case, pwedeng dito mo ilagay yung code para malaman kung which side haharap yung hero, (left or right)
					
//				Vector3 direction = gameObject.transform.position - target.transform.position;
//				gameObject.transform.rotation = Quaternion.AngleAxis(
//					Mathf.Atan2 (direction.y, direction.x) * 180 / Mathf.PI;
//					new Vector3 (0,0,1) );
			}
	}

	void OnEnemyDestroy (GameObject enemy){
		enemiesInRange.Remove (enemy);
	}

	void OnTriggerEnter2D (Collider2D other){
	//	Debug.Log(other.gameObject.tag + " has entered the range");
		if(other.gameObject.tag.Equals("Enemy")) {
			enemiesInRange.Add(other.gameObject);		// add the enemy that entered the collider, on the list of enemies in range
			EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
			del.enemyDelegate += OnEnemyDestroy;		// calls OnEnemyDestroy when the enemy is destroyed, idk kung pano nangyare yon

		}
	}

	void OnTriggerExit2D (Collider2D other){
		if(other.gameObject.tag.Equals("Enemy")){
			enemiesInRange.Remove(other.gameObject);
			EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
			del.enemyDelegate -= OnEnemyDestroy;			// you unregister the enemies in the delegate, now you know whic enemies are in range.
		}
	}

	void Shoot (Collider2D target){
		 bulletPrefab = heroData.bullet;

		 Vector3 startPosition = gameObject.transform.position;
		 Vector3 targetPosition = target.transform.position;
		 targetPosition.z = bulletPrefab.transform.position.z;		// so that the bullet would appear above the enemy
		 startPosition.z = bulletPrefab.transform.position.z;			// bullet should appear below the hero firing it

		 GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
		 newBullet.transform.position = startPosition;
		 BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
		 bulletComp.target = target.gameObject;
		 bulletComp.startPosition = startPosition;
		 bulletComp.targetPosition = targetPosition;

		 // HERE you make the game more interesting
		 // play a sound, and animate the hero to fire.

		 // Animator animator = hero.getComponent<Animator>();
		 // animator.SetTrigger("HeroFires");
		 // AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		 // audioSource.PlayOneShot(audioSource.clip);
	}

	// checks if the the insect is kasama sa mga target enemy ng hero (example, flying, walking or both ba yung insect?)
	bool checkTarget(GameObject target){

		// this is for the grasshopper that can jump para hindi sya ma attack pag tumalon sya.
		if (target.name == "Grasshopper jumping"){
			if(target.GetComponent<grasshopper_jump>().cannotAttack){
				Debug.Log("Hey " + transform.parent.gameObject.name + " you cannot attack this jumping grasshopper");
				return false;
			}
		}

		Debug.Log("insect path: " + target.GetComponent<EnemyData>().insectPath);
		Debug.Log("hero target: " + heroData.target);
		Debug.Log (target.GetComponent<EnemyData>().insectPath == EnemyData.pathWay.both);

		// this is for the grasshopper that can jump para hindi sya ma attack pag tumalon sya.
		if (target.name == "Grasshopper jumping"){
			if(target.GetComponent<grasshopper_jump>().cannotAttack){
				Debug.Log("you cannot attack this jumping grasshopper");
				return false;
			}
		}

		if(target.GetComponent<EnemyData>().insectPath == heroData.target ){
			return true; //target
		}
		else if(heroData.target == EnemyData.pathWay.both){	// basta both (flying, walking) ang target ng hero, a atakihin nya yung enemy
			return true; //target
		}
		else if(target.GetComponent<EnemyData>().insectPath == EnemyData.pathWay.both){		// in special cases sa hard mode, an insect can be both in air & land pathway
			Debug.Log(target.name + " is both air and land. ATTACK!");
			return true;
		}
		else{
			enemiesInRange.Remove(target.gameObject);
			EnemyDestructionDelegate del = target.gameObject.GetComponent<EnemyDestructionDelegate>();
			del.enemyDelegate -= OnEnemyDestroy;			// you unregister the enemies in the delegate, now you know whic enemies are in range.
			return false;	// meaning hindi niya target enemy yan :D
		}
	}

	void playSound(){
		if(PlayerPrefs.GetInt("sounds") == 1){		// sounds: ON
			attackSound.PlayOneShot(attackSound.clip, 0.7f);
		}
	}

	void ActivateScript(){
		this.enabled = true;
	}

}
