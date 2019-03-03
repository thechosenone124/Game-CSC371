using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddModule : MonoBehaviour {
	private const int NOAHGUN = 5;
	private bool partSpawned = false;

	public GameObject ShipConfigurer;
	public int x = 1, y = 4;

        
	void OnTriggerStay2D(Collider2D other){
		
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
		if(other.CompareTag("Cockpit") && (Input.GetButtonDown("J1X") || Input.GetButtonDown("J2X"))){
			GameController.instance.setHealth(100);
			GameController.instance.UpdateHealthBar();
			GameController.instance.EnableUpgradeMenu();
			GameObject.Find("Ship").transform.rotation = Quaternion.identity;
			GameObject.Find("Ship").GetComponent<ShipMovementDavin>().velocity = Vector3.zero;
		}
	}
}
