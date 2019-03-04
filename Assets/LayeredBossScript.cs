using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayeredBossScript : MonoBehaviour {

    public Sprite[] layers;

    private SpriteRenderer sr;
    private int layerIndex = 0;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = layers[0];
    }

    // Update is called once per frame
    void Update()
    {
        sr.sprite = layers[layerIndex];
    }

    public void IncreaseLayerIndex()
    {
        layerIndex++;
    }
}
