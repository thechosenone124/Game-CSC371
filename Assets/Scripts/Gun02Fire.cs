using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun02Fire : MonoBehaviour
{

   public GameObject projectile;

   void Update()
   {

      if (Input.GetButtonDown("Fire2"))
      {
         Instantiate(projectile, transform.position, transform.rotation);
         Instantiate(projectile, transform.position, transform.rotation);
         Instantiate(projectile, transform.position, transform.rotation);
      }
   }
}
