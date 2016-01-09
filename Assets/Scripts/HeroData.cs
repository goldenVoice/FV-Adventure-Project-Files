using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroData : MonoBehaviour {

//	public enum pathWay{
//		flying,
//		walking,
//	}
//
//	public enum targetEnemy{
//		flying,
//		walking,
//		both,
//	}

	[System.Serializable]
	public class speedLevel{
		public float fireRate;
	}

	[System.Serializable]
	public class attackLevel{
		public float damage;
	}

	[System.Serializable]
	public class HPLevel{
		public float health;
	}

  	public int cost;
  	public GameObject bullet;

	public EnemyData.pathWay target;

  	public float fireRate;						// fire rate of the carrot
	public float refundPercent;
	public int waterRefund;
	public ElementManager.Element heroElement;

	public List<speedLevel> speedLevels;
	public List<attackLevel> attackLevels;
	public List<HPLevel> healthLevels;

	double result;

	void Awake(){

		// PERFORM CHECKS IF USER IS ON HARD MODE. IF YES, ENABLE THE HEALTH BAR BG AND HEALTH BAR OBJECTS


		// for HP of hero, first check if healthbar is active in hierarchy, if not meaning wala pa sa hard yung user
		if(gameObject.transform.parent.GetChild(6).gameObject.activeSelf){
			int HP_Level = PlayerPrefs.GetInt(gameObject.name + " HP");
			HealthBar heroHP = gameObject.transform.parent.GetChild(6).GetComponent<HealthBar>();
			heroHP.maxHealth = healthLevels[HP_Level].health;				// get the current level of health of the hero, then set it.
			heroHP.currentHealth = heroHP.maxHealth;						// set the current & max here, kase di alam kung sino mauuna sa 2 script (kung healthbar ba o eto)
		}
	}

	// Use this for initialization
	void Start () {
										// ex: 'Carrot speed' which is the same as the speed button name in shop
		int FR_Level = PlayerPrefs.GetInt(gameObject.name + " speed"); 	// value to be given to the var fireRate. automatic na mag se set bec. of the upgrade shop
		fireRate = speedLevels[FR_Level].fireRate;						// ex. speedLevels[0]. the element 0 or level 0 ng speed. kunin mo tas lagay mo sa fireRate. thats how upgrading heroes thru shop works
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
