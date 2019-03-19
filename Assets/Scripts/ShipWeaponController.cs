using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Zachary Barram
 * Noah Paige
 */
public class ShipWeaponController : MonoBehaviour {

    private ArrayList guns = new ArrayList();
    public GameObject weaponAimControlCenter;
    public GameObject reticle;
    public GameObject aimPoint;
    public float reticleDeadZone = 0.2f;
    public float reticleRotationSpeed = 5f;

    [Range(1f, 100f)]
    public float reticleDistance = 20f;

    private void Update()
    {
        RotateGunsTowardsReticle();
    }

    public void TryShoot(PlayerInputContainer pcon)
    {
        for(int i = 0; i < guns.Count; i++)
        {
            ((GameObject)guns[i]).GetComponent<GunControl>().Fire(pcon);
        }
    }

    public void MoveReticle(PlayerInputContainer pcon)
    {
        Vector2 stickInput = new Vector2(pcon.GetHorizontal(), pcon.GetVertical());
        stickInput = Vector2.ClampMagnitude(stickInput, 1);   //clamp magnitude to keep circle boundary for x/y-axis, instead of square
        if (stickInput.magnitude < reticleDeadZone)
        {
            stickInput.x = 0;
            stickInput.y = 0;
        }

        if (stickInput.x == 0 && stickInput.y == 0) { }
        else
        {
            Quaternion eulerRot = Quaternion.Euler(0, 0, Mathf.Atan2(-stickInput.x, stickInput.y) * 180 / Mathf.PI);
            weaponAimControlCenter.transform.rotation = Quaternion.Slerp(weaponAimControlCenter.transform.rotation, eulerRot, Time.deltaTime * reticleRotationSpeed);

        }
    }

    void RotateGunsTowardsReticle()
    {
        if(GameController.instance.State == (int)GameController.GameStates.MODIFYINGSHIP){
            return;
        }
        for (int i = 0; i < guns.Count; i++)
        {
            ((GameObject)guns[i]).GetComponent<GunControl>().RotateAtReticle(aimPoint.transform.position);
        }
    }

    public void AddGun(GameObject gun){
        guns.Add(gun);
    }
    
    public void RemoveGun(GameObject gun){
        guns.Remove(gun.transform.GetChild(1).gameObject);
    }
}
