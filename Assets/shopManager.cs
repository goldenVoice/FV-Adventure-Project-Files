using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shopManager : MonoBehaviour {

	private int money;

	public Text moneyText;

	private int slowQty;
	public int slowPrice;
	public Text slowPriceText;
	private Text slowQtyText;


	public int lifePotionPrice;

	void Awake(){
		if( !(PlayerPrefs.HasKey("slow qty:")) ){	// if wala pa. meaning 1st time ng user sa shop, 1st time mag laro
			PlayerPrefs.SetInt("slow qty:", 0);
		}
		if( !(PlayerPrefs.HasKey("life potion qty:")) ){	// if wala pa. meaning 1st time ng user sa shop, 1st time mag laro
			PlayerPrefs.SetInt("life potion qty:", 0);
		}


	}

	// Use this for initialization
	void Start () {
		money = PlayerPrefs.GetInt("Money");
		moneyText.text = "₱" + money;


		slowPriceText.text = "₱" + slowPrice;
		slowQty = PlayerPrefs.GetInt("slow qty:");
		slowQtyText = slowPriceText.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Text>();	// the qty gameObject of the item
		slowQtyText.text = "" + slowQty + "/3";		

		//PlayerPrefs.SetInt("slow qty:", 0);	// TAGA RESET
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void buySlow(){
		if(money >= slowPrice && slowQty < 3){						// To set the max to 3, less than 3 lang dapat walang equal. idk why

			PlayerPrefs.SetInt("slow qty:", slowQty + 1);			// add the qty of the item
			slowQty = PlayerPrefs.GetInt("slow qty:");				// store again on the variable
			slowQtyText.text = "" + slowQty + "/3";					// update the qty txt gameObject of the item
			Debug.Log(slowQty);
			PlayerPrefs.SetInt("Money", money - slowPrice);			// update the money variable, deduct price of item
			moneyText.text = PlayerPrefs.GetInt("Money").ToString();// then display it again
		}
	}

}
