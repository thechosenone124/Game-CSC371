using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun02Control : MonoBehaviour
{

   private Animator gunAnim;

   void Start()
   {
      gunAnim = GetComponent<Animator>();
   }

   void Update()
   {

      Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

      if (Input.GetButtonDown("Fire2"))
      {
         gunAnim.SetTrigger("Fire");
      }
   }
}
