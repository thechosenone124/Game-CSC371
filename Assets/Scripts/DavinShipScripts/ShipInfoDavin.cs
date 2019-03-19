using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Davin Johnson
 * Zachary Barram
 * Noah Paige
 */
public class ShipInfoDavin : MonoBehaviour {

    public float maxHealth = 100f;
    private ShipMovementDavin moveCon;
    private ShipWeaponController weapCon;
    private ShipRepairController repairCon;
    private GameObject pilot = null;
    private GameObject gunner = null;
    private GameObject fixer = null;
    private bool isDeflecting = false;


	 // Use this for initialization
	 void Start () {
        moveCon = GetComponent<ShipMovementDavin>();
        weapCon = GetComponent<ShipWeaponController>();
        repairCon = GetComponent<ShipRepairController>();
	 }

    // Update is called once per frame
    void Update () {
      
        if (pilot  != null && GameController.instance.State == (int)GameController.GameStates.FREEROAM){
            moveCon.MoveShip(pilot.GetComponent<PlayerInputContainer>());
        }
        if (gunner != null && GameController.instance.State == (int)GameController.GameStates.FREEROAM)
        {
            weapCon.TryShoot(gunner.GetComponent<PlayerInputContainer>());
            weapCon.MoveReticle(gunner.GetComponent<PlayerInputContainer>());
        }
        if(fixer != null && GameController.instance.State == (int)GameController.GameStates.FREEROAM)
        {
            repairCon.TryRepair(fixer.GetComponent<PlayerInputContainer>());
        }
		
	 }

    private void FixedUpdate()
    {
        moveCon.ApplyDrag();
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

    public bool hasPilot()
    {
        if (pilot != null)
            return true;
        else
            return false;
    }

    public bool SetFixer(GameObject player)
    {
        if(fixer == null)
        {
            fixer = player;
            return true;
        }
        return false;
    }

    public bool RemoveFixer(GameObject player)
    {
        if (fixer == player)
        {
            fixer = null;
            return true;
        }
        return false;
    }

    public bool hasFixer()
    {
        if (fixer != null)
            return true;
        else
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
            //weapCon.reticle.GetComponent<SpriteRenderer>().enabled = false;
            return true;
        }
        return false;
    }

    public bool hasGunner()
    {
        if (gunner != null)
            return true;
        else
            return false;
    }

    public void freezePlayer(GameObject player)
    {
        player.GetComponent<PlayerInputContainer>().isFrozen = true;
    }

    public void unfreezePlayer(GameObject player)
    {
        player.GetComponent<PlayerInputContainer>().isFrozen = false;
    }

}
