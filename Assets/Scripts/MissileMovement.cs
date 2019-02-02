using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMovement : MonoBehaviour {

   public float spawnLength = 0.5f;
   public float speed = 10f;
   public float timeToFlurry = 0.15f;
   public float timeBetweenTurns = 0.5f;
   public Rigidbody2D rb;

   private float timer = 0;
   private float timeSinceLastTurn = 0;

   void Start()
   {
      transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, Random.Range(-30f, 30f));
      rb.velocity = transform.up * speed;
   }

   private void FixedUpdate()
   {
      timer += Time.deltaTime;

      if (timer >= 0.15f && timeSinceLastTurn >= timeBetweenTurns)
      {
         transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, Random.Range(-30f, 30f));
         rb.velocity = transform.up * speed;
         timeSinceLastTurn = 0;
      }
      else
      {
         timeSinceLastTurn+= Time.deltaTime;
      }

      if (timer >= spawnLength)
      {
         Destroy(gameObject);
         timer = 0;
      }
   }
}
