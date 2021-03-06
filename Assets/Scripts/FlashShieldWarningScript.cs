﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* Contributors:
 * Brandon Ryan
 */
public class FlashShieldWarningScript : MonoBehaviour
{

    public double flashTime = 0.5;
    public Sprite repairSprite;
    private Image sprite;
    private double timeStamp;
    private bool toggle = false;
    private bool stampSet;

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<Image>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.getCurrentShield() <= .2*GameController.instance.maxShield)
        {
            if (!stampSet)
            {
                timeStamp = Time.time + flashTime;
                stampSet = true;
            }
            if (Time.time >= timeStamp)
            {
                FlashSprite();
                stampSet = false;
            }

        }
        else
        {
            sprite.enabled = false;
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
