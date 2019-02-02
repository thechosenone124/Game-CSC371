using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun01Fire : MonoBehaviour {

   public GameObject projectile;

   void Fire()
   {
      Instantiate(projectile, transform.position, transform.rotation);
   }
}
