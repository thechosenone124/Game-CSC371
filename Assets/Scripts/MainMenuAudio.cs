using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Bryan
public class MainMenuAudio : MonoBehaviour {

    private AudioSource hoverSource;
    private AudioSource pressSource;
    public AudioClip hover;
    public AudioClip press;

    public static MainMenuAudio instance;


    void Awake()
    {
        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
        //hoverSource = AddAudio(hover, 1.0f);
        pressSource = AddAudio(press, 1.0f);
    }

    private AudioSource AddAudio(AudioClip clip, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = false;
        newAudio.playOnAwake = false;
        newAudio.volume = vol;
        return newAudio;
    }

    void Update()
    {

    }

    public void HoverSound()
    {
        //hoverSource.PlayOneShot(hover);
    }

    public void PressSound()
    {
        pressSource.PlayOneShot(press);
    }
}
