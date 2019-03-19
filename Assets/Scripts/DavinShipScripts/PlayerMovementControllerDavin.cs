using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Davin Johnson
 * Zachary Barram
 */
public class PlayerMovementControllerDavin : MonoBehaviour {

    [Range(0.1f, 1.5f)]
    public float MovementSpeed = 1f;

    private PlayerInputContainer pic;
    private GameObject ship;
    private Vector3 playerCurPos;

    // Use this for initialization
    void Start()
    {
        pic = GetComponent<PlayerInputContainer>();
        ship = GameObject.Find("Ship");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localRotation = Quaternion.Euler(Vector3.zero);   //always keep player oriented with ship direction.
        if (!pic.isOperatingStation)    //Player is not controlling a station.
        {
            if (!pic.isFrozen)  //If player is not currently frozen.
            {
                transform.localPosition += new Vector3(pic.GetHorizontal(), pic.GetVertical(), 0f) * MovementSpeed * 0.1f;
                playerCurPos = transform.localPosition;
            }
            else    //Player is currently frozen.
            {
                transform.localPosition = playerCurPos;     //freeze player position
            }
        }
        else    //Player is controlling a station
        {
            transform.localPosition = playerCurPos;     //freeze player position
        }
    }

    public void SetPlayerCurrentPosition(Vector3 newPos){
        playerCurPos = newPos;
    }
    
}