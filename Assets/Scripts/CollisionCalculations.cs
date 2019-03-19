using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Zachary Barram
 * Brandon Ryan
 */
public class CollisionCalculations : MonoBehaviour {

    public float coolDownPeriod = 2;
    private float timeStamp;
    private bool regenShield = false;

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
            timeStamp = Time.time +coolDownPeriod;
            Destroy(collision.gameObject);
            GameController.instance.SendMessage("TakeDamage", 6);
        }
        if (collision.gameObject.CompareTag("EnemyMissile"))
        {
            Destroy(collision.gameObject);
            GameController.instance.SendMessage("TakeDamage", 20);
        }
        if (collision.gameObject.CompareTag("EnemyPlasma"))
        {
            Destroy(collision.gameObject);
            GameController.instance.SendMessage("TakeDamage", 10);
        }
    }
}
