using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Zachary Barram
 */
public class SpawnBoss : MonoBehaviour {
	public GameObject spawnable;
    public GameObject spawnableMapIcon;
	private bool shouldSpawn = false;
	private bool hasSpawned = false;
	private int bossStartHealth;
    private int bossNumber;
    private Vector3 bossStartLoc;

	void Start(){
		bossStartHealth = spawnable.GetComponent<BossTakesDamage>().enemyHealth;
        bossStartLoc = spawnable.transform.position;
        bossNumber = spawnable.GetComponent<BossTakesDamage>().bossNumber;
        spawnable.SetActive(false);
	}

	void Update(){
		if(shouldSpawn && !hasSpawned){
			spawnable.SetActive(true);
			shouldSpawn = false;
			hasSpawned = true;
		}
        if (spawnableMapIcon != null && GameController.instance.IsThisBossDead(bossNumber))
        {
            spawnableMapIcon.SetActive(false);
        }
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Cockpit")){
			shouldSpawn = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag("Cockpit") && !GameController.instance.IsThisBossDead(bossNumber)){
			spawnable.GetComponent<BossTakesDamage>().enemyHealth = bossStartHealth;
			shouldSpawn = false;
			hasSpawned = false;
            spawnable.transform.position = bossStartLoc;
			spawnable.SetActive(false);
		}
	}

}
