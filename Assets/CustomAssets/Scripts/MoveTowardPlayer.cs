using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardPlayer : MonoBehaviour
{
    public Vector3 vel;
    public float speed = 10.0f;
    public GameObject target;

    private Vector3 firingDir;
    void Start()
    {
        target = GameObject.Find("Ship");
        firingDir = Vector3.Normalize(target.transform.position - transform.position);
        vel = new Vector3(firingDir.x, firingDir.y, 0);
    }
    // Update is called once per frame
    void Update() {
        transform.position = transform.position + vel;
    }
}
