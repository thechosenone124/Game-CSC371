using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    [Header("See item types in Inventory.cs")]
    public int itemType;
    [Range(0.1f, 5f)]    public float rotationOverTime = 0.5f;
    [Range(0.01f, 0.1f)] public float sizeOverTime     = 5f;
    [Range(0.01f, 0.8f)] public float alphaOverTime    = 0.5f;

    [Range(0.01f, 1f)] public float minSize  = 0.1f;
    [Range(0.01f, 1f)] public float maxSize  = 1f;
    [Range(0.2f, 1f)]  public float minAlpha = 0.1f;
    [Range(0.2f, 1f)]  public float maxAlpha = 0.1f;

    private bool sizeDir = false;
    private bool alphaDir = false;
    private float scaleConstant = 0.5f;


    private SpriteRenderer rend;

    // Use this for initialization
    void Start () {
        rend = GetComponent<SpriteRenderer>();
        rend.color = new Color(rend.color.r, rend.color.b, rend.color.g, maxAlpha);
        transform.localScale = new Vector3(maxSize, maxSize, maxSize);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, rotationOverTime * Time.deltaTime);
        ChangeAlpha();
        ChangeSize();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void ChangeAlpha()
    {
        if (alphaDir)
        {
            if(rend.color.a + alphaOverTime > maxAlpha)
            {
                rend.color = new Color(rend.color.r, rend.color.b, rend.color.g, maxAlpha);
                alphaDir = false;
            }
            else rend.color = new Color(rend.color.r, rend.color.b, rend.color.g, rend.color.a + alphaOverTime);
        }
        else
        {
            if(rend.color.a - alphaOverTime < minAlpha)
            {
                rend.color = new Color(rend.color.r, rend.color.b, rend.color.g, minAlpha);
                alphaDir = true;
            }
            else rend.color = new Color(rend.color.r, rend.color.b, rend.color.g, rend.color.a - alphaOverTime);
        }

    }

    private void ChangeSize()
    {
        if (sizeDir)
        {
            if (transform.localScale.x + sizeOverTime > maxSize)
            {
                transform.localScale = new Vector3(maxSize, maxSize, maxSize);
                sizeDir = false;
            }
            else transform.localScale = new Vector3(transform.localScale.x + sizeOverTime, transform.localScale.y + sizeOverTime, transform.localScale.z + sizeOverTime);
        }
        else
        {
            if (transform.localScale.x - sizeOverTime < minSize)
            {
                transform.localScale = new Vector3(minSize, minSize, minSize);
                sizeDir = true;
            }
            else transform.localScale = new Vector3(transform.localScale.x - sizeOverTime, transform.localScale.y - sizeOverTime, transform.localScale.z - sizeOverTime);
        }
    }
}
