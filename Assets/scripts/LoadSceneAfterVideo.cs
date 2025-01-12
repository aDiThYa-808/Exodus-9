using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadSceneAfterVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer; // Assign your VideoPlayer in the Inspector
        // Set the name of the scene to load in the Inspector

    private void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component is not assigned!");
            return;
        }

        // Subscribe to the VideoPlayer's loopPointReached event
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // Load the specified scene when the video finishes playing
        SceneManager.LoadScene("Menu");
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}
