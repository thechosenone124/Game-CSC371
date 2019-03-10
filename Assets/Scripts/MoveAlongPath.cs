using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPath : MonoBehaviour {

	public GameObject[] path;
	private int currentPathIndex = 0;
	// Use this for initialization
	private Vector3 direction;
	public float rotationSpeed;
	public float moveSpeed;
	public float borderSize;
	public bool doRotation = true;

	public float detectionDistance;
	void Start () {
		direction = path[currentPathIndex].transform.position - transform.position;
		Quaternion eulerRot = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.x, direction.y) * 180 / Mathf.PI);
		transform.rotation = eulerRot;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateCurrentPathPoint();
		direction = path[currentPathIndex].transform.position - transform.position;
		MoveShip(direction);
		if(Vector3.Magnitude(direction) <= detectionDistance){
			GetComponent<WeaponPointManager>().FireWeapons();
		}
		else{
			GetComponent<WeaponPointManager>().StopWeapons();
		}
	}

	private void UpdateCurrentPathPoint(){
		float topOfBorder = path[currentPathIndex].transform.position.y + borderSize;
		float bottomOfBorder = path[currentPathIndex].transform.position.y - borderSize;
		float rightOfBorder = path[currentPathIndex].transform.position.x + borderSize;
		float leftOfBorder = path[currentPathIndex].transform.position.x - borderSize;
		if(transform.position.y < topOfBorder && transform.position.y > bottomOfBorder && transform.position.x <= rightOfBorder && transform.position.x >= leftOfBorder){
			currentPathIndex++;
		}
		if(currentPathIndex == path.Length){
			currentPathIndex = 0;
		}
	}

	private void MoveShip(Vector3 movementDir){
		transform.position += Vector3.Normalize(movementDir) * Time.deltaTime *moveSpeed;

		Quaternion eulerRot = Quaternion.Euler(0, 0, Mathf.Atan2(-movementDir.x, movementDir.y) * 180 / Mathf.PI);
		if(doRotation){
			transform.rotation = Quaternion.Slerp(transform.rotation, eulerRot, Time.deltaTime * rotationSpeed);
		}
	}
}
