using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Singleton pattern to ensure only one instance exists
    public static SoundManager Instance { get; private set; }
    

    // AudioSources for player and enemy sounds
    public AudioSource playerAudioSource;

    // Audio clips for player and enemy fire sounds
    public AudioClip PlayerFire;


    private void Awake()
    {
        // Check if the instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Keep this object persistent across scenes
        }
    }

    // Play the player fire sound with highest priority
    public void PlayerFireSound()
    {
        if (PlayerFire != null)  // Check if the sound is assigned
        {
            playerAudioSource.priority = 128;
            playerAudioSource.PlayOneShot(PlayerFire,0.5f);
        }
        else
        {
            Debug.LogWarning("Player fire sound is not assigned!");
        }
    }

   

}
