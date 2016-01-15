using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarrotCannon_Impact_Behavior : MonoBehaviour {

	public Transform range_center;
	public CircleCollider2D range_radius;
	public LayerMask enemyLayerMask;

  	public GameObject hero;
  	public float speed = 10;
  	public float damage;
  	public GameObject target;
  	
  	ElementManager elementManager;
  	ElementManager.Element hero_element;

  	private GameManagerBehavior gameManager;    // rewards player when they destroy the enemy

 	public GameObject bulletImpact_particle;

	private Collider2D[] enemies;

	float currentDamage;
	
	private ParticleSystem cannonParticle; 		// to be used to track if the cannon impact is still not destroyed
	// Use this for initialization
	void Start () {
		int currentLevel = PlayerPrefs.GetInt(hero.name + " attack");													// ex: 'Carrot attack' this is same with the shop. iisa lang format ng name para sa player prefs
		currentDamage = hero.transform.GetChild(1).GetComponent<HeroData>().attackLevels[currentLevel].damage;	// then, you look up the corresponding damage depending on the user's current level of attack upgrade
		damage = currentDamage;
//		Debug.Log("hero.GetComponentInChildren<HeroData>().attackLevels[" + currentLevel + "]");
//		Debug.Log("damage: " + damage);

		elementManager = (ElementManager) FindObjectOfType(typeof(ElementManager));
		hero_element = hero.transform.GetChild(1).GetComponent<HeroData>().heroElement;
    	GameObject gm = GameObject.Find("GameManager");
		gameManager = gm.GetComponent<GameManagerBehavior>();

		
	}

	void FixedUpdate(){


	}
	// Update is called once per frame
	void Update () {
//		Debug.Log (CarrotCannonImpactParticle);
//		if(CarrotCannonImpactParticle == null) {
//		//	Destroy(gameObject);
//			Debug.Log("he was destroyed. but im  still alive");
//		}
	}

	void OnTriggerStay2D (Collider2D other){

//			// if walking ang enemy. deduct. kase 
//			if(other.GetComponent<EnemyData>().insectPath == EnemyData.pathWay.walking){
//			Destroy(gameObject);
//		}
	}

	public void ApplyDamage(Collider2D other){
		//Debug.Log ("name in applyDamage: " + other.name);
		Transform healthBarTransform = other.transform.parent.FindChild("HealthBar");
		HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
		damage = elementManager.checkElement(hero_element, other.GetComponent<EnemyData>().enemyElement, currentDamage); 	// ex: fire defeats air: damage x 2
		//sDebug.Log("Damage: " + damage);
		//Debug.Log("Health before the damage: " + healthBar.currentHealth);
		healthBar.currentHealth -= Mathf.Max(damage, 0);
		//Debug.Log("after: " + healthBar.currentHealth);
		//Debug.Log("ilang bes to friend:?");
		if(healthBar.currentHealth <= 0){
			// dahil yung mismong parent na enemy gameObject ang i destroy para mawala yung lahat ng components ng enemy
			Destroy(other.transform.parent.gameObject);
			// code below, mag play ng sound ng enemy pag namatay, KUNG may sound sa gameObject na enemy
			//AudioSource audioSource = target.GetComponent<AudioSource>();
			//AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
			
			// reward the user water when the enemy is destroyed
			gameManager.water += other.GetComponent<EnemyData>().waterRewarded;
			gameManager.displayWater();
		}
		// set a condition in
		


	}
}
