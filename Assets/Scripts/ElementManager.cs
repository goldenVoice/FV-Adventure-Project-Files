using UnityEngine;
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
	private SpawnEnemy spawnEnemy_road2;	// for the 2nd road
	
	public Random rnd;

	// Awake() is called before loading any script in the scene. so pwede ka mag initialize dito before the Start() is called :)
	void Awake(){

		// this should find the only object that has the SpawnEnemy script, which is the object road
		rnd = new Random();
		
		spawnEnemy = (SpawnEnemy) GameObject.Find ("Road").GetComponent<SpawnEnemy>();

		if (GameObject.Find ("Road2") != null) {
			spawnEnemy_road2 = (SpawnEnemy) GameObject.Find ("Road2").GetComponent<SpawnEnemy>();
		}
		
		/* Element
		 * 	Fire = 0
		 * 	Water = 1
		 * 	Air = 2
		 */
		// loop through the waves and assign a random element
		for(int i = 0; i < spawnEnemy.waves.Length; i++){
			// convert the resulting integer into an enum element
			spawnEnemy.waves[i].WaveElement = (Element)(Random.Range(0, 3));   // ex: range returned 2, convert to Element, then 2 = Air

			if(spawnEnemy_road2 != null){
				Debug.Log ("the 2nd road!");
				spawnEnemy_road2.waves[i].WaveElement = spawnEnemy.waves[i].WaveElement;
			}
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
			Debug.Log("Damage fire to air: " + ((damage + ( damage * 0.1f))));
			return damage + ( damage * 0.1f);
		}
		else if(attacker == Element.Fire && defender == Element.Water){
			Debug.Log("Damage fire to water: " + ((damage - ( damage * 0.1f))));
			return  damage - ( damage * 0.1f);
		}
		else if(attacker == Element.Fire && defender == Element.Fire){
			return damage;
		}

		// matches and effects for water element
		else if(attacker == Element.Water && defender == Element.Fire){
			Debug.Log("dammage(" + damage + ") + 30% (" + ( damage * 0.1f) + ")");
			return damage + ( damage * 0.1f);
		}
		else if(attacker == Element.Water && defender == Element.Air){
			return damage  - ( damage * 0.1f);
		}
		else if(attacker == Element.Water && defender == Element.Water){
			return damage;
		}

		// matches and effects for Air element
		else if(attacker == Element.Air && defender == Element.Water){
			return damage + ( damage * 0.1f);
		}
		else if(attacker == Element.Air && defender == Element.Fire){
			return damage  - ( damage * 0.1f);
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