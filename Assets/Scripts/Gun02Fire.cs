using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun02Fire : MonoBehaviour
{

   public GameObject projectile;

   void Fire()
   {
      Instantiate(projectile, transform.position, transform.rotation);
      Instantiate(projectile, transform.position, transform.rotation);
      Instantiate(projectile, transform.position, transform.rotation);
   }
}
