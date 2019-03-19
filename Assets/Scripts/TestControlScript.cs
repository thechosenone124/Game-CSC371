using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Brandon Ryan
 */
public class TestControlScript : MonoBehaviour

{
    public float baseSpeed;
    public float speedIncrease;
    public float coolDownPeriod = 2;
    public float usageAmt;
    public float regenAmt;

    private Vector3 movementVector;

    private CharacterController characterController;

    private float movementSpeed;
    private bool boosting;
    private float timeStamp;

    void Start()

    {
        movementSpeed = baseSpeed;
        characterController = GetComponent<CharacterController>();

    }

    void Update()

    {
        if(Input.GetKey(KeyCode.Space) && GameController2.instance.GetCurrentBoost() > 0)
        {
            Debug.Log("pressing X");
            
            movementSpeed = baseSpeed + speedIncrease;
            GameController2.instance.SendMessage("UseBoost", usageAmt);
            timeStamp = Time.time + coolDownPeriod;
        }               

        if(Time.time >= timeStamp)
        {
            GameController2.instance.SendMessage("RegenerateBoost", regenAmt);
        }
        movementVector.x = Input.GetAxis("Horizontal") * movementSpeed;

        movementVector.y = Input.GetAxis("Vertical") * movementSpeed;

        characterController.Move(movementVector * Time.deltaTime);

        Debug.Log(movementSpeed);
        movementSpeed = baseSpeed;
    }

}
