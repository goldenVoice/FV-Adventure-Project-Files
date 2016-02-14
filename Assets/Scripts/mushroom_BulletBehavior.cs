using UnityEngine;
using System.Collections;

public class mushroom_BulletBehavior : MonoBehaviour {

  	public GameObject hero;
  	public float speed = 10;
  	public float damage;
  	public GameObject target;
  	public Vector3 startPosition;
  	public Vector3 targetPosition;

  	ElementManager elementManager;
  	ElementManager.Element hero_element;
	
  	private float distance;     // track the bullets position
  	private float startTime;    

  	private GameManagerBehavior gameManager;    // rewards player when they destroy the enemy

 	public GameObject bulletImpact_particle;

	string currentProfile;
	void Awake(){
		
		currentProfile = PlayerPrefs.GetString ("currentProfile");
	}
	// Use this for initialization
	void Start () {
//		Debug.Log(hero.GetComponentInChildren<HeroData>());
		int currentLevel = PlayerPrefs.GetInt(currentProfile + hero.name + " attack");													// ex: 'Carrot attack' this is same with the shop. iisa lang format ng name para sa player prefs
		float currentDamage = hero.transform.GetChild(1).GetComponent<HeroData>().attackLevels[currentLevel].damage;	// then, you look up the corresponding damage depending on the user's current level of attack upgrade
		damage = currentDamage;
		Debug.Log("hero.GetComponentInChildren<HeroData>().attackLevels[" + currentLevel + "]");
		Debug.Log("start pos from the bullet: " + startPosition);
		startTime = Time.time;
    	distance = Vector3.Distance (startPosition, targetPosition);
    	GameObject gm = GameObject.Find("GameManager");
    	gameManager = gm.GetComponent<GameManagerBehavior>();
		elementManager = (ElementManager) FindObjectOfType(typeof(ElementManager));
		hero_element = hero.transform.GetChild(1).GetComponent<HeroData>().heroElement;
	
	}
	
	// Update is called once per frame
	void Update () {
    	
		float timeInterval = Time.time - startTime;
//			Debug.Log("should show kase tapos na!");

    	// calculate the new bullets position then interpolate using vector3.Lerp
    	gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);
    	// baka dito ilagay  yung code for angular velocity, para umikot ikot ang bullet,
    	// if(bulletIsIkotIkot)  

    		if(gameObject.transform.position.Equals(targetPosition) ){
      		// if you reach the targetPosition you verify if the target exists
       			if(target != null){
          			Transform healthBarTransform = target.transform.parent.FindChild("HealthBar");
          			HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
          			Instantiate(bulletImpact_particle, targetPosition, transform.rotation);
					Debug.Log(hero.name + " element: " + hero_element);
		  			// call the method checkElement, to know if the hero_element is weaker/ stronger to the enemy's element, then change the damage depending on the condition, 
		  			damage = elementManager.checkElement(hero_element, target.GetComponentInChildren<EnemyData>().enemyElement, damage); 	// ex: fire defeats air: damage x 2
		  			Debug.Log ("Damage after checkElement: " + damage);
		  			healthBar.currentHealth -= Mathf.Max(damage, 0);
       			
					if( !(target.GetComponent<EnemyData>().slowed) && hero.name == "Onion" ){	// check if the var slowed is false,
						float enemySpeed = target.transform.parent.GetComponent<MoveEnemy>().speed;
						target.transform.parent.GetComponent<MoveEnemy>().speed -= (enemySpeed * 0.2f);		// slow the enemy by 20%
						target.GetComponent<EnemyData>().slowed = true;
					}

          			if(healthBar.currentHealth <= 0){
						// dahil yung mismong parent na enemy gameObject ang i destroy para mawala yung lahat ng components ng enemy
            			Destroy(target.transform.parent.gameObject);
            			// code below, mag play ng sound ng enemy pag namatay, KUNG may sound sa gameObject na enemy
            			//AudioSource audioSource = target.GetComponent<AudioSource>();
            			//AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

            			// reward the user water when the enemy is destroyed
            			gameManager.water += target.GetComponent<EnemyData>().waterRewarded;
            			gameManager.displayWater();
          			}
        		}
        		Destroy(gameObject);  // destroy the bullet
			}
	}

}
