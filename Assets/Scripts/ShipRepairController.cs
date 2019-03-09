using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRepairController : MonoBehaviour {
	private float timer = 0;
	private float coolDownTime = 100;
	private float regenAmt = 3.0f;
	private bool regenBoost = false;

	public void TryRepair(PlayerInputContainer pcon){
		if(pcon.GetRTButton() == 1 && GameController.instance.boostBroken){
			timer += (float).25f;
			Debug.Log("timer: " + timer);
			GameController.instance.SetBoost(timer);
			GameController.instance.UpdateBoost();
			if(timer >= coolDownTime){
				timer = 0;
				GameController.instance.boostBroken = false;
			}
		}
	}
}
