using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroData : MonoBehaviour {

	[System.Serializable]
	public class speedLevel{
		public float fireRate;
		public int priceInShop;		// if edit man sa inspector, gawin sa shop
	}


  	public int cost;
  	public GameObject bullet;
						
  	public float fireRate;						// fire rate of the carrot
	public float refundPercent;
	public int waterRefund;
	public ElementManager.Element heroElement;

	public List<speedLevel> speedLevels;
  	
	double result;
	// Use this for initialization
	void Start () {
										// ex: 'Carrot speed' which is the same as the speed button name in shop
		int FR_Level = PlayerPrefs.GetInt(gameObject.name + " speed"); 	// value to be given to the var fireRate. automatic na mag se set bec. of the upgrade shop
		fireRate = speedLevels[FR_Level].fireRate;		// ex. speedLevels[0]. the element 0 or level 0 ng speed. kunin mo tas lagay mo sa fireRate. thats how upgrading heroes thru shop works
														// FR = fire rate


		refundPercent = refundPercent / 100;
//		print (refundPercent);
		result = cost * refundPercent;		// refund is 40% of the hero's cost, as defined in the inspector
		waterRefund = Mathf.RoundToInt((float)result);	// cast the result into a float then round it to an int
														
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
