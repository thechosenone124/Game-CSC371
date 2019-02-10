using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
   public int numProjectiles = 1;
   public GameObject projectile;

   void Fire()
   {
      for (int i = 0; i < numProjectiles; i++)
      {
         Instantiate(projectile, transform.position, transform.rotation);
      }
   }
}
