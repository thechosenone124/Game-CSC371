using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealShip : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Cockpit")){
         GameController.instance.setHealth(100);
         GameController.instance.UpdateHealthBar();
		}
	}
}
