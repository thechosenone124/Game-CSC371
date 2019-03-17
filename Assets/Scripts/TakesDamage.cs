using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakesDamage : MonoBehaviour {

	// Use this for initialization
	private int gotHit = 0;
	public int enemyHealth;
	private float damageTime = 0;

    public PickupDropper[] droppers;
    private MakeBarriersInvulnerable barrier;
	void Start () {
        barrier = GetComponent<MakeBarriersInvulnerable>();
	}
	
	// Update is called once per frame
	void Update () {
		if(gotHit > 0){
            enemyHealth -= gotHit;
            gotHit = 0;
            damageTime += .1f;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            //SendMessage("IncreaseLayerIndex");
        }
        if(enemyHealth <= 0){
            for(int i = 0; i < droppers.GetLength(0); i++){
                droppers[i].TrySpawnPickup();
            }
            if(barrier != null) barrier.WeakenBarrier();
            
            Destroy(gameObject);
        }
		if(damageTime > 0){
            damageTime -= Time.deltaTime;
        }
        else{
            damageTime = 0;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
	}
	void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Projectile"))
        {
            gotHit = 3;
        }
        else if (col.gameObject.CompareTag("PlayerMissile"))
        {
            gotHit = 10;
        }
        else if (col.gameObject.CompareTag("Plasma"))
        {
            gotHit = 5;
        }
    }

    public int GetCurrentHealth(){
        return enemyHealth;
    }
}
