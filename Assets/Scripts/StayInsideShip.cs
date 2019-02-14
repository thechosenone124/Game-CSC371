using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInsideShip : MonoBehaviour {
    [Header("Keeps player objects inside the ship")]
    [Header("If this script is on player, there is no need to make the ship the player's parent.")]
    [Header("put ship GameObject below")]
    public GameObject ship;
    

    private Vector3 previousShipPosition;
    private float previousZRotation;

	// Use this for initialization
	void Start () {
        previousShipPosition = ship.transform.position;
        previousZRotation = ship.transform.rotation.z;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

         
        Vector3 currentShipPosition = ship.transform.position;
        float currentZRotation = ship.transform.rotation.z;
        Quaternion q = Quaternion.Euler(0, currentZRotation - previousZRotation, 0);

        transform.position += currentShipPosition;

        transform.position += currentShipPosition - previousShipPosition;
        transform.rotation = q;

        previousShipPosition = currentShipPosition;
        previousZRotation = currentZRotation;
	}

    
}
