using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationControlsCollider : MonoBehaviour {

    private GameObject ship;
    private ShipInformation shipInfo;
	 // Use this for initialization
	 void Start () {
        ship = GameObject.Find("Ship");
        shipInfo = ship.GetComponent<ShipInformation>();
	 }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerInputContainer pcon = collision.GetComponent<PlayerInputContainer>();
            if (pcon.GetAButton() && !pcon.isOperatingStation)
            {
                if (shipInfo.SetPilot(collision.gameObject)) // returns true when there is nobody controlling
                {
                    Debug.Log("Name on pilot collision: " + collision.gameObject.name);
                    Debug.Log(collision.gameObject.name + "'s a button: " + pcon.GetAButton());
                    pcon.isOperatingStation = true;
                    //collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    collision.gameObject.GetComponent<Collider2D>().isTrigger = true;
                    //collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;    
                    //disabling box collider fixes awkward flying if player is colliding with control station while flying, but then player can't exit station
                    collision.gameObject.GetComponent<CameraHolder>().playerCamera.SetActive(false);
                }
            }
            else if (pcon.GetBButton() && pcon.isOperatingStation)
            {
                Debug.Log("Attempting to remove pilot");
                if (shipInfo.RemovePilot(collision.gameObject)) // returns true if collision.gameObject is the player set as pilot
                {
                    pcon.isOperatingStation = false;
                    //collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    collision.gameObject.GetComponent<Collider2D>().isTrigger = false;
                    //collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    collision.gameObject.GetComponent<CameraHolder>().playerCamera.SetActive(true);
                }
            }
        }
    }
}
