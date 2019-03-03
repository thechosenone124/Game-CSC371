using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other){
		if(GameObject.Find("Player1").GetComponent<PlayerInputContainer>().isOperatingStation)
	}

}
