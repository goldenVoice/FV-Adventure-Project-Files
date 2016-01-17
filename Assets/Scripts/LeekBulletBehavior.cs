using UnityEngine;
using System.Collections;

public class LeekBulletBehavior : MonoBehaviour {

  	public GameObject hero;
  	public float speed = 10;
  	public float damage;
  	public GameObject target;
  	public Vector3 startPosition;
  	public Vector3 targetPosition;

//	public GameObject target;			// use this to reference the real enemy. so you can track its moving direction

  	ElementManager elementManager;
  	ElementManager.Element hero_element;
	
  	private float distance;     // track the bullets position
  	private float startTime;    

  	private GameManagerBehavior gameManager;    // rewards player when they destroy the enemy

 	public GameObject bulletImpact_particle;

	float timeCounter;
	float DPS_countingInterval;
	public float DPSinterval;		// ilang seconds bago mag damage ule? Lets say, every 0.7 secs dumadamage
	public float maxSeconds;		// maximum duration of the tornado

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
		DPS_countingInterval = Time.time;
		timeCounter = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
    	float timeInterval = Time.time - startTime;

		// dps countinginterval ang nag co count kung kelan mag da damage ang tornado base on dps interval
		DPS_countingInterval = Time.time - timeCounter;

    	// calculate the new bullets position then interpolate using vector3.Lerp
    	gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);
    	// baka dito ilagay  yung code for angular velocity, para umikot ikot ang bullet,
    	// if(bulletIsIkotIkot)  
		//		Debug.Log("ANYOOOONE?");
		
    	if(gameObject.transform.position.Equals(targetPosition) ){
      	// if you reach the targetPosition you verify if the target exists
       		
			// continually check for the time of dps if tapos na. if yes, destroy
			if(timeInterval >= maxSeconds){
				Destroy(gameObject);
			}

			if (target == null){

				Destroy(gameObject);
			}

			if(target != null){
				gameObject.transform.position = target.transform.parent.position;
//				if()

				if(DPS_countingInterval >= DPSinterval){		// maxSeconds = 2; dps interval = 0.5; pero ang dame lang ng bes na mababawasan sya ay 3 times. hindi 4 times (kase 2 / 0.5 = 4) there something about this computation na ganon yung nangyayare. and im too lazy now to figure out why

					Transform healthBarTransform = target.transform.parent.FindChild("HealthBar");
					HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
					Debug.Log(hero.name + " element: " + hero_element);
					// call the method checkElement, to know if the hero_element is weaker/ stronger to the enemy's element, then change the damage depending on the condition, 
					damage = elementManager.checkElement(hero_element, target.GetComponentInChildren<EnemyData>().enemyElement, currentDamage); 	// ex: fire defeats air: damage x 2
					Debug.Log ("Damage after checkElement: " + damage);
					
					Instantiate(bulletImpact_particle, gameObject.transform.position, transform.rotation);
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
						Destroy(gameObject);
					}

					timeCounter = Time.time;	// restart time counter to start counting again
				}
        	}
			
			// do not yer destroy the gameobject
        	//Destroy(gameObject);  // destroy the bullet
    	}
	}

	public void ApplyDamage(Collider2D target){
			
			Transform healthBarTransform = target.transform.parent.FindChild("HealthBar");
			HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
			Debug.Log(hero.name + " element: " + hero_element);
			// call the method checkElement, to know if the hero_element is weaker/ stronger to the enemy's element, then change the damage depending on the condition, 
			damage = elementManager.checkElement(hero_element, target.GetComponentInChildren<EnemyData>().enemyElement, damage); 	// ex: fire defeats air: damage x 2
			Debug.Log ("Damage after checkElement: " + damage);
			
			Instantiate(bulletImpact_particle, gameObject.transform.position, transform.rotation);
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
				Destroy(gameObject);
			}
			
			timeCounter = Time.time;	// restart time counter to start counting again
	}

}
