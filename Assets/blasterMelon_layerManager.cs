using UnityEngine;
using System.Collections;

public class blasterMelon_layerManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
		gameObject.renderer.sortingLayerName = "hero_lowerSide";
		gameObject.renderer.sortingOrder = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
