using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddModule : MonoBehaviour {
	private const int NOAHGUN = 5;
	private bool partSpawned = false;

	public GameObject ShipConfigurer;
	public int x = 1, y = 4;
	private bool tog = false;

        
	void OnTriggerEnter2D(Collider2D other){
		/*if(other.CompareTag("Cockpit")){
			if(Input.GetButtonDown("Y") && !tog){
				ShipConfigurer.GetComponent<UpgradeCanvasManager>().enableUI();
				tog = !tog;
			}
			if(Input.GetButtonDown("Y") && tog){
				ShipConfigurer.GetComponent<UpgradeCanvasManager>().disableUI();
				tog = !tog;
			}
		}*/
		if(other.CompareTag("Cockpit")){
			GameController.instance.setHealth(100);
			GameController.instance.UpdateHealthBar();
		}
	}
}
