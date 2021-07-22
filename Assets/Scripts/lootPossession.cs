using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lootPossession : MonoBehaviour
{

	public bool hasLoot = false;
	public GameObject loot;

	bool lootVisible = false;

	void Start(){
	}

	void Update(){
		if(hasLoot && !lootVisible && loot.tag == "key"){
			lootVisible = true;
			this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
		}
	}
}
