using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Zachary Barram
 * Davin Johnson
 */
public class FireMissileBay : MonoBehaviour {
	// Use this for initialization
	private GameObject target;
	public GameObject projectile;
	public float shootFreq;
	private float timer = 0;
	void Start () {
		target = GameObject.Find("Ship");
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= shootFreq){
			Shoot();
			timer = 0;
		}
	}

	private void Shoot()
   	{
		Vector3 playerDirection1 = target.transform.position - transform.position;
		GameObject bullet1 = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		bullet1.transform.rotation = Quaternion.LookRotation(Vector3.forward, playerDirection1);
   	}
}
