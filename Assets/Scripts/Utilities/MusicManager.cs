using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager instance = null;

    public int songNum = 0;
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private Coroutine nextTrackCO;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        songNum = 0;
        audioSource.clip = audioClips[songNum];
        if (GameManager.instance.musicOn)
            audioSource.Play();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Quit"))
            Transition();
    }

    public void RestartMusic()
    {
        if (nextTrackCO != null)
            StopCoroutine(nextTrackCO);
        nextTrackCO = null;
        nextTrackCO = StartCoroutine(MusicReset());
    }

    private IEnumerator MusicReset()
    {
        audioSource.loop = true;
        float duration = 1f;
        float elapsedTime = 0f;
        float startPos = audioSource.volume;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            audioSource.volume = Mathf.Lerp(startPos, 0, elapsedTime / duration);
            yield return null;
        }
        songNum = 0;
        audioSource.volume = 1f;
        audioSource.clip = audioClips[songNum];
        audioSource.Play();
    }

    public void TransitionTo(int songNumber)
    {
        songNum = songNumber - 1;
        Transition();
    }

    public void Transition()
    {
        audioSource.loop = false;
        songNum++;
        songNum = Mathf.Min(songNum, audioClips.Length - 1);
        nextTrackCO = StartCoroutine(NextTrack());
    }

    // instead I could just have the track fade out and new one fade in during stage change
    private IEnumerator NextTrack()
    {
        yield return new WaitUntil(() => audioSource.isPlaying == false);
        audioSource.clip = audioClips[songNum];
        audioSource.Play();
        audioSource.loop = true;
        if (audioSource.clip.name == "Fast04")
            audioSource.volume = .6f;
        else
            audioSource.volume = 1f;
    }
}
