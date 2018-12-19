﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

	public GameObject healthCratePrefab;
	public GameObject ammoCratePrefab;
	public int spawnTime;

	private GameObject[] itemSpawnArray;
	private List<GameObject> itemObjectList;
	 
	// Use this for initialization
	void Start () {
		itemSpawnArray = GameObject.FindGameObjectsWithTag("Item");
		itemObjectList = new List<GameObject>();
		InvokeRepeating ("spawnItems", 5.0f, 20.0f);
	}

	private void spawnItems(){
		//Function responsible for randomly spawning items at the designated spawns.

		GameObject item;
		Transform itemSpawnLocation;

		if (itemObjectList.Count != 0) {
			foreach (GameObject itemObject in itemObjectList){
				Destroy (itemObject);
			}
			itemObjectList.Clear ();
		}
	
		foreach (GameObject itemSpawn in itemSpawnArray) {
			itemSpawnLocation = itemSpawn.transform;
			if (Random.value<0.5f) {
				item = Instantiate (healthCratePrefab, 
					                        itemSpawnLocation.position, itemSpawnLocation.rotation);
				
			} else {
				item = Instantiate (ammoCratePrefab, 
					itemSpawnLocation.position, itemSpawnLocation.rotation);
			}

			itemObjectList.Add (item);
		}
	}

}
