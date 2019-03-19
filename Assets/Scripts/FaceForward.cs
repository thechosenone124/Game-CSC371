using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Zachary Barram
 */
public class FaceForward : MonoBehaviour {

	// Use this for initialization
	public GameObject target;
	void Awake(){
		if(target == null){
			target = GameObject.Find("Ship");
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f,0f,angle-90f);
	}
}
