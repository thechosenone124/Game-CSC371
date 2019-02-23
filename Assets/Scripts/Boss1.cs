using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1 : MonoBehaviour
{

   public GameObject target;
   public GameObject projectile;
   public GameObject projectileSpawnPoint1;
   public GameObject projectileSpawnPoint2;
   public GameObject healthBar;
   public float speed;
   public float shootRange;
   public float triggerRange;
   public float bulletFreq;
   public float maxHealth;

   private GameObject currentHealthBar;
   private GameObject ratioText;
   private GameObject healthBarBackground;
   private GameObject healthBarBorder;
   private float currentHealth;

   private float timer = 0;

   // Use this for initialization
   void Awake()
   {
      if (target == null)
      {
         target = GameObject.Find("Ship");
      }
      if (healthBar == null)
      {
         healthBar = GameObject.Find("BossHealthBar");                                          //Find BossHealthBar gameObject
         currentHealthBar = healthBar.transform.Find("CurrentHealth").gameObject;               //Set CurrentHealthBar to active
         currentHealthBar.SetActive(true);
         ratioText = healthBar.transform.Find("RatioText").gameObject.gameObject;               //Set RatioText to active
         ratioText.SetActive(true);
         healthBarBackground = healthBar.transform.Find("HealthBarBackground").gameObject;      //Set HealthBarBackground to active
         healthBarBackground.SetActive(true);
         healthBarBorder = healthBar.transform.Find("HealthBarBorder").gameObject;              //Set HealthBarBorder to active
         healthBarBorder.SetActive(true);
      }
   }

   void Start()
   {
      maxHealth = gameObject.GetComponent<BossTakesDamage>().enemyHealth;
      currentHealth = maxHealth;
      UpdateHealthBar();
   }

   public void UpdateHealthBar()
   {
      float ratio = currentHealth / maxHealth;
      currentHealthBar.GetComponent<Image>().rectTransform.localScale = new Vector3(ratio, 1, 1);
      ratioText.GetComponent<Text>().text = currentHealth.ToString("0") + " / " + maxHealth.ToString("0");
   }

   // Update is called once per frame
   void Update()
   {

      timer += Time.deltaTime;

      float step = speed * Time.deltaTime;
      if (CheckDistance() <= triggerRange)
      {
         transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
      }

      if (timer >= bulletFreq && CheckDistance() <= shootRange + .5)
      {
         Shoot();
         timer = 0;
      }

      currentHealth = gameObject.GetComponent<BossTakesDamage>().enemyHealth;
      UpdateHealthBar();
      if (currentHealth == 0)
      {
         healthBar.SetActive(false);
         gameObject.SetActive(false);
      }
   }

   private float CheckDistance()
   {
      float dist = Vector2.Distance(target.transform.position, transform.position);
      return dist;
   }

   private void Shoot()
   {
      //Quaternion playerDirection = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
      //projectileSpawnPoint1.transform.rotation = playerDirection;
      //projectileSpawnPoint2.transform.rotation = playerDirection;
      Vector3 playerDirection1 = target.transform.position - projectileSpawnPoint1.transform.position;
      Vector3 playerDirection2 = target.transform.position - projectileSpawnPoint2.transform.position;
      GameObject bullet1 = Instantiate(projectile, projectileSpawnPoint1.transform.position, Quaternion.identity) as GameObject;
      GameObject bullet2 = Instantiate(projectile, projectileSpawnPoint2.transform.position, Quaternion.identity) as GameObject;
      bullet1.transform.rotation = Quaternion.LookRotation(Vector3.forward, playerDirection1);
      bullet2.transform.rotation = Quaternion.LookRotation(Vector3.forward, playerDirection2);

   }
}
