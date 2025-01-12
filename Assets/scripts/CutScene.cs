using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public string nextSceneName; // The name of the scene to load

    public void LoadNextScene()
    {
        var scene = SceneManager.LoadSceneAsync(nextSceneName);
        scene.allowSceneActivation = false;

        if (scene.progress < 0.9f)
        {
            scene.allowSceneActivation = true;
        }
    }
}
