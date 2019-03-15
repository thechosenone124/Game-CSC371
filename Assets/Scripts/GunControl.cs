using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour {

    public float shootFrequency = 0.5f;
    [Header("Buttons: {A, X, Y, RT}")]
    public string fireButton;
    public int projectilesFiredPerPress = 1;
    public GameObject projectile;

    private Animator gunAnim;
    private float timer = 0;

    void Start ()
    {
      gunAnim = GetComponent<Animator>();
	}
	
	void Update ()
    {
      timer += Time.deltaTime;
	}

    public void Fire(PlayerInputContainer pcon)
    {
        if (timer < shootFrequency) return;

        if( (fireButton == "A" && pcon.GetAButton()) ||
            (fireButton == "X" && pcon.GetXButton()) ||
            (fireButton == "Y" && pcon.GetYButton()) ||
            (fireButton == "RT" && pcon.GetRTButton() == 1))
        {
            for (int i = 0; i < projectilesFiredPerPress; i++)
            {
                Instantiate(projectile, transform.position, transform.rotation);
            }
            if(gunAnim != null){
                gunAnim.SetTrigger("Fire");
            }
            timer = 0;

        }
    }

    public void RotateAtReticle(Vector3 retPos)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, retPos - transform.position);
    }
}
