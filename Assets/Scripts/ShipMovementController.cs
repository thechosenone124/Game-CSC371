using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementController : MonoBehaviour {

    public float speed = 0.5f;

    private Vector2 input;
   
    public void MoveShip(PlayerInputContainer pic)
    {
        if (pic.isOperatingStation)
        {
            transform.position += new Vector3(pic.GetHorizontal(), pic.GetVertical(), 0f) * speed;
        }
    }
}
