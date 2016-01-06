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

	private int lifePotionQty;
	public int lifePotionPrice;
	public Text lifePotionText;
	private Text lifePotionQtyText;
	
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
		moneyText.text = "" + money;

		slowPriceText.text = "" + slowPrice;
		slowQty = PlayerPrefs.GetInt("slow qty:");
		slowQtyText = slowPriceText.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Text>();	// the qty gameObject of the item
		slowQtyText.text = "" + slowQty + "/3";		

		lifePotionText.text = "" + lifePotionPrice;
		lifePotionQty = PlayerPrefs.GetInt("life potion qty:");
		lifePotionQtyText = lifePotionText.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Text>();	// the qty gameObject of the item
		lifePotionQtyText.text = "" + lifePotionQty + "/3";		

//		PlayerPrefs.SetInt("slow qty:", 0);	// TAGA RESET
//		PlayerPrefs.SetInt("life potion qty:", 0);	// TAGA RESET
//		PlayerPrefs.SetInt("Money", 1200);
		
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
			money = PlayerPrefs.GetInt("Money");
			moneyText.text = "" + money;// then display it again
		}
	}

	public void buylifePotion(){
		if(money >= lifePotionPrice && lifePotionQty < 3){						// To set the max to 3, less than 3 lang dapat walang equal. idk why
			
			PlayerPrefs.SetInt("life potion qty:", lifePotionQty + 1);			// add the qty of the item
			lifePotionQty = PlayerPrefs.GetInt("life potion qty:");				// store again on the variable
			lifePotionQtyText.text = "" + lifePotionQty + "/3";					// update the qty txt gameObject of the item
			Debug.Log(lifePotionQty);
			PlayerPrefs.SetInt("Money", money - lifePotionPrice);			// update the money variable, deduct price of item
			money = PlayerPrefs.GetInt("Money");
			moneyText.text = "" + money;// then display it again
		}
	}

}
