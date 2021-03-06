﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Noah
 * Davin
 */
public class InputController : MonoBehaviour {

    public GameObject Player1;
    public GameObject Player2;

    public PlayerInputContainer container1;
    public PlayerInputContainer container2;

	// Use this for initialization
	void Start () {
        container1 = Player1.GetComponent<PlayerInputContainer>();
        container2 = Player2.GetComponent<PlayerInputContainer>();
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log("Player1 B: " + Input.GetButton("J1B"));

        if (container1 != null) SendPlayer1Input();
        if (container2 != null) SendPlayer2Input();


    }

    void SendPlayer1Input()
    {
        container1.SetInput(Input.GetAxis("J1Horizontal"), 
                           -Input.GetAxis("J1Vertical"), 
                            Input.GetButton("J1A"), 
                            Input.GetButton("J1B"),
                            Input.GetButton("J1X"),
                            Input.GetButton("J1Y"),
                            Input.GetAxis("J1RT"));
    }

    void SendPlayer2Input()
    {
        container2.SetInput(Input.GetAxis("J2Horizontal"), 
                           -Input.GetAxis("J2Vertical"), 
                            Input.GetButton("J2A"), 
                            Input.GetButton("J2B"),
                            Input.GetButton("J2X"),
                            Input.GetButton("J2Y"),
                            Input.GetAxis("J2RT"));
    }
}
