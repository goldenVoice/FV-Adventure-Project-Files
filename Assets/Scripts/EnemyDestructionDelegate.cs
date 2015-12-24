using UnityEngine;
using System.Collections;

public class EnemyDestructionDelegate : MonoBehaviour {

  // a delegate is a container function that can be passed around like a variable,
  public delegate void EnemyDelegate (GameObject enemy);
  public EnemyDelegate enemyDelegate;   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnDestroy(){
    if(enemyDelegate != null){
      enemyDelegate(gameObject);
    }
  }

}
