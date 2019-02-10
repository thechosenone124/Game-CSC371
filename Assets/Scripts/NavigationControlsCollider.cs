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
                    Debug.Log("Name on pilot collision: " + collision.gameObject.name);
                    Debug.Log(collision.gameObject.name + "'s a button: " + pcon.GetAButton());
                    pcon.isOperatingStation = true;
                    collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }
            }
            else if (pcon.GetBButton() && pcon.isOperatingStation)
            {
                Debug.Log("Attempting to remove pilot");
                if (shipInfo.RemovePilot(collision.gameObject)) // returns true if collision.gameObject is the player set as pilot
                {
                    pcon.isOperatingStation = false;
                    collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                }

            }
        }
    }
}
