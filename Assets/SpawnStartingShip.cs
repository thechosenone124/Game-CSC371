using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStartingShip : MonoBehaviour {

	// Use this for initialization
	public GameObject[] modules;
	void Start () {
		SpawnModule(4,modules[1]);
		SpawnModule(5,modules[0]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnModule(int child, GameObject module){
		transform.GetChild(child).gameObject.GetComponent<CreateRoom>().BuildRoom(module);
	}
}
