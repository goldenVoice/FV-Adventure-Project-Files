using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyData : MonoBehaviour {
	public enum pathWay{
		flying,
		walking,
		both,	// actually tong 'both' never mong gagamitin to for enemy. ang gagamit lang nito is hero
	}

//	[System.Serializable]
//	public class speedLevel{
//		public float speed;
//	}

	[System.Serializable]
	public class healthLevel{
		public float health;
	}

	[System.Serializable]
	public class attackLevel{
		public float attack;
	}


	[HideInInspector]
	public bool slowed;		// para sa onion hero. will be used to check if this enemy is already hit first time. para di maging super slow

  	public int waterRewarded;
	public ElementManager.Element enemyElement;
	public pathWay insectPath;
//	public List<speedLevel> enemySpeed;
	public List<healthLevel> enemyHP;
	public List<attackLevel> enemyAttack;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
