using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: Manages music and song transitions.
 * Is a single instance static object - There should only be 1 MusicManager.
 * Stores each music track and accommodates new tracks, and manages transitions between them.
 */
public class MusicManager : MonoBehaviour {

    // Static instance of the object.
    public static MusicManager instance = null;

    // Public objects.
    public int songNum = 0;
    public int initSong = 0;
    // Songs (there can be any number)
    public AudioClip[] audioClips;

    // Private objects.
    private AudioSource audioSource;
    private Coroutine nextTrackCO;

    private void Awake()
    {
        // Ensure that there is only one instance of the MusicManager.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        if (initSong < 0 || initSong >= audioClips.Length)
            Debug.Log("Error: An initSong value of " + initSong + " is not acceptable. Enter a value greater than 0 and less than the length of the audioClip Array");

        // Retrieve the AudioSource.
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // If music is supposed to be playing, start the music at the first track.
        songNum = 0;
        audioSource.clip = audioClips[songNum];
        if (GameManager.instance.musicOn)
            audioSource.Play();
    }

    private void Update()
    {
        // For testing purposes. Cycles through each song.
        if (Input.GetButtonDown("Quit"))
            Transition();
    }

    /*
     * Stops the next track from starting after this loop finishes, and calls the music reset coroutine.
     * Invoked in GameManager class when stopping the game.
     */
    public void RestartMusic()
    {
        if (nextTrackCO != null)
            StopCoroutine(nextTrackCO);
        nextTrackCO = null;
        nextTrackCO = StartCoroutine(RestartMusicCO());
    }

    /*
     * Fades out current music track and starts the initial song.
     * Invoked in RestartMusic() when restarting the game.
     */
    private IEnumerator RestartMusicCO()
    {
        // Lerp the volume to 0
        audioSource.loop = true;
        float duration = 1f;
        float elapsedTime = 0f;
        float startVol = audioSource.volume;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            audioSource.volume = Mathf.Lerp(startVol, 0, elapsedTime / duration);
            yield return null;
        }
        // Play the intended initial song.
        audioSource.volume = 1f;
        audioSource.clip = audioClips[initSong];
        audioSource.Play();
    }

    /*
     * Plays the next song in the AudioClip array when the current song is over.
     * Invoked in the GameManager class in StartGame() and PlayerHit() on a new round.
     */
    public void Transition()
    {
        audioSource.loop = false;
        songNum++;
        songNum = Mathf.Min(songNum, audioClips.Length - 1);
        nextTrackCO = StartCoroutine(NextTrack());
    }

    /*
     * Wait until the current song is finished and play the queued song.
     * Invoked Transition().
     */
    private IEnumerator NextTrack()
    {
        yield return new WaitUntil(() => audioSource.isPlaying == false);
        audioSource.clip = audioClips[songNum];
        audioSource.Play();
        audioSource.loop = true;
        
        // One of the songs used in testing was very loud, so I lowered the volume here.
        if (audioSource.clip.name == "Fast04")
            audioSource.volume = .6f;
        else
            audioSource.volume = 1f;
    }
}
