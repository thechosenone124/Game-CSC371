using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoom : MonoBehaviour {

	// Use this for initialization
	public GameObject obj1;
	public GameObject obj2;
	private int flag = 0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver(){
		if(Input.GetMouseButtonDown(0) && flag == 0){
			Instantiate(obj1,transform.position,Quaternion.identity,transform);
			flag = 1;
    	}
		else if(Input.GetMouseButtonDown(0) && flag == 1){
			Destroy(transform.GetChild(0).gameObject);
			Instantiate(obj2,transform.position,Quaternion.identity,transform);
			flag = 2;
		}
		else if(Input.GetMouseButtonDown(0) && flag == 2){
			Destroy(transform.GetChild(0).gameObject);
			flag = 0;
		}
	}
}
