using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineConfigurer : MonoBehaviour {
	private GameObject upDoor,leftDoor,rightDoor;
	private GameObject upPath,leftPath,rightPath;
	void Awake(){
		upDoor = transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).gameObject;
		leftDoor = transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).gameObject;
		rightDoor = transform.GetChild(1).transform.GetChild(1).transform.GetChild(2).gameObject;

		upPath = transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject;
		leftPath = transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).gameObject;
		rightPath = transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).gameObject;
	}

	public void Doors(int neighbors){
		if((neighbors & 1) > 0){
			Debug.Log("Up");
			upDoor.SetActive(false);
			upPath.SetActive(true);
		}
		else{
			upDoor.SetActive(true);
			upPath.SetActive(false);
		}
		if((neighbors & 4) > 0){
			Debug.Log("Left");
			leftDoor.SetActive(false);
			leftPath.SetActive(true);
		}
		else{
			leftDoor.SetActive(true);
			leftPath.SetActive(false);
		}
		if((neighbors & 8) > 0){
			Debug.Log("Right");
			rightDoor.SetActive(false);
			rightPath.SetActive(true);
		}
		else{
			rightDoor.SetActive(true);
			rightPath.SetActive(false);
		}
	}
}
