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

	// Use this for initialization
	void Start () {
		elementManager = (ElementManager) FindObjectOfType(typeof(ElementManager));
		hero_element = hero.transform.GetChild(1).GetComponent<HeroData>().heroElement;
    	GameObject gm = GameObject.Find("GameManager");
		gameManager = gm.GetComponent<GameManagerBehavior>();
		
	}

	void FixedUpdate(){


	}
	// Update is called once per frame
	void Update () {
		enemies = Physics2D.OverlapCircleAll(range_center.position, range_radius.radius, enemyLayerMask);

		for (int i = 0; i < enemies.Length; i++){
			Debug.Log(enemies[i].name);
		}
	}

	void OnTriggerEnter2D (Collider2D other){

			// if walking ang enemy. deduct. kase 
			if(other.GetComponent<EnemyData>().insectPath == EnemyData.pathWay.walking){
			Transform healthBarTransform = other.transform.parent.FindChild("HealthBar");
			HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
			damage = elementManager.checkElement(hero_element, other.GetComponent<EnemyData>().enemyElement, damage); 	// ex: fire defeats air: damage x 2
			Debug.Log("Damage: " + damage);
			Debug.Log("Health before the damage: " + healthBar.currentHealth);
			healthBar.currentHealth -= Mathf.Max(damage, 0);
			Debug.Log("after: " + healthBar.currentHealth);
			Debug.Log("ilang bes to friend:?");
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

			Destroy(gameObject);
		}
	}
}
