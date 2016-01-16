using UnityEngine;
using System.Collections;

// this script is for the implementation of the carrot cannon sa land enemies. para ma apply yung damage ng maayos. hinde sobra sobra
// kase pag ang cheking ginawa sa cannon impact mismo mahirap kase di kaya ng ontriggerEnter2D ng sabay sabay na collider
// pag OnTriggerStay2D ang ginamet sobra sobra naman ang bawas. yung pag ka detect sa kanila sobra sobra
public class CarrotCannon_LandEnemy_ApplyDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		// 
		
		if (gameObject.GetComponent<EnemyData>().insectPath == EnemyData.pathWay.walking) {
			if (other.name == "carrotCannon_areaImpact(Clone)" || other.name == "blasterMelon_areaImpact(Clone)") {
//				Debug.Log("gumana friend name is: " + other.name);
					
				other.SendMessage("ApplyDamage", gameObject.GetComponent<Collider2D>());
			}
		}

	}
}
