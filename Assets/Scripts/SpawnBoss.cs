using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour {
	public GameObject spawnable;
	public float triggerRadius;
    [Range (0f,1f)]
    public float spawnRadiusRatio = 0.5f;
	private bool shouldSpawn = false;
	private bool hasSpawned = false;
    private float spawnRadius;
	private int bossStartHealth;

	void Start(){
		gameObject.GetComponent<CircleCollider2D>().radius = triggerRadius;
        spawnRadius = triggerRadius * spawnRadiusRatio;
		bossStartHealth = spawnable.GetComponent<BossTakesDamage>().enemyHealth;
		spawnable.SetActive(false);
	}

	void Update(){
		if(shouldSpawn && !hasSpawned){
			spawnable.SetActive(true);
			shouldSpawn = false;
			hasSpawned = true;
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Cockpit")){
			shouldSpawn = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag("Cockpit") && !GameController.instance.IsThisBossDead(spawnable.GetComponent<BossTakesDamage>().bossNumber)){
			spawnable.GetComponent<BossTakesDamage>().enemyHealth = bossStartHealth;
			shouldSpawn = false;
			hasSpawned = false;
			spawnable.SetActive(false);
		}
	}

}
