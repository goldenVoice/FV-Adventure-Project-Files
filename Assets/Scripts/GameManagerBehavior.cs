using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour {

	string thisSceneFin;		// pangalan ng current level na nilalaro ng user.
	public int moneyToReward;		// money rewarded when level is finished

  	public Text waterText;
	public Text moneyText;

  	// para di mo to mapalitan accidentally,
  	//[HideInInspector]
  	public int initialWater;
  	public int water;
  	public int money;

	bool moneyComputed;				// para sa money reward  pag nanalo
	bool perfectStatus;					// is level perfect or Cleared?

  	public int wave;
  	public bool gameOver = false;
  	public Text waveText;
  
  	public Text healthText;
  	public int health;
	[HideInInspector]
	public int maxhealth;

  	GameObject canvas_restart;
  	GameObject restartConfrimTxt;

  	public GameObject currentSelectedHero;
	public int curHeroWatercost;			

  	public PotholeManager potholeManager;
  
	//private GameObject nextWaveIndicator;
  
  	SpriteManager spriteManager;

  	private bool heroSelected;
  	GameObject canvas_PlayerWin;

  	// Use this for initialization
	void Start () {
     // object.component.property
		thisSceneFin = Application.loadedLevelName;
		// the starting max health is initialized before the start of the game. before level 1-1. sa may end ng STORYLINE
		health = PlayerPrefs.GetInt("max health");

//		Debug.Log ("Time scale!: " + Time.timeScale);

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
		healthText.GetComponent<Text>().text = "" + health;
		moneyComputed = false;
     	heroSelected = false;
		perfectStatus = false;

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

	public void displayHealth(){
		healthText.GetComponent<Text>().text = "" + health;
	}

  	public void deductHealth(){
    	health -= 1;
    	healthText.GetComponent<Text>().text = "" + health;
  	}

  	public void didPlayerWin(bool playerWin){     // check if player won, 
    	if(playerWin == false){
      		Time.timeScale = 0.0f;  // pause the game
      		restartConfrimTxt.GetComponent<Text>().text = "You Lost! Go back to level select?";
      		canvas_restart.GetComponent<Canvas>().enabled = true;
      		GameObject NoButton_backToMap = GameObject.Find("NoButton_backToMap");
      		GameObject NoButton_restart = GameObject.Find("NoButton_restart");
      		NoButton_backToMap.SetActive(true);

			if(NoButton_restart != null){
      			NoButton_restart.SetActive(false);
			}

    	}
    	else{
     		Debug.Log("You Won!");
			LevelFin();
     		canvas_PlayerWin.GetComponent<Canvas>().enabled = true;
      		Time.timeScale = 0.0f;  // pause the game
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
		Text moneyRewardText = canvas_PlayerWin.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>();		// from canvas player win
		Text statusText = canvas_PlayerWin.transform.GetChild(1).GetChild(0).GetComponent<Text>();
		Debug.Log ("moneyToReward: " + moneyToReward);
		Debug.Log(PlayerPrefs.HasKey(thisSceneFin + "_status"));
		if(!moneyComputed){										// pag di pa na co compute money ng user
			if(PlayerPrefs.HasKey(thisSceneFin + "_status")){	// returns true if the user came back to the level, pwedeng gusto nya i perfect or what ever
				if(PlayerPrefs.GetFloat(thisSceneFin + "_status") < 1){
					moneyToReward = moneyRewardComeBack();		// pate sa laro may comeback
					moneyComputed = true;
				}
				else{		// naka perfect na sya last time. natapos nya ule.
					Debug.Log("dito ka dumiretso, dapat. perffect ka nanaman eh");
					moneyToReward = Mathf.RoundToInt(moneyToReward * 0.05f);		// give him 5 percent. HAHAHA
					moneyComputed = true;
					perfectStatus = true;
				}
			}
			else{
				Debug.Log ("dito dapat didiretso kapag 1st time mag laro");
				moneyToReward = moneyDependOnLivesLeft();		// compute money depending on lives of user
				moneyComputed = true;
			}
		}

		PlayerPrefs.SetInt("Money", money + moneyToReward);
		moneyRewardText.text = moneyToReward.ToString();
		// add the other rewards: (items, etc) here

		// para sa status: perfect / cleared when level finished
		if(health >= maxhealth){						// actually di maganda tong code na to kase what if di naman nanalo yung user?
			statusText.text = "PERFECT!";		// pero ayos lang kase di naman mag sho show tong canvas na to pag natalo yung user :D
			statusText.color = Color.green;
		}
		else{	
			statusText.text = "CLEARED!";
			statusText.color = Color.yellow;
		}

		PlayerPrefs.SetInt (thisSceneFin, 1);	// ex: thisSceneFin = 'Level 1-1'. set to 1. meaning tapos na yung level.
  	}

	int moneyDependOnLivesLeft(){											// function to compute how much money to reward the user depending on his current lives
		float currentMoneyToReward = moneyToReward;
		float _health = health;												// store in a float variable para di mag integer division (discard decimal)
		float _maxhealth = maxhealth;
		
		float healthPercent = ( _health / _maxhealth);							// where health is how many lives left on user: .9 means 90%
		Debug.Log(currentMoneyToReward + " * " + healthPercent + " = " + (currentMoneyToReward * healthPercent) );
		int moneyComputed = Mathf.RoundToInt(currentMoneyToReward * healthPercent);	// ex: 500 * .80	means 80% of 500

		if(healthPercent == 1){										// walang bawas yung life
			PlayerPrefs.SetFloat(thisSceneFin + "_status", 1);		// ex: 'Level 1-1_status', value of 1 means PERFECT
			perfectStatus = true;
		}
		else if(healthPercent < 1){									// if less than 1 meaning di perfect
			PlayerPrefs.SetFloat(thisSceneFin + "_status", healthPercent);		// ex: Level 1-1_status, healthpercent ranges from (0 - 99%) gives a status of CLEARED
		}

		return moneyComputed;
	}

	int moneyRewardComeBack(){		// when the user comes back to the level to achieve PERFECT status.
		int moneyComputed = moneyToReward;
		float lastPercent = PlayerPrefs.GetFloat(thisSceneFin + "_status");
		float percentLeft = 1f - lastPercent;		// ex: last time, naka 80% lang sya. bale kailangan this time yung lives na meron sya. mapantayan yung 

		float _health = health;												
		float _maxhealth = maxhealth;
		
		float curhealthPercent = ( _health / _maxhealth);						
		Debug.Log ("curhealthPercent: " + curhealthPercent);
		float percentAllowance = curhealthPercent - lastPercent;		// ex: lastpercent: 80% ,curhealthPercent(yung ngayon): 90% = 10% yung nakuha nyang percent
																		// may bubunuin pa syang 10% next game nya. kase di nya ren naman na perfect
		Debug.Log ("percentAllowance(" + percentAllowance + ") = " + "curhealthPercent(" + curhealthPercent + ") - last percent(" + lastPercent + ") ");
		if(percentAllowance <= 0){										// pag negative lumabas. natapos nya nga yung level. di nya naman na perfect at napuno yung kulang na health percent from last game
			Debug.Log("Seems that the user failed, di nya nahigitan ang dati nya ng nagawa");
			moneyComputed = Mathf.RoundToInt(moneyComputed * 0.05f);	// since di nya nanaman na perfect. the user will get a small default 5% of the moneyToReward
			PlayerPrefs.SetFloat(thisSceneFin + "_status", lastPercent);// set the status to the last percent, since di ren naman na achieve ng user yung mahigitan yung last percent nya
			return moneyComputed;
		}
		else{															// ex: percentcomputed = 10% add it to the laspercent of the user para pag nag laro ule sya. 90% na yung lastpercent nya. 10% percent na lang yung bubunuin nya :D
			if(percentAllowance + lastPercent == 1){						// naka perfect na si User! 
				Debug.Log ("Finally! naka perfect ka na den. May " + percentAllowance +"% allowance kang nadagdag mula sa huli mong laro");
				PlayerPrefs.SetFloat(thisSceneFin + "_status", 1); 
				moneyComputed = Mathf.RoundToInt(moneyComputed * percentAllowance);	// 500 * .1 = 10% of 500 nakaperfect na si user pero ang money reward nya ay yung kulang nya na percent.
				Debug.Log("moneyComputed =  moneyComputed(" + moneyComputed + " * percentAllowance(" + percentAllowance + ") ");
				perfectStatus = true;	// para sa status text
				return moneyComputed;				
			}
			else{
				float newPercent = lastPercent + percentAllowance;
				PlayerPrefs.SetFloat(thisSceneFin + "_status", newPercent);	// if di pa ren perfect, pero nahigitan nya naman yung last percent nya
																								//add yung last percent sa percent allowance na nakuha nya ngayon
				Debug.Log("Try again next time be :( ayos lang yan may nadagdag na " +percentAllowance+ "% sa last game mong naka "+lastPercent+"% ka lamang :D");
				moneyComputed = Mathf.RoundToInt(moneyToReward * percentAllowance);
				return moneyComputed;
			}
		}
	}

}
