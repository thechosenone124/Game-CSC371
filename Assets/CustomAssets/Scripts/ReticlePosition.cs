using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticlePosition : MonoBehaviour {

   public float reticleDistance = 2.8f;
   public GameObject reticleOrigin;

	void Start () {
      transform.position = reticleOrigin.transform.position + new Vector3(0, reticleDistance, 0);
	}
	
}
