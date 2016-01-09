using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyChecker : MonoBehaviour {

	int boosterPrice;
	private Text moneyText;

	void Awake(){
		moneyText = GameObject.Find ("MoneyText").GetComponent<Text>();			// the moneyText gameObject
	}
	
	// Use this for initialization
	void Start () {
		boosterPrice = int.Parse(gameObject.transform.GetChild(0).GetComponent<Text>().text);
		
	}
	
	// Update is called once per frame
	void Update () {
		checkMoney();
	}

	void checkMoney(){	
		int money = int.Parse( moneyText.text);
		if( money < boosterPrice){	// if kulang pera ng user
			gameObject.GetComponent<Button>().interactable = false;	// disable upgrade button	
		}
	}
}
