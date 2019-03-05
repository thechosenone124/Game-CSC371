using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashScript : MonoBehaviour {

    public double flashTime = 0.5;
    private Image sprite;
    private double timeStamp;
    private bool toggle = true;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<Image>();
        sprite.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(GameController2.instance.GetCurrentBoost() == 0)
        {
            timeStamp = Time.time + flashTime;
            if (Time.time >= timeStamp)
                FlashSprite();
        }
	}

    private void FlashSprite()
    {
            toggle = !toggle;
            if (toggle)
                sprite.enabled = true;
            else
                sprite.enabled = false;
    }
}
