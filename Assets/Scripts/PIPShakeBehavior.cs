using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIPShakeBehavior : MonoBehaviour {

    // Transform of the GameObject you want to shake
    private RectTransform Rtransform;

    // Desired duration of the shake effect
    private float currentShakeDuration = 0f;

    public float shakeDuration = 1f;

    // A measure of magnitude for the shake. Tweak based on your preference
    public float shakeMagnitude = 2f;

    // A measure of how quickly the shake effect should evaporate
    public float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    void Awake()
    {
        if (Rtransform == null)
        {
            Rtransform = GetComponent(typeof(RectTransform)) as RectTransform;
        }
    }

    void OnEnable()
    {
        initialPosition = Rtransform.anchoredPosition;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (currentShakeDuration > 0)
        {
            float newMagnitute = shakeMagnitude * (currentShakeDuration / shakeDuration);
            Rtransform.anchoredPosition = initialPosition + Random.insideUnitSphere * newMagnitute;

            currentShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            currentShakeDuration = 0f;
            Rtransform.anchoredPosition = initialPosition;
        }

    }

    public void TriggerShake()
    {
        Debug.Log("ShakeBehavior.TriggerShake -- SHAKING");
        currentShakeDuration = shakeDuration;
    }
}
