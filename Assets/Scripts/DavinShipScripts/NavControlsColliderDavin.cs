using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Davin Johnson
 */
public class NavControlsColliderDavin : MonoBehaviour
{

    private GameObject ship;

    private ShipInfoDavin shipInfo;
    // Use this for initialization
    void Start()
    {
        ship = GameObject.Find("Ship");
        shipInfo = ship.GetComponent<ShipInfoDavin>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInputContainer pcon = collision.GetComponent<PlayerInputContainer>();
            if (pcon.GetAButton() && !pcon.isOperatingStation)
            {
                if (shipInfo.SetPilot(collision.gameObject)) // returns true when there is nobody controlling
                {
                    //Debug.Log("Name on pilot collision: " + collision.gameObject.name);
                    //Debug.Log(collision.gameObject.name + "'s a button: " + pcon.GetAButton());
                    pcon.isOperatingStation = true;
                    shipInfo.freezePlayer(collision.gameObject);
                    //collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    //collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    //collision.gameObject.GetComponent<Collider2D>().isTrigger = true;
                    collision.gameObject.GetComponent<CameraHolder>().playerCamera.SetActive(false);
                }
            }
            else if (pcon.GetBButton() && pcon.isOperatingStation)
            {
                //Debug.Log("Attempting to remove pilot");
                if (shipInfo.RemovePilot(collision.gameObject)) // returns true if collision.gameObject is the player set as pilot
                {
                    pcon.isOperatingStation = false;
                    shipInfo.unfreezePlayer(collision.gameObject);
                    //collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    //collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    //collision.gameObject.GetComponent<Collider2D>().isTrigger = false;
                    collision.gameObject.GetComponent<CameraHolder>().playerCamera.SetActive(true);
                }

            }
        }
    }
}
