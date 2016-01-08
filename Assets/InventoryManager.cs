using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	bool inventoryEnabled;
	bool slowActivate = false;
	
	public Toggle inventoryToggle;

	float timeCounter;
	float lastActivateTime;

	public Button poisonButton;
	private Text poisonQty;
	
	public Button LifePotionButton;
	private Text LifePotionQty;

	public Button waterBoosterButton;
	private Text waterBoosterQty;

	public Button heroPotionButton;
	private Text heroPotionQty;

	public GameObject[] enemies;

	private GameManagerBehavior gameManager;

	GameObject inventoryPanel;
	
	// Use this for initialization
	void Start () {

		gameManager = (GameManagerBehavior) GameObject.Find ("GameManager").GetComponent<GameManagerBehavior>();
		inventoryEnabled = false;

		inventoryPanel = gameObject.transform.GetChild(0).gameObject;

		// ilagay sa qtyText kung ilang boosters meron ang user
		poisonQty = poisonButton.transform.GetChild(0).GetChild(0).GetComponent<Text>();
		poisonQty.text = PlayerPrefs.GetInt("poison qty:").ToString();

		LifePotionQty = LifePotionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>();
		LifePotionQty.text = PlayerPrefs.GetInt("life potion qty:").ToString();

		waterBoosterQty = waterBoosterButton.transform.GetChild(0).GetChild(0).GetComponent<Text>();
		waterBoosterQty.text = PlayerPrefs.GetInt("water booster qty:").ToString();

		heroPotionQty = heroPotionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>();
		heroPotionQty.text = PlayerPrefs.GetInt("hero potion qty:").ToString();

		if(PlayerPrefs.GetInt("poison qty:") == 0){
			// disable button kase 0 yung booster
			poisonButton.interactable = false;
		}
		if(PlayerPrefs.GetInt("life potion qty:") == 0){
			// disable button kase 0 yung booster
			LifePotionButton.interactable = false;
		}
		if(PlayerPrefs.GetInt("water booster qty:") == 0){
			// disable button kase 0 yung booster
			waterBoosterButton.interactable = false;
		}
		if(PlayerPrefs.GetInt("hero potion qty:") == 0){
			// disable button kase 0 yung booster
			heroPotionButton.interactable = false;
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
		if(!inventoryPanel.activeSelf){	// meaning naka hide ang inventory
//			gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;			
//			inventoryToggle.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
			inventoryPanel.SetActive(true);
		}
		else if(inventoryPanel.activeSelf){				// if naka off ang toggle
//			inventoryToggle.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
//			inventoryToggle.transform.GetChild(0).GetComponent<Image>().enabled = true;
			inventoryPanel.SetActive(false);
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

	public void UseLP(){		// LP = Life Potion
		int newHealth = gameManager.health + 3;			// add extra 3 lives
		if(newHealth > gameManager.maxhealth){			// ex: lives 3. tas nag potion sya. edi 6 na yon. eh example ang max lang ay 5 so check mo pag oo..
			gameManager.health = gameManager.maxhealth; // display max health of user
			gameManager.displayHealth();
		}
		else{											// kung di naman nag exceed, edi go
			gameManager.health = newHealth;				// value should either be less or equal sa max health ng player
			gameManager.displayHealth();
		}

	}

}
