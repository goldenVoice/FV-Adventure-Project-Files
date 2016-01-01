using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

	public List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
		items.Add(new Item("Potion", 0, "Restores 50% of the player's health", 10, 10, 1,(Item.ItemType.Consumable) ) );
	}

}
