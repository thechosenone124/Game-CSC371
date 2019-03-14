﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    private AudioSource audio;
    public AudioClip clip;
	// Use this for initialization
	void Awake (){
        audio = gameObject.AddComponent<AudioSource>();
        audio.clip = clip;
        audio.loop = true;
        audio.playOnAwake = true;
        audio.volume = 1.0f;
        audio.Play();
    }
}
