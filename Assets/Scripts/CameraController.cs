﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Davin Johnson
 */
public class CameraController : MonoBehaviour {

	public GameObject followObject;
   [Range(0f, 100f)]
   public float followSpeed = 2.0f;

   private void Start()
   {
      transform.position = new Vector3(0, 0, -10);
   }

   void LateUpdate () {
      float interpolation = followSpeed * Time.deltaTime;

      Vector3 position = this.transform.position;
      position.y = Mathf.Lerp(this.transform.position.y, followObject.transform.position.y, interpolation * 2);
      position.x = Mathf.Lerp(this.transform.position.x, followObject.transform.position.x, interpolation);

      this.transform.position = position;
	}
}
