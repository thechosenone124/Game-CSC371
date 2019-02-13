using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComplexEnemies : MonoBehaviour {
	public int numberOfEnemies;
	public GameObject spawnable;
	public float triggerRadius;
	private bool shouldSpawn = false;
	private bool hasSpawned = false;

	void Start(){
		gameObject.GetComponent<CircleCollider2D>().radius = triggerRadius;
	}

	void Update(){
		if(shouldSpawn && !hasSpawned){
			for(int i = 0; i < numberOfEnemies; i++){
				Instantiate(spawnable,transform.position,Quaternion.identity,transform);
			}
			shouldSpawn = false;
			hasSpawned = true;
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("EngineRoom")){
			shouldSpawn = true;
		}
	}

}
