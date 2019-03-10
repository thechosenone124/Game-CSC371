using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddModule : MonoBehaviour {
	private const int NOAHGUN = 5;
	private bool partSpawned = false;
	private GameObject players;
	private GameObject player1;
	private GameObject player2;
	private GameObject ship;
        
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
		if(other.CompareTag("EngineRoom") && (Input.GetButtonDown("J1X") || Input.GetButtonDown("J2X"))){
			GameController.instance.setHealth(100);
			GameController.instance.UpdateHealthBar();
			GameController.instance.EnableUpgradeMenu();
			GameObject.Find("Ship").transform.rotation = Quaternion.identity;
			GameObject.Find("Ship").GetComponent<ShipMovementDavin>().velocity = Vector3.zero;
            GameController.instance.SetStateToModifyingShip();

			players = GameObject.Find("Players");
			player1 = players.transform.GetChild(0).gameObject;
			player2 = players.transform.GetChild(1).gameObject;
			ship = GameObject.Find("Ship").gameObject;
			if (ship.GetComponent<ShipInfoDavin>().RemoveGunner(player1))
			{
				player1.GetComponent<PlayerInputContainer>().isOperatingStation = false;
			}
			if (ship.GetComponent<ShipInfoDavin>().RemovePilot(player1))
			{
				player1.GetComponent<PlayerInputContainer>().isOperatingStation = false;
			}
			if (ship.GetComponent<ShipInfoDavin>().RemoveFixer(player1))
			{
				player1.GetComponent<PlayerInputContainer>().isOperatingStation = false;
			}
			if (ship.GetComponent<ShipInfoDavin>().RemoveGunner(player2))
			{
				player2.GetComponent<PlayerInputContainer>().isOperatingStation = false;
			}
			if (ship.GetComponent<ShipInfoDavin>().RemovePilot(player2))
			{
				player2.GetComponent<PlayerInputContainer>().isOperatingStation = false;
			}
			if (ship.GetComponent<ShipInfoDavin>().RemoveFixer(player2))
			{
				player2.GetComponent<PlayerInputContainer>().isOperatingStation = false;
			}
			player1.GetComponent<CameraHolder>().playerCamera.SetActive(false);
			player2.GetComponent<CameraHolder>().playerCamera.SetActive(false);
			player1.GetComponent<PlayerMovementControllerDavin>().SetPlayerCurrentPosition(new Vector3(-1.5f,-7.0f,0));
			player2.GetComponent<PlayerMovementControllerDavin>().SetPlayerCurrentPosition(new Vector3(1.5f,-7.0f,0));
			ship.GetComponent<ShipInfoDavin>().freezePlayer(player1);
			ship.GetComponent<ShipInfoDavin>().freezePlayer(player2);
		}
	}
}
