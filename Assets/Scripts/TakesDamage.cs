using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakesDamage : MonoBehaviour {

	// Use this for initialization
	private bool gotHit = false;
	public int enemyHealth;
	private float damageTime = 0;

    private PickupDropper dropper;
    private MakeBarriersInvulnerable barrier;
	void Start () {
		dropper = GetComponent<PickupDropper>();
        barrier = GetComponent<MakeBarriersInvulnerable>();
	}
	
	// Update is called once per frame
	void Update () {
		if(gotHit){
            enemyHealth -= 1;
            gotHit = false;
            damageTime += .1f;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            //SendMessage("IncreaseLayerIndex");
        }
        if(enemyHealth == 0){
            if (dropper != null) dropper.TrySpawnPickup();
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
            gotHit = true;
        }
    }

    public int GetCurrentHealth(){
        return enemyHealth;
    }
}
