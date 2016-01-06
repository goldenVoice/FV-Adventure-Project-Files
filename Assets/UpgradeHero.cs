using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeHero : MonoBehaviour {

	public int[] upgradePrice;
	int currentLevel;
	private Text moneyText;

	// Use this for initialization
	void Start () {
		moneyText = GameObject.Find ("MoneyText").GetComponent<Text>();			// the moneyText gameObject

		// ex: kung ilang upgrade na, hal: naka 3 na sya.
		currentLevel = PlayerPrefs.GetInt(gameObject.name);				// hal: playerPrefs('Carrot speed') 
							
		// display current bars upgraded
		displayBars();

		// display the current price for upgrade
		displayUpgradePrice();

		// int.Parse(string) convert string to int
		checkMoney();	// if money is not enough disable upgrade button
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Upgrade(){
		Debug.Log("level before upgrade: " + currentLevel);
		currentLevel++;														// increment the level upgrade
		Debug.Log("level after upgrade: " + currentLevel);
		int money = PlayerPrefs.GetInt("Money");							// deduct user money
		PlayerPrefs.SetInt("Money", money - upgradePrice[currentLevel]);	// deduct user money
		money = PlayerPrefs.GetInt("Money");								// get new money
		
		PlayerPrefs.SetInt(gameObject.name, currentLevel);					// dagdagan ng isa, hero upgraded na
		displayBars();														// refresh the bars, nag upgrade na kase
		displayUpgradePrice();												// refresh upgrade price
		moneyText.text = "" + money;										// display money left
		checkMoney();														// if may enough money pa. if not, disable button
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
		Debug.Log (moneyText.text);
		int price = int.Parse( moneyText.text);
		if( price < upgradePrice[currentLevel] ){	// if kulang pera ng user
			gameObject.transform.GetChild(2).GetComponent<Button>().interactable = false;	// disable upgrade button	
		}
	}
}
