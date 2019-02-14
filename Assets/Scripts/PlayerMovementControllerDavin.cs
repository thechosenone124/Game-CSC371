using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControllerDavin : MonoBehaviour
{

   [Range(0.1f, 5f)]
   public float MovementSpeed = 2f;

   private Rigidbody2D rb2d;
   private Vector2 input;
   private PlayerInputContainer pic;

   // Use this for initialization
   void Start()
   {
      rb2d = GetComponent<Rigidbody2D>();
      pic = GetComponent<PlayerInputContainer>();

   }

   // Update is called once per frame
   void Update()
   {
      if (!pic.isOperatingStation)
      {
         input = new Vector2(pic.GetHorizontal(), pic.GetVertical()) * MovementSpeed * Time.deltaTime;

         rb2d.MovePosition(new Vector2(rb2d.position.x + input.x, rb2d.position.y + input.y));
      }
   }
}
