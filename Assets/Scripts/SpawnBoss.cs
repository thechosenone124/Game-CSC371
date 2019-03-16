using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour {
	public GameObject spawnable;
	private bool shouldSpawn = false;
	private bool hasSpawned = false;
	private int bossStartHealth;

	void Start(){
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
