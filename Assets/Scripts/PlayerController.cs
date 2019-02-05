using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour{

    public float speed;
    public float maxSpeed = 10f;
    public float drag = 5f;
    private Rigidbody2D rb2d;
    public GameObject projectile;
    private bool canShoot;
    private int score;
    public bool navigationControls;
    public bool gunControls;

    void Start(){
        score = 0;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.drag = drag;
        canShoot = false;
    }

    void FixedUpdate(){
        float moveHori = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        Vector2 move = new Vector2 (moveHori,moveVert);

        if((rb2d.velocity + move).magnitude < maxSpeed) rb2d.AddForce(move *speed);
    }

    void Update() {
        if (navigationControls)
        {
           if (Input.GetKeyDown(KeyCode.LeftShift) && canShoot && gunControls)
           {
               GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
           }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("PlayerController -- Hit something");
        if (collision.gameObject.CompareTag("PickUp"))
        {
            collision.gameObject.SetActive(false); 
            score = score + 1;
            canShoot = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("PlayerController -- Hit enemy");
            //collision.gameObject.SetActive(false); 
            SendMessage("TakeDamage", 1);
        }
    }
    
    public void disableMovement()
    {
        rb2d.velocity = Vector2.zero;
        print("here");
    }

    public void turnDead()
    {
        navigationControls = false;
        gunControls = false;
    }
}
