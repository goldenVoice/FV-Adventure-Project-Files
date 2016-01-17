using UnityEngine;
using System.Collections;

public class LaserBehavior : MonoBehaviour {

  	public GameObject hero;
  	public float damage;
  	public GameObject target;

  	ElementManager elementManager;
  	ElementManager.Element hero_element;
	

  	private GameManagerBehavior gameManager;    // rewards player when they destroy the enemy

 	public GameObject bulletImpact_particle;

	float currentDamage;

	public LaserBeam laserBeam1;
	// Use this for initialization
	void Start () {
		int currentLevel = PlayerPrefs.GetInt(hero.name + " attack");													// ex: 'Carrot attack' this is same with the shop. iisa lang format ng name para sa player prefs
		currentDamage = hero.transform.GetChild(1).GetComponent<HeroData>().attackLevels[currentLevel].damage;	// then, you look up the corresponding damage depending on the user's current level of attack upgrade
		damage = currentDamage;
		Debug.Log("hero.GetComponentInChildren<HeroData>().attackLevels[" + currentLevel + "]");
		Debug.Log("damage: " + damage);
    	GameObject gm = GameObject.Find("GameManager");
    	gameManager = gm.GetComponent<GameManagerBehavior>();
		elementManager = (ElementManager) FindObjectOfType(typeof(ElementManager));
		hero_element = hero.transform.GetChild(1).GetComponent<HeroData>().heroElement;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ApplyDamage(GameObject target){
			
			Transform healthBarTransform = target.transform.parent.FindChild("HealthBar");
			HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
			Debug.Log(hero.name + " element: " + hero_element);
			// call the method checkElement, to know if the hero_element is weaker/ stronger to the enemy's element, then change the damage depending on the condition, 
			damage = elementManager.checkElement(hero_element, target.GetComponentInChildren<EnemyData>().enemyElement, currentDamage); 	// ex: fire defeats air: damage x 2
			Debug.Log ("Damage after checkElement: " + damage);
			
			Instantiate(bulletImpact_particle, target.transform.position, transform.rotation);
			healthBar.currentHealth -= Mathf.Max(damage, 0);
			
			if(healthBar.currentHealth <= 0){
				// dahil yung mismong parent na enemy gameObject ang i destroy para mawala yung lahat ng components ng enemy
				Destroy(target.transform.parent.gameObject);
				// code below, mag play ng sound ng enemy pag namatay, KUNG may sound sa gameObject na enemy
				//AudioSource audioSource = target.GetComponent<AudioSource>();
				//AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
				
				// reward the user water when the enemy is destroyed
				gameManager.water += target.GetComponent<EnemyData>().waterRewarded;
				gameManager.displayWater();
				// disable laser beam, wala ka ng titirahin
				laserBeam1.enabled = false;
				laserBeam1.GetComponent<LineRenderer>().enabled = false;
			}
			
//			timeCounter = Time.time;	// restart time counter to start counting again
	}

}
