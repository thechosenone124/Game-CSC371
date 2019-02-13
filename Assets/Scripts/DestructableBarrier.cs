using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBarrier : MonoBehaviour {

	// Use this for initialization
	public int barrierHealth = 5;
	private bool gotHit = false;
	private float damageTime = 0f;
	public float whiteTime;
	private int numOfAsteroids = 13;
	void Update(){
		if(gotHit){
			barrierHealth--;
			if(barrierHealth == 0){
				transform.parent.gameObject.SetActive(false);
			}
			
			for(int i = 0; i < numOfAsteroids; i++){
				transform.parent.GetChild(0).GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
			}
			
			gotHit = false;
			damageTime += whiteTime;
		}
		if(damageTime > 0){
			damageTime -= Time.deltaTime;
		}
		if(damageTime < 0){
			for(int i = 0; i < numOfAsteroids; i++){
				transform.parent.GetChild(0).GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = new Color(.45f,.45f,.45f,1f);
			}
			damageTime = 0;
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Projectile")){
			gotHit = true;
		}
	}
}
