using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Zachary Barram
 */
public class SimpleEnemyBay : MonoBehaviour {

	// Use this for initialization
	public GameObject enemyShip;
	public float launchFreq;
	private float timer = 0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= launchFreq){
			SpawnSimpleShip();
			timer = 0;
		}
	}

	void SpawnSimpleShip(){
		Instantiate(enemyShip,transform.position,Quaternion.identity);
	}
}
