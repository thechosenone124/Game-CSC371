using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMeTimer : MonoBehaviour {
	private float timer = 0; 
	
	void Update () {
		timer += Time.deltaTime;
		if(timer >= .5f){
			Destroy(gameObject);
			timer = 0;
		}
	}
}
