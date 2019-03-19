using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Davin Johnson
 */
public class ReticlePosition : MonoBehaviour
{

   public GameObject reticleOrigin;

   [Range(1.0f, 100.0f)]
   public float reticleDistance = 20f;

	void Start () {
      transform.position = reticleOrigin.transform.position + new Vector3(0, reticleDistance, 0);
	}
	
}
