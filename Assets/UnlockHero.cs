using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlockHero : MonoBehaviour {

	GameObject heroName;
	private Text moneyText;
	int heroPrice;

	// Use this for initialization
	void Start () {

		moneyText = GameObject.Find ("MoneyText").GetComponent<Text>();			// the moneyText gameObject
		heroPrice = int.Parse( gameObject.transform.GetChild(0).GetComponent<Text>().text);	// kung mag kano yung hero para ma unlock
		
		checkMoney();

		heroName = gameObject.transform.parent.gameObject; // 'watermelon_circle' should be the same with the level 1 na watermelon_circle gameObject. for unlocking
		if(PlayerPrefs.GetInt(heroName.name) == 1){
			heroName.transform.GetChild(3).gameObject.SetActive(true);
			heroName.transform.GetChild(4).gameObject.SetActive(true);
			heroName.transform.GetChild(5).gameObject.SetActive(true);
			gameObject.SetActive(false);
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		checkMoney();
	}

	public void buyHero(){
		PlayerPrefs.SetInt(heroName.name, 1);
		heroName.transform.GetChild(3).gameObject.SetActive(true);
		heroName.transform.GetChild(4).gameObject.SetActive(true);
		heroName.transform.GetChild(5).gameObject.SetActive(true);

		gameObject.SetActive(false);

		int money = PlayerPrefs.GetInt("Money");							// deduct user money
		PlayerPrefs.SetInt("Money", money - heroPrice);						// deduct user money
		money = PlayerPrefs.GetInt("Money");								// get new money
		moneyText.text = "" + money;										// display money left
	}

	void checkMoney(){
		
		int money = int.Parse( moneyText.text);

		if( money <  heroPrice){	// if kulang pera ng user
			gameObject.GetComponent<Button>().interactable = false;	// disable upgrade button	
		}
	}

}
