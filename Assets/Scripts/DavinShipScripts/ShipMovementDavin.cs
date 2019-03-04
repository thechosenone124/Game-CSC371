using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementDavin : MonoBehaviour {
   
    public float inputAcceleration = 1;

    public float maxSpeed = 10;
    public float rotationSpeed = 5;

    public float velocityDrag = 1;

    public Vector3 velocity;
    private Vector3 acceleration;

    public void MoveShip(PlayerInputContainer pic)
    {
        if (pic.isOperatingStation)
        {
         // apply forward input
         acceleration = new Vector3 (pic.GetHorizontal() * inputAcceleration, pic.GetVertical() * inputAcceleration, 0f);
         velocity += acceleration * Time.deltaTime;
      }
    }

     public void ApplyDrag()
     {
         // apply velocity drag
         velocity = velocity * (1 - Time.deltaTime * velocityDrag);
 
         // clamp to maxSpeed
         velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

         // update transform
         transform.position += velocity * Time.deltaTime;
         //transform.rotation = Quaternion.LookRotation(Vector3.forward, velocity);

         if (!(acceleration.x == 0 && acceleration.y ==  0))
         {
            Quaternion eulerRot = Quaternion.Euler(0, 0, Mathf.Atan2(-acceleration.x, acceleration.y) * 180 / Mathf.PI);
         
            transform.rotation = Quaternion.Slerp(transform.rotation, eulerRot, Time.deltaTime * rotationSpeed);
         }
     }
 }