using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

	GameObject road;
	public Button nextWaveButton;
	public SpriteManager nextWaveIndicator;
	
	// Use this for initialization
	void Start () {
		road = GameObject.Find("Road");
		nextWaveIndicator  = (SpriteManager) FindObjectOfType(typeof(SpriteManager));

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startWave (){
		nextWaveButton.GetComponent<Animator>().SetTrigger("start_wave");
		road.GetComponent<SpawnEnemy>().enabled = true;
		nextWaveIndicator.gameObject.GetComponent<Image>().enabled = false;
		//nextWaveButton.GetComponent<Image>().enabled = false;
		//nextWaveButton.enabled = false;
	}
}
