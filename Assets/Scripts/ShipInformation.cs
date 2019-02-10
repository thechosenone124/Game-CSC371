using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInformation : MonoBehaviour {

    public float maxHealth = 100f;
    
    private ShipMovementController moveCon;
    private ShipWeaponController weapCon;
    private GameObject pilot;
    private GameObject gunner;
	// Use this for initialization
	void Start () {
        moveCon = GetComponent<ShipMovementController>();
        weapCon = GetComponent<ShipWeaponController>();
	}
	
	// Update is called once per frame
	void Update () {

        if (pilot  != null) moveCon.MoveShip(pilot.GetComponent<PlayerInputContainer>());
        if (gunner != null)
        {
            weapCon.TryShoot(gunner.GetComponent<PlayerInputContainer>());
            weapCon.MoveReticle(gunner.GetComponent<PlayerInputContainer>());
        }
		
	}

    public bool SetPilot(GameObject player)
    {
        if(pilot == null)
        {
            pilot = player;
            return true;
        }
        return false;
    }

    public bool RemovePilot(GameObject player)
    {
        if (pilot == player)
        {
            pilot = null;
            return true;
        }
        return false;
    }

    public bool SetGunner(GameObject player)
    {
        if (gunner == null)
        {
            gunner = player;
            weapCon.reticle.SetActive(true);
            return true;
        }
        return false;
    }

    public bool RemoveGunner(GameObject player)
    {
        if (gunner == player)
        {
            gunner = null;
            weapCon.reticle.SetActive(false);
            return true;
        }
        return false;
    }


}
