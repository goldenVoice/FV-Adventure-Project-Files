using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	bool inventoryEnabled;
	public Toggle inventoryToggle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showInventory(){
		if(inventoryToggle.GetComponent<Toggle>().isOn){	// naka on ang toggle
			inventoryToggle.transform.GetChild(0).GetComponent<Image>().enabled = false;			
			inventoryToggle.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
			gameObject.GetComponent<Canvas>().enabled = true;			// this is the inventory canvas
		}
		else if(!(inventoryToggle.GetComponent<Toggle>().isOn) ){				// if naka off ang toggle
			inventoryToggle.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
			inventoryToggle.transform.GetChild(0).GetComponent<Image>().enabled = true;
			gameObject.GetComponent<Canvas>().enabled = false;			// this is the inventory canvas
		}
	}
}
