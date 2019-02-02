﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerComplex : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public float speed;
    private float distance;
    private Vector2 center;
    private float angle;
    private Vector2 offset;
    private int flag = 1;
    private Vector2 heading;
    public float Radius = 7f;
    public float bulletFreq;
    public GameObject projectile;
    public float RotateSpeed = 5f;
    private float timer = 0;
    private float timer1 = 0;
    private float rand;
    private bool timerFlag;

    void start(){
        rand = Random.Range(0,2f);
        timerFlag = false;
    }
    void Update()
    { 
        timer += Time.deltaTime;
        timer1 += Time.deltaTime;
        if(timer1 >= rand){
            timerFlag = timerFlag ^ true;
            rand = Random.Range(0,2f);
            timer1 = 0;
        }
        distance = Vector2.Distance(transform.position,target.transform.position);
        if(distance >= Radius){
            if(distance >= Radius + 1){
                flag = 1;
            }
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else{
            heading = transform.position - target.transform.position;
            if(flag == 1){
                angle = Mathf.Atan2(heading.y, heading.x);
                flag = 0;
            }
            center = target.transform.position;
            if(timerFlag){
                angle += RotateSpeed * Time.deltaTime;
            }
            else{
                angle -= RotateSpeed * Time.deltaTime;
            }
            offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Radius;
            transform.position = center + offset;
        }
        Debug.Log(timer);
        if (timer >= bulletFreq)
        {
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            timer = 0;
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