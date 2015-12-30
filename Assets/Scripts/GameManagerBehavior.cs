using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour {

	public string thisSceneFin;		// pangalan ng current level na nilalaro ng user.
	public int moneyToReward;		// money rewarded when level is finished

  	public Text waterText;
	public Text moneyText;

  	// para di mo to mapalitan accidentally,
  	//[HideInInspector]
  	public int initialWater;
  	public int water;
  	public int money;

	bool moneyComputed;				// para sa money reward  pag nanalo
	
  	public int wave;
  	public bool gameOver = false;
  	public Text waveText;
  
  	public Text healthText;
  	public int health;
	private int maxhealth;

  	GameObject canvas_restart;
  	GameObject restartConfrimTxt;

  	public GameObject currentSelectedHero;
  
  	public PotholeManager potholeManager;
  
	//private GameObject nextWaveIndicator;
  
  	SpriteManager spriteManager;

  	private bool heroSelected;
  	GameObject canvas_PlayerWin;

  	// Use this for initialization
	void Start () {
     // object.component.property
		Debug.Log ("Time scale!: " + Time.timeScale);
		maxhealth = health;
    	canvas_restart = GameObject.Find("Canvas_RestartDialog");  // get the gameObject canvasRestartDialog
	  	restartConfrimTxt = GameObject.Find("RestartConfirmTxt"); // get the restart text object. tapos i access mamaya pag natalo yung player, tatanungin kung gusto nya mag restart
     	waterText.GetComponent<Text>().text = "" + initialWater;
     	water = initialWater;
     	currentSelectedHero = null;
     	wave = 0;    // kailangan talaga 0 to, kase sa Wave[] array sa SpawnEnemy script yung elements non nag sisimula sa 0
     	waveText.GetComponent<Text>().text = "Wave: " + (wave + 1);

		money = PlayerPrefs.GetInt("Money");
		moneyText.GetComponent<Text>().text = "" + money; 
		healthText.GetComponent<Text>().text = "Health: " + health;
		moneyComputed = false;
     	heroSelected = false;
    	canvas_PlayerWin = GameObject.Find("Canvas_playerWin");
		SpawnEnemy spawnEnemy = (SpawnEnemy) FindObjectOfType(typeof(SpawnEnemy));
		spriteManager  = (SpriteManager) FindObjectOfType(typeof(SpriteManager));
		LevelFinished levelFinised = (LevelFinished) FindObjectOfType(typeof(LevelFinished));
		
		// display the element of the first wave in the nextWave indicator
		spriteManager.displayNextElement(spawnEnemy.waves, 0);
	}
	
	// Update is called once per frame
	void Update () {
	

	}

 	 public void deductWater(int cost){
      	water -= cost;
      	waterText.GetComponent<Text>().text = "" + water;
  	}

  	public void displayWater(){
     	waterText.GetComponent<Text>().text = "" + water; 
  	}

	public void displayMoney(){
		moneyText.GetComponent<Text>().text = "" + money; 
	}

  	public void displayWave(int waveNumber){
    	waveText.GetComponent<Text>().text = "Wave: " + (waveNumber + 1);   // so you wont start counting with 0
  	}

  	public void deductHealth(){
    	health -= 1;
    	healthText.GetComponent<Text>().text = "Health: " + health;
  	}

  	public void didPlayerWin(bool playerWin){     // check if player won, 
    	if(playerWin == false){
      		Time.timeScale = 0.0f;  // pause the game
      		restartConfrimTxt.GetComponent<Text>().text = "You Lost! Restart Level?";
      		canvas_restart.GetComponent<Canvas>().enabled = true;
      		GameObject NoButton_backToMap = GameObject.Find("NoButton_backToMap");
      		GameObject NoButton_restart = GameObject.Find("NoButton_restart");
      		NoButton_backToMap.SetActive(true);
      		NoButton_restart.SetActive(false);
    	}
    	else{
     		Debug.Log("You Won!");
			LevelFin();
     		canvas_PlayerWin.GetComponent<Canvas>().enabled = true;
    	}
  	}  

   	// get the current selected hero from gameManager
  	public void getHero(GameObject heroPrefab){
      	// function is called when the hero in a circle is pressed
      	// kaya kung heroSelected is false, set it to true then make the heroPrefab the currentSelectedHero
      	currentSelectedHero = heroPrefab;
  	}

  	// this function is for the invisible button na nasa scene, para kahet san mag click yung user
  	// while may selected syang hero, ma ca cancel yung selection nya.
  	public void cancelSelectedHero(){
		currentSelectedHero = null;
  	}      

  	public void LevelFin(){
		Text moneyRewardText = canvas_PlayerWin.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>();		// from canvas player win

		if(!moneyComputed){								// pag di pa na co compute money ng user
			moneyToReward = moneyDependOnLivesLeft();	// compute money depending on lives of user
			moneyComputed = true;
		}
		PlayerPrefs.SetInt("Money", money + moneyToReward);
		moneyRewardText.text = moneyToReward.ToString();
		// add the other rewards: (items, etc) here
		PlayerPrefs.SetInt (thisSceneFin, 1);	// ex: thisSceneFin = 'Level_1-1'. set to 1. meaning tapos na yung level.
  	}

	int moneyDependOnLivesLeft(){											// function to compute how much money to reward the user depending on his current lives
		float currentMoneyToReward = moneyToReward;
		float _health = health;												// store in a float variable para di mag integer division (discard decimal)
		float _maxhealth = maxhealth;
		
		float healthPercent = ( _health / _maxhealth);							// where health is how many lives left on user: .9 means 90%
		Debug.Log(currentMoneyToReward + " * " + healthPercent + " = " + (currentMoneyToReward * healthPercent) );
		int moneyComputed = Mathf.RoundToInt(currentMoneyToReward * healthPercent);	// ex: 500 * .80	means 80% of 500

		if(healthPercent == 1){										// walang bawas yung life
			PlayerPrefs.SetInt(thisSceneFin + "_status", 1);		// ex: 'Level 1-1_status', value of 1 means PERFECT
		}
		else if(healthPercent < 1){									// if less than 1 meaning di perfect
			PlayerPrefs.SetInt(thisSceneFin + "_status", 0);		// ex: Level 1-1_status, so 0 gives status of CLEARED
		}

		return moneyComputed;
	}

}
