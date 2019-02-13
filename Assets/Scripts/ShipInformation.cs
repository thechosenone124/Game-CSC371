using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipInformation : MonoBehaviour {

    public float maxHealth = 100f;

    //public Image currentHealthBar;
    //public Text ratioText;

    private float currentHealth;
    private ShipMovementController moveCon;
    private ShipWeaponController weapCon;
    private GameObject pilot;
    private GameObject gunner;
	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        moveCon = GetComponent<ShipMovementController>();
        weapCon = GetComponent<ShipWeaponController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //Health
        UpdateHealthBar();

        if (pilot  != null) moveCon.MoveShip(pilot.GetComponent<PlayerInputContainer>());
        if (gunner != null)
        {
            weapCon.TryShoot(gunner.GetComponent<PlayerInputContainer>());
            weapCon.MoveReticle(gunner.GetComponent<PlayerInputContainer>());
        }
		
	}

    public bool SetPilot(GameObject player)
    {
        if(pilot == null)
        {
            pilot = player;
            return true;
        }
        return false;
    }

    public bool RemovePilot(GameObject player)
    {
        if (pilot == player)
        {
            pilot = null;
            return true;
        }
        return false;
    }

    public bool SetGunner(GameObject player)
    {
        if (gunner == null)
        {
            gunner = player;
            weapCon.reticle.GetComponent<SpriteRenderer>().enabled = true;
            return true;
        }
        return false;
    }

    public bool RemoveGunner(GameObject player)
    {
        if (gunner == player)
        {
            gunner = null;
            weapCon.reticle.GetComponent<SpriteRenderer>().enabled = false;
            return true;
        }
        return false;
    }

    //Health manipulation
    private void UpdateHealthBar()
    {
        float ratio = currentHealth / maxHealth;
        currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString("0") + "%";
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameCtrl.instance.playerDied();
        }
        UpdateHealthBar();
    }


}
