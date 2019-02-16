using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCalculations : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject);
            GameController.instance.SendMessage("TakeDamage", 3);
        }
    }
}
