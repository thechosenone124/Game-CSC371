using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComplexEnemies : MonoBehaviour {
	public int numberOfEnemies;
	public GameObject spawnable;
	public float triggerRadius;
   [Range (0f,1f)]
   public float spawnRadiusRatio = 0.5f;
	private bool shouldSpawn = false;
	private bool hasSpawned = false;
   private float spawnRadius;

	void Start(){
		gameObject.GetComponent<CircleCollider2D>().radius = triggerRadius;
      spawnRadius = triggerRadius * spawnRadiusRatio;
	}

	void Update(){
		if(shouldSpawn && !hasSpawned){
			for(int i = 0; i < numberOfEnemies; i++){
				Instantiate(spawnable,transform.position + new Vector3(Random.Range(-spawnRadius,spawnRadius),Random.Range(-spawnRadius,spawnRadius),0),Quaternion.identity);
			}
			shouldSpawn = false;
			hasSpawned = true;
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Cockpit")){
			shouldSpawn = true;
		}
	}

}
