using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class CutSceneTrigger : MonoBehaviour
{
    public TextMeshProUGUI messageText; // Reference to the TextMeshPro text
    public string sceneName = "EndScene"; // Name of the scene to load
    public float delay = 3f; // Delay before loading the scene

    private void Start()
    {
        messageText.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player entered the trigger
        {
            StartCoroutine(ShowMessageAndLoadScene());
        }
    }

    private IEnumerator ShowMessageAndLoadScene()
    {
        messageText.gameObject.SetActive(true); // Show the text
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        SceneManager.LoadScene(sceneName); // Load the specified scene
    }
}