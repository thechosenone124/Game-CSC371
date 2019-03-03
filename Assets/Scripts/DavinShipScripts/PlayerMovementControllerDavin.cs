using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControllerDavin : MonoBehaviour {

   [Range(0.1f, 1.5f)]
   public float MovementSpeed = 2f;
   public Rigidbody2D shipRB;

   private PlayerInputContainer pic;
   private Vector2 prevShipPos;
   private Vector2 offset;

   // Use this for initialization
   void Start()
   {
      pic = GetComponent<PlayerInputContainer>();
      prevShipPos = shipRB.position;
   }

   // Update is called once per frame
   void FixedUpdate()
   {
      //offset = shipRB.position - GetComponent<Rigidbody2D>().position;
      //GetComponent<Rigidbody2D>().position = shipRB.position + offset;
      if (!pic.isOperatingStation)  //If player is not controlling a station.
      {
         transform.localPosition += new Vector3(pic.GetHorizontal(), pic.GetVertical(), 0f) * MovementSpeed * 0.1f;
         //GetComponent<Rigidbody2D>().position += new Vector2(pic.GetHorizontal(), pic.GetVertical()) * MovementSpeed * 0.1f;
         /*Vector2 curShipPos = shipRB.position;
         GetComponent<Rigidbody2D>().position += curShipPos - prevShipPos;
         prevShipPos = curShipPos; */
      }
   }
}