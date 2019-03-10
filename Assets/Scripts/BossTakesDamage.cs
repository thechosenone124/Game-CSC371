using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTakesDamage : MonoBehaviour
{

   // Use this for initialization
   private bool gotHit = false;
   public int enemyHealth = 200;
   private float damageTime = 0;
   public float redTime;
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      if (gotHit)
      {
         enemyHealth -= 1;
         gotHit = false;
         damageTime += redTime;
         gameObject.GetComponent<SpriteRenderer>().color = Color.red;
      }
      if (damageTime > 0)
      {
         damageTime -= Time.deltaTime;
      }
      else
      {
         damageTime = 0;
         gameObject.GetComponent<SpriteRenderer>().color = Color.white;
      }
      if (enemyHealth == 0)
      {
         GameController.instance.PlayerWins();
      }
   }
   void OnTriggerEnter2D(Collider2D col)
   {
      if (col.gameObject.CompareTag("Projectile"))
      {
         gotHit = true;
      }
   }
}