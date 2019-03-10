using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMeTimer : MonoBehaviour {
	private float timer = 0; 
	public float timeTilDeletion;
	void Update () {
		timer += Time.deltaTime;
		if(timer >= timeTilDeletion){
			Destroy(gameObject);
			timer = 0;
		}
	}
}
