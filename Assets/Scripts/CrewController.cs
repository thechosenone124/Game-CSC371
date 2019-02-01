using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewController : MonoBehaviour {
   private bool freeMove; //True when player can move around, false when manning a station
   public GameObject ship;
   public float speed;
   private PlayerController playerControl;
	// Use this for initialization
	void Start () {
		freeMove = true;
      playerControl = ship.GetComponent<PlayerController>();
	}
	void activateControls() //Activates the controls of the part of the ship crewmember is in
   {
      freeMove = false;
      if (transform.localPosition.x >= 0) //the right side of the ship is navigation
         playerControl.navigationControls = true;
      else if (transform.localPosition.x < 0)
         playerControl.gunControls = true;
   }
   void freeMovement() //Deactivates controls and allows crew to move freely
   {
      freeMove = true;  
      playerControl.navigationControls = false;
      playerControl.gunControls = false;
   }
   bool InShip(Vector3 pos)
   {
      if (pos.x > 1.2f || pos.x < -1.2f)
      {
         return false;
      }
      if (pos.y > 0.9f || pos.y < -0.9f)
      {
         return false;
      }
      return true;
   }
	void Update() {
        if (freeMove)
        {
           float moveHori = Input.GetAxis("Horizontal");
           float moveVert = Input.GetAxis("Vertical");
           Vector3 move = new Vector3 (moveHori,moveVert, 0) * speed;
           Vector3 new_pos = transform.localPosition + move;
           if (InShip(transform.localPosition + move))
              transform.localPosition = new_pos;
        }
        if (Input.GetKeyDown(KeyCode.Space) && freeMove)
        {
           activateControls();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !freeMove)
        {
           freeMovement();
        }
    }
}
