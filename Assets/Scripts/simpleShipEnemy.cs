using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleShipEnemy : MonoBehaviour {

    public GameObject target;
    public GameObject projectile;
    public float speed;
    public float shootRange;
    public float triggerRange;
    public float bulletFreq;

    private float timer = 0;
    private 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        float step = speed * Time.deltaTime;
        if(CheckDistance() < triggerRange && CheckDistance() > shootRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }

        if (timer >= bulletFreq && CheckDistance() <= shootRange + .5)
        {
            Shoot();
            timer = 0;
        }
            
    }

    private float CheckDistance()
    {
        float dist = Vector2.Distance(target.transform.position, transform.position);
        return dist;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);

    }
}
