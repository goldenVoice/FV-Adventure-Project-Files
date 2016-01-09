using UnityEngine;
using System.Collections;

public class OnionBulletEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Enemy"){
			Debug.Log("enemy will be slowed");
			if( !(other.GetComponent<EnemyData>().slowed) ){	// check if the var slowed is false,
				float enemySpeed = other.transform.parent.GetComponent<MoveEnemy>().speed;
				other.transform.parent.GetComponent<MoveEnemy>().speed -= (enemySpeed * 0.2f);		// slow the enemy by 20%
				other.GetComponent<EnemyData>().slowed = true;
			}
		}
	}

}
