using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {

	bool inventoryEnabled;
	bool poisonActivate = false;

	bool second1;
	bool second2;
	bool second3;
	bool second4;
	bool second5;
	bool second6;
	bool second7;
	bool second8;
	bool second9;
	bool second10;
	
	//public Toggle inventoryToggle;

	int counter;

	float timeCounter;
	float lastActivateTime;

	public ParticleSystem poisonParticle;

	public Button LifePotionButton;
	private Text LifePotionQty;

	public Button poisonButton;
	private Text poisonQty;

	public Button waterBoosterButton;
	private Text waterBoosterQty;

	public Button heroPotionButton;
	private Text heroPotionQty;

	private GameObject[] enemyArray;
	public List<GameObject> enemies;

	private GameManagerBehavior gameManager;

	GameObject inventoryPanel;
	
	// Use this for initialization
	void Start () {

		counter = 0;

		bool second1 = true;
		bool second2 = true;
		bool second3 = true;
		bool second4 = true;
		bool second5 = true;
		bool second6 = true;
		bool second7 = true;
		bool second8 = true;
		bool second9 = true;
		bool second10 = true;

		timeCounter = 0;

		gameManager = (GameManagerBehavior) GameObject.Find ("GameManager").GetComponent<GameManagerBehavior>();
		inventoryEnabled = false;

		inventoryPanel = gameObject.transform.GetChild(0).gameObject;

		// ilagay sa qtyText kung ilang boosters meron ang user
		poisonQty = poisonButton.transform.GetChild(0).GetChild(0).GetComponent<Text>();
		poisonQty.text = PlayerPrefs.GetInt("poison qty:").ToString();

		LifePotionQty = LifePotionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>();
		LifePotionQty.text = PlayerPrefs.GetInt("life potion qty:").ToString();

		waterBoosterQty = waterBoosterButton.transform.GetChild(0).GetChild(0).GetComponent<Text>();
		waterBoosterQty.text = PlayerPrefs.GetInt("water booster qty:").ToString();

		heroPotionQty = heroPotionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>();
		heroPotionQty.text = PlayerPrefs.GetInt("hero potion qty:").ToString();

		if(PlayerPrefs.GetInt("poison qty:") == 0){
			// disable button kase 0 yung booster
			poisonButton.interactable = false;
		}
		if(PlayerPrefs.GetInt("life potion qty:") == 0){
			// disable button kase 0 yung booster
			LifePotionButton.interactable = false;
		}
		if(PlayerPrefs.GetInt("water booster qty:") == 0){
			// disable button kase 0 yung booster
			waterBoosterButton.interactable = false;
		}
		if(PlayerPrefs.GetInt("hero potion qty:") == 0){
			// disable button kase 0 yung booster
			heroPotionButton.interactable = false;
		}

		// code to check if HERO POTION should be visible, (sa hard lang kse dapat makita)
		string curLevel = Application.loadedLevelName;
		
		string stageNumber;
		
		if((Application.loadedLevelName.Length) == 10){		// kapag 10 characters na ang meron, meaning nasa last stage na sya. "Level 10-1" oh diba 10 characters na?
			stageNumber = curLevel.Substring (6, 2) ;
		}
		else{
			stageNumber = curLevel.Substring (6, 1) ;
		}
		
		int stageNum = int.Parse (stageNumber);
		
		if (stageNum <= 7) {
			transform.GetChild(0).GetChild(3).gameObject.SetActive (false);
		}


	}
	
	// Update is called once per frame
	void Update () {
	
		if(poisonActivate){
			timeCounter = Time.time - lastActivateTime;
			poisonButton.interactable = false;
			int timeInSeconds = Mathf.RoundToInt(timeCounter);

			if(timeInSeconds == 1 && second1){
				// condition above is for every second saka lang mag bawas ng hp sa enemy
				Debug.Log("time in seconds: " + timeInSeconds);
				poison();
				second1 = false;
			}
			else if(timeInSeconds == 2 && second2){
				Debug.Log("time in seconds: " + timeInSeconds);
				poison();
				second2 = false;
			}
			else if(timeInSeconds == 3 && second3){
				Debug.Log("time in seconds: " + timeInSeconds);
				poison();
				second3 = false;
			}
			else if(timeInSeconds == 4 && second4){
				Debug.Log("time in seconds: " + timeInSeconds);
				poison();
				second4 = false;
			}
			else if(timeInSeconds == 5 && second5){
				Debug.Log("time in seconds: " + timeInSeconds);
				poison();
				second5 = false;
			}
			else if(timeInSeconds == 6 && second6){
				Debug.Log("time in seconds: " + timeInSeconds);
				poison();
				second6 = false;
			}
			else if(timeInSeconds == 7 && second7){
				Debug.Log("time in seconds: " + timeInSeconds);
				poison();
				second7 = false;
			}
			else if(timeInSeconds == 8 && second8){
				Debug.Log("time in seconds: " + timeInSeconds);
				poison();
				second8 = false;
			}
			else if(timeInSeconds == 9 && second9){
				Debug.Log("time in seconds: " + timeInSeconds);
				poison();
				second9 = false;
			}
			else if(timeInSeconds == 10 && second10){
				Debug.Log("time in seconds: " + timeInSeconds);
				poison();
				second10 = false;
			}

//			Debug.Log(timeCounter);
			if(timeCounter >= 10){
				Debug.Log("10 sec passed. poison dissipating...");
				poisonActivate = false;
				int qty = int.Parse(poisonQty.text);
				if(qty > 0){
					poisonButton.interactable = true;
				}
				counter = 0;		// restore ule sa 0.

				// clear the list of heroes para pag pinindot bago ule yung list ng ipo poison
				enemies.Clear();
			}
		}
	}

	public void showInventory(){
		if(!inventoryPanel.activeSelf){	// meaning naka hide ang inventory
//			gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;			
//			inventoryToggle.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
			inventoryPanel.SetActive(true);
		}
		else if(inventoryPanel.activeSelf){				// if naka off ang toggle
//			inventoryToggle.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
//			inventoryToggle.transform.GetChild(0).GetComponent<Image>().enabled = true;
			inventoryPanel.SetActive(false);
		}
	}

//	public void ActivateSlow(){	// the function of slow booster
//		slowActivate = true;
//		enemies = GameObject.FindGameObjectsWithTag("Enemy");
//
//		foreach(GameObject enemy in enemies){
//			float OrigSpeed = enemy.transform.parent.gameObject.GetComponent<MoveEnemy>().speed;		// kaya sa parent ko kinuha. kase yung tag na 'Enemy' is nasa child na gameObject, tapos yung moveEnemy script nasa may parent
//			Vector3 origPosition = enemy.transform.position;
//			Debug.Log("enemy orig pos: " + origPosition);
//			enemy.transform.parent.GetComponent<MoveEnemy>().speed -= (OrigSpeed * 0.3f);	// para hindi 2 tag of the same enemy object yung ma store sa list. 
//			enemy.transform.position = origPosition; 
//			Debug.Log("New enemy pos: " + enemy.transform.position);
//		}
//
//		lastActivateTime = Time.time;
//	}

	public void UseLP(){		// LP = Life Potion
		int newHealth = gameManager.health + 3;			// add extra 3 lives
		if(newHealth > gameManager.maxhealth){			// ex: lives 3. tas nag potion sya. edi 6 na yon. eh example ang max lang ay 5 so check mo pag oo..
			gameManager.health = gameManager.maxhealth; // display max health of user
			gameManager.displayHealth();
		}
		else{											// kung di naman nag exceed, edi go
			gameManager.health = newHealth;				// value should either be less or equal sa max health ng player
			gameManager.displayHealth();
		}

		int qty = PlayerPrefs.GetInt("life potion qty:");
		qty--;
		PlayerPrefs.SetInt("life potion qty:", qty);
		qty = PlayerPrefs.GetInt("life potion qty:");
		LifePotionQty.text = PlayerPrefs.GetInt("life potion qty:").ToString();
		
		if(qty <= 0){
			LifePotionButton.interactable = false;
		}

	}

	public void useWB(){	// WB = Water Booster
		gameManager.water += 200;
		gameManager.displayWater();

		int qty = PlayerPrefs.GetInt("water booster qty:");
		qty--;
		PlayerPrefs.SetInt("water booster qty:", qty);
		qty = PlayerPrefs.GetInt("water booster qty:");
		waterBoosterQty.text = PlayerPrefs.GetInt("water booster qty:").ToString();

		if(qty <= 0){
			waterBoosterButton.interactable = false;
		}
	}

	public void usePoison(){
		// IMPLEMENTATION HERE
		enemyArray = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject enemy in enemyArray){
			enemies.Add (enemy);
		}

		// for the counting of by second
		 second1 = true;
		 second2 = true;
		 second3 = true;
		 second4 = true;
		 second5 = true;
		 second6 = true;
		 second7 = true;
		 second8 = true;
		 second9 = true;
		 second10 = true;

		poisonActivate = true;
		
		lastActivateTime = Time.time;

		int qty = PlayerPrefs.GetInt("poison qty:");
		qty--;
		PlayerPrefs.SetInt("poison qty:", qty);
		qty = PlayerPrefs.GetInt("poison qty:");
		poisonQty.text = PlayerPrefs.GetInt("poison qty:").ToString();
		
		if(qty <= 0){
			poisonButton.interactable = false;
		}
	}

	public void useHP(){	// HP = hero potion
		// IMPLEMENTATION HERE

		int qty = PlayerPrefs.GetInt("hero potion qty:");
		qty--;
		PlayerPrefs.SetInt("hero potion qty:", qty);
		qty = PlayerPrefs.GetInt("hero potion qty:");
		heroPotionQty.text = PlayerPrefs.GetInt("hero potion qty:").ToString();
		
		if(qty <= 0){
			heroPotionButton.interactable = false;
		}
	}

	void poison (){

		for(int i = enemies.Count - 1; i >= 0; i--){
			if(enemies[i] != null){
				counter++;
				// enemy position
				// instatntiate poisonParticle
				Instantiate(poisonParticle, enemies[i].transform.position, enemies[i].transform.rotation);
				HealthBar enemyHealth = enemies[i].transform.parent.GetChild(2).gameObject.GetComponent<HealthBar>();		// kaya sa parent ko kinuha. kase yung tag na 'Enemy' is nasa child na gameObject, tapos yung moveEnemy script nasa may parent
				enemyHealth.currentHealth -= (enemyHealth.maxHealth * 0.03f);		// 3% of the max health. 10 times to mauulet so bale ang mababawas ay 30% of the monsters max health
				Debug.Log("currentHealth (" + enemyHealth.currentHealth + ") -= maxhealth(" + enemyHealth.maxHealth + ") * 0.03f");
//				Debug.Log("maxhealth 3% : " + enemyHealth.maxHealth * 0.03f);

				if(enemyHealth.currentHealth <= 0){
					enemies.Remove(enemies[i]);
					Destroy(enemies[i].transform.parent.gameObject);
				}
//				if(enemy[i] == null){
//					enemies.Remove(enemy[i]);
//				}
//				if(counter == 9){
//					Debug.Log("rounding ...");
//					enemyHealth.currentHealth = Mathf.RoundToInt(enemyHealth.currentHealth);
//				}

			}
			else if(enemies[i] == null){
				enemies.Remove(enemies[i]);
			}
			//			Vector3 origPosition = enemy.transform.position;
			//			enemy.transform.parent.GetComponent<MoveEnemy>().speed -= (OrigSpeed * 0.3f);	// para hindi 2 tag of the same enemy object yung ma store sa list. 
			//			enemy.transform.position = origPosition; 
		}
	}
}
