using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunConfigurer : MonoBehaviour {
	public void FaceNeighbor(int neighbors){
		if((neighbors & 4) > 0){
			
		}
		else if((neighbors & 8) > 0){
			transform.Rotate(0,180,0);
		}
		else if((neighbors & 1) > 0){
			transform.Rotate(0,0,-90);
		}
		else if((neighbors & 2) > 0){
			transform.Rotate(0,0,90);
		}
	}

	public void AddSelfToGuns(){
		GameObject ship = transform.parent.transform.parent.transform.parent.gameObject;
		ship.GetComponent<ShipWeaponController>().AddGun(transform.GetChild(1).gameObject);
	}
}
