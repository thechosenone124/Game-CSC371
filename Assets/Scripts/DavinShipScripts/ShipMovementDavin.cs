using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementDavin : MonoBehaviour {
   
    public float inputAcceleration = 1;

    public float maxSpeed = 25f;
    public float speedIncrease = 20f;
    public float accelIncrease = 0.5f;
    public float dragDecrease = 0.5f;
    public float coolDownPeriod = 2;
    public float usageAmt = 0.5f;
    public float regenAmt = 0.1f;

    public float rotationSpeed = 5;

    public float velocityDrag = 1;

    private float movementSpeed;
    private float timeStamp;

    private float baseDrag;
    private float baseAccel;
    private float baseSpeed;
    private Vector3 velocity;
    private Vector3 acceleration;

    public void Start()
    {
        baseDrag = velocityDrag;
        baseAccel = inputAcceleration;
        baseSpeed = maxSpeed;
    }

    public void MoveShip(PlayerInputContainer pic)
    {
        if (pic.isOperatingStation)
        {
            if (pic.GetXButton() && GameController.instance.GetCurrentBoost() > 0)
            {
                Debug.Log("pressing X");

                GameController.instance.SendMessage("UseBoost", usageAmt);
                timeStamp = Time.time + coolDownPeriod;
                inputAcceleration = inputAcceleration + accelIncrease;
                velocityDrag = velocityDrag - dragDecrease;
                maxSpeed = maxSpeed + speedIncrease;
                Debug.Log(maxSpeed);
            }

            if (Time.time >= timeStamp)
            {
                GameController.instance.SendMessage("RegenerateBoost", regenAmt);
            }
            // apply forward input
            acceleration = new Vector3 (pic.GetHorizontal() * inputAcceleration, pic.GetVertical() * inputAcceleration, 0f);
            velocity += acceleration * Time.deltaTime;
        }

        velocityDrag = baseDrag;
        inputAcceleration = baseAccel;
        

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
        maxSpeed = baseSpeed;
    }
 }