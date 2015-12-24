using UnityEngine;
using System.Collections;

public class Potholes_list : MonoBehaviour {

	public GameObject[] potholesList;

	// Use this for initialization
	void Start () {
		potholesList = GameObject.FindGameObjectsWithTag("potholes");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
