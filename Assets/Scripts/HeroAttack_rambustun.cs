using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroAttack_rambustun : MonoBehaviour {

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

	//LineRenderer laser;
//	public Transform point1;		// starting point
//	public Transform point2;		// another starting point
//
//	public LineRenderer laser1;
//	public LineRenderer laser2;
	
	public GameObject hero;
//	public LaserBeam laserBeam;
	ElementManager elementManager;
	ElementManager.Element hero_element;
	private GameManagerBehavior gameManager;    // rewards player when they destroy the enemy

	float currentDamage;
	public float damage;

	public ParticleSystem bulletImpact_particle;

	void Awake(){
		attackSound = gameObject.GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		enemiesInRange = new List<GameObject>();
		// at the start, there are no enemies, so you create an empty list
		int currentLevel = PlayerPrefs.GetInt(hero.name + " attack");													// ex: 'Carrot attack' this is same with the shop. iisa lang format ng name para sa player prefs
		currentDamage = hero.transform.GetChild(1).GetComponent<HeroData>().attackLevels[currentLevel].damage;	// then, you look up the corresponding damage depending on the user's current level of attack upgrade

		GameObject parent_hero = gameObject.transform.parent.gameObject;
    	anim = (Animator)parent_hero.transform.GetChild(1).GetComponent<Animator>(); 
		lastShotTime = Time.time;
		heroData = (HeroData)parent_hero.transform.GetChild(1).GetComponent<HeroData>();
		//laser = GetComponent<LineRenderer> ();
		damage = currentDamage;
		
		elementManager = (ElementManager) FindObjectOfType(typeof(ElementManager));
		hero_element = hero.transform.GetChild(1).GetComponent<HeroData>().heroElement;
		GameObject gm = GameObject.Find("GameManager");
		gameManager = gm.GetComponent<GameManagerBehavior>();

		
	}
	
	void FixedUpdate(){
		// arguments:			     (center of the circle, radius of the circle, layerMask for filtering objects)
		Physics2D.OverlapCircle(range_center.position, range_radius.radius, enemyLayerMask);
//		Debug.Log(Physics2D.OverlapCircle(range_center.position, range_radius.radius, enemyLayerMask) );
		
	}
	// Update is called once per frame
	void Update () {

		// the hero 
//						Debug.Log("laser beam: " + laserBeam.enabled);

			 target = null;
			// 
			float minimalEnemyDistance = float.MaxValue;		// the maximum possible distance
			foreach(GameObject enemy in enemiesInRange){		// iterate through the list of enemies
				if(enemy != null){
					float distanceToGoal = enemy.transform.parent.GetComponent<MoveEnemy>().distanceToGoal();		// get the distanceToGoal of the current enemy
					if( (distanceToGoal < minimalEnemyDistance) && enemy != null){	// kapag yung distance to the end of the stage area (yung goal) ay mas maliit sa minimalEnemyDistance
						target = enemy;
						minimalEnemyDistance = distanceToGoal;		// set as new minimal distance.
					}
				}
			}	
		
			if (target != null) {

//		Debug.Log ( "statement: " + (Time.time - lastShotTime  ) );
			
			if (target.transform.position.x < gameObject.transform.position.x) {
							
				anim.SetBool ("enemy_at_leftSide", true);		// make the hero face left
				anim.SetBool ("enemy_at_rightSide", false);
							
				if (Time.time - lastShotTime > heroData.fireRate && checkTarget (target) ) {
					//	anim.SetTrigger("attack");
					anim.SetTrigger ("attack_left");
					playSound ();
				
					Stun(target.GetComponent<Collider2D>() );	// function shoot, the targets collider2D is used as parameter
					lastShotTime = Time.time;
				}

			}
			else if (target.transform.position.x > gameObject.transform.position.x) {
				anim.SetBool ("enemy_at_rightSide", true);		// make the hero face right
				anim.SetBool ("enemy_at_leftSide", false);

				if (Time.time - lastShotTime > heroData.fireRate && checkTarget (target) ) {
					//	anim.SetTrigger("attack");
					anim.SetTrigger ("attack");
					playSound ();
					Stun(target.GetComponent<Collider2D>() );	// function shoot, the targets collider2D is used as parameter
					lastShotTime = Time.time;
				}
			}
		} 
		// wala na yung target
		else {
// 			laserBeam.target = null;
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

	void Stun (Collider2D target){
		 bulletPrefab = heroData.bullet;

		 Vector3 startPosition = gameObject.transform.position;
		 Vector3 targetPosition = target.transform.position;

		target.transform.parent.gameObject.SendMessage ("StunEnemy");
		target.GetComponent<Animator>().enabled = false;
		Instantiate(bulletImpact_particle, targetPosition, transform.rotation);
		
		Transform healthBarTransform = target.transform.parent.FindChild("HealthBar");
		HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
		damage = elementManager.checkElement(hero_element, target.GetComponent<EnemyData>().enemyElement, currentDamage); 	// ex: fire defeats air: damage x 2
		//sDebug.Log("Damage: " + damage);
		//Debug.Log("Health before the damage: " + healthBar.currentHealth);
		healthBar.currentHealth -= Mathf.Max(damage, 0);
		//Debug.Log("after: " + healthBar.currentHealth);
		//Debug.Log("ilang bes to friend:?");
		if(healthBar.currentHealth <= 0){
			// dahil yung mismong parent na enemy gameObject ang i destroy para mawala yung lahat ng components ng enemy
			Destroy(target.transform.parent.gameObject);
			// code below, mag play ng sound ng enemy pag namatay, KUNG may sound sa gameObject na enemy
			//AudioSource audioSource = target.GetComponent<AudioSource>();
			//AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
			
			// reward the user water when the enemy is destroyed
			gameManager.water += target.GetComponent<EnemyData>().waterRewarded;
			gameManager.displayWater();

		//	StartCoroutine (Wait_sting ());
		}
		//StartCoroutine (Wait_sting ());


		//		laser.enabled = true;	
//		laser.SetPosition (0, point1.position);
//		laser.SetPosition (1, targetPosition);
			
//		 targetPosition.z = bulletPrefab.transform.position.z;		// so that the bullet would appear above the enemy
//		 startPosition.z = bulletPrefab.transform.position.z;			// bullet should appear below the hero firing it
//
//		 GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
//		 newBullet.transform.position = startPosition;
//		 LeekBulletBehavior bulletComp = newBullet.GetComponent<LeekBulletBehavior>();
//		 bulletComp.target = target.gameObject;
//		 bulletComp.startPosition = startPosition;
//		 bulletComp.targetPosition = targetPosition;

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

//	IEnumerator Wait_sting() {
//		yield return new WaitForSeconds(1);
//		laser1.enabled = false;
//		laser1.SetPosition (0, new Vector3(0f,0f,0f) );
//		laser1.SetPosition (1, new Vector3(0f,0f,0f) );
//
//		laser2.enabled = false;
//		laser2.SetPosition (0, new Vector3(0f,0f,0f) );
//		laser2.SetPosition (1, new Vector3(0f,0f,0f) );
//	}
}
