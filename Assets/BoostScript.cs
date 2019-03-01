using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostScript : MonoBehaviour {

    private ParticleSystem ps;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
        ps.enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameController.instance.isBoosting)
        {
            ps.enableEmission = true;
        }
	}
}
