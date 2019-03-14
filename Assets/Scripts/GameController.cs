using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
                            MISSILELAUNCHER,
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

    private bool tutorialBossDefeated = false;
    private bool boss1Defeated = false;
    private bool boss2Defeated = false;
    private bool boss3Defeated = false;
    private bool boss4Defeated = false;

    //shield
    public float maxShield = 100f;
    public Image currentShieldBar;
    public Text shieldRatioText;
    private float currentShield;
    public bool shieldBroken = false;

    private ShipInfoDavin shipInfo;

    public Text tutorialText;
    private int tutorialTextNumber = 0;
    public bool isTutorial;

    private int BarrierDestroyed = 0;

    private int changedState;

    private int inventoryCount = 0;

    public GameObject capitolShip;

    public GameObject AsteroidBarrier;

    private bool tutorialOver = false;
    private float timer = 0f;

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

        shipInfo = GameObject.Find("Ship").GetComponent<ShipInfoDavin>();
   }

   // Update is called once per frame
   void Update()
   {
       
       if(isTutorial){
            TutorialState();
            changedState = State;
            if(tutorialBossDefeated == true){
                capitolShip.SetActive(true);
                AsteroidBarrier.SetActive(true);
                AsteroidBarrier.transform.GetChild(1).GetComponent<DestructableBarrier>().barrierHealth = 1000000;

            }
            if(tutorialOver){
                Debug.Log(timer);
                timer += Time.deltaTime;
                if(timer >= 3.0f){
                    SceneManager.LoadScene(2);
                }
            }
       }
       if(boss1Defeated && boss2Defeated && boss3Defeated && !capitolShip.activeInHierarchy){
           capitolShip.SetActive(true);
       }
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

    private void RegenerateShield(float shieldAmt)
    {
        currentShield += shieldAmt;
        if (currentShield >= maxShield)
        {
            currentShield = maxShield;
        }
        UpdateShield();
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

    public void AddHealthCapped(float percentage)
    {
        float addme = (percentage / 100f) * maxHealth;
        currentHealth = Mathf.Min(addme + currentHealth, maxHealth);
    }

    public void AddSheildCapped(float percentage)
    {
        float addme = (percentage / 100f) * maxShield;
        currentShield = Mathf.Min(addme + currentShield, maxShield);
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
       if(isTutorial){
            GameObject.Find("Ship").SetActive(false);
            gameStateText.text = "Jump Away!";
            tutorialOver = true;
       }
       else{
            GameObject.Find("Ship").SetActive(false);
            gameStateText.text = "You Died!";
       }
   }
   


   public void DisableUpgradeMenu(){
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
        return timeToBreak;       
    }

    public float getTimeToBreak()
    {
        return CalculateRandomChance();
    }

    public void SetStateToFreeRoam()      { State = (int)GameStates.FREEROAM;      }
    public void SetStateToModifyingShip() { State = (int)GameStates.MODIFYINGSHIP; }

    private void AdvanceText(){
        if(tutorialTextNumber == 0){
            tutorialText.text = "The cockpit controls the ships movement, use the left stick to fly the ship and right trigger to boost. "+
                                "The weapons station controls the guns, use the left stick to aim and right trigger to fire. "+
                                "The engine room must be repaired if the boost breaks, this is done by interacting with it and holding right trigger until boost is fixed.";
        }
        else if(tutorialTextNumber == 1){
            tutorialText.text = "There are Federation scout ships ahead. Destroy them with your guns! When an enemy is destroyed there is a chance it will drop a new ship component. "+
                                "You can dodge enemy fire by moving around and careful use of the boost.";
        }
        else if(tutorialTextNumber == 2){
            tutorialText.text = "Good job! You got a new ship component. Continue onward!";
        }
        else if(tutorialTextNumber == 3){
            tutorialText.text =  "If you fly to a space station and hit \"X\" you will open the ship upgrade menu.";
        }
        else if(tutorialTextNumber == 4){
            tutorialText.text = "This is the upgrade menu for the ship. Select a component from the menu that you have picked up from looting. "+
                                "Find a valid spot in the menu to place the item and hit \"A\" to place the item. "+
                                "When finished select the finish upgrade button.";
        }
        else if(tutorialTextNumber == 5){
            tutorialText.text = "Oh no! A Federation flag ship is approaching from above! It will destroy the space station! You must try to defend it!";
        }
        else if(tutorialTextNumber == 6){
            tutorialText.text = "You did it! You destroyed the flag ship! But wait something is coming on the scanners!";
        }
        else if (tutorialTextNumber > 6){
            tutorialText.text = "";
        }
    }
    private void TutorialState(){
        if((shipInfo.hasFixer() || shipInfo.hasGunner() || shipInfo.hasPilot()) && tutorialTextNumber == 0){
            AdvanceText();
            tutorialTextNumber++;

        }
        else if(BarrierDestroyed == 1 && tutorialTextNumber == 1){
            AdvanceText();
            tutorialTextNumber++;

        }
        else if(inventoryCount >= 1 && tutorialTextNumber == 2){
            GetComponent<MakeBarriersInvulnerable>().WeakenBarrier();
            AdvanceText();
            tutorialTextNumber++;

        }
        else if(BarrierDestroyed == 2 && tutorialTextNumber == 3){
            AdvanceText();
            tutorialTextNumber++;
        }
        else if(State == (int)GameStates.MODIFYINGSHIP && tutorialTextNumber == 4){
            AdvanceText();
            tutorialTextNumber++;
        }
        else if(changedState == (int)GameStates.MODIFYINGSHIP && State == (int)GameStates.FREEROAM && tutorialTextNumber == 5){
            AdvanceText();
            tutorialTextNumber++;
        }
        else if(tutorialBossDefeated && tutorialTextNumber == 6){
            AdvanceText();
            tutorialTextNumber++;
        }
    }

    public void DestroyBarrier(){
        BarrierDestroyed++;
    }

    public void AddToInventory(){
        inventoryCount++;
    }
    public void TutorialBossIsDead(){
        gameStateText.text = "Warning!";
        tutorialBossDefeated = true;
    }
    public void Boss1IsDead(){
        boss1Defeated = true;
    }
    public void Boss2IsDead(){
        boss2Defeated = true;
    }
    public void Boss3IsDead(){
        boss3Defeated = true;
    }
    public void Boss4IsDead(){
        boss4Defeated = true;
    }
    public bool IsThisBossDead(int bossNum){
        if(bossNum == 0)
            return tutorialBossDefeated;
        else if(bossNum == 1)
            return boss1Defeated;
        else if(bossNum == 2)
            return boss2Defeated;
        else if(bossNum == 3)
            return boss3Defeated;
        else
            return boss4Defeated;
    }
}