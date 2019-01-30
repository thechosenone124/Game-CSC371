using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController2 : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb2d;
    public GameObject projectile;
    private bool shoot;
    private int count;

    private int health;

    void Start()
    {
        health = 3;
        count = 0;
        //winText.text = "";
        SetCountText();
        rb2d = GetComponent<Rigidbody2D>();
        shoot = true;
    }
    void FixedUpdate()
    {
        float moveHori = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(moveHori, moveVert);
        rb2d.AddForce(move * speed);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && shoot)
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
            shoot = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            collision.gameObject.SetActive(false);
            count = count - 1;
            SetCountText();
            GameCtrl.instance.damagedByEnemy();
        }
    }

    void SetCountText()
    {
        //countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            // winText.text = "You Win!";
        }
    }
    //stead
}
