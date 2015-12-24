using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RangeManager : MonoBehaviour {

	public bool heroSelected;

	public GameObject range;
	public GameObject x_mark;

	Button hiddenButton;	// akala ng user, yung pipindutin nila, yung hero, actually eto yun.

	private List_hero list_hero;

	bool selected = false;

	// Use this for initialization
	void Start () {
//		range = gameObject.transform.Find("range");
		
		list_hero = (List_hero)GameObject.Find("list_hero").GetComponent<List_hero>();	
		//Debug.Log("DAPAT LALABAS TO PAG KATANIM");
//		range.renderer.enabled = false;
		x_mark.SetActive(false);

		//hiddenButton = (Button)gameObject.AddComponent<Button>;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if(this.heroSelected){
			range.renderer.enabled = false;
			x_mark.SetActive(false);
			this.heroSelected = false;
		}
		else{
		this.heroSelected = true;
	//	Debug.Log("Instance ID from RAngeManager: " + this.GetInstanceID());
		list_hero.checkOtherHeroes(this);		// this function loops through the whole list of planted heroes. then disables 
												// 'this' variable represents the script instance. 
		}

	}

 // void OnMouseUp(){
 //		HideRange();
 //	}

	public void ShowRange(){
		Debug.Log("range: " + range);
			range.renderer.enabled = true;
			selected = false;
	}

	public void HideRange(){
			range.renderer.enabled = false;
	}
}
