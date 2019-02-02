using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAimControl : MonoBehaviour {

   public float deadZone = 0.2f;
   public float rotationSpeed = 5f;

   void Update()
   {
      float horizontalAxis = Input.GetAxisRaw("RightJoystickX");
      float verticalAxis = Input.GetAxisRaw("RightJoystickY");

      Vector2 stickInput = new Vector2(horizontalAxis, verticalAxis);
      stickInput = Vector2.ClampMagnitude(stickInput, 1);   //clamp magnitude to keep circle boundary for x/y-axis, instead of square
      print(stickInput.x + "," + stickInput.y);

      if (stickInput.magnitude < deadZone)
      {
         stickInput.x = 0;
         stickInput.y = 0;
      }

      if (stickInput.x == 0 && stickInput.y == 0)
      {
      }
      else
      {
         Quaternion eulerRot = Quaternion.Euler(0, 0, Mathf.Atan2(-stickInput.x, -stickInput.y) * 180 / Mathf.PI);
         transform.rotation = Quaternion.Slerp(transform.rotation, eulerRot, Time.deltaTime * rotationSpeed);
      }
   }
      
}
