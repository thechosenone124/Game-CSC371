using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTakesDamage : MonoBehaviour
{

   // Use this for initialization
    private bool gotHit = false;
    public int enemyHealth = 200;
    public int bossNumber;
    public int numberOfLayers;
    private float damageTime = 0;
    private float damageTaken = 0;
    public float redTime;
    private float damageNeededToIncreaseLayer;
     
   void Start()
   {
        damageNeededToIncreaseLayer = enemyHealth / numberOfLayers;
   }

   // Update is called once per frame
   void Update()
   {
        if(damageTaken > damageNeededToIncreaseLayer)
        {
            SendMessage("IncreaseLayerIndex");
            damageTaken = 0;
        }
        if (gotHit)
      {
            enemyHealth -= 1;
            damageTaken += 1;
            gotHit = false;
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
      if (enemyHealth == 0)
      {
            if(bossNumber == 0)
            {
                GameController.instance.TutorialBossIsDead();
            }
            else if(bossNumber == 1)
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
        }
   }
   void OnTriggerEnter2D(Collider2D col)
   {
      if (col.gameObject.CompareTag("Projectile"))
      {
         gotHit = true;
      }
   }
}