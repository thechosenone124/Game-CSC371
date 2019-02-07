using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSideways : MonoBehaviour {

    // Use this for initialization
    public GameObject target;
    
    // Update is called once per frame
    void Update () {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,angle);
    }
}
