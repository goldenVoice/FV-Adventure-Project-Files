using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour {

	public Sprite[] elementIndicator;
	string waveElement;
	string insectPathWay;
	bool isFlying, isWalking;
	string indicator_spriteName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void displayNextElement(Wave[] waves, int nextWave){
//		Debug.Log("Current wave number: " + nextWave);
//		Debug.Log("Current wave element: " + waves[nextWave].WaveElement);
		if(waves[nextWave].WaveElement == ElementManager.Element.Fire){
			waveElement = "fire";
		}
		else if(waves[nextWave].WaveElement == ElementManager.Element.Water){
			waveElement = "water";
			
		}
		else if(waves[nextWave].WaveElement == ElementManager.Element.Air){
			waveElement = "air";
			
		}
		foreach(GameObject enemy in waves[nextWave].enemies){
			// loop through each enemy, check if it is a flying or walking insect
			// if they are both, set the insect pathway string to 'both'
//			Debug.Log ("NEMY: " + enemy);
			// check if the enemy is flying
			if(enemy.transform.GetChild(0).GetComponent<EnemyData>().insectPath == EnemyData.pathWay.flying){
				isFlying = true;
			}
			else if(enemy.transform.GetChild(0).GetComponent<EnemyData>().insectPath == EnemyData.pathWay.walking){
				isWalking = true;
			}
		}

		// set the string 
		if(isFlying && isWalking){
			insectPathWay = "both";
		}
		else if(isWalking){
			insectPathWay = "walk";
		}
		else if(isFlying){
			insectPathWay = "fly";
		}

		// now that you got the information, its time to set the string to find the sprite
		indicator_spriteName = waveElement + "_" + insectPathWay;
//		Debug.Log ("CURRENT NAME: " + indicator_spriteName);
		// check which sprite matches the name you got
		foreach(Sprite currentIndicator in elementIndicator){

			if(currentIndicator.name == indicator_spriteName){
				gameObject.GetComponent<Image>().sprite = currentIndicator;
				break;
			}
		}

		// now change the source image name, to the newly acquired sprite name

	}
}
