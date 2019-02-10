using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInformation : MonoBehaviour {

    public float maxHealth = 100f;
    
    public ShipMovementController shipCon;
    //public WeaponController weapCon;


    private GameObject pilot;
    private GameObject gunner;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (pilot != null) shipCon.MoveShip(pilot.GetComponent<PlayerInputContainer>());
		
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


}
