﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour{

    public float speed;
    public Text countText;
    public Text winText;
    private Rigidbody2D rb2d;
    public GameObject projectile;
    private int count;
    void Start(){
        count = 0;
        winText.text = "";
        SetCountText();
        rb2d = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate(){
        float moveHori = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        Vector2 move = new Vector2 (moveHori,moveVert);
        rb2d.AddForce(move *speed);
    }
    void Update() {
       if (Input.GetKeyDown(KeyCode.LeftShift))
         {
             GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
         }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickUp"))
        {
            collision.gameObject.SetActive(false); 
            count = count + 1;
            SetCountText();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false); 
            count = count - 1;
            SetCountText();
        }
    }

    void SetCountText(){
        countText.text = "Count: " + count.ToString();
        if(count >= 12){
            winText.text = "You Win!";
        }
    }
}
