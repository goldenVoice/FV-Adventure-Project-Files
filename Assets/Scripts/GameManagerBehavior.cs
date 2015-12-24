using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour {

  public Text waterText;
  public int initialWater;
  public int Gold;

  // para di mo to mapalitan accidentally,
  //[HideInInspector]
  public int water;

  public int wave;
  public bool gameOver = false;
  public Text waveText;
  
  public Text healthText;
  public int health;

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
    	canvas_restart = GameObject.Find("Canvas_RestartDialog");  // get the gameObject canvasRestartDialog
	  	restartConfrimTxt = GameObject.Find("RestartConfirmTxt"); // get the restart text object. tapos i access mamaya pag natalo yung player, tatanungin kung gusto nya mag restart
     	waterText.GetComponent<Text>().text = "" + initialWater;
     	water = initialWater;
     	currentSelectedHero = null;
     	wave = 0;    // kailangan talaga 0 to, kase sa Wave[] array sa SpawnEnemy script yung elements non nag sisimula sa 0
     	waveText.GetComponent<Text>().text = "Wave: " + (wave + 1);
     	healthText.GetComponent<Text>().text = "Health: " + health;

     	heroSelected = false;
    	canvas_PlayerWin = GameObject.Find("Canvas_playerWin");
		SpawnEnemy spawnEnemy = (SpawnEnemy) FindObjectOfType(typeof(SpawnEnemy));
		spriteManager  = (SpriteManager) FindObjectOfType(typeof(SpriteManager));
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

     Debug.Log("GAME OVER CONGRATS DHAI XD");
     canvas_PlayerWin.GetComponent<Canvas>().enabled = true;

    }
  }  

   // get the current selected hero from gameManager
  public void getHero(GameObject heroPrefab){

      // function is called when the hero in a circle is pressed
      // kaya kung heroSelected is false, set it to true then make the heroPrefab the currentSelectedHero

      //if(heroSelected == false){
      //  heroSelected = true;
      currentSelectedHero = heroPrefab;

      //potholeManager.
     // }
      // pag pinindot ule yung circle 
      //else if (heroSelected == true){
      //  heroSelected = false;
      //  currentSelectedHero = null; 
      //}


  }

  // this function is for the invisible button na nasa scene, para kahet san mag click yung user
  // while may selected syang hero, ma ca cancel yung selection nya.
  public void cancelSelectedHero(){

    currentSelectedHero = null;
  }      
}
