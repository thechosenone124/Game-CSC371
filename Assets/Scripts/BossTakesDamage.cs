using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTakesDamage : MonoBehaviour
{

   // Use this for initialization
    private int gotHit = 0;
    public PickupDropper[] droppers;
    public int enemyHealth = 200;
    private int maxHealth;
    public int bossNumber;
    public int numberOfLayers;
    private float damageTime = 0;
    private float damageTaken = 0;
    private float regenTimer = 0f;
    public float regenTime;
    public float redTime;
    private float damageNeededToIncreaseLayer;
    public int regenHealthAmt = 1;
     
   void Start()
   {
        maxHealth = enemyHealth;
        damageNeededToIncreaseLayer = enemyHealth / numberOfLayers;
   }

   // Update is called once per frame
   void Update()
   {
        regenTimer += Time.deltaTime;
        if(damageTaken > damageNeededToIncreaseLayer && numberOfLayers != 1)
        {
            SendMessage("IncreaseLayerIndex");
            damageTaken = 0;
        }
        if (gotHit > 0)
      {
            enemyHealth -= gotHit;
            damageTaken += 1;
            gotHit = 0;
            damageTime += redTime;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
      }
      if (damageTime > 0)
      {
         damageTime -= Time.deltaTime;
      }
      else
      {
         damageTime = 0;
         gameObject.GetComponent<SpriteRenderer>().color = Color.white;
      }
      if (enemyHealth <= 0)
      {
            if(GameController.instance.isTutorial && bossNumber == 1)
            {
                GameController.instance.TutorialBossIsDead();
            }
            else if((!GameController.instance.isTutorial) && bossNumber == 1)
            {
                GameController.instance.Boss1IsDead();
            }
            else if(bossNumber == 2)
            {
                GameController.instance.Boss2IsDead();
            }
            else if (bossNumber == 3)
            {
                GameController.instance.Boss3IsDead();
            }
            else if (bossNumber == 4)
            {
                GameController.instance.Boss4IsDead();
            }
            for(int i = 0;i < droppers.GetLength(0); i++){
                droppers[i].TrySpawnPickup();
            }
            if(bossNumber == 4){
                GameController.instance.PlayerWins();
            }
            Destroy(gameObject);
        }
        if(enemyHealth < maxHealth && regenTimer >= regenTime){
            enemyHealth += regenHealthAmt;
            regenTimer = 0;
        }
   }
   void OnTriggerEnter2D(Collider2D col)
   {
      if (col.gameObject.CompareTag("Projectile"))
      {
         gotHit = 3;
      }
      else if (col.gameObject.CompareTag("PlayerMissile"))
      {
         gotHit = 10;
      }
      else if (col.gameObject.CompareTag("Plasma"))
      {
         gotHit = 5;
      }
   }
}