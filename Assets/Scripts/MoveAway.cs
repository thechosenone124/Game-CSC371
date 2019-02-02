using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAway : MonoBehaviour
{
    public Vector3 vel;
    public float speed=10.0f;
    void Start()
    {
       Vector3 values = Vector3.Normalize(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
       vel= new Vector3(values.x, values.y, 0);
    }

    void Update() {
        transform.position =transform.position + vel;
    }
}
