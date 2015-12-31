using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shopManager : MonoBehaviour {

	public Text moneyText;
	// Use this for initialization
	void Start () {
		moneyText.text = PlayerPrefs.GetInt("Money").ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
