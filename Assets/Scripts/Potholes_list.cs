using UnityEngine;
using System.Collections;

public class Potholes_list : MonoBehaviour {

	public GameObject[] potholesList;

	void Awake(){
		potholesList = GameObject.FindGameObjectsWithTag("potholes");
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
