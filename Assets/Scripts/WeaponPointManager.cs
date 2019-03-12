using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPointManager : MonoBehaviour {
	public GameObject WeaponPoints;
	public void FireWeapons(){
		if(!WeaponPoints.activeInHierarchy){
			WeaponPoints.SetActive(true);
		}
	}
	public void StopWeapons(){
		if(WeaponPoints.activeInHierarchy){
			WeaponPoints.SetActive(false);
		}
	}
}
