using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Davin Johnson
 */
public class WeaponAimCenterAnchor : MonoBehaviour {

   public GameObject ship;

	// Update is called once per frame
	void Update () {
      transform.position = ship.transform.position;
	}
}
