using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

    public Image currentHealthBar;
    public Text ratioText;

    private float hitPoint = 3;
    private float maxHitPoint = 3;

	// Use this for initialization
	void Start () {
        UpdateHealthBar();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void UpdateHealthBar()
    {
        float ratio = hitPoint / maxHitPoint;
        currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString("0") + "%";
    }

    private void TakeDamage(float damage)
    {
        hitPoint -= damage;
        if (hitPoint <= 0)
        {
            hitPoint = 0;
            GameCtrl.instance.playerDied();
        }
        UpdateHealthBar();
    }
}
