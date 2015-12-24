using UnityEngine;
using System.Collections;

public class invisible_colliderManager : MonoBehaviour {

  private List_hero list_hero;

	// Use this for initialization
	void Start () {
	     list_hero = (List_hero) GameObject.FindObjectOfType(typeof(List_hero));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnMouseDown(){
//    print("invisible collider is touched");
      list_hero.deselectAllHeroes();
  }

}
