﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Davin Johnson
 * Zachary Barram
 * Noah Paige
 * Brandon Ryan
 */
public class ShipMovementDavin : MonoBehaviour {

    [Header("Base Speed Settings:")]
    public float inputAcceleration = 1;
    public float maxSpeed = 10;
    public float velocityDrag = 1;
    public float rotationSpeed = 5;
    public float deflectLength = 1;
    public float speedIncrease = 20f;
    public float accelIncrease = 0.5f;
    public float dragDecrease = 0.5f;
    public float coolDownPeriod = 2;
    public float usageAmt = 0.5f;
    public float regenAmt = 0.1f;

    [Header("Boost Speed Settings:")]

    public float maxSpeedIncrease;

    [Header("Player GameObjects")]
    public GameObject player1;
    public GameObject player2;
    public GameObject player1PIP;
    public GameObject player2PIP;

    [Space(15)]
    public Vector3 velocity;

    private float timeStamp;
    private float baseDrag;
    private float baseAccel;
    private float baseSpeed;
    private Vector3 acceleration;
    private float timer = 0;
    private bool isDeflecting = false;
    private ShipInfoDavin shipInfo;
    private float boostTimeStamp;

    private void Start()
    {
        shipInfo = GetComponent<ShipInfoDavin>();
        baseDrag = velocityDrag;
        baseAccel = inputAcceleration;
        baseSpeed = maxSpeed;
    }

    private void Update()
    {
        if (isDeflecting)
        {
            if (timer <= deflectLength)     //ship is being deflected
            {
                timer += Time.deltaTime;
                shipInfo.freezePlayer(player1);
                shipInfo.freezePlayer(player2);
            }
            else    //ship is done deflecting
            {
                isDeflecting = false;
                timer = 0;
                shipInfo.unfreezePlayer(player1);
                shipInfo.unfreezePlayer(player2);
            }
        }
        if(GameController.instance.GetCurrentBoost() == 100){
            boostTimeStamp = 0;
        }
    }

    public void MoveShip(PlayerInputContainer pic)
    {
        if (pic.isOperatingStation && !isDeflecting)
        {
            if (pic.GetRTButton() == 1 && !GameController.instance.boostBroken)
            {
                GameController.instance.isBoosting = true;
                GameController.instance.SendMessage("UseBoost", usageAmt);
                timeStamp = Time.time + coolDownPeriod;
                if(boostTimeStamp == 0 && GameController.instance.GetCurrentBoost() == 0){
                    boostTimeStamp = Time.time + GameController.instance.getTimeToBreak();
                }
                inputAcceleration = baseAccel + accelIncrease;
                velocityDrag = baseDrag - dragDecrease;
                maxSpeed = baseSpeed + maxSpeedIncrease;
                //Debug.Log("pressing RT, max speed: " + maxSpeed);
            }
            else
            {
                GameController.instance.isBoosting = false;
                //velocityDrag = baseDrag;
                inputAcceleration = baseAccel;
                maxSpeed = baseSpeed;
            }
            if(Time.time > boostTimeStamp && GameController.instance.isBoosting && GameController.instance.GetCurrentBoost() == 0)
            {
                GameController.instance.boostBroken = true;
            }

            if (Time.time >= timeStamp && !GameController.instance.boostBroken)
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
        //Debug.Log("Current speed: " + velocity.magnitude);
                 
        // update transform
        transform.position += velocity * Time.deltaTime;
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, velocity);

        if (!(acceleration.x == 0 && acceleration.y ==  0))
        {
           Quaternion eulerRot = Quaternion.Euler(0, 0, Mathf.Atan2(-acceleration.x, acceleration.y) * 180 / Mathf.PI);
         
           transform.rotation = Quaternion.Slerp(transform.rotation, eulerRot, Time.deltaTime * rotationSpeed);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("SpaceDebris") || collision.collider.CompareTag("SpaceStation")) {     //ship collided with space debris, etc.
            
            velocity = -velocity;   //reverse ship velocitytemporarily
            isDeflecting = true;    //tell the ship it's being deflected
            if (player1.GetComponent<PlayerInputContainer>().isOperatingStation == false)
            {
                player1PIP.GetComponent<PIPShakeBehavior>().TriggerShake();  //trigger player1 camera screen shake if not operating station
            }
            if (player2.GetComponent<PlayerInputContainer>().isOperatingStation == false)
            {
                player2PIP.GetComponent<PIPShakeBehavior>().TriggerShake();  //trigger player2 camera screen shake if not operating station
            }
            GameController.instance.SendMessage("TakeDamage", 3);   //trigger ship damage  

        }
    }
 }