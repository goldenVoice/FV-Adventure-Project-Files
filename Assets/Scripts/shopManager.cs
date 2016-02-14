using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shopManager : MonoBehaviour {

	private int money;

	public Text moneyText;

	private int poisonQty;
	public int poisonPrice;
	public Text poisonPriceText;
	private Text poisonQtyText;

	private int lifePotionQty;
	public int lifePotionPrice;
	public Text lifePotionText;
	private Text lifePotionQtyText;

	private int waterBoosterQty;
	public int waterBoosterPrice;
	public Text waterBoosterText;
	private Text waterBoosterQtyText;

	private int heroPotionQty;
	public int heroPotionPrice;
	public Text heroPotionText;
	private Text heroPotionQtyText;

	string currentProfile;
	
	void Awake(){

		currentProfile = PlayerPrefs.GetString ("currentProfile");

		if( !(PlayerPrefs.HasKey(currentProfile + "poison qty:")) ){	// if wala pa. meaning 1st time ng user sa shop, 1st time mag laro
			PlayerPrefs.SetInt(currentProfile + "poison qty:", 0);
		}
		if( !(PlayerPrefs.HasKey(currentProfile + "life potion qty:")) ){	// if wala pa. meaning 1st time ng user sa shop, 1st time mag laro
			PlayerPrefs.SetInt(currentProfile + "life potion qty:", 0);
		}
		
		money = PlayerPrefs.GetInt(currentProfile + "Money");
		moneyText.text = "" + money;
		
		poisonPriceText.text = "" + poisonPrice;
		poisonQty = PlayerPrefs.GetInt(currentProfile + "poison qty:");
		poisonQtyText = poisonPriceText.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Text>();	// the qty gameObject of the item
		poisonQtyText.text = "" + poisonQty + "/3";		
		
		lifePotionText.text = "" + lifePotionPrice;
		lifePotionQty = PlayerPrefs.GetInt(currentProfile + "life potion qty:");
		lifePotionQtyText = lifePotionText.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Text>();	// the qty gameObject of the item
		lifePotionQtyText.text = "" + lifePotionQty + "/3";		
		
		waterBoosterText.text = "" + waterBoosterPrice;
		waterBoosterQty = PlayerPrefs.GetInt(currentProfile + "water booster qty:");
		waterBoosterQtyText = waterBoosterText.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Text>();	// the qty gameObject of the item
		waterBoosterQtyText.text = "" + waterBoosterQty + "/3";		
		
		heroPotionText.text = "" + heroPotionPrice;
		heroPotionQty = PlayerPrefs.GetInt(currentProfile + "hero potion qty:");
		heroPotionQtyText = heroPotionText.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Text>();	// the qty gameObject of the item
		heroPotionQtyText.text = "" + heroPotionQty + "/3";		

	}

	// Use this for initialization
	void Start () {

//		PlayerPrefs.SetInt("slow qty:", 0);	// TAGA RESET
//		PlayerPrefs.SetInt("life potion qty:", 0);	// TAGA RESET
//		PlayerPrefs.SetInt("Money", 1200);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void buypoison(){
		if(money >= poisonPrice && poisonQty < 3){						// To set the max to 3, less than 3 lang dapat walang equal. idk why

			PlayerPrefs.SetInt(currentProfile + "poison qty:", poisonQty + 1);			// add the qty of the item
			poisonQty = PlayerPrefs.GetInt(currentProfile + "poison qty:");				// store again on the variable
			poisonQtyText.text = "" + poisonQty + "/3";					// update the qty txt gameObject of the item
			Debug.Log(poisonQty);
			PlayerPrefs.SetInt(currentProfile + "Money", money - poisonPrice);			// update the money variable, deduct price of item
			money = PlayerPrefs.GetInt(currentProfile + "Money");
			moneyText.text = "" + money;// then display it again
		}
	}

	public void buylifePotion(){
		if(money >= lifePotionPrice && lifePotionQty < 3){						// To set the max to 3, less than 3 lang dapat walang equal. idk why
			
			PlayerPrefs.SetInt(currentProfile + "life potion qty:", lifePotionQty + 1);			// add the qty of the item
			lifePotionQty = PlayerPrefs.GetInt(currentProfile + "life potion qty:");				// store again on the variable
			lifePotionQtyText.text = "" + lifePotionQty + "/3";					// update the qty txt gameObject of the item
			Debug.Log(lifePotionQty);
			PlayerPrefs.SetInt(currentProfile + "Money", money - lifePotionPrice);			// update the money variable, deduct price of item
			money = PlayerPrefs.GetInt(currentProfile + "Money");
			moneyText.text = "" + money;// then display it again
		}
	}

	public void buywaterBooster(){
		if(money >= waterBoosterPrice && waterBoosterQty < 3){						// To set the max to 3, less than 3 lang dapat walang equal. idk why
			
			PlayerPrefs.SetInt(currentProfile + "water booster qty:", waterBoosterQty + 1);			// add the qty of the item
			waterBoosterQty = PlayerPrefs.GetInt(currentProfile + "water booster qty:");				// store again on the variable
			waterBoosterQtyText.text = "" + waterBoosterQty + "/3";					// update the qty txt gameObject of the item
			Debug.Log(waterBoosterQty);
			PlayerPrefs.SetInt(currentProfile + "Money", money - waterBoosterPrice);			// update the money variable, deduct price of item
			money = PlayerPrefs.GetInt(currentProfile + "Money");
			moneyText.text = "" + money;// then display it again
		}
	}

	public void buyheroPotion(){
		if(money >= heroPotionPrice && heroPotionQty < 3){						// To set the max to 3, less than 3 lang dapat walang equal. idk why
			
			PlayerPrefs.SetInt(currentProfile + "hero potion qty:", heroPotionQty + 1);			// add the qty of the item
			heroPotionQty = PlayerPrefs.GetInt(currentProfile + "hero potion qty:");				// store again on the variable
			heroPotionQtyText.text = "" + heroPotionQty + "/3";					// update the qty txt gameObject of the item
			Debug.Log(heroPotionQty);
			PlayerPrefs.SetInt(currentProfile + "Money", money - heroPotionPrice);			// update the money variable, deduct price of item
			money = PlayerPrefs.GetInt(currentProfile + "Money");
			moneyText.text = "" + money;// then display it again
		}
	}

}
