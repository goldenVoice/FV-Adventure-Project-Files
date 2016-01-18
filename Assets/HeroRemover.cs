using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroRemover : MonoBehaviour {
	public GameObject heroSlot;		// this slot references the original slot kung nasan yung hero
									// may isa pang set of slot, kung san naka attach tong script na to. to handle removing of heroes	
	public GameObject heroSelection;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RemoveHero(){
		if (heroSlot.transform.childCount != 0) {
			// enable the removed hero at the selection
			string heroName = heroSlot.transform.GetChild(0).gameObject.name;
			heroName = heroName.Substring(0, heroName.Length - 7);
			print(heroName);
			heroSelection.transform.Find(heroName).GetComponent<Button>().interactable = true;
			heroSelection.transform.Find(heroName).GetComponent<Image>().color = Color.white;
			
			// remove the hero from this slot, 
			Destroy(heroSlot.transform.GetChild(0).gameObject);
		}
	}

}
