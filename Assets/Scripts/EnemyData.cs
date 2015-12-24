using UnityEngine;
using System.Collections;

public class EnemyData : MonoBehaviour {
	public enum pathWay{
		flying,
		walking,
	}
  	public int waterRewarded;
	public ElementManager.Element enemyElement;
	public pathWay insectPath;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
