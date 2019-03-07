﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCalculations : MonoBehaviour {

    public float coolDownPeriod = 2;
    private float timeStamp;

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
            timeStamp = Time.time + coolDownPeriod;
            Destroy(collision.gameObject);
            GameController.instance.SendMessage("TakeDamage", 3);
            if(Time.time > timeStamp && !GameController.instance.shieldBroken)
            {
                GameController.instance.SendMessage("RegenerateShield");
            }
        }
    }
}
