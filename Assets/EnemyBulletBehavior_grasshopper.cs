using UnityEngine;
using System.Collections;

public class EnemyBulletBehavior_grasshopper : MonoBehaviour {

	public GameObject enemy;
	public float speed = 10;
	public float damage;
	public GameObject target;
	public Vector3 startPosition;
	public Vector3 targetPosition;

	ElementManager elementManager;
	ElementManager.Element enemyElement;

	public int currentEnemyLevel;

	private GameManagerBehavior gameManager;    // rewards player when they destroy the enemy

	private float distance;     // track the bullets position
	private float startTime;    
	
	public GameObject bulletImpact_particle;

	//int damage;

	// Use this for initialization
	void Start () {

		float currentDamage = enemy.transform.GetChild(0).GetComponent<EnemyData>().enemyAttack[currentEnemyLevel].attack;
		damage = currentDamage;
		startTime = Time.time;
		distance = Vector3.Distance (startPosition, targetPosition);

		GameObject gm = GameObject.Find("GameManager");
		gameManager = gm.GetComponent<GameManagerBehavior>();
		elementManager = (ElementManager) FindObjectOfType(typeof(ElementManager));
		enemyElement = enemy.transform.GetChild(0).GetComponent<EnemyData>().enemyElement;

	}
	
	// Update is called once per frame
	void Update () {
		float timeInterval = Time.time - startTime;
		gameObject.transform.position = Vector3.Lerp (startPosition, targetPosition, timeInterval * speed / distance);

		if (gameObject.transform.position.Equals (targetPosition)) {
			if (target != null) {
				Transform healthBarTransform = target.transform.FindChild ("HealthBar");
				HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar> ();
				Instantiate (bulletImpact_particle, targetPosition, transform.rotation);
				damage = elementManager.checkElement (enemyElement, target.GetComponentInChildren<HeroData> ().heroElement, damage); 	// ex: fire defeats air: damage x 2
				healthBar.currentHealth -= Mathf.Max (damage, 0);

				if (healthBar.currentHealth <= 0) {
					// dahil yung mismong parent na enemy gameObject ang i destroy para mawala yung lahat ng components ng enemy

					target.transform.FindChild("x mark").gameObject.SetActive(true);
					target.transform.FindChild("x mark").gameObject.renderer.enabled = false;

					// destroys the hero and removes from the pothole
					target.transform.FindChild("x mark").gameObject.SendMessage("RemoveKilledHero");
					//Destroy (target.gameObject);
//					Debug.Log(target.transform.FindChild("x mark").gameObject);


					// code below, mag play ng sound ng enemy pag namatay, KUNG may sound sa gameObject na enemy
					//AudioSource audioSource = target.GetComponent<AudioSource>();
					//AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
					
					// reward the user water when the enemy is destroyed
					//	gameManager.water += target.GetComponent<EnemyData>().waterRewarded;
					//	gameManager.displayWater();
				}
				// dito mag lagay in case of special cases saten halimbawa 2x dadget sponsor sa inyo ha 
			}
			Destroy (gameObject);  // destroy the bullet
			
		}
	}

}
