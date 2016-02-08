using UnityEngine;
using System.Collections;

public class HeroPotion_StageChecker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string curLevel = Application.loadedLevelName;

		string stageNumber;
		
		if((Application.loadedLevelName.Length) == 10){		// kapag 10 characters na ang meron, meaning nasa last stage na sya. "Level 10-1" oh diba 10 characters na?
			stageNumber = curLevel.Substring (6, 2) ;
		}
		else{
			stageNumber = curLevel.Substring (6, 1) ;
		}

		int stageNum = int.Parse (stageNumber);


		if (stageNum <= 7) {
			gameObject.SetActive (false);
		}
		else {
			gameObject.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
