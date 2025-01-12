using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using UnityEditor;
public class Pause : MonoBehaviour
{
    public GameObject pausescreen;
    void Update()
    {
        if (CrossPlatformInputManager.GetButton("Pause"))
        {
            Time.timeScale = 0f;
            pausescreen.SetActive(true);

        }   
    }
    public void resume()
    {
        Time.timeScale = 1f;
        pausescreen.SetActive(false);
    }
    
    public async void quitgame()
    {
        var scene = SceneManager.LoadSceneAsync("Menu");
        scene.allowSceneActivation = false;

        if (scene.progress < 0.9f)
        {
            scene.allowSceneActivation = true;
        }
    }
}
