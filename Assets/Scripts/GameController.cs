using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

   public static GameController instance;

   //Health
   public float maxHealth = 100f;
   public Image currentHealthBar;
   public Text ratioText;
   public Text gameStateText;
   private float currentHealth;
   public GameObject UpgradeMenu;
   public int State;
    
    // Enums are better because they guarantee we don't set two const equal to the same number
    public enum ItemTypes { COCKPIT,
                            WEAPONSROOM,
                            ENGINEROOM,
                            NOAHGUN,
                            FOURWAYROOM,
                            GUN,
                            NUMBEROFTYPES }; // make sure the last element is NUMBEROFTYPES. this serves as the length of the list

    public enum GameStates { FREEROAM,
                             MODIFYINGSHIP,
                             PLAYERWON,
                             NUMBEROFSTATES}; // make sure the last element is NUMBEROFSTATES. this serves as the length of the list

    //boost
    public float maxBoost = 100f;
    public Image currentBoostBar;
    public Text boostRatioText;
    private float currentBoost;
    public bool isBoosting;
    public bool boostBroken;
    private float timeToBreak;

    //shield
    public float maxShield = 100f;
    public Image currentShieldBar;
    public Text shieldRatioText;
    private float currentShield;
    public bool shieldBroken = false;

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
   void Start()
   {
        State = (int)GameStates.FREEROAM;
        //healthbar initialization
        currentHealth = maxHealth;
        UpdateHealthBar();

        //boost initialization
        currentBoost = maxBoost;

        //shield initialization
        currentShield = maxShield;

        //game state text initialization
        gameStateText.text = "";
   }

   // Update is called once per frame
   void Update()
   {

   }

   //Health manipulation
   public void UpdateHealthBar()
   {
      float ratio = currentHealth / maxHealth;
      currentHealthBar.rectTransform.localScale = new Vector3(ratio * .6f, .6f, 1);
      ratioText.text = (ratio * 100).ToString("0") + "%";
   }

    //boost manipulation
    public void UpdateBoost()
    {
        float ratio = currentBoost / maxBoost;
        currentBoostBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        boostRatioText.text = (ratio * 100).ToString("0") + "%";
    }

    //shield manipulation
    public void UpdateShield()
    {
        float ratio = currentShield / maxShield;
        currentShieldBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        shieldRatioText.text = (ratio * 100).ToString("0") + "%";
    }

    private void UseBoost(float boostAmt)
    {
        currentBoost -= boostAmt;
        if (currentBoost <= 0)
        {
            currentBoost = 0;
            if(timeToBreak == 0)
                timeToBreak = CalculateRandomChance();
        }
        UpdateBoost();
        isBoosting = true;
    }

    private void RegenerateBoost(float boostAmt)
    {
        currentBoost += boostAmt;
        if (currentBoost >= maxBoost)
        {
            currentBoost = maxBoost;
        }
        UpdateBoost();
    }

    public float GetCurrentBoost()
    {
        return currentBoost;
    }

    public void SetBoost(float newBoost)
    {
        currentBoost = newBoost;
    }

    private void TakeDamage(float damage)
   {
        if (shieldBroken)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                PlayerLoses();
            }
        }
        else
        {
            currentShield -= damage;
            if(currentShield <= 0)
            {
                currentShield = 0;
                shieldBroken = true;
            }
        }
        UpdateHealthBar();
        UpdateShield();
   }

   public float getCurrentHealth()
   {
      return currentHealth;
   }

    public float getCurrentShield()
    {
        return currentShield;
    }

    public void setHealth(float newHealth)
   {
      currentHealth = newHealth;
   }

   public void PlayerWins()
   {
      gameStateText.text = "You Win!";
   }

   public void PlayerLoses()
   {
      GameObject.Find("Ship").SetActive(false);
      gameStateText.text = "You Died!";
   }

   public void DisableUpgradeMenu(){
       GameObject.Find("Ship").GetComponent<ShipInfoDavin>().unfreezePlayer(GameObject.Find("Players").transform.GetChild(0).gameObject);
	   GameObject.Find("Ship").GetComponent<ShipInfoDavin>().unfreezePlayer(GameObject.Find("Players").transform.GetChild(1).gameObject);
       UpgradeMenu.SetActive(false);
   }

   public void EnableUpgradeMenu(){
      UpgradeMenu.SetActive(true);
      GameObject.Find("UpgradeCanvas").GetComponent<UpgradeMenuController>().ActivateUpgrade();
   }

    private float CalculateRandomChance()
    {
        float timeToBreak;
        timeToBreak = Random.Range(3.5f, 7.0f);
        Debug.Log(timeToBreak);
        return timeToBreak;       
    }

    public float getTimeToBreak()
    {
        return timeToBreak;
    }

    public void SetStateToFreeRoam()      { State = (int)GameStates.FREEROAM;      }
    public void SetStateToModifyingShip() { State = (int)GameStates.MODIFYINGSHIP; }
}