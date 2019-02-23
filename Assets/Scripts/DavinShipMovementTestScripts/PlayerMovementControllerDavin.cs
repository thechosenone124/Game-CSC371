using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControllerDavin : MonoBehaviour {

   [Range(0.1f, 1.5f)]
   public float MovementSpeed = 2f;
   private PlayerInputContainer pic;

   // Use this for initialization
   void Start()
   {
      pic = GetComponent<PlayerInputContainer>();
   }

   // Update is called once per frame
   void Update()
   {
      if (!pic.isOperatingStation)  //If player is not controlling a station.
      {
         transform.localPosition += new Vector3(pic.GetHorizontal(), pic.GetVertical(), 0f) * MovementSpeed * 0.1f;
      }
   }
}