using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Projectile"))
        {
            gameObject.SetActive(false);
            col.gameObject.SetActive(false);
        }

    }
}
