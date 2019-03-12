using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDropper : MonoBehaviour {

    [Range(0.0001f, 100f)] public float spawnPercentage = 50f;
    public GameObject pickup;
	

    public void TrySpawnPickup()
    {
        float chance = Random.Range(0f, 100f);
        Debug.Log("PickupDropper: TrySpawnPickup -- spawnPercentage: " + spawnPercentage + "       chance: " + chance);
        if (chance <= spawnPercentage) Instantiate(pickup, transform.position, Quaternion.identity);
    }
}
