using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour {

    public static GameCtrl instance;
    public bool gameOver = false;

    public Text balanceText;
    public Text healthText;

    public GameObject gameOverText;
    public PlayerController2 pc2;

    //public int initialHealth = 3;

    private int balance;
    private int health;
    

	// Use this for initialization
	void Awake () {
		if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
	}

    void Start()
    {
        //health = initialHealth;
        //healthText.text = "Health: " + health;
    }
	
	// Update is called once per frame
	void Update () {
        if(gameOver == true && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void playerScored()
    {
        if (gameOver)
        {
            return;
        }

        balance += 900;
        balanceText.text = "Balance: $" + balance.ToString();
    }

    /*public void damagedByEnemy()
    {
        health -= 1;
        healthText.text = "Health: " + health.ToString();
        if (health <= 0)
        {
            playerDied();
        }
    }*/


    public void playerDied()
    {
        gameOverText.SetActive(true);
        gameOver = true;
        pc2.disableMovement();
        pc2.turnDead();
    }
}
