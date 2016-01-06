using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	bool inventoryEnabled;
	bool slowActivate = false;
	
	public Toggle inventoryToggle;

	float timeCounter;
	float lastActivateTime;

	public Button slowButton;
	private Text slowQty;
	
	public Button LifePotionButton;
	
	public GameObject[] enemies;
	
	// Use this for initialization
	void Start () {
		inventoryEnabled = false;
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
			timeCounter = lastActivateTime - Time.time;

			
			Debug.Log(timeCounter);

		}
	}

	public void showInventory(){
		if(!inventoryEnabled){	// meaning naka hide ang inventory
//			gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;			
//			inventoryToggle.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
			gameObject.transform.GetChild(0).gameObject.SetActive(true);			// this is the inventory panel
			inventoryEnabled = true;
		}
		else if(inventoryEnabled){				// if naka off ang toggle
//			inventoryToggle.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
//			inventoryToggle.transform.GetChild(0).GetComponent<Image>().enabled = true;
			gameObject.transform.GetChild(0).gameObject.SetActive(false);			// this is the inventory panel
			inventoryEnabled = false;
		}
	}

	public void ActivateSlow(){	// the function of slow booster
		slowActivate = true;
		enemies = GameObject.FindGameObjectsWithTag("Enemy");

		foreach(GameObject enemy in enemies){
			float OrigSpeed = enemy.transform.parent.gameObject.GetComponent<MoveEnemy>().speed;		// kaya sa parent ko kinuha. kase yung tag na 'Enemy' is nasa child na gameObject, tapos yung moveEnemy script nasa may parent
			Vector3 origPosition = enemy.transform.position;
			Debug.Log("enemy orig pos: " + origPosition);
			enemy.transform.parent.GetComponent<MoveEnemy>().speed -= (OrigSpeed * 0.3f);	// para hindi 2 tag of the same enemy object yung ma store sa list. 
			enemy.transform.position = origPosition; 
			Debug.Log("New enemy pos: " + enemy.transform.position);
		}

		lastActivateTime = Time.time;
	}
}
