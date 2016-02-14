using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {

	//public GameObject[] profiles;
	string profile; 
	// Use this for initialization

	bool erase;
	bool overwrite;
	LoadingScreen1 loadingScreen;
	
	void Start () {
		erase = false;
		overwrite = false;
		loadingScreen = (LoadingScreen1) GameObject.FindObjectOfType (typeof (LoadingScreen1));
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClearData(){		// reset ALL current user data
		PlayerPrefs.DeleteAll();
	}

	public void LoadProgress(){

	}
	public void SetWhichProfile(Button whichProfile){
		profile = whichProfile.name;
		Debug.Log ("profile: " + profile);
		GameObject.Find ("confirmPanel").transform.GetChild(0).GetComponent<Text>().text = "Delete current player data?";
		erase = true;
		overwrite = false;
	}

	public void OverWriteData(Button whichProfile){
		profile = whichProfile.name;
		GameObject.Find ("confirmPanel").transform.GetChild(0).GetComponent<Text>().text = "Overwrite current player data?";
		overwrite = true;
		erase = false;
	}

	public void ClearThisData(){

		PlayerPrefs.DeleteKey (profile + "Money");
		PlayerPrefs.DeleteKey (profile + "Tutorial");
		PlayerPrefs.DeleteKey (profile + "playerReturns_toLvl_1");

		// boosters
		PlayerPrefs.DeleteKey (profile + "poison qty:");
		PlayerPrefs.DeleteKey (profile + "life potion qty:");
		PlayerPrefs.DeleteKey (profile + "water booster qty:");
		PlayerPrefs.DeleteKey (profile + "hero potion qty:");

		// upgrade for heroes
		PlayerPrefs.DeleteKey (profile + "Carrot attack");
		PlayerPrefs.DeleteKey (profile + "Carrot speed");
		PlayerPrefs.DeleteKey (profile + "Carrot HP");

		PlayerPrefs.DeleteKey (profile + "Banana attack");
		PlayerPrefs.DeleteKey (profile + "Banana speed");
		PlayerPrefs.DeleteKey (profile + "Banana HP");

		PlayerPrefs.DeleteKey (profile + "Watermelon attack");
		PlayerPrefs.DeleteKey (profile + "Watermelon speed");
		PlayerPrefs.DeleteKey (profile + "Watermelon HP");

		PlayerPrefs.DeleteKey (profile + "Orange attack");
		PlayerPrefs.DeleteKey (profile + "Orange speed");
		PlayerPrefs.DeleteKey (profile + "Orange HP");

		PlayerPrefs.DeleteKey (profile + "Rambutan attack");
		PlayerPrefs.DeleteKey (profile + "Rambutan speed");
		PlayerPrefs.DeleteKey (profile + "Rambutan HP");

		PlayerPrefs.DeleteKey (profile + "Leek attack");
		PlayerPrefs.DeleteKey (profile + "Leek speed");
		PlayerPrefs.DeleteKey (profile + "Leek HP");

		PlayerPrefs.DeleteKey (profile + "Mushroom attack");
		PlayerPrefs.DeleteKey (profile + "Mushroom speed");
		PlayerPrefs.DeleteKey (profile + "Mushroom HP");

		PlayerPrefs.DeleteKey (profile + "Onion attack");
		PlayerPrefs.DeleteKey (profile + "Onion speed");
		PlayerPrefs.DeleteKey (profile + "Onion HP");

		PlayerPrefs.DeleteKey (profile + "Coconut attack");
		PlayerPrefs.DeleteKey (profile + "Coconut speed");
		PlayerPrefs.DeleteKey (profile + "Coconut HP");

		PlayerPrefs.DeleteKey (profile + "CarrotCannon attack");
		PlayerPrefs.DeleteKey (profile + "CarrotCannon speed");
		PlayerPrefs.DeleteKey (profile + "CarrotCannon HP");

		PlayerPrefs.DeleteKey (profile + "HealingBanana attack");
		PlayerPrefs.DeleteKey (profile + "HealingBanana speed");
		PlayerPrefs.DeleteKey (profile + "HealingBanana HP");

		PlayerPrefs.DeleteKey (profile + "BlasterMelon attack");
		PlayerPrefs.DeleteKey (profile + "BlasterMelon speed");
		PlayerPrefs.DeleteKey (profile + "BlasterMelon HP");

		PlayerPrefs.DeleteKey (profile + "OrangeSaucer attack");
		PlayerPrefs.DeleteKey (profile + "OrangeSaucer speed");
		PlayerPrefs.DeleteKey (profile + "OrangeSaucer HP");

		PlayerPrefs.DeleteKey (profile + "Rambustun attack");
		PlayerPrefs.DeleteKey (profile + "Rambustun speed");
		PlayerPrefs.DeleteKey (profile + "Rambustun HP");

		PlayerPrefs.DeleteKey (profile + "LaserLeek attack");
		PlayerPrefs.DeleteKey (profile + "LaserLeek speed");
		PlayerPrefs.DeleteKey (profile + "LaserLeek HP");

		PlayerPrefs.DeleteKey (profile + "NecroMushroom attack");
		PlayerPrefs.DeleteKey (profile + "NecroMushroom speed");
		PlayerPrefs.DeleteKey (profile + "NecroMushroom HP");

		PlayerPrefs.DeleteKey (profile + "MegaOnion attack");
		PlayerPrefs.DeleteKey (profile + "MegaOnion speed");
		PlayerPrefs.DeleteKey (profile + "MegaOnion HP");

		// the heroes
		PlayerPrefs.DeleteKey (profile + "circle_carrot");
		PlayerPrefs.DeleteKey (profile + "circle_banana");
		PlayerPrefs.DeleteKey (profile + "circle_watermelon");
		PlayerPrefs.DeleteKey (profile + "circle_orange");
		PlayerPrefs.DeleteKey (profile + "circle_rambutan");
		PlayerPrefs.DeleteKey (profile + "circle_leek");
		PlayerPrefs.DeleteKey (profile + "circle_mushroom");
		PlayerPrefs.DeleteKey (profile + "circle_onion");
		PlayerPrefs.DeleteKey (profile + "circle_coconut");
		PlayerPrefs.DeleteKey (profile + "circle_carrotCannon");
		PlayerPrefs.DeleteKey (profile + "circle_healingBanana");
		PlayerPrefs.DeleteKey (profile + "circle_blasterMelon");
		PlayerPrefs.DeleteKey (profile + "circle_orangeSaucer");
		PlayerPrefs.DeleteKey (profile + "circle_rambustun");
		PlayerPrefs.DeleteKey (profile + "circle_laserLeek");
		PlayerPrefs.DeleteKey (profile + "circle_necroMushroom");
		PlayerPrefs.DeleteKey (profile + "circle_megaOnion");

		// level statuses
		PlayerPrefs.DeleteKey (profile + "Level 1-1");
		PlayerPrefs.DeleteKey (profile + "Level 1-2");
		PlayerPrefs.DeleteKey (profile + "Level 1-3");
		PlayerPrefs.DeleteKey (profile + "Level 2-1");
		PlayerPrefs.DeleteKey (profile + "Level 2-2");
		PlayerPrefs.DeleteKey (profile + "Level 2-3");
		PlayerPrefs.DeleteKey (profile + "Level 3-1");
		PlayerPrefs.DeleteKey (profile + "Level 3-2");
		PlayerPrefs.DeleteKey (profile + "Level 3-3");
		PlayerPrefs.DeleteKey (profile + "Level 4-1");
		PlayerPrefs.DeleteKey (profile + "Level 4-2");
		PlayerPrefs.DeleteKey (profile + "Level 4-3");
		PlayerPrefs.DeleteKey (profile + "Level 5-1");
		PlayerPrefs.DeleteKey (profile + "Level 5-2");
		PlayerPrefs.DeleteKey (profile + "Level 5-3");
		PlayerPrefs.DeleteKey (profile + "Level 6-1");
		PlayerPrefs.DeleteKey (profile + "Level 6-2");
		PlayerPrefs.DeleteKey (profile + "Level 6-3");
		PlayerPrefs.DeleteKey (profile + "Level 7-1");
		PlayerPrefs.DeleteKey (profile + "Level 7-2");
		PlayerPrefs.DeleteKey (profile + "Level 7-3");
		PlayerPrefs.DeleteKey (profile + "Level 8-1");
		PlayerPrefs.DeleteKey (profile + "Level 8-2");
		PlayerPrefs.DeleteKey (profile + "Level 8-3");
		PlayerPrefs.DeleteKey (profile + "Level 9-1");
		PlayerPrefs.DeleteKey (profile + "Level 9-2");
		PlayerPrefs.DeleteKey (profile + "Level 9-3");
		PlayerPrefs.DeleteKey (profile + "Level 10-1");
		PlayerPrefs.DeleteKey (profile + "Level 10-2");
		PlayerPrefs.DeleteKey (profile + "Level 10-3");

		//statuses
		PlayerPrefs.DeleteKey (profile + "Level 1-1_status");
		PlayerPrefs.DeleteKey (profile + "Level 1-2_status");
		PlayerPrefs.DeleteKey (profile + "Level 1-3_status");
		PlayerPrefs.DeleteKey (profile + "Level 2-1_status");
		PlayerPrefs.DeleteKey (profile + "Level 2-2_status");
		PlayerPrefs.DeleteKey (profile + "Level 2-3_status");
		PlayerPrefs.DeleteKey (profile + "Level 3-1_status");
		PlayerPrefs.DeleteKey (profile + "Level 3-2_status");
		PlayerPrefs.DeleteKey (profile + "Level 3-3_status");
		PlayerPrefs.DeleteKey (profile + "Level 4-1_status");
		PlayerPrefs.DeleteKey (profile + "Level 4-2_status");
		PlayerPrefs.DeleteKey (profile + "Level 4-3_status");
		PlayerPrefs.DeleteKey (profile + "Level 5-1_status");
		PlayerPrefs.DeleteKey (profile + "Level 5-2_status");
		PlayerPrefs.DeleteKey (profile + "Level 5-3_status");
		PlayerPrefs.DeleteKey (profile + "Level 6-1_status");
		PlayerPrefs.DeleteKey (profile + "Level 6-2_status");
		PlayerPrefs.DeleteKey (profile + "Level 6-3_status");
		PlayerPrefs.DeleteKey (profile + "Level 7-1_status");
		PlayerPrefs.DeleteKey (profile + "Level 7-2_status");
		PlayerPrefs.DeleteKey (profile + "Level 7-3_status");
		PlayerPrefs.DeleteKey (profile + "Level 8-1_status");
		PlayerPrefs.DeleteKey (profile + "Level 8-2_status");
		PlayerPrefs.DeleteKey (profile + "Level 8-3_status");
		PlayerPrefs.DeleteKey (profile + "Level 9-1_status");
		PlayerPrefs.DeleteKey (profile + "Level 9-2_status");
		PlayerPrefs.DeleteKey (profile + "Level 9-3_status");
		PlayerPrefs.DeleteKey (profile + "Level 10-1_status");
		PlayerPrefs.DeleteKey (profile + "Level 10-2_status");
		PlayerPrefs.DeleteKey (profile + "Level 10-3_status");

		// insects
		PlayerPrefs.DeleteKey (profile + "ant");
		PlayerPrefs.DeleteKey (profile + "fly");
		PlayerPrefs.DeleteKey (profile + "Cicada");
		PlayerPrefs.DeleteKey (profile + "Grasshopper");
		PlayerPrefs.DeleteKey (profile + "Snail");
		PlayerPrefs.DeleteKey (profile + "Caterpillar");
		PlayerPrefs.DeleteKey (profile + "StinkBug");
		PlayerPrefs.DeleteKey (profile + "Beetles");
		PlayerPrefs.DeleteKey (profile + "MoleCricket");
		PlayerPrefs.DeleteKey (profile + "Locusts");
		PlayerPrefs.DeleteKey (profile + "Insect Queen");
		PlayerPrefs.DeleteKey (profile + "Queen BitterG");
		PlayerPrefs.DeleteKey (profile + "Mice");
		PlayerPrefs.DeleteKey (profile + "Mole");
		PlayerPrefs.DeleteKey (profile + "Boss Fly");
		PlayerPrefs.DeleteKey (profile + "Beetle 1");
		PlayerPrefs.DeleteKey (profile + "Prideful worm");
		PlayerPrefs.DeleteKey (profile + "Boss Caterpillar");
		PlayerPrefs.DeleteKey (profile + "Beetle 2");
		PlayerPrefs.DeleteKey (profile + "Beetle 3");

		if(overwrite){
			loadingScreen.LoadScene("start_storyline");
		}

		else if(erase){
			loadingScreen.LoadScene("profile select 2");
		}
//		PlayerPrefs.DeleteKey (profile + "");
//		PlayerPrefs.DeleteKey (profile + "");
//		PlayerPrefs.DeleteKey (profile + "");
//		PlayerPrefs.DeleteKey (profile + "");
//		PlayerPrefs.DeleteKey (profile + "");
//		PlayerPrefs.DeleteKey (profile + "");
//		PlayerPrefs.DeleteKey (profile + "");
//		PlayerPrefs.DeleteKey (profile + "");
//		PlayerPrefs.DeleteKey (profile + "");
//		PlayerPrefs.DeleteKey (profile + "");
//		PlayerPrefs.DeleteKey (profile + "");
//		PlayerPrefs.DeleteKey (profile + "");
//		PlayerPrefs.DeleteKey (profile + "");
//		PlayerPrefs.DeleteKey (profile + "");
//		PlayerPrefs.DeleteKey (profile + "");
	}
}
