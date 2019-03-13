using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDropper : MonoBehaviour {

    [Range(0.0001f, 100f)] public float spawnPercentage = 50f;

    public Vector2 dropOffset;
    public GameObject pickup;
	

    public void TrySpawnPickup()
    {
        float chance = Random.Range(0f, 100f);
        Debug.Log("PickupDropper: TrySpawnPickup -- spawnPercentage: " + spawnPercentage + "       chance: " + chance);
        Vector3 dropPosition = new Vector3(transform.position.x + dropOffset.x, transform.position.y + dropOffset.y, transform.position.z);
        if (chance <= spawnPercentage) Instantiate(pickup, dropPosition, Quaternion.identity);
    }
}
