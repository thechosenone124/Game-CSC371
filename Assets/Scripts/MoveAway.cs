using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAway : MonoBehaviour
{
    
    public Vector3 vel;
    public float speed=100.0f;

    private float mouseRatioX;
    private float mouseRatioY;

    public GameObject cHair;

    void Start()
    {
        cHair = GameObject.Find("Crosshair");

        //Vector3 values = Vector3.Normalize(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        Vector3 values = (transform.position-cHair.transform.position);
        print("cHair position: " + cHair.transform.position);
        print("ship position: " + transform.position);

        
        vel = new Vector3(values.x*-1, values.y*-1, 0);
        print("vel: " + vel.ToString());
    }

    void Update() {
        transform.position =transform.position + vel/10;
    }
}
