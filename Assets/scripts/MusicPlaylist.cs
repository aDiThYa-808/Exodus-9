using UnityEngine;
using System.Collections;

public class MusicPlaylist : MonoBehaviour
{
    public AudioClip[] songs; // Array to store your songs
    private AudioSource audioSource;
    private int currentSongIndex = 0;

    [Range(0, 1)] public float volume = 0.5f; // Volume variable (default is 50%)
    public float fadeDuration = 1.0f; // Duration of the fade in/out in seconds
    public bool shuffle = true; // Option to shuffle songs
    public bool loopPlaylist = true; // Option to loop the playlist

    private bool isFading = false; // Prevent multiple fade coroutines

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume; // Set initial volume

        if (songs.Length > 0)
        {
            PlayNextSong(); // Start playing the first song
        }
        else
        {
            Debug.LogWarning("No songs in the playlist!");
        }
    }

    void Update()
    {
        // Dynamically adjust volume based on the variable
        audioSource.volume = volume;

        // Check if the current song is about to finish
        if (!audioSource.isPlaying && !audioSource.loop && !isFading)
        {
            StartCoroutine(FadeOutAndPlayNext());
        }
    }

    IEnumerator FadeOutAndPlayNext()
    {
        isFading = true;
        float startVolume = audioSource.volume;

        // Gradually reduce the volume to 0 over the fade duration
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        // Stop the current song and reset the volume
        audioSource.Stop();
        audioSource.volume = startVolume;

        // Play the next song
        PlayNextSong();
        isFading = false;
    }

    void PlayNextSong()
    {
        if (shuffle)
        {
            currentSongIndex = Random.Range(0, songs.Length);
        }
        else
        {
            if (!loopPlaylist && currentSongIndex == songs.Length - 1)
            {
                audioSource.Stop();
                Debug.Log("Playlist finished.");
                return;
            }

            currentSongIndex = (currentSongIndex + 1) % songs.Length;
        }

        audioSource.clip = songs[currentSongIndex];
        audioSource.Play();

        // Start fade-in effect for the new song
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float targetVolume = volume;
        audioSource.volume = 0;

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += targetVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }
}