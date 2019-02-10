using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsRoomConfigurer : MonoBehaviour {
	private GameObject upDoor,downDoor,leftDoor,rightDoor;
	private GameObject upPath,downPath,leftPath,rightPath;
	void Awake(){
		upDoor = transform.GetChild(2).transform.GetChild(1).transform.GetChild(0).gameObject;
		leftDoor = transform.GetChild(2).transform.GetChild(1).transform.GetChild(1).gameObject;
		downDoor = transform.GetChild(2).transform.GetChild(1).transform.GetChild(2).gameObject;
		rightDoor = transform.GetChild(2).transform.GetChild(1).transform.GetChild(3).gameObject;

		upPath = transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).gameObject;
		leftPath = transform.GetChild(2).transform.GetChild(0).transform.GetChild(1).gameObject;
		downPath = transform.GetChild(2).transform.GetChild(0).transform.GetChild(2).gameObject;
		rightPath = transform.GetChild(2).transform.GetChild(0).transform.GetChild(3).gameObject;
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
		if((neighbors & 2) > 0){
			Debug.Log("Down");
			downDoor.SetActive(false);
			downPath.SetActive(true);
		}
		else{
			downDoor.SetActive(true);
			downPath.SetActive(false);
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
