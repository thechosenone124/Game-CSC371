using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierShipEnemy : MonoBehaviour {

    public GameObject target;
    public GameObject projectile;
    public float speed;
    public float shootRange;
    public float triggerRange;
    public float bulletFreq;
    public float rotateSpeed;
    public float radius;

    private float angle;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float step = speed * Time.deltaTime;
        if (CheckDistance() < triggerRange && CheckDistance() > shootRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }


        if (CheckDistance() <= shootRange + .5)
        {
            CircleAround();
            if(timer >= bulletFreq)
            {
                Shoot();
                timer = 0;
            }
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

    private void CircleAround()
    {
        angle += rotateSpeed * Time.deltaTime;

        Vector3 offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;

        transform.position = target.transform.position + offset;

    }

}

