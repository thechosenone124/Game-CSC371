using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Zachary Barram
 */
public class HammerController : MonoBehaviour {
	public float speed;
	public GameObject target;
	public float radius;
	private float distance;
    void Update()
    { 
		distance = Vector2.Distance(target.transform.position,transform.position);
		if(distance >= radius){
        	transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
		}
		else{
			transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * 5 * Time.deltaTime);
		}
		

    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Projectile"))
        {
            gameObject.SetActive(false);
            col.gameObject.SetActive(false);
        }

    }
}
