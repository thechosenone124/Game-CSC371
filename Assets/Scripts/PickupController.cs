using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class PickupController : MonoBehaviour {

    string[] types = Enum.GetNames(typeof(GameController.ItemTypes));
    

    [Header("See item types in Inventory.cs")]
    public int itemType;
    [Range(20f, 100f)] public float rotationOverTime = 50f;
    [Range(0.01f, 5f)] public float sizeOverTime     = 5f;
    [Range(0.5f, 2f)]  public float alphaOverTime    = 1f;

    [Range(0.01f, 1f)] public float minSize  = 0.1f;
    [Range(0.01f, 2f)] public float maxSize  = 2f;
    [Range(0.2f, 1f)]  public float minAlpha = 0.1f;
    [Range(0.2f, 1f)]  public float maxAlpha = 0.8f;

    private bool sizeDir = false;
    private bool alphaDir = false;


    private SpriteRenderer rend;

    // Use this for initialization
    void Start () {
        rend = GetComponent<SpriteRenderer>();
        rend.color = new Color(rend.color.r, rend.color.b, rend.color.g, maxAlpha);
        transform.localScale = new Vector3(maxSize, maxSize, maxSize);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0,0,1), rotationOverTime * Time.deltaTime);
        ChangeAlpha();
        ChangeSize();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.root.CompareTag("PlayerShip"))
        {
            GameController.instance.GetComponent<Inventory>().AddItem(itemType);
        }
    }

    private void ChangeAlpha()
    {
        Color curColor = rend.color;
        if (alphaDir)
        {
            if (curColor.a + alphaOverTime * Time.deltaTime > maxAlpha)
            {
                curColor.a = maxAlpha;
                rend.color = curColor;
                alphaDir = false;
            }
            else
            {
                curColor.a += alphaOverTime * Time.deltaTime;
                rend.color = curColor;
            }
        }
        else
        {
            if(curColor.a - alphaOverTime * Time.deltaTime < minAlpha)
            {
                curColor.a = minAlpha;
                rend.color = curColor;
                alphaDir = true;
            }
            else
            {
                curColor.a -= alphaOverTime * Time.deltaTime;
                rend.color = curColor;
            }
        }

    }

    private void ChangeSize()
    {
        if (sizeDir)
        {
            if (transform.localScale.x + sizeOverTime * Time.deltaTime > maxSize)
            {
                transform.localScale = new Vector3(maxSize, maxSize, maxSize);
                sizeDir = false;
            }
            else transform.localScale = new Vector3(transform.localScale.x + sizeOverTime * Time.deltaTime, transform.localScale.y + sizeOverTime * Time.deltaTime, transform.localScale.z + sizeOverTime * Time.deltaTime);
        }
        else
        {
            if (transform.localScale.x - sizeOverTime * Time.deltaTime < minSize)
            {
                transform.localScale = new Vector3(minSize, minSize, minSize);
                sizeDir = true;
            }
            else transform.localScale = new Vector3(transform.localScale.x - sizeOverTime * Time.deltaTime, transform.localScale.y - sizeOverTime * Time.deltaTime, transform.localScale.z - sizeOverTime * Time.deltaTime);
        }
    }
}
