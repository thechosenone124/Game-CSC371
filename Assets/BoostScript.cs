using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostScript : MonoBehaviour {

    private ParticleSystem ps;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
        ps.Stop();
	}

	// Update is called once per frame
	void FixedUpdate () {
        if (GameController.instance.isBoosting)
        {
            ps.Play();
        }
        else
        {
            ps.Stop();
        }
    }
}
