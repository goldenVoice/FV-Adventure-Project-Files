using UnityEngine;
using System.Collections;

public class OS_BulletBehavior : MonoBehaviour {

  	public GameObject hero;
  	public float speed = 10;
  	public float damage;
//  	public GameObject target;
  	public Vector3 startPosition;
  	public Vector3 targetPosition;

  	ElementManager elementManager;
  	ElementManager.Element hero_element;
	
  	private float distance;     // track the bullets position
  	private float startTime;    

  	private GameManagerBehavior gameManager;    // rewards player when they destroy the enemy

 	public GameObject bulletImpact_particle;
	float currentDamage;

	// Use this for initialization
	void Start () {
//		Debug.Log(hero.GetComponentInChildren<HeroData>());
		int currentLevel = PlayerPrefs.GetInt(hero.name + " attack");													// ex: 'Carrot attack' this is same with the shop. iisa lang format ng name para sa player prefs
		currentDamage = hero.transform.GetChild(1).GetComponent<HeroData>().attackLevels[currentLevel].damage;	// then, you look up the corresponding damage depending on the user's current level of attack upgrade
		damage = currentDamage;
		Debug.Log("hero.GetComponentInChildren<HeroData>().attackLevels[" + currentLevel + "]");
		Debug.Log("damage: " + damage);
		startTime = Time.time;
    	distance = Vector3.Distance (startPosition, targetPosition);
    	GameObject gm = GameObject.Find("GameManager");
    	gameManager = gm.GetComponent<GameManagerBehavior>();
		elementManager = (ElementManager) FindObjectOfType(typeof(ElementManager));
		hero_element = hero.transform.GetChild(1).GetComponent<HeroData>().heroElement;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other){
		EnemyData tempEnemy = other.GetComponent<EnemyData>();

		// this is for the grasshopper that can jump para hindi sya ma attack pag tumalon sya.
		if(other.name == "Grasshopper jumping"){
			// pwedeng ma damage ang grasshopper
			if(other.GetComponent<grasshopper_jump>().cannotAttack == false){	
				Instantiate(bulletImpact_particle, other.transform.position, other.transform.rotation);
				
				Transform healthBarTransform = other.transform.parent.FindChild ("HealthBar");
				HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar> ();
				Debug.Log (hero.name + " element: " + hero_element);
				// call the method checkElement, to know if the hero_element is weaker/ stronger to the enemy's element, then change the damage depending on the condition, 
				damage = elementManager.checkElement (hero_element, other.GetComponentInChildren<EnemyData> ().enemyElement, currentDamage); 	// ex: fire defeats air: damage x 2
				Debug.Log ("Damage after checkElement: " + damage);
				healthBar.currentHealth -= Mathf.Max (damage, 0);
				
				if (healthBar.currentHealth <= 0) {
					// dahil yung mismong parent na enemy gameObject ang i destroy para mawala yung lahat ng components ng enemy
					Destroy (other.transform.parent.gameObject);
					// code below, mag play ng sound ng enemy pag namatay, KUNG may sound sa gameObject na enemy
					//AudioSource audioSource = other.GetComponent<AudioSource>();
					//AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
					
					// reward the user water when the enemy is destroyed
					gameManager.water += other.GetComponent<EnemyData> ().waterRewarded;
					gameManager.displayWater ();
					
				}
			}
			else{
				// ibig sabihin naka talon si grasshopper you wont do anything. di sya pwede ma damage 
			}
		}

		else if (other.tag == "Enemy" && tempEnemy.insectPath == EnemyData.pathWay.walking 
		    	|| other.GetComponent<EnemyData>().insectPath == EnemyData.pathWay.both) {					// also damage those insects that are both land/air ex: locusts

			Instantiate(bulletImpact_particle, other.transform.position, other.transform.rotation);

			Transform healthBarTransform = other.transform.parent.FindChild ("HealthBar");
			HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar> ();
			Debug.Log (hero.name + " element: " + hero_element);
			// call the method checkElement, to know if the hero_element is weaker/ stronger to the enemy's element, then change the damage depending on the condition, 
			damage = elementManager.checkElement (hero_element, other.GetComponentInChildren<EnemyData> ().enemyElement, currentDamage); 	// ex: fire defeats air: damage x 2
			Debug.Log ("Damage after checkElement: " + damage);
			healthBar.currentHealth -= Mathf.Max (damage, 0);
			
			if (healthBar.currentHealth <= 0) {
				// dahil yung mismong parent na enemy gameObject ang i destroy para mawala yung lahat ng components ng enemy
				Destroy (other.transform.parent.gameObject);
				// code below, mag play ng sound ng enemy pag namatay, KUNG may sound sa gameObject na enemy
				//AudioSource audioSource = other.GetComponent<AudioSource>();
				//AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
				
				// reward the user water when the enemy is destroyed
				gameManager.water += other.GetComponent<EnemyData> ().waterRewarded;
				gameManager.displayWater ();

			}
		}


	}
}
