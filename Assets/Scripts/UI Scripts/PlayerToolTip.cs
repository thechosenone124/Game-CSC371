using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToolTip : MonoBehaviour {
    
    private ShipInfoDavin shipInfo;
    private bool nearNavStation = false;
    private bool nearWeapStation = false;
    private bool nearEngStation = false;

    private void Start()
    {
        shipInfo = GameObject.Find("Ship").GetComponent<ShipInfoDavin>();
    }

    private void Update()
    {
        if (nearNavStation && !shipInfo.hasPilot())
        {
            showButtonA();
        }
        else if (nearWeapStation && !shipInfo.hasGunner())
        {
            showButtonA();
        }
        else if (nearEngStation && !shipInfo.hasFixer())
        {
            showButtonA();
        }
        else
        {
            hideButtons();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CockpitControls"))
        {
            nearNavStation = true;
        }
        else if (collision.gameObject.CompareTag("WeaponControls"))
        {
            nearWeapStation = true;
        }
        else if (collision.gameObject.CompareTag("EngineStation"))
        {
            nearEngStation = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        nearNavStation = false;
        nearWeapStation = false;
        nearEngStation = false;
        /*transform.Find("ButtonPopUps").transform.Find("aButton").gameObject.SetActive(false);       //hide A button tool tip.
        transform.Find("ButtonPopUps").transform.Find("bButton").gameObject.SetActive(false);       //hide B button tool tip.
        transform.Find("ButtonPopUps").transform.Find("xButton").gameObject.SetActive(false);       //hide X button tool tip.
        transform.Find("ButtonPopUps").transform.Find("yButton").gameObject.SetActive(false);       //hide Y button tool tip.
        */
    }

    public void showButtonA()
    {
        transform.Find("ButtonPopUps").transform.Find("aButton").gameObject.SetActive(true);    //show A button tool tip.
    }

    public void showButtonB()
    {
        transform.Find("ButtonPopUps").transform.Find("bButton").gameObject.SetActive(true);    //show B button tool tip.
    }

    public void showButtonX()
    {
        transform.Find("ButtonPopUps").transform.Find("xButton").gameObject.SetActive(true);    //show X button tool tip.
    }

    public void showButtonY()
    {
        transform.Find("ButtonPopUps").transform.Find("yButton").gameObject.SetActive(true);    //show Y button tool tip.
    }

    public void hideButtons()
    {
        transform.Find("ButtonPopUps").transform.Find("aButton").gameObject.SetActive(false);       //hide A button tool tip.
        transform.Find("ButtonPopUps").transform.Find("bButton").gameObject.SetActive(false);       //hide B button tool tip.
        transform.Find("ButtonPopUps").transform.Find("xButton").gameObject.SetActive(false);       //hide X button tool tip.
        transform.Find("ButtonPopUps").transform.Find("yButton").gameObject.SetActive(false);       //hide Y button tool tip.
    }
}
