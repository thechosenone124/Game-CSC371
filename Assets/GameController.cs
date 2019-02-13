using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;

    //Health
    public float maxHealth = 100f;
    public Image currentHealthBar;
    public Text ratioText;
    private float currentHealth;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {

        //healthbar initialization
        currentHealth = maxHealth;
        UpdateHealthBar();
   }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Health manipulation
    private void UpdateHealthBar()
    {
        float ratio = currentHealth / maxHealth;
        currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString("0") + "%";
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        UpdateHealthBar();
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }
}
