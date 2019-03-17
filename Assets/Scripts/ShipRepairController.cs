using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRepairController : MonoBehaviour {
	private float timer = 0;
	private float coolDownTime = 100;
	public float regenAmt = .25f;
	private bool regenBoost = false;

	public void TryRepair(PlayerInputContainer pcon){
		if(pcon.GetRTButton() == 1 && GameController.instance.boostBroken){
			timer += regenAmt;
			GameController.instance.SetBoost(timer);
			GameController.instance.UpdateBoost();
			if(timer >= coolDownTime){
				timer = 0;
				GameController.instance.boostBroken = false;
			}
		}
	}
}
