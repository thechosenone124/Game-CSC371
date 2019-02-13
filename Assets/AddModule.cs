using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddModule : MonoBehaviour {
	private const int NOAHGUN = 5;
	private bool partSpawned = false;
	public int x = 1, y = 4;
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("EngineRoom")){
			if(!partSpawned){
				other.transform.parent.parent.gameObject.GetComponent<SpawnStartingShip>().SpawnModuleAtLocation(x,y,NOAHGUN);
				partSpawned = true;
			}
		}
	}
}
