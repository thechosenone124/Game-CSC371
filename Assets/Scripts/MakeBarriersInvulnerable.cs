using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Zachary Barram
 * Davin Johnson
 */
public class MakeBarriersInvulnerable : MonoBehaviour {
	public GameObject barrier;
	public void WeakenBarrier(){
		barrier.GetComponent<DestructableBarrier>().barrierHealth = 15;
	}
}
