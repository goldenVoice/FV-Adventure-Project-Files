using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyData : MonoBehaviour {
	public enum pathWay{
		flying,
		walking,
	}

	[System.Serializable]
	public class speedLevel{
		public float speed;
	}

	[System.Serializable]
	public class healthLevel{
		public float health;
	}

  	public int waterRewarded;
	public ElementManager.Element enemyElement;
	public pathWay insectPath;
	public List<speedLevel> enemySpeed;
	public List<healthLevel> enemyHP;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
