using UnityEngine;
using System.Collections;

public class MushroomSummon : MonoBehaviour {
	HeroAttack heroattack;
	
	// Use this for initialization
	void Start () {
		heroattack = GetComponent<HeroAttack>();
	}
	
	// Update is called once per frame
	void Update () {
		if(heroattack.enabled == true){

		}
	}
}
