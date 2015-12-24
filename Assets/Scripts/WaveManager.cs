using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

	GameObject road;
	public Button nextWaveButton;
	// Use this for initialization
	void Start () {
		road = GameObject.Find("Road");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startWave (){
		nextWaveButton.GetComponent<Animator>().SetTrigger("start_wave");
		road.GetComponent<SpawnEnemy>().enabled = true;
		//nextWaveButton.GetComponent<Image>().enabled = false;
		//nextWaveButton.enabled = false;
	}
}
