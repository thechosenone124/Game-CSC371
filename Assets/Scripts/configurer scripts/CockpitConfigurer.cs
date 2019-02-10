using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockpitConfigurer : MonoBehaviour {
	private GameObject downDoor;
	private GameObject downPath;
	void Awake(){
		downDoor = transform.GetChild(3).gameObject;

		downPath = transform.GetChild(2).gameObject;
	}

	public void Doors(int neighbors){
		if((neighbors & 2) > 0){
			Debug.Log("CockpitDown");
			downDoor.SetActive(false);
			downPath.SetActive(true);
		}
		else{
			downDoor.SetActive(true);
			downPath.SetActive(false);
		}
	}
}
