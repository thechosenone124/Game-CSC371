using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    // Enums are better because they guarantee we don't set two const equal to the same number
    public enum ItemTypes
    {
        COCKPIT,
        WEAPONSROOM,
        ENGINEROOM,
        NOAHGUN,
        MISSILELAUNCHER,
        PLASMABAY,
        FOURWAYROOM,
        GUN,
        NUMBEROFTYPES
    }; // make sure the last element is NUMBEROFTYPES. this serves as the length of the list

    public enum GameStates
    {
        FREEROAM,
        MODIFYINGSHIP,
        PLAYERWON,
        NUMBEROFSTATES
    }; // make sure the last element is NUMBEROFSTATES. this serves as the length of the list

    private ShipInfoDavin shipInfo;

    //Game State--------------------------------
    [Header("Game State Info"), Space(5)]
    public Text gameStateText;
    public int State;

    private int changedState;
    //------------------------------------------


    //Upgrade Menu/Inventory Info---------------
    [Header("Upgrade Menu Info"), Space(5)]
    public GameObject UpgradeMenu;

    private int inventoryCount = 0;
    //------------------------------------------

    //Map Info----------------------------------
    [Header("Map Info"), Space(5)]
    public GameObject map;
    public bool mapStatus = false;
    //------------------------------------------


    //Music Info--------------------------------
    [Header("Music Info"), Space(5)]
    public MusicController ambience;
    //------------------------------------------


    //Health------------------------------------
    [Header("Ship Health Info"), Space(5)]
    public float maxHealth = 100f;
    public Image currentHealthBar;
    public Text ratioText;

    private float currentHealth;
    //------------------------------------------


    //Shield------------------------------------
    [Header("Shield Info"), Space(5)]
    public float maxShield = 100f;
    public Image currentShieldBar;
    public Text shieldRatioText;
    public bool shieldBroken = false;

    private float currentShield;
    //------------------------------------------


    //Boost-------------------------------------
    [Header("Boost Info"), Space(5)]
    public float maxBoost = 100f;
    public Image currentBoostBar;
    public Text boostRatioText;
    public bool isBoosting;
    public bool boostBroken;

    private float currentBoost;
    private float timeToBreak;
    //------------------------------------------

    //Boss Info---------------------------------
    [Header("Boss Info"), Space(5)]
    public GameObject capitolShip;

    private bool tutorialBossDefeated = false;
    private bool boss1Defeated = false;
    private bool boss2Defeated = false;
    private bool boss3Defeated = false;
    private bool boss4Defeated = false;
    //------------------------------------------


    //Tutorial Info-----------------------------
    [Header("Tutorial Info"), Space(5)]
    public bool isTutorial;
    public Text tutorialText;
    public GameObject AsteroidBarrier;

    private int BarrierDestroyed = 0;
    private int tutorialTextNumber = 0;
    private bool tutorialOver = false;
    private float timer = 0f;
    //------------------------------------------

    private GameObject ship;

    private bool healthAddedThisFrame = false;
    private bool sheildAddedThisFrame = false;

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
        ambience = GameObject.Find("Main Camera").GetComponent<MusicController>();
        ambience.SetVolume(0.7f);
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
        UpdateShield();

        //game state text initialization
        gameStateText.text = "";

        ship = GameObject.Find("Ship");
        shipInfo = ship.GetComponent<ShipInfoDavin>();

        mapStatus = false;
    }

    // Update is called once per frame
    void Update()
    { 
        if(isTutorial){     //Game is in Tutorial Level
            TutorialState();
            changedState = State;
            if(tutorialBossDefeated == true){
                capitolShip.SetActive(true);
                AsteroidBarrier.SetActive(true);
                AsteroidBarrier.transform.GetChild(1).GetComponent<DestructableBarrier>().barrierHealth = 1000000;

            }
            if(tutorialOver){
                timer += Time.deltaTime;
                if(timer >= 3.0f){
                    SceneManager.LoadScene(2);
                }
            }
        }
        else    //Game is in Main Level
        {
            if (boss1Defeated && boss2Defeated && boss3Defeated && !capitolShip.activeInHierarchy)
            {
                capitolShip.SetActive(true);
            }
            SetMap();
        }
        sheildAddedThisFrame = false;
        healthAddedThisFrame = false;

    }

    //Health manipulation
    public void UpdateHealthBar()
    {
        float ratio = currentHealth / maxHealth;
        currentHealthBar.rectTransform.localScale = new Vector3(ratio * .6f, .6f, 1);
        ratioText.text = currentHealth.ToString("0") + " / " + maxHealth;
    }

    //boost manipulation
    public void UpdateBoost()
    {
        float ratio = currentBoost / maxBoost;
        currentBoostBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        //boostRatioText.text = (ratio * 100).ToString("0") + "%";
    }

    //shield manipulation
    public void UpdateShield()
    {
        float ratio = currentShield / maxShield;
        currentShieldBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        shieldRatioText.text = currentShield.ToString("0") + " / " + maxShield;
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
        if (healthAddedThisFrame) return;
        float addme = (percentage / 100f) * maxHealth;
        Debug.Log("Current Health: " + currentHealth + "      New Health: " + Mathf.Min(addme + currentHealth, maxHealth));
        currentHealth = Mathf.Min(addme + currentHealth, maxHealth);
        UpdateHealthBar();
        healthAddedThisFrame = true;

    }

    public void AddSheildCapped(float percentage)
    {
        if (sheildAddedThisFrame) return;
        float addme = (percentage / 100f) * maxShield;
        Debug.Log("Current Sheild: " + currentShield + "      New Sheild: " + Mathf.Min(addme + currentShield, maxShield));
        currentShield = Mathf.Min(addme + currentShield, maxShield);
        UpdateShield();
        sheildAddedThisFrame = true;
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
            ship.SetActive(false);
            gameStateText.text = "Jump Away!";
            tutorialOver = true;
        }
        else{
            ship.SetActive(false);
            gameStateText.text = "You Died!";
        }
    }
   
    public void SetMap()
    {
        if (Input.GetButtonDown("SelectButton"))
        {
            mapStatus = !mapStatus;
        }

        if (!mapStatus)
        {
            map.SetActive(false);   //Deactivate Map
        }
        else
        {
            map.SetActive(true);    //Activate Map
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
            tutorialText.text = "The cockpit controls the ship's movement. Use the Left Joystick to fly the ship and \"RT\" to boost. "+
                                "The weapons station controls the guns. Use the Left Joystick to aim and \"RT\" to fire. " +
                                "The engine room must be repaired if your ship's boost breaks. Fix the boost by interacting with the engine room station and holding \"RT\" until the boost bar refills completely.";
        }
        else if(tutorialTextNumber == 1){
            tutorialText.text = "Careful, there are Federation scout ships ahead. Destroy them with your guns! When an enemy is destroyed there is a chance it will drop a new ship component. "+
                                "You can dodge enemy fire by moving around and careful use of the boost.";
        }
        else if(tutorialTextNumber == 2){
            tutorialText.text = "Nice, it looks you picked up an extra ship component dropped from that enemy ship you destroyed. That'll come in handy. We still need to find that contact, continue your search.";
        }
        else if(tutorialTextNumber == 3){
            tutorialText.text =  "Looks like there's a space station ahead. You should stop by it an upgrade your ship with that ship component you scavenged. If you fly next to a space station and hit \"X\" you will open the ship Upgrade Menu.";
        }
        else if(tutorialTextNumber == 4){
            tutorialText.text = "This is the Upgrade Menu for the ship. Select a scavenged component you want to add from the menu. "+
                                "Then, find a place you would like to add the component on the ship and hit \"A\" to place the item. "+
                                "When you are finished modifying your ship select the \"Finish Upgrade\" button.";
        }
        else if(tutorialTextNumber == 5){
            tutorialText.text = "Oh no! The scanners show a Federation flag ship is approaching up ahead! This job was trap! No running away now, looks like you'll have you fight your way out of here!";
        }
        else if(tutorialTextNumber == 6){
            tutorialText.text = "Great job! You destroyed the flag ship! But wait, there's another ship coming on the scanners. It's HUGE! Get out of there!!";
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