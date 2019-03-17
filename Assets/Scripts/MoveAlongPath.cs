using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveAlongPath : MonoBehaviour {

	public GameObject[] path;
	private int currentPathIndex = 0;
	// Use this for initialization
	private Vector3 direction;
	public float rotationSpeed;
	public float moveSpeed;
	public float borderSize;
	public bool doRotation = true;
    public bool endOfPath = false;

	public float detectionDistance;
    
	private GameObject currentHealthBar;
   	private GameObject ratioText;
   	private GameObject healthBarBackground;
   	private GameObject healthBarBorder;
   	private float currentHealth;
	public GameObject healthBar;
	private float maxHealth;
    private GameObject playerShip;


	void Awake()
   	{
		currentHealthBar = healthBar.transform.Find("CurrentHealth").gameObject;               //Set CurrentHealthBar to active
		currentHealthBar.SetActive(true);
		ratioText = healthBar.transform.Find("RatioText").gameObject.gameObject;               //Set RatioText to active
		ratioText.SetActive(true);
		healthBarBackground = healthBar.transform.Find("HealthBarBackground").gameObject;      //Set HealthBarBackground to active
		healthBarBackground.SetActive(true);
		healthBarBorder = healthBar.transform.Find("HealthBarBorder").gameObject;              //Set HealthBarBorder to active
		healthBarBorder.SetActive(true);
   }
	void Start () {
		direction = path[currentPathIndex].transform.position - transform.position;
		Quaternion eulerRot = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.x, direction.y) * 180 / Mathf.PI);
		transform.rotation = eulerRot;
		maxHealth = gameObject.GetComponent<BossTakesDamage>().enemyHealth;
      	currentHealth = maxHealth;
      	UpdateHealthBar();
        playerShip = GameObject.Find("Ship");
    }
	
	// Update is called once per frame
	void Update () {
		currentHealth = GetComponent<BossTakesDamage>().enemyHealth;
        if (currentHealth == 0)
        {
            healthBar.SetActive(false);
        }
        UpdateHealthBar();

        UpdateCurrentPathPoint();
		direction = path[currentPathIndex].transform.position - transform.position;
        if (!endOfPath)
        {
            MoveShip(direction);
        }

		if(Vector3.Magnitude(playerShip.transform.position - transform.position) <= detectionDistance){
			GetComponent<WeaponPointManager>().FireWeapons();
		}
		else{
			GetComponent<WeaponPointManager>().StopWeapons();
		}
	}

	public void UpdateHealthBar()
	{
		float ratio = currentHealth / maxHealth;
		currentHealthBar.GetComponent<Image>().rectTransform.localScale = new Vector3(ratio, 1, 1);
		ratioText.GetComponent<Text>().text = currentHealth.ToString("0") + " / " + maxHealth.ToString("0");
	}

	private void UpdateCurrentPathPoint(){
		float topOfBorder = path[currentPathIndex].transform.position.y + borderSize;
		float bottomOfBorder = path[currentPathIndex].transform.position.y - borderSize;
		float rightOfBorder = path[currentPathIndex].transform.position.x + borderSize;
		float leftOfBorder = path[currentPathIndex].transform.position.x - borderSize;
        if (transform.position.y < topOfBorder && transform.position.y > bottomOfBorder 
            && transform.position.x <= rightOfBorder && transform.position.x >= leftOfBorder){  //check if the ship is closer distance than 'borderSize' to the next point
            if (path.Length > 1)
            {
                currentPathIndex++;
            }
            else
            {
                endOfPath = true;
            }
		}
		if (currentPathIndex == path.Length){
			currentPathIndex = 0;
		}
	}

	private void MoveShip(Vector3 movementDir){
		transform.position += Vector3.Normalize(movementDir) * Time.deltaTime *moveSpeed;

		Quaternion eulerRot = Quaternion.Euler(0, 0, Mathf.Atan2(-movementDir.x, movementDir.y) * 180 / Mathf.PI);
		if(doRotation){
			transform.rotation = Quaternion.Slerp(transform.rotation, eulerRot, Time.deltaTime * rotationSpeed);
		}
	}

	void OnDisable(){
      GameController.instance.ambience.On();
      currentHealthBar.SetActive(false);
      ratioText.SetActive(false);
      healthBarBackground.SetActive(false);
      healthBarBorder.SetActive(false);
   }

   void OnEnable(){
      if (GameController.instance != null)
          GameController.instance.ambience.Off();
      currentHealthBar.SetActive(true);
      ratioText.SetActive(true);
      healthBarBackground.SetActive(true);
      healthBarBorder.SetActive(true);
   }
}
