using UnityEngine;
using System.Collections;

public class saucerLayerManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// make the layer name of the sacuer same as its parent, to achieve nakatago saucer effect
		GameObject orangeHero = gameObject.transform.parent.parent.gameObject;
		gameObject.renderer.sortingLayerName = orangeHero.renderer.sortingLayerName;	
		orangeHero.transform.GetChild(0).renderer.sortingLayerName = orangeHero.renderer.sortingLayerName;	
		

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
