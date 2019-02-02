using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

   public float spawnLength = 0.5f;
   public float speed = 10f;
   public Rigidbody2D rb;

   private float timer = 0;

	void Start () {
      rb.velocity = transform.up * speed;
	}

   void FixedUpdate() {
      timer += Time.deltaTime;
      if (timer >= spawnLength)
      {
         Destroy(gameObject);
         timer = 0;
      }
   }
}
