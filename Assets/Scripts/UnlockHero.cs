using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlockHero : MonoBehaviour {

	GameObject heroName;
	private Text moneyText;
	int heroPrice;

	string currentProfile;
	void Awake(){
		
		currentProfile = PlayerPrefs.GetString ("currentProfile");
	}
	// Use this for initialization
	void Start () {

		moneyText = GameObject.Find ("MoneyText").GetComponent<Text>();			// the moneyText gameObject
		heroPrice = int.Parse( gameObject.transform.GetChild(0).GetComponent<Text>().text);	// kung mag kano yung hero para ma unlock
		
		checkMoney();

		heroName = gameObject.transform.parent.gameObject; // 'watermelon_circle' should be the same with the level 1 na watermelon_circle gameObject. for unlocking
		if(PlayerPrefs.GetInt(currentProfile + heroName.name) == 1){
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
		PlayerPrefs.SetInt(currentProfile + heroName.name, 1);
		heroName.transform.GetChild(3).gameObject.SetActive(true);
		heroName.transform.GetChild(4).gameObject.SetActive(true);
		heroName.transform.GetChild(5).gameObject.SetActive(true);

		gameObject.SetActive(false);

		int money = PlayerPrefs.GetInt(currentProfile + "Money");							// deduct user money
		PlayerPrefs.SetInt(currentProfile + "Money", money - heroPrice);						// deduct user money
		money = PlayerPrefs.GetInt(currentProfile + "Money");								// get new money
		moneyText.text = "" + money;										// display money left
	}

	void checkMoney(){
		
		int money = int.Parse( moneyText.text);

		if( money <  heroPrice){	// if kulang pera ng user
			gameObject.GetComponent<Button>().interactable = false;	// disable upgrade button	
		}
	}

	public void SE_unlockHero(){
		PlayerPrefs.SetInt(currentProfile + heroName.name, 1);
		List_ofHardHero list_hardHero = (List_ofHardHero) GameObject.Find ("List_HardHero").GetComponent<List_ofHardHero>();

		foreach (GameObject circleHero in list_hardHero.heroesToUnlock_SE) {
			if(circleHero.name == heroName.name){
				Debug.Log("na unlock na si: " + heroName.name);
				circleHero.gameObject.SetActive(true);
//				circleHero.transform.GetChild(3).gameObject.SetActive(true);
//				circleHero.transform.GetChild(4).gameObject.SetActive(true);
//				circleHero.transform.GetChild(5).gameObject.SetActive(true);
				break;
			}
		}

		int money = PlayerPrefs.GetInt(currentProfile + "Money");							// deduct user money
		PlayerPrefs.SetInt(currentProfile + "Money", money - heroPrice);						// deduct user money
		money = PlayerPrefs.GetInt(currentProfile + "Money");								// get new money
		moneyText.text = "" + money;										// display money left

		gameObject.SetActive (false);
		gameObject.transform.parent.GetChild (3).gameObject.SetActive (true);
	}

	public void SE_lockHero(){
		PlayerPrefs.SetInt(currentProfile + heroName.name, 0);
	}
}
