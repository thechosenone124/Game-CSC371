using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Zachary Barram
 * Brandon Ryan
 */
public class RepairControlsCollider : MonoBehaviour {


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
                if (shipInfo.SetFixer(collision.gameObject)) // returns true when there is nobody controlling
                {
                    pcon.isOperatingStation = true;
                    shipInfo.freezePlayer(collision.gameObject);
                    collision.gameObject.GetComponent<CameraHolder>().playerCamera.SetActive(false);
                }
            }
            else if (pcon.GetBButton() && pcon.isOperatingStation)
            {
                //Debug.Log("Attempting to remove pilot");
                if (shipInfo.RemoveFixer(collision.gameObject)) // returns true if collision.gameObject is the player set as pilot
                {
                    pcon.isOperatingStation = false;
                    shipInfo.unfreezePlayer(collision.gameObject);
                    collision.gameObject.GetComponent<CameraHolder>().playerCamera.SetActive(true);
                }

            }
        }
    }
}
