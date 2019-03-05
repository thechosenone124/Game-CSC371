using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementDavin : MonoBehaviour {
   
    public float inputAcceleration = 1;
    public float maxSpeed = 10;
    public float rotationSpeed = 5;
    public float velocityDrag = 1;
    public Vector3 velocity;
    public float deflectLength = 1;

    public GameObject player1;
    public GameObject player2;
    public GameObject player1PIP;
    public GameObject player2PIP;

    private Vector3 acceleration;
    private float timer = 0;
    private bool isDeflecting = false;
    private ShipInfoDavin shipInfo;

    private void Start()
    {
        shipInfo = GetComponent<ShipInfoDavin>();
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
    }

    public void Start()
    {
        baseDrag = velocityDrag;
        baseAccel = inputAcceleration;
        baseSpeed = maxSpeed;
    }

    public void MoveShip(PlayerInputContainer pic)
    {
        if (pic.isOperatingStation && !isDeflecting)
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

            GameController.instance.isBoosting = false;

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
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("SpaceDebris")) {     //ship collided with space debris, etc.
            
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