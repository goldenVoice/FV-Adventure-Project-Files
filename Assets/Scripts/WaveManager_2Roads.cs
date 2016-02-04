using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveManager_2Roads : MonoBehaviour {

	GameObject road1;
	GameObject road2;
	public Button nextWaveButton;
	public SpriteManager nextWaveIndicator;
	
	// Use this for initialization
	void Start () {
		road1 = GameObject.Find("Road");
		road2 = GameObject.Find("Road2");
		nextWaveIndicator  = (SpriteManager) FindObjectOfType(typeof(SpriteManager));

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startWave (){
		nextWaveButton.GetComponent<Animator>().SetTrigger("start_wave");
		road1.GetComponent<SpawnEnemy>().enabled = true;
		road2.GetComponent<SpawnEnemy>().enabled = true;
		nextWaveIndicator.gameObject.GetComponent<Image>().enabled = false;
		//nextWaveButton.GetComponent<Image>().enabled = false;
		//nextWaveButton.enabled = false;
	}
}
