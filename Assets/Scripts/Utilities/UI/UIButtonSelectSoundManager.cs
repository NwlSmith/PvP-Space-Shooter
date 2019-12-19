using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: Central location for button sound effects attached to each Canvas.
 */
public class UIButtonSelectSoundManager: MonoBehaviour {

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /*
     * If not already playing a sound, play one now.
     * Invoked on UI Button OnClick() functions.
     */
    public void PlaySound()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}
