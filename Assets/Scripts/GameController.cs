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
                             MODIFYINGSHIP}; 

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
      gameStateText.text = "You Died!";
   }

   public void DisableUpgradeMenu(){
      UpgradeMenu.SetActive(false);
   }

   public void EnableUpgradeMenu(){
      UpgradeMenu.SetActive(true);
      GameObject.Find("UpgradeCanvas").GetComponent<UpgradeMenuController>().ActivateUpgrade();
   }

    public void SetStateToFreeRoam()      { State = (int)GameStates.FREEROAM;      }
    public void SetStateToModifyingShip() { State = (int)GameStates.MODIFYINGSHIP; }
}