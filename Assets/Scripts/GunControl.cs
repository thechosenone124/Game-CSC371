using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour {

   public float shootFrequency = 0.5f;
   public string fireButton;
   public GameObject reticle;
   public GameObject projectileSpawner;

   private Animator gunAnim;
   private float timer = 0;

	void Start () {
      gunAnim = GetComponent<Animator>();
	}
	
	void Update () {

      timer += Time.deltaTime;
      transform.rotation = Quaternion.LookRotation(Vector3.forward, reticle.transform.position - transform.position);

      if ((Input.GetButtonDown(fireButton) || Input.GetAxis(fireButton) == 1) && timer >= shootFrequency)
      {
         gunAnim.SetTrigger("Fire");
         projectileSpawner.SendMessage("Fire");
         timer = 0;
      }
	}
}
