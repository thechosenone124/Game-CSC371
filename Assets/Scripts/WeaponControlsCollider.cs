using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControlsCollider : MonoBehaviour {

    private GameObject ship;
    private bool AButtonReleased = true;

    private ShipInformation shipInfo;
    // Use this for initialization
    void Start()
    {
        ship = GameObject.Find("Ship");
        shipInfo = ship.GetComponent<ShipInformation>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Ship controls collided with a player");
            PlayerInputContainer pcon = collision.GetComponent<PlayerInputContainer>();
            Debug.Log(AButtonReleased);
            if (pcon.GetAButton() && !pcon.isOperatingStation)
            {
                if (shipInfo.SetGunner(collision.gameObject)) // returns true when there is nobody controlling
                {
                    Debug.Log("Name on gunner collision: " + collision.gameObject.name);
                    Debug.Log(collision.gameObject.name + "'s a button: " + pcon.GetAButton());
                    pcon.isOperatingStation = true;
                    //collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    collision.gameObject.GetComponent<CameraHolder>().playerCamera.SetActive(false);
                    collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    collision.gameObject.GetComponent<Collider2D>().isTrigger = true;
                }
                AButtonReleased = false;
            }
            
            else if (pcon.GetBButton() && pcon.isOperatingStation)
            {
                Debug.Log("Attempting to remove gunner");
                if (shipInfo.RemoveGunner(collision.gameObject)) // returns true if collision.gameObject is the player set as pilot
                {
                    pcon.isOperatingStation = false;
                    //collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    collision.gameObject.GetComponent<Collider2D>().isTrigger = false;
                    collision.gameObject.GetComponent<CameraHolder>().playerCamera.SetActive(true);
                }
                AButtonReleased = false;
            }

            //if (pcon.GetBButton()) Debug.Log("B was pressed inside gunner collider");
            //if (pcon.GetAButton()) Debug.Log("A was pressed inside gunner collider");
        }
    }
}
