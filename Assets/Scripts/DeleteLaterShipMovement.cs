using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLaterShipMovement : MonoBehaviour {

    [Header("Delete this later. Just using this script to test ship movement.")]
    public float speed = 0.5f;

    private Vector2 input;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        input = new Vector2(0f, 0f);


        if (Input.GetKey(KeyCode.I))
            input.y += 1 * speed;
        if (Input.GetKey(KeyCode.K))
            input.y -= 1 * speed;
        if (Input.GetKey(KeyCode.J))
            input.x -= 1 * speed;
        if (Input.GetKey(KeyCode.L))
            input.x += 1 * speed;



        transform.position += new Vector3(input.x, input.y, 0f);
		
	}
}
