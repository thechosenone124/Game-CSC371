using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject followObject;
   [Range(1f, 100f)]
   public float followSpeed = 2.0f;

	void LateUpdate () {
      float interpolation = followSpeed * Time.deltaTime;

      Vector3 position = this.transform.position;
      position.y = Mathf.Lerp(this.transform.position.y, followObject.transform.position.y, interpolation);
      position.x = Mathf.Lerp(this.transform.position.x, followObject.transform.position.x, interpolation);

      this.transform.position = position;
	}
}
