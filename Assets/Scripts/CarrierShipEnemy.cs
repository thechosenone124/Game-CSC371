using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarrierShipEnemy : MonoBehaviour
{

    public GameObject target;
    public GameObject projectile;
    public float speed;
    public float shootRange;
    public float triggerRange;
    public float bulletFreq;
    public float rotateSpeed;
    public float radius;
    public float swivelSpeed;

    private float angle;
    private float timer = 0;
    private Vector2 heading;
    private Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float step = speed * Time.deltaTime;
        if (CheckDistance() < triggerRange && CheckDistance() > shootRange)
        {
            FaceForward();
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }


        if (CheckDistance() <= shootRange + .5)
        {
            FaceSideways();
            CircleAround();
            if (timer >= bulletFreq)
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
        heading = transform.position - target.transform.position;

        angle = Mathf.Atan2(heading.y, heading.x);
        angle -= rotateSpeed * Time.deltaTime;

        offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        //Vector3 offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;

        transform.position = target.transform.position + offset;

    }

    private float FaceForward()
    {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float directionAngle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, directionAngle - 90f);
        return directionAngle;
    }

    private void FaceSideways()
    {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float directionAngle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, directionAngle);
    }

    private void transitionAngle()
    {
        float prevAngle = FaceForward();
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float directionAngle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        //transform.rotation = new Vector3(Mathf.Lerp(prevAngle, directionAngle, swivelSpeed += Time.deltaTime), 0, 0);
        transform.rotation = Quaternion.Euler(0f, 0f, directionAngle);
    }
}

