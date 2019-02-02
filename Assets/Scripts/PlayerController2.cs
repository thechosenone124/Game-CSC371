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
    private bool isDead = false;
    private int count;

    private int health;

    //controller support
    private Vector3 movementVector;
    private Transform mousePosition;
    public GameObject crosshair;
    public float dampener;

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
        if (isDead != true)
        {
            /*float moveHori = Input.GetAxis("Horizontal");
            float moveVert = Input.GetAxis("Vertical");
            Vector2 move = new Vector2(moveHori, moveVert);
            rb2d.AddForce(move * speed);*/

            //controller support
            movementVector.x = Input.GetAxis("LeftJoystickX") * speed;
            movementVector.y = Input.GetAxis("LeftJoystickY") * speed *-1;
            rb2d.AddForce(movementVector);

            mousePosition = crosshair.transform;

            float rStickX = Input.GetAxis("RightJoystickX");
            float rStickY = Input.GetAxis("RightJoystickY") *-1;

            Vector3 movement = new Vector3(rStickX/dampener, rStickY/dampener, 0);
            crosshair.transform.position = crosshair.transform.position + movement;
            //mousePosition = crosshair.transform; 
            

        }
    }
    void Update()
    {
        float shooting = Input.GetAxis("RightTrigger");
        bool isShooting = false;
        if(shooting > 0)
        {
            isShooting = true;
        }

        if ((Input.GetKeyDown(KeyCode.LeftShift) || isShooting) && shoot)
        {
            //GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
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
            //GameCtrl.instance.damagedByEnemy();
            SendMessage("TakeDamage", 1);
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

    public void disableMovement()
    {
        rb2d.velocity = Vector2.zero;
        print("here");
    }

    public void turnDead()
    {
        isDead = true;
    }

    public Transform getCrosshairTransform()
    {
        return crosshair.transform;
    }

}
