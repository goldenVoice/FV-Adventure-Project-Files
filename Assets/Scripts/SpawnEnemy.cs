using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Serializable: you can change values in the inspector
[System.Serializable]
public class Wave {
	public GameObject[] enemies;
  	public float spawnInterval = 2;
  	public float timerDelay = 2;
  	public float timeBetweenWaves;
	public ElementManager.Element WaveElement;	
}

public class SpawnEnemy : MonoBehaviour {

  	// for the moving of the enemies
  	public GameObject[] waypoints;
	
  	public GameObject nextWaveButton;	// reference to the actual nextWavebutton gameObject
  	private GameObject nextWave;     //  private variable to manipulate the nextWavebutton gameObject
  	public GameObject startPosition;
  	private MoveEnemy MoveTheEnemy;

	public SpriteManager nextWaveIndicator;

 	GameObject prefab;

	bool appearAnimation_finished = false;
	int timeCounter = 0;

	public int enemyLevel;

  	//  for the waves
  	public Wave[] waves;
//  	public int timeBetweenWaves = 5;

  	private GameManagerBehavior gameManager;
//	private ElementManager elementManager;

  	private float lastSpawnTime;
  	private int enemiesSpawned = 0;

  	private int sortingCounter = 0;

	float timeInterval;
  	private int enemyIndex = 0;

  	float hbgCounter = 1;
  	float hbCounter = 0;

	private LibraryChecker libraryChecker;
	string toRemove = "(Clone)";

	// Use this for initialization
	void Start () {
       	lastSpawnTime = Time.time;   // set lastSpawnTime to current time
	   	gameManager = (GameManagerBehavior)FindObjectOfType(typeof(GameManagerBehavior));
//		elementManager = (ElementManager) FindObjectOfType(typeof(ElementManager));
		nextWaveIndicator  = (SpriteManager) FindObjectOfType(typeof(SpriteManager));
//		print("next wave object: " + nextWaveIndicator.gameObject);
		libraryChecker = (LibraryChecker) GameObject.Find ("List_insects").GetComponent<LibraryChecker> ();
	 }
	
	// Update is called once per frame
	void Update () {
  
		SpawnTheEnemy();

	}

	void SpawnTheEnemy(){
		int currentWave = gameManager.wave;
//		Debug.Log ("currentWave number: " + currentWave);
//		Debug.Log ("waves.length: " + waves.Length);
//		Debug.Log ("health: " + gameManager.health);
//		Debug.Log ("" + gameManager.health);
//
		// check kung yung current wave is hindi pa huling wave
		if (currentWave < waves.Length) {
			timeInterval = (Time.time) - lastSpawnTime; 	
			float spawnInterval = waves[currentWave].spawnInterval;	
			if (timeInterval > spawnInterval && enemyIndex < waves[currentWave].enemies.Length) {			// you havent spawned all enemies for this wave, 
	
				lastSpawnTime = Time.time;
				GameObject newEnemy = (GameObject)
					Instantiate(waves[currentWave].enemies[enemyIndex]);									// instantiate the current enemyPrefab using enemyIndex, to determine which enemy to instantiate
//				Debug.Log("new enemy name: " + newEnemy.name);

				string newEnemy_name = newEnemy.name.Substring(0, newEnemy.name.Length - toRemove.Length);	// subtract the length of "(Clone)" to the overall size of the enemy's name, then use substr, then nakuha mo na yung original name ng enemy
	//			Debug.Log("new enemy name after trimmed: " + newEnemy_name);

				// iterate through the list of ALL the insects then see if this insect (newEnemy) is already unlocked
				foreach(InsectInfo insect in libraryChecker.insects){
					if(insect.insectPrefab.name == newEnemy_name){
						if(!insect.unlockInLibrary){					// if di pa unlock sa library
							libraryChecker.unlockInsect(newEnemy_name);	// unlock here,
							insect.unlockInLibrary = true;				// then set to true para di na mag paulet ulet
						}
						break;
					}
				}
				enemyIndex++;		// increment to instantiate the next enemy, next update frame
				// set the element of this enemy to the current wave element
				newEnemy.GetComponentInChildren<EnemyData>().enemyElement = waves[currentWave].WaveElement;

				// set the speed & HP of the enemy here. (pate DAMAGE on hard mode)
				EnemyData enemyData = newEnemy.transform.GetChild(0).GetComponent<EnemyData>();				//get the enemy data from the child
				// di na nag babago ang speed so i commented the line below
				//newEnemy.GetComponent<MoveEnemy>().speed = enemyData.enemySpeed[enemyLevel].speed;			// get the enemySpeed array/list then access the appropriate speed using the 'enemyLevel' as the index

				HealthBar enemyHealthBar = newEnemy.transform.GetChild(2).GetComponent<HealthBar>();		// get the enemyHealth array/list then 
//				Debug.Log(newEnemy.transform.GetChild(2).GetComponent<HealthBar>());
				enemyHealthBar.maxHealth = enemyData.enemyHP[enemyLevel].health;							// access the appropriate heatlh using the 'enemyLevel' as the index
				enemyHealthBar.currentHealth = enemyHealthBar.maxHealth;

				// code to get child of an object
				// if gumamit ng '/' mag tra-travel yung code sa pag hahanap ng childs of the head sa hierarchy,
				// syntax:
				// <type> <var name> = gameObject.transform.Find("/shoulder/arms");
				Transform healthBarBackground =  newEnemy.transform.Find("HealthBarBackground"); 
				Transform healthBar =  newEnemy.transform.Find("HealthBar");
				
				if(waves[currentWave].WaveElement == ElementManager.Element.Air){
					newEnemy.transform.GetChild(3).gameObject.SetActive(true);
//					enemy_element = newEnemy.transform.GetChild(3).gameObject.transform;
				}
				else if(waves[currentWave].WaveElement == ElementManager.Element.Fire){
					newEnemy.transform.GetChild(4).gameObject.SetActive(true);
//					enemy_element = newEnemy.transform.GetChild(4).gameObject.transform;
				}
				else if(waves[currentWave].WaveElement == ElementManager.Element.Water){
					newEnemy.transform.GetChild(5).gameObject.SetActive(true);
//					enemy_element = newEnemy.transform.GetChild(5).gameObject.transform;
				}
				else if(waves[currentWave].WaveElement == ElementManager.Element.Earth){
					newEnemy.transform.GetChild(6).gameObject.SetActive(true);
					//					enemy_element = newEnemy.transform.GetChild(5).gameObject.transform;
				}

				// before changing the z coordinates, store the orig x and y values ng healthbarBackground
				float hbg_x = healthBarBackground.transform.localPosition.x;
				float hbg_y = healthBarBackground.transform.localPosition.y;
				
				// pate na rin ng healthbar
				float hb_x = healthBar.transform.localPosition.x;
				float hb_y = healthBar.transform.localPosition.y;

				// 
				// saka i-store yung z coordinatess
				healthBarBackground.transform.localPosition = new Vector3(hbg_x, hbg_y, hbgCounter+= 0.1f);
				healthBar.transform.localPosition =  new Vector3(hb_x, hb_y, hbCounter+= 0.1f);
				
				// this is for the z coordinates. 
				hbgCounter += 1;
				hbCounter += 1;
				
				newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
			//	enemiesSpawned++;
			}

			// im putting this here para isang ROAD lang yung nag ha handle ng pag manipulate/ show ng next wave button
			if(gameObject.name == "Road"){
				if (enemyIndex == waves[currentWave].enemies.Length &&  // check kung na spawn na yung max na dame ng enemies
				    timeInterval >= waves[currentWave].timerDelay ) {	// check if the time that passed is greater than the defined timerDelay before the timer shows up
					if( !nextWaveButton.activeInHierarchy && currentWave < waves.Length - 1){ 		    // if next wave button is not yet in the hierarchy meaning it is not yet instantiated, and if its not yet the last wave
						nextWaveButton.SetActive(true);
						nextWaveButton.GetComponentInChildren<Text>().text =  "" + waves[currentWave].timeBetweenWaves;
					}
					else if(nextWaveButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle") // check if the button is already idle
					        && !appearAnimation_finished){  																// and if the appear animation is not yet finished
						appearAnimation_finished = true;	// set it to true para di na daanan tong if statement na to				       																	
						lastSpawnTime = Time.time;
						waves[currentWave].timerDelay  = 0;
						timeInterval = 0;				    // to start the counting						
					}
					// check if the nextwavebutton is idle
					if(nextWaveButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle") &&
					   currentWave < waves.Length){
						//					Debug.Log (timeInterval);
						// show the next wave element
						nextWaveIndicator.displayNextElement(waves, currentWave + 1);	// this is currentWave + 1, kase nga idi display yung element ng NEXT wave, hindi ng current
						nextWaveIndicator.gameObject.GetComponent<Image>().enabled = true;
						Debug.Log("this is the time interval: " + timeInterval);
						if(timeInterval >= 1){ // check if one second has passed
							waves[currentWave].timeBetweenWaves -= 1;	// if so play the pulsate animation
							lastSpawnTime = Time.time;
							waves[currentWave].timerDelay  = 1.5f;			// this will automatic count by 1/
							timeInterval = 0;		// switch back to 0 to start counting again
							
							nextWaveButton.GetComponentInChildren<Text>().text =  "" + waves[currentWave].timeBetweenWaves;
							nextWaveButton.GetComponent<Animator>().SetTrigger("pulsate");
							
							//						kung gusto mo ng 3 seconds remaining tas gawing pula. gamitin mo tong condition nato
							//						if(timeBetweenWaves <= 3 && timeBetweenWaves > 0){
							//							ToDo: CHANGE TEXT COLOR to red
							//   					}
							
							// time for the next wave!
							if(waves[currentWave].timeBetweenWaves <= 0){
								NextWave();		// start the next wave of enemies
								Debug.Log("hey next wave na");
								if(GameObject.Find("Road2") != null){
									Debug.Log("hey may 2nd road na?!");
									
									GameObject.Find("Road2").SendMessage("NextWave_2ndroad");
								}
							}
						}
					}
					else if(currentWave == waves.Length - 1){
						gameManager.wave++;
					}
				}
			}
//			if(gameObject.name == "Road2"){
//
//				if(timeInterval >= 1){ 							// check if one second has passed
//					waves[currentWave].timeBetweenWaves -= 1;	
//					lastSpawnTime = Time.time;
//					timeInterval = 0;							// switch back to 0 to start counting again
//
//					if(waves[currentWave].timeBetweenWaves <= 0){
//						NextWave_2ndroad();		// start the next wave of enemies
//					}
//				}
//			}
		}
		// if eto na yung last wave, then di pa 0 yung health ng player. NANALO SYA
		else if ((gameManager.health > 0 && GameObject.FindGameObjectWithTag("Enemy") == null) && gameObject.name == "Road"){
//			Debug.Log ("forever");
			gameManager.gameOver = true;    // here, gameOver means tapos na yung laro, 
			gameManager.didPlayerWin(true); // dito iche-check kung nanalo sya, which is true
		}
		if (gameManager.health <= 0 && gameObject.name == "Road"){  // tapos na yung wave, at wala na deng lives yung user.
			gameManager.gameOver = true;
			gameManager.didPlayerWin(false);   // TALO SYA			
		}

	}

	public void NextWave(){
		// play the animator
		nextWaveButton.GetComponent<Animator>().SetTrigger("next_wave");
//		nextWaveButton.GetComponentInChildren<Text>().text =  "" + waves[currentWave+1].timeBetweenWaves;
		nextWaveButton.SetActive(false);
		nextWaveIndicator.gameObject.GetComponent<Image>().enabled = false;
		gameManager.wave++;
		hbgCounter = 1;		// balik sa 1 ule para di umabot sa mataas na bilang yung .z ng gameObject, baka mag log
		hbCounter = 0;    // BALIK SA 0, just like at the top
		gameManager.displayWave(gameManager.wave);
		enemiesSpawned = 0;
		enemyIndex = 0;
		timeInterval = 0;
		lastSpawnTime = Time.time;

	}

	// still the same code, without incrementing again, kaya nag pro proceed sa next next wave agad eh
	public void NextWave_2ndroad(){	
		// play the animator
		nextWaveButton.GetComponent<Animator>().SetTrigger("next_wave");
		//		nextWaveButton.GetComponentInChildren<Text>().text =  "" + waves[currentWave+1].timeBetweenWaves;
		nextWaveButton.SetActive(false);
		nextWaveIndicator.gameObject.GetComponent<Image>().enabled = false;
//		gameManager.wave++;
		hbgCounter = 1;		// balik sa 1 ule para di umabot sa mataas na bilang yung .z ng gameObject, baka mag log
		hbCounter = 0;    // BALIK SA 0, just like at the top
		gameManager.displayWave(gameManager.wave);
		enemiesSpawned = 0;
		enemyIndex = 0;
		timeInterval = 0;
		lastSpawnTime = Time.time;
		
	}

}
