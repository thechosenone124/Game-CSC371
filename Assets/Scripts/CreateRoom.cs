using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoom : MonoBehaviour {

	// Use this for initialization
	public GameObject obj1;
	public GameObject obj2;
	private int flag = 0;

	private int hasChild = 0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver(){
		if(Input.GetMouseButtonDown(0) && flag == 0){
			BuildRoom(obj1);
			flag = 1;
    	}
		else if(Input.GetMouseButtonDown(0) && flag == 1){
			BuildRoom(obj2);
			flag = 2;
		}
		else if(Input.GetMouseButtonDown(0) && flag == 2){
			flag = 0;
		}
	}

	public void BuildRoom(GameObject module){
		if(hasChild == 1){
			Destroy(transform.GetChild(0).gameObject);
			hasChild = 0;
		}
		Instantiate(module,transform.position,Quaternion.identity,transform);
		hasChild = 1;
	}
}
