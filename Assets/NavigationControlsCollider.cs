using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationControlsCollider : MonoBehaviour {

    public GameObject ship;

    private ShipInformation shipInfo;
	// Use this for initialization
	void Start () {
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
                    pcon.isOperatingStation = true;
                }
            }
            else if (pcon.GetBButton() && pcon.isOperatingStation)
            {
                if (shipInfo.RemovePilot(collision.gameObject)) // returns true if collision.gameObject is the player set as pilot
                {
                    pcon.isOperatingStation = false;
                }

            }
        }
    }
}
