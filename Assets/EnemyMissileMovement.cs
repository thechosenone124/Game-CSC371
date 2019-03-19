using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Davin Johnson
 */
public class EnemyMissileMovement : MonoBehaviour {

   public float spawnLength = 0.5f;
   public float speed = 10f;
   public float timeToFlurry = 0.25f;
   public float timeBetweenTurns = 0.5f;
   public float turnAngleRange = 30f;
   public Rigidbody2D rb;

   private float timer = 0;
   private float timeSinceLastTurn = 0;

   void Start()
   {
      transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, Random.Range(-10f, 10f));
      rb.velocity = transform.up * speed;
   }

   private void FixedUpdate()
   {
      timer += Time.deltaTime;

      if (timer >= timeToFlurry && timeSinceLastTurn >= timeBetweenTurns)
      {
         transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, Random.Range(-turnAngleRange, turnAngleRange));
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

   void OnTriggerEnter2D(Collider2D other){
      if(other.CompareTag("SpaceDebris") || other.CompareTag("MapBorder"))
      {
         Destroy(gameObject);
      }
   }
}
