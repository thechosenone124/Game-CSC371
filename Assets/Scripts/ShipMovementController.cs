using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Davin Johnson
 */
public class ShipMovementController : MonoBehaviour {

    public float baseSpeed = 0.5f;
    public float speedIncrease;
    public float coolDownPeriod = 2;
    public float usageAmt;
    public float regenAmt;

    private Vector2 input;
    private float movementSpeed;
    private float timeStamp;

    public void MoveShip(PlayerInputContainer pic)
    {
        if (pic.isOperatingStation)
        {
            if(pic.GetXButton() && GameController.instance.GetCurrentBoost() > 0)
            {
                Debug.Log("pressing X");

                movementSpeed = baseSpeed + speedIncrease;
                GameController2.instance.SendMessage("UseBoost", usageAmt);
                timeStamp = Time.time + coolDownPeriod;
            }

            if (Time.time >= timeStamp)
            {
                GameController.instance.SendMessage("RegenerateBoost", regenAmt);
            }

            transform.position += new Vector3(pic.GetHorizontal(), pic.GetVertical(), 0f) * movementSpeed;
        }

        Debug.Log(movementSpeed);
        movementSpeed = baseSpeed;
    }
}
