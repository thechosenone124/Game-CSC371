using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInfoDavin : MonoBehaviour {

    public float maxHealth = 100f;
    private ShipMovementDavin moveCon;
    private ShipWeaponController weapCon;
    private GameObject pilot;
    private GameObject gunner;

	 // Use this for initialization
	 void Start () {
        moveCon = GetComponent<ShipMovementDavin>();
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

    private void FixedUpdate()
    {
        if (pilot  != null) moveCon.ApplyDrag();
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
            //weapCon.reticle.GetComponent<SpriteRenderer>().enabled = true;
            return true;
        }
        return false;
    }

    public bool RemoveGunner(GameObject player)
    {
        if (gunner == player)
        {
            gunner = null;
            weapCon.reticle.GetComponent<SpriteRenderer>().enabled = false;
            return true;
        }
        return false;
    }

}
