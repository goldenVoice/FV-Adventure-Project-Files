using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	bool inventoryEnabled;
	bool slowActivate = false;

	[HideInInspector]
	public Toggle inventoryToggle;

	public Button slowButton;
	private Text slowQty;
	
	public Button LifePotionButton;
	

	// Use this for initialization
	void Start () {
		// ilagay sa qtyText kung ilang boosters meron ang user
		slowQty = slowButton.transform.GetChild(0).GetChild(0).GetComponent<Text>();
		slowQty.text = PlayerPrefs.GetInt("slow qty:").ToString();

		LifePotionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("life potion qty:").ToString();


		if(PlayerPrefs.GetInt("slow qty:") == 0){
			// disable button kase 0 yung booster
			slowButton.interactable = false;
		}
		if(PlayerPrefs.GetInt("life potion qty:") == 0){
			// disable button kase 0 yung booster
			LifePotionButton.interactable = false;
		}

	}
	
	// Update is called once per frame
	void Update () {

		if(slowActivate){

		}
	}

	public void showInventory(){
		if(inventoryToggle.GetComponent<Toggle>().isOn){	// naka on ang toggle
			inventoryToggle.transform.GetChild(0).GetComponent<Image>().enabled = false;			
			inventoryToggle.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
			gameObject.transform.GetChild(1).gameObject.SetActive(true);			// this is the inventory panel
		}
		else if(!(inventoryToggle.GetComponent<Toggle>().isOn) ){				// if naka off ang toggle
			inventoryToggle.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
			inventoryToggle.transform.GetChild(0).GetComponent<Image>().enabled = true;
			gameObject.transform.GetChild(1).gameObject.SetActive(false);			// this is the inventory panel
		}
	}

	public void ActivateSlow(){	// the function of slow booster
		slowActivate = true;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		foreach(GameObject enemy in enemies){
			float OrigSpeed = enemy.GetComponent<MoveEnemy>().speed;

			enemy.GetComponent<MoveEnemy>().speed -= (OrigSpeed * 0.3f);
			 
		}
	}
}
