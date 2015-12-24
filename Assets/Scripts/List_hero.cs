using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class List_hero : MonoBehaviour {

  public List<GameObject> plantedHeroes;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	       
	}

  public void addHero(GameObject hero){
      plantedHeroes.Add(hero);
  }
                                // the parameter is a RangeManager script
  public void checkOtherHeroes(RangeManager currentSelectedHero){
      foreach(GameObject hero in plantedHeroes){
              // check if the hero is the currentSelectedHero by checking if their InstanceID are the same.
            //print(hero.GetComponent<RangeManager>().GetInstanceID());
			if(currentSelectedHero.GetInstanceID() == hero.GetComponent<RangeManager>().GetInstanceID() ){
//                Debug.Log("makita ka dapt");
				hero.GetComponent<RangeManager>().range.renderer.enabled = true;
				hero.GetComponent<RangeManager>().x_mark.SetActive(true);
              }
              else{
//                print("Range is disabled");
				hero.GetComponent<RangeManager>().range.renderer.enabled = false;
				hero.GetComponent<RangeManager>().heroSelected = false;
				hero.GetComponent<RangeManager>().x_mark.SetActive(false);
              }
      }
  }

  public void deselectAllHeroes(){
      foreach(GameObject hero in plantedHeroes){
            hero.GetComponent<RangeManager>().range.renderer.enabled = false;
            hero.GetComponent<RangeManager>().x_mark.SetActive(false);
			hero.GetComponent<RangeManager>().heroSelected = false;
      }      
  }
  	
  public void removeHeroFromList(GameObject heroToRemove){
		foreach(GameObject hero in plantedHeroes){
			if(hero == heroToRemove){
				plantedHeroes.Remove(heroToRemove);
				break;		// once the hero to remove is found. stop the loop. so you wont get the error: "InvalidOperation Exception"
			}
		}
	}
}
