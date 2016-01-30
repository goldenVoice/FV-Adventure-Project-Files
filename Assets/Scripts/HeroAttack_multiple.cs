using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroAttack_multiple : MonoBehaviour {

	public Transform range_center;
	public CircleCollider2D range_radius;
	public LayerMask enemyLayerMask;

	public List<GameObject> enemiesInRange;

	private float lastShotTime;
	private HeroData heroData;

	private GameObject target;

	private GameObject bulletPrefab;

	public int maxEnemyAttack;
	int attackCounter;
	
	private finishedPlanted_carrot planted_carrotScript;

	Animator anim;

	private AudioSource attackSound;

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
		attackCounter = 1;
	}
	
	void FixedUpdate(){
		// arguments:			     (center of the circle, radius of the circle, layerMask for filtering objects)
		Physics2D.OverlapCircle(range_center.position, range_radius.radius, enemyLayerMask);
//		Debug.Log(Physics2D.OverlapCircle(range_center.position, range_radius.radius, enemyLayerMask) );

	}
	// Update is called once per frame
	void Update () {

			if(enemiesInRange.Count > 0){
					if(Time.time - lastShotTime > heroData.fireRate){

						anim.SetTrigger("attack_left");
						playSound();
						foreach(GameObject enemy in enemiesInRange){
							if(checkTarget(enemy)){
								Shoot(enemy.GetComponent<Collider2D>() );	// function shoot, the targets collider2D is used as parameter
								attackCounter++;
								if(attackCounter > maxEnemyAttack){			// max number of enemy to attack. if it exceeds. break the loop. stop attacking
									break;
								}
							}
						}
						attackCounter = 1;
						lastShotTime = Time.time;
					}
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

			// ang 
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
		Debug.Log("insect path: " + target.GetComponent<EnemyData>().insectPath);
		Debug.Log("hero target: " + heroData.target);
		if(target.GetComponent<EnemyData>().insectPath == heroData.target){
			return true; //target
		}
		else if(heroData.target == EnemyData.pathWay.both){	// basta both (flying, walking) ang target ng hero, a atakihin nya yung enemy
			return true; //target
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
