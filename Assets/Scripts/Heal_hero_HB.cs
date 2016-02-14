using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Heal_hero_HB : MonoBehaviour {

	public Transform range_center;
	public CircleCollider2D range_radius;
	public LayerMask enemyLayerMask;

	//public List<GameObject> heroesInRange;

	List_hero list_hero;

	public List<GameObject> heroesToHeal;

	private float lastShotTime;
	private HeroData heroData;

	private GameObject target;

//	private GameObject bulletPrefab;
	public ParticleSystem healingParticle;

	private AudioSource attackSound;
//	public GameObject bulletPrefab;
	
	private finishedPlanted_carrot planted_carrotScript;

	float healingPower; 

	Animator anim;

	public GameObject hero;

	public int maxHeroToHeal;

	int counter;
	string currentProfile;
	void Awake(){
		
		currentProfile = PlayerPrefs.GetString ("currentProfile");
		attackSound = gameObject.GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		list_hero = (List_hero)GameObject.FindObjectOfType(typeof(List_hero));
		counter = 0;
		//heroesInRange = new List<GameObject>();

		int currentLevel = PlayerPrefs.GetInt(currentProfile + hero.name + " attack");													// ex: 'Carrot attack' this is same with the shop. iisa lang format ng name para sa player prefs
		healingPower = hero.transform.GetChild(1).GetComponent<HeroData>().attackLevels[currentLevel].damage;

		GameObject parent_hero = gameObject.transform.parent.gameObject;
    	anim = (Animator)parent_hero.transform.GetChild(1).GetComponent<Animator>(); 
		lastShotTime = Time.time;
		heroData = (HeroData)parent_hero.transform.GetChild(1).GetComponent<HeroData>();
	}
	
//	void FixedUpdate(){
//		// arguments:			     (center of the circle, radius of the circle, layerMask for filtering objects)
//		Physics2D.OverlapCircle(range_center.position, range_radius.radius, enemyLayerMask);
////		Debug.Log(Physics2D.OverlapCircle(range_center.position, range_radius.radius, enemyLayerMask) );
//		
//	}
//	// Update is called once per frame
	void Update () {
		
		if (Time.time - lastShotTime > heroData.fireRate) {
			heroesToHeal = list_hero.plantedHeroes;
			foreach (GameObject hero in list_hero.plantedHeroes) {		// iterate through the list of enemies
				if(hero != null){

					HealthBar heroHealth = hero.transform.GetChild (6).GetComponent<HealthBar> ();
					if (heroHealth.currentHealth < heroHealth.maxHealth) {
						if(counter < maxHeroToHeal){
							anim.SetTrigger ("attack_left");
					Debug.Log("di lalabas ng madameng time");
							// heal
							lastShotTime = Time.time;
							if (heroHealth.currentHealth + healingPower >= heroHealth.maxHealth) {
								heroHealth.currentHealth = heroHealth.maxHealth;
								Instantiate(healingParticle, heroHealth.transform.parent.GetChild(1).transform.position, transform.rotation);
								counter++;
							} 
							else {
								heroHealth.currentHealth += healingPower;
								Instantiate(healingParticle, heroHealth.transform.parent.GetChild(1).transform.position, transform.rotation);
								counter++;
							}
						}
						else {	// pag nag exceed na sa max na dame ng heroes. tigil na.
							break;
						}
					}
					//Debug.Log("di lalabas ng madameng time");
				}
			}
		}
		counter = 0;	// refresh
	}

//	void OnEnemyDestroy (GameObject enemy){
//		heroesInRange.Remove (enemy);
//	}
//
//	void OnTriggerStay2D (Collider2D other){
//	//	Debug.Log(other.gameObject.tag + " has entered the range");
//		if(other.gameObject.tag.Equals("Hero")) {
//			if(!(heroesInRange.Contains(other.gameObject)) ){
//				heroesInRange.Add(other.gameObject);		// add the enemy that entered the collider, on the list of enemies in range
//				EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
//				del.enemyDelegate += OnEnemyDestroy;		// calls OnEnemyDestroy when the enemy is destroyed, idk kung pano nangyare yon
//			}
//
//		}
//	}
//
//	void OnTriggerExit2D (Collider2D other){
//		if(other.gameObject.tag.Equals("Hero")){
//			heroesInRange.Remove(other.gameObject);
//			EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
//			del.enemyDelegate -= OnEnemyDestroy;			// you unregister the enemies in the delegate, now you know whic enemies are in range.
//		}
//	}
//
//	void Shoot (Collider2D target){
//		 bulletPrefab = heroData.bullet;
//
//		 Vector3 startPosition = gameObject.transform.position;
//		 Vector3 targetPosition = target.transform.position;
//		 targetPosition.z = bulletPrefab.transform.position.z;		// so that the bullet would appear above the enemy
//		 startPosition.z = bulletPrefab.transform.position.z;			// bullet should appear below the hero firing it
//
//		 GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
//		 newBullet.transform.position = startPosition;
//		 BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
//		 bulletComp.target = target.gameObject;
//		 bulletComp.startPosition = startPosition;
//		 bulletComp.targetPosition = targetPosition;
//
//		 // HERE you make the game more interesting
//		 // play a sound, and animate the hero to fire.
//
//		 // Animator animator = hero.getComponent<Animator>();
//		 // animator.SetTrigger("HeroFires");
//		 // AudioSource audioSource = gameObject.GetComponent<AudioSource>();
//		 // audioSource.PlayOneShot(audioSource.clip);
//	}
//
//	// checks if the the insect is kasama sa mga target enemy ng hero (example, flying, walking or both ba yung insect?)
////	bool checkTarget(GameObject target){
////		Debug.Log("insect path: " + target.GetComponent<EnemyData>().insectPath);
////		Debug.Log("hero target: " + heroData.target);
////		if(target.GetComponent<EnemyData>().insectPath == heroData.target){
////			return true; //target
////		}
////		else if(heroData.target == EnemyData.pathWay.both){	// basta both (flying, walking) ang target ng hero, a atakihin nya yung enemy
////			return true; //target
////		}
////		else{
////			heroesInRange.Remove(target.gameObject);
////			EnemyDestructionDelegate del = target.gameObject.GetComponent<EnemyDestructionDelegate>();
////			del.enemyDelegate -= OnEnemyDestroy;			// you unregister the enemies in the delegate, now you know whic enemies are in range.
////			return false;	// meaning hindi niya target enemy yan :D
////		}
////	}

	void playSound(){
		if(PlayerPrefs.GetInt("sounds") == 1){		// sounds: ON
			attackSound.PlayOneShot(attackSound.clip, 0.7f);
		}
	}

	void ActivateScript(){
		this.enabled = true;
	}

}
