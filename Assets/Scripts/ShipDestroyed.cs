using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDestroyed : MonoBehaviour {
	void Update () {
		if(GameController.instance.getCurrentHealth() <= 0){
			PlayerDied();
		}
	}
	void PlayerDied(){
		gameObject.SetActive(false);
		GameController.instance.PlayerLoses();	
	}
}
