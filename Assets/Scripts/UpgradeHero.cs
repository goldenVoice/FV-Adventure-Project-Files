using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeHero : MonoBehaviour {

	public int[] upgradePrice;
	int currentLevel;
	private Text moneyText;

//	public GameObject otherUpgrade1;
//	public GameObject otherUpgrade2;

	bool heroUpgraded = false;	// determines if an upgrade happened

	void Awake(){
		moneyText = GameObject.Find ("MoneyText").GetComponent<Text>();			// the moneyText gameObject
	}

	// Use this for initialization
	void Start () {
		heroUpgraded = false;

		// ex: kung ilang upgrade na, hal: naka 3 na sya.
		currentLevel = PlayerPrefs.GetInt(gameObject.name);				// hal: playerPrefs('Carrot speed') 
							
		// display current bars upgraded
		displayBars();

		// display the current price for upgrade
		displayUpgradePrice();

		// int.Parse(string) convert string to int
		checkMoney();	// if money is not enough disable upgrade button

		checkIfMaxUpgrade ();
		
	}
	
	// Update is called once per frame
	void Update () {

		// continually check the money. 
		checkMoney();

//		if(heroUpgraded){	// kung na upgrade, saka lang i check yung money
//			checkMoney();	// to refresh
//			heroUpgraded = false;
//			Debug.Log("3 TIMES LANG DAPT");
////		}
	}

	public void Upgrade(){
		Debug.Log("level before upgrade: " + currentLevel);
		int money = PlayerPrefs.GetInt("Money");							// deduct user money
		PlayerPrefs.SetInt("Money", money - upgradePrice[currentLevel]);	// deduct user money
		money = PlayerPrefs.GetInt("Money");								// get new money
		currentLevel++;														// increment the level upgrade
		PlayerPrefs.SetInt(gameObject.name, currentLevel);					// dagdagan ng isa, hero upgraded na
		displayBars();														// refresh the bars, nag upgrade na kase
		displayUpgradePrice();												// refresh upgrade price
		moneyText.text = "" + money;										// display money left
		checkMoney();														// if may enough money pa. if not, disable button
		Debug.Log("level after upgrade: " + currentLevel);
		checkIfMaxUpgrade ();
	}

	void displayBars(){
		GameObject bars = gameObject.transform.GetChild(1).gameObject;
		for(int i = 0; i < currentLevel; i++){		// loop through the bars. then make it green while i < currentLevel
			bars.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.green;		// ex: turn green the child(0) = bar 1. 
		}

	}

	void displayUpgradePrice(){
		gameObject.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "" + upgradePrice[currentLevel];		// upgrdePrice defined in the inspector, looks for the current upgrade of the user then displays teh appropriate amount
	}

	void checkMoney(){

			int money = int.Parse( moneyText.text);
			if( money < upgradePrice[currentLevel] ){	// if kulang pera ng user
				gameObject.transform.GetChild(2).GetComponent<Button>().interactable = false;	// disable upgrade button	
			}

			// call the check money function of the other 2 upgrade buttons.
			// baka kase mamaya, nung nag upgrade ka. 100 na lang natira sayo. eh may avail na upgrade kanina (before ng upgrade na to.) so pwede pa ren yun pindutin
//			otherUpgrade1.GetComponent<UpgradeHero>().checkMoney_alone();
//			otherUpgrade2.GetComponent<UpgradeHero>().checkMoney_alone();
	}

	public void magicPera(){	// ikaw ay mabibigyan ng pa bwenas na 500. (for debugging purposes only)
		int money = PlayerPrefs.GetInt("Money");
		PlayerPrefs.SetInt("Money", money + 500);
		money = PlayerPrefs.GetInt("Money");

		moneyText.text = "" + money;
	}

	public void clearUpgrades(){		// for debugging purposes only
		PlayerPrefs.SetInt("Carrot attack", 0);
		PlayerPrefs.SetInt("Carrot speed", 0);
		PlayerPrefs.SetInt("Carrot HP", 0);
	}
		
	public void checkMoney_alone(){		// check money of this function only. para di mag stack overflow
		//Debug.Log(moneyText);
		int money = int.Parse( moneyText.text);
		if( money < upgradePrice[currentLevel] ){	// if kulang pera ng user
			gameObject.transform.GetChild(2).GetComponent<Button>().interactable = false;	// disable upgrade button	
		}
	}

	void checkIfMaxUpgrade(){
		
		if (currentLevel == upgradePrice.Length - 1) {	// if kulang pera ng user
			gameObject.transform.GetChild (2).GetComponent<Button> ().interactable = false;	// disable upgrade button	
			gameObject.transform.GetChild (2).GetChild(0).GetComponent<Text>().text = "MAX";
		}
	}
}
