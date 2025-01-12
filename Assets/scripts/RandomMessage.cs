using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomMessage: MonoBehaviour
{
    public string[] messages; // Array to hold your messages
    public TextMeshProUGUI messageText;  // UI Text element to display the message

    void Start()
    {
        // Check if there are messages to display
        if (messages.Length > 0)
        {
            // Get a random index from the messages array
            int randomIndex = Random.Range(0, messages.Length);

            // Set the UI Text to a random message
            messageText.text = messages[randomIndex];
        }
        else
        {
            Debug.LogWarning("No messages set in the array!");
        }
    }
}