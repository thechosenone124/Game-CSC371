using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementController : MonoBehaviour {

   public Rigidbody2D shipRB;
   public float speed = 0.5f;

   private Vector2 input;

   // Use this for initialization
   void Start () {
      shipRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   public void MoveShip(PlayerInputContainer pic)
   {
      if (pic.isOperatingStation)
      {
         shipRB.AddForce(new Vector2(pic.GetHorizontal(), pic.GetVertical()) * speed);
         /*if (pic.GetHorizontal() == 0 && pic.GetVertical() == 0)
         {
            shipRB.velocity = Vector3.zero;
         }
         else
         {
            shipRB.AddForce(new Vector2(pic.GetHorizontal(), pic.GetVertical()) * speed);
            //transform.position += new Vector3(pic.GetHorizontal(), pic.GetVertical(), 0f) * speed;
         }*/
      }
   }
}
