using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetAudioMixerGroupVolume : MonoBehaviour {

    public AudioMixer masterAudioMixer;
    public string groupString;

    public void SetAMGVol(float i)
    {
        masterAudioMixer.SetFloat("Vol" + groupString, i);
    }
}
