﻿using UnityEngine;
using System.Collections;

/* Elemental Precedence
 * 
 * Water defeats Fire
 * Fire defeats Air
 * Air defeats Water
 */

public class ElementManager : MonoBehaviour {
	// this is how to define an enum
	public enum Element {	// the Element is the name of the enum
		Fire,		// the Fire, Water, Air, each items are
		Water,		// called enumerator
		Air,
	}

	private SpawnEnemy spawnEnemy;
	public Random rnd;

	// Awake() is called before loading any script in the scene. so pwede ka mag initialize dito before the Start() is called :)
	void Awake(){

		// this should find the only object that has the SpawnEnemy script, which is the object road
		rnd = new Random();
		
		spawnEnemy = (SpawnEnemy) FindObjectOfType(typeof(SpawnEnemy));
		
		/* Element
		 * 	Fire = 0
		 * 	Water = 1
		 * 	Air = 2
		 */
		// loop through the waves and assign a random element
		for(int i = 0; i < spawnEnemy.waves.Length; i++){
			// convert the resulting integer into an enum element
			spawnEnemy.waves[i].WaveElement = (Element)(Random.Range(0, 3));   // ex: range returned 2, convert to Element, then 2 = Air
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float checkElement(Element attacker, Element defender, float damage){

		// matches and effects for fire element
		if(attacker == Element.Fire && defender == Element.Air){
			return damage * 2.0f;
		}
		else if(attacker == Element.Fire && defender == Element.Water){
			return damage / 2.0f;
		}
		else if(attacker == Element.Fire && defender == Element.Fire){
			return damage;
		}

		// matches and effects for water element
		else if(attacker == Element.Water && defender == Element.Fire){
			return damage * 2.0f;
		}
		else if(attacker == Element.Water && defender == Element.Air){
			return damage / 2.0f;
		}
		else if(attacker == Element.Water && defender == Element.Water){
			return damage;
		}

		// matches and effects for Air element
		else if(attacker == Element.Air && defender == Element.Water){
			return damage * 2.0f;
		}
		else if(attacker == Element.Air && defender == Element.Fire){
			return damage / 2.0f;
		}
		else if(attacker == Element.Air && defender == Element.Air){
			return damage;
		}
		// this shouldnt match
		else{
			Debug.LogError("Element checker reached an impossible match! Damage may ");
			return damage;
		}
		
	}

}