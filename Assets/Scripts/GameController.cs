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
   public const int COCKPIT = 0;
	public const int WEAPONSROOM = 1;
	public const int ENGINEROOM = 2;
	public const int GUN = 5;
	public const int FOURWAYROOM = 4;
	public const int NOAHGUN = 3;
   public GameObject UpgradeMenu;

   public bool pause = false;

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
}